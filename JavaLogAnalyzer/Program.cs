using System;
using System.Collections.Generic;
using System.IO;

namespace JavaLogAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> listColumns = new List<string>();
            Dictionary<string, List<long>> dictValues = new Dictionary<string, List<long>>();
            foreach (string arg in args)
            {
                if (File.Exists(arg))
                {
                    long memory = 0;
                    long totalMemory = 0;
                    using (TextReader tr = File.OpenText(arg))
                    {
                        string line;
                        while ((line = tr.ReadLine()) != null)
                        {
                            if (line.StartsWith("Start Benchmark", StringComparison.InvariantCultureIgnoreCase))
                            {
                                break;
                            }
                        }
                        while ((line = tr.ReadLine())!=null)
                        {
                            if (line.StartsWith("[Full GC ", StringComparison.InvariantCultureIgnoreCase))
                            {
                                tr.ReadLine();
                                tr.ReadLine();
                                break;
                            }
                        }
                        while ((line = tr.ReadLine())!=null)
                        {
                            if (line.StartsWith("ResetMemory:", StringComparison.InvariantCultureIgnoreCase))
                            {
                                long mem = 0;
                                if (Int64.TryParse(line.Substring("ResetMemory:".Length).Trim(' '), out mem))
                                    memory = totalMemory + mem;
                            }
                            else if (line.StartsWith("   [Eden:", StringComparison.InvariantCultureIgnoreCase))
                            {
                                totalMemory += GetValue(line, "Eden: ") + GetValue(line, "Heap: ") + GetValue(line, "Survivors: ");
                                //totalMemory += GetValue(line, "Heap: ");
                            }
                            else if (line.StartsWith("Watcher: ", StringComparison.InvariantCultureIgnoreCase))
                            {
                                string column = GetColumn(line);
                                long mem = totalMemory + GetValueSimple(line, "Memory: ") - memory;
                                long time = GetValueSimple(line, "Time: ");

                                AddValue(listColumns, dictValues, "Time:" + column, time);
                                AddValue(listColumns, dictValues, "Memory:" + column, mem);
                            }
                            else if (line.StartsWith("CountWatcher: ", StringComparison.InvariantCultureIgnoreCase))
                            {
                                string column = GetColumn(line);
                                long count = totalMemory + GetValueSimple(line, "Count: ");

                                AddValue(listColumns, dictValues, "Count:" + column, count);
                            }
                            else if (line.StartsWith("[GC cleanup ", StringComparison.InvariantCultureIgnoreCase))
                            {
                                string str = line.Substring("[GC cleanup ".Length);
                                int index = 0;
                                if((index = str.IndexOf(",")) > 0 || (index = str.IndexOf(",")) > 0)
                                {
                                    str = str.Substring(0, index);
                                    if ((index = str.IndexOf("(")) > 0)
                                        str = str.Substring(0, index);
                                    string[] vals = str.Split("->");
                                    totalMemory += GetLongValue(vals[0]) - GetLongValue(vals[1]);
                                }
                            } else if (line.StartsWith("[GC concurrent-string-deduplication", StringComparison.InvariantCultureIgnoreCase))
                            {
                                int index0 = line.IndexOf('(', "[GC concurrent-string-deduplication".Length);
                                if(index0>0)
                                {
                                    int index1 = line.IndexOf(')', index0);
                                    if(index1>0)
                                    {
                                        string str = line.Substring(index0 + 1, index1 - index0-1);
                                        //Console.WriteLine(str);
                                        totalMemory += GetLongValue(str);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine("#CSV_START");
            if (listColumns.Count>0)
            {
                bool notEnd = true;
                int k = 0;

                for (int i = 0; i < listColumns.Count; i++)
                {
                    if (i > 0) Console.Write(";");
                    Console.Write(listColumns[i]);
                }
                Console.WriteLine();


                while (notEnd)
                {
                    notEnd = false;
                    for (int i = 0; i < listColumns.Count; i++)
                        if (dictValues[listColumns[i]].Count > k)
                            notEnd = true;
                    if (notEnd)
                        for (int i = 0; i < listColumns.Count; i++)
                        {
                            if (i > 0) Console.Write(";");
                            if (dictValues[listColumns[i]].Count > k)
                            {
                                Console.Write(dictValues[listColumns[i]][k]);
                            }
                        }
                    k++;
                    Console.WriteLine();
                }
                
            }
        }

        static void AddValue(List<string> list, Dictionary<string,List<long>> dict,string column, long value)
        {
            if(!dict.ContainsKey(column))
            {
                list.Add(column);
                dict[column] = new List<long>();
            }
            dict[column].Add(value);
        }

        static string GetColumn(string line)
        {
            int startIndex = line.IndexOf("[");
            int endIndex = line.IndexOf("]", startIndex);
            if(endIndex>startIndex && startIndex>0)
            {
                return line.Substring(startIndex + 1, endIndex - startIndex - 1);
            }
            return "ERROR";
        }

        static long GetValueSimple(string line, string findText)
        {
            int textIndex = line.IndexOf(findText);
            if(textIndex >=0)
            {
                int spaceIndex = line.IndexOf(" ", textIndex + findText.Length);
                if (spaceIndex <= 0)
                    spaceIndex = line.Length;
                string text = line.Substring(textIndex + findText.Length, spaceIndex - textIndex - findText.Length);
                long result = 0;
                Int64.TryParse(text, out result);
                return result;
            }
            return 0;
        }

        static long GetValue(string line, string findText)
        {
            int textIndex = line.IndexOf(findText);
            if (textIndex >= 0)
            {
                int spaceIndex = line.IndexOf(" ", textIndex + findText.Length);
                if (spaceIndex <= 0)
                    spaceIndex = line.IndexOf("]", textIndex + findText.Length);
                if (spaceIndex > 0)
                {
                    string text = line.Substring(textIndex + findText.Length, spaceIndex - textIndex- findText.Length);
                    text = text.Trim(' ');
                    string[] values = text.Split("->");
                    if (values.Length != 2)
                        throw new Exception("Syntax is wrong");
                    int leftBIndex = values[0].IndexOf("(");
                    if (leftBIndex > 0)
                        values[0] = values[0].Substring(0, leftBIndex);
                    leftBIndex = values[1].IndexOf("(");
                    if (leftBIndex > 0)
                        values[1] = values[1].Substring(0, leftBIndex);
                    
                    return GetLongValue(values[0]) - GetLongValue(values[1]);
                }
            }
            return 0;
        }


        static long GetLongValue(string str)
        {
            long size0 = 1;
            switch (str[str.Length - 1])
            {
                case 'B':
                    break;
                case 'K':
                    size0 *= 1024;
                    break;
                case 'M':
                    size0 *= 1024 * 1024;
                    break;
                case 'G':
                    size0 *= 1024 * 1024 * 1024;
                    break;
            }
            str = str.Substring(0, str.Length - 1);

            double result = 0;
            Double.TryParse(str, out result);
            return (long)(result*size0);
        }
    }
}
