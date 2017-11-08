package detrav;


import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;
import org.xml.sax.SAXException;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;
import java.io.ByteArrayInputStream;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.HashMap;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class MainClass  {
    private Pattern pattern;

    public static void main(String[] args) throws IOException,SAXException,ParserConfigurationException {
        ForJIT();
        //JIT
        System.out.println("Start Benchmark");
        ForJIT();
    }

    private static void ForJIT() throws IOException,SAXException,ParserConfigurationException {
        Watcher watch = new Watcher(new String[] { "xml" }, 100);
        byte[] doc = Files.readAllBytes(Paths.get("XmlBench.xml"));
        for (int j = 0; j < 100; j++) {
            watch.ReStart();
            XmlTest(doc, j, watch);

        }
        watch.Stop();
    }

    private static void XmlTest(byte[] bytes, int testNumber, Watcher watch) throws IOException, SAXException, ParserConfigurationException {
        DocumentBuilderFactory documentBuildFactory = DocumentBuilderFactory.newInstance();
        documentBuildFactory.setNamespaceAware(false);
        documentBuildFactory.setValidating(false);
        documentBuildFactory.setFeature("http://xml.org/sax/features/namespaces", false);
        documentBuildFactory.setFeature("http://xml.org/sax/features/validation", false);
        documentBuildFactory.setFeature("http://apache.org/xml/features/nonvalidating/load-dtd-grammar", false);
        documentBuildFactory.setFeature("http://apache.org/xml/features/nonvalidating/load-external-dtd", false);
        DocumentBuilder doccumentBuilder = documentBuildFactory.newDocumentBuilder();
        try(ByteArrayInputStream stream = new ByteArrayInputStream(bytes)) {
            Document document =
                    doccumentBuilder.parse(stream);
            HashMap<String,HashMap<String,Long>> locations = new HashMap<String,HashMap<String,Long>>();
            HashMap<String,Long> mailWords = new HashMap<String,Long>();
            NodeList categoriess = document.getElementsByTagName("categories");
            if(categoriess.getLength() > 0)
            {
                Node categories = categoriess.item(0);
                NodeList nodeList = categories.getChildNodes();
                for(int i =0; i< nodeList.getLength(); i++)
                {
                    Node node = nodeList.item(i);
                    if(node.hasAttributes())
                    {
                        Node id = node.getAttributes().getNamedItem("id");
                        if(id!=null) {
                            String text = id.getNodeValue();
                            locations.put(text, new HashMap<String, Long>());
                            mailWords.put(text, 0L);
                        }
                    }
                }
                NodeList regionss = document.getElementsByTagName("regions");
                Pattern pattern = Pattern.compile("\\w+");
                if(regionss.getLength()>0)
                {
                    Node regions = regionss.item(0);
                    if(regions instanceof Element) {

                        NodeList items = ((Element)regions).getElementsByTagName("item");
                        for (int i = 0; i < items.getLength();i++)
                        {
                            if(items.item(i) instanceof Element) {
                                Element item = (Element) items.item(i);
                                long words = 0;
                                NodeList mails = item.getElementsByTagName("mail");
                                for(int j = 0; j< mails.getLength(); j++)
                                {
                                    if(mails.item(j) instanceof Element) {
                                        Element mail = (Element)mails.item(j);
                                        NodeList childs = mail.getChildNodes();
                                        for(int k = 0; k< childs.getLength(); k++)
                                        {
                                            Node el = childs.item(k);
                                            if (el.getNodeName() == "text") {
                                                String text = el.getTextContent();
                                                if (text != null) {
                                                    Matcher m = pattern.matcher(text);
                                                    while (m.find())
                                                        words ++;
                                                }
                                            }
                                        }
                                    }
                                }
                                String location = "";
                                NodeList locationList = item.getElementsByTagName("location");
                                if(locationList.getLength()>0)
                                    location = locationList.item(0).getTextContent();
                                if(location == null) location = "";
                                NodeList incategories = item.getElementsByTagName("incategory");
                                for(int j= 0; j< incategories.getLength(); j++)
                                {
                                    Node incategory = incategories.item(j);
                                    if (incategory.hasAttributes()) {
                                        Node category = incategory.getAttributes().getNamedItem("category");
                                        if(category!=null) {
                                            String text = category.getNodeValue();
                                            if (!locations.get(text).containsKey(location))
                                                locations.get(text).put(location, 1L);
                                            else
                                                locations.get(text).put(location, locations.get(text).get(location) + 1L);
                                            mailWords.put(text, mailWords.get(text) + words);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            watch.AddAndReset(0, testNumber,mailWords.size());
        }
    }
}