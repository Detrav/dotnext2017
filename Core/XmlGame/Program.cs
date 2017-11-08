using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace XmlGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Watcher watch = new Watcher(new string[] { "xml" }, 100);
            byte[] doc = File.ReadAllBytes("XmlBench.xml");
            for (int j = 0; j < 100; j++)
            {
                watch.ReStart();
                XmlTest(doc,j, watch);
            }
            watch.Stop();
        }

        private static void XmlTest(byte[] bytes,int testNumber, Watcher watch)
        {
            //calculate item location for each category
            //calculate item mail text word count for each category
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                XmlDocument document = new XmlDocument();
                document.Load(ms);
                Dictionary<string, Dictionary<string,long>> locations = new Dictionary<string, Dictionary<string,long>>();
                Dictionary<string, long> mailWords = new Dictionary<string, long>();
                XmlNodeList categoriess = document.GetElementsByTagName("categories");
                if(categoriess.Count>0)
                {
                    XmlElement categories = categoriess[0] as XmlElement;
                    foreach(XmlElement category in categories)
                    {
                        if(category.HasAttribute("id"))
                        {
                            locations[category.GetAttribute("id")] = new Dictionary<string, long>();
                            mailWords[category.GetAttribute("id")] = 0;
                        }
                    }
                }
                XmlNodeList regionss = document.GetElementsByTagName("regions");
                Regex regex = new Regex("\\w+");
                if(regionss.Count>0)
                {
                    XmlElement regions = regionss[0] as XmlElement;
                    foreach(XmlElement item in regions.GetElementsByTagName("item"))
                    {
                        long words = 0;
                        foreach(XmlElement mail in item.GetElementsByTagName("mail"))
                        {
                            foreach(XmlElement el in mail)
                            {
                                if (el.Name == "text")
                                {
                                    if (el.InnerText != null)
                                        words += regex.Matches(el.InnerText).Count;
                                }
                            }
                        }
                        string location = "";
                        foreach (XmlElement loc in item.GetElementsByTagName("location"))
                            location = loc.Value ?? location;
                        foreach (XmlElement incategory in item.GetElementsByTagName("incategory"))
                        {
                            if (incategory.HasAttribute("category"))
                            {
                                string category = incategory.GetAttribute("category");
                                if (!locations[category].ContainsKey(location))
                                    locations[category][location] = 1;
                                else locations[category][location]++;
                                mailWords[category] += words;
                            }
                        }
                    }
                }
                watch.AddAndReset(0, testNumber, mailWords.Count);
            }
        }
    }
}
