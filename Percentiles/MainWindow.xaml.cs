using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Percentiles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const float PERCENTILE_K = 0.8f;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {

            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.SelectedPath = Environment.CurrentDirectory;
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    allDataCore = new Dictionary<string, Dictionary<string, List<long>>>();
                    allDataJava = new Dictionary<string, Dictionary<string, List<long>>>();
                    foreach (var resultFile in Directory.GetFiles(System.IO.Path.Combine(dialog.SelectedPath, "Core"), "*.results"))
                    {
                        string[] resultData = File.ReadAllLines(resultFile);
                        Load(System.IO.Path.GetFileNameWithoutExtension(resultFile), resultData, allDataCore);
                    }
                    foreach (var resultFile in Directory.GetFiles(System.IO.Path.Combine(dialog.SelectedPath, "Java"), "*.results"))
                    {
                        if (System.IO.Path.GetFileNameWithoutExtension(resultFile).EndsWith(".1")) continue;
                        string[] resultData = File.ReadAllLines(resultFile);
                        Load(System.IO.Path.GetFileNameWithoutExtension(resultFile), resultData, allDataJava);
                    }

                    List<string> files = new List<string>();
                    foreach (string key in allDataCore.Keys) if (!files.Contains(key)) files.Add(key);
                    foreach (string key in allDataJava.Keys) if (!files.Contains(key)) files.Add(key);
                    lbFiles.ItemsSource = null;
                    lbFiles.ItemsSource = files;
                }
            }
        }

        private void Load(string name, string[] resultData, Dictionary<string, Dictionary<string, List<long>>> allData)
        {
            var dataFile = allData[name] = new Dictionary<string, List<long>>();
            int finding = -1;
            string[] keys = new string[0];
            List<long[]> tempData = new List<long[]>();
            foreach (string row in resultData)
            {
                if (finding == -1)
                {
                    if (row.StartsWith("#CSV_START", StringComparison.InvariantCultureIgnoreCase))
                        finding = 0;
                }
                else if (finding == 0)
                {
                    keys = row.Split(';');
                    finding++;
                }
                else if (finding > 15)
                {
                    List<long> vals = new List<long>();
                    foreach (string valStr in row.Split(';'))
                    {
                        long val;
                        long.TryParse(valStr, out val);
                        vals.Add(val);
                    }
                    tempData.Add(vals.ToArray());
                }
                else finding++;
            }

            for (int i = 0; i < keys.Length; i++)
            {
                List<long> values = new List<long>();
                foreach (long[] vals in tempData)
                    if (i < vals.Length)
                        values.Add(vals[i]);
                dataFile[keys[i]] = values;
            }
        }


        private Dictionary<string, Dictionary<string, List<long>>> allDataCore;
        private Dictionary<string, Dictionary<string, List<long>>> allDataJava;


        private void lbData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string file = lbFiles.SelectedValue?.ToString();
            string column = lbData.SelectedValue?.ToString();
            if (file == null || column == null) return;
            float sdCore;
            long coreData = GetData(allDataCore, file, column, out sdCore);
            float sdJava;
            long javaData = GetData(allDataJava, file, column, out sdJava);
            gMain.Children.Clear();
            Chart c = new Chart();
            gMain.Children.Add(c);
            ColumnSeries cs = new ColumnSeries();
            cs.IndependentValueBinding = new Binding("Key");
            cs.DependentValueBinding = new Binding("Value");
            Dictionary<string, double> newDataSource = new Dictionary<string, double>();
            newDataSource["Core"] = (float)coreData / 1000 / 1000;
            newDataSource["Java"] = (float)javaData / 1000 / 1000;
            tbValues.Text = String.Format("Core = {0}+-{1}, Java = {2}+-{3}", (float)coreData / 1000 / 1000, sdCore / 1000 / 1000, (float)javaData / 1000 / 1000, sdJava / 1000 / 1000);
            cs.ItemsSource = newDataSource;
            c.Series.Add(cs);
        }

        private long GetData(Dictionary<string, Dictionary<string, List<long>>> allData, string file, string column, out float sd)
        {
            if (allData.ContainsKey(file))
            {
                if (allData[file].ContainsKey(column))
                {
                    List<long> values = allData[file][column];
                    return Analyze(values.ToArray(), out sd);
                }
            }
            MessageBox.Show("Check columns: " + column + " in " + file + " file ");
            sd = 0;
            return 0;
        }

        private long Analyze(long[] v, out float sd)
        {
            v = v.OrderByDescending(a => a).ToArray();
            int number = (int)(v.Length * (1 - PERCENTILE_K));

            double st = 0;
            for (int i = 0; i < v.Length; i++)
            {
                st += ((v[i] - v[number]) * (v[i] - v[number])) / v.Length;
            }
            sd = (float)Math.Sqrt(st);
            return v[number];
        }

        private void lbFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> columns = new List<string>();
            string file = lbFiles.SelectedValue?.ToString();
            SelChanged(allDataCore, file, columns);
            SelChanged(allDataJava, file, columns);
            lbData.ItemsSource = columns;
        }

        private void SelChanged(Dictionary<string, Dictionary<string, List<long>>> allData, string file, List<string> columns)
        {
            if (file == null) return;
            if (allData.ContainsKey(file))
            {
                foreach (string key in allData[file].Keys)
                {
                    if (!columns.Contains(key))
                        columns.Add(key);
                }
            }
        }

        private void btnGetCSV_Click(object sender, RoutedEventArgs e)
        {
            List<Array> resultFile = new List<Array>();
            
            TestStringCon(resultFile);
            TestStringWithoutCon(resultFile);
            TestStringBuilder(resultFile);
            EmptyLine(resultFile);
            TestObjects(resultFile);
            EmptyLine(resultFile);
            TestMemotyStream(resultFile);
            EmptyLine(resultFile);
            EmptyLine(resultFile);
            TestVariables(resultFile);
            TestRandom(resultFile);
            TestMath(resultFile);
            TestFullMath(resultFile);
            EmptyLine(resultFile);
            TestRecursive(resultFile);
            TestDice(resultFile);
            TestAkkerman(resultFile);
            EmptyLine(resultFile);
            TestRegex(resultFile);
            EmptyLine(resultFile);
            TestArrays(resultFile);
            TestHashMap(resultFile);
            TestQueue(resultFile);
            TestStack(resultFile);
            EmptyLine(resultFile);
            EmptyLine(resultFile);
            TestThreadSync(resultFile);
            TestThreadAsync(resultFile);
            EmptyLine(resultFile);
            TestTaskSync(resultFile);
            TestTaskASync(resultFile);
            EmptyLine(resultFile);
            EmptyLine(resultFile);
            TestDeterminant(resultFile);
            EmptyLine(resultFile);
            TestXML(resultFile);

            SaveFileDialog sfd = new SaveFileDialog();
            if(sfd.ShowDialog() == true)
            {
                using (TextWriter tw = new StreamWriter(sfd.OpenFile()))
                {
                    foreach(Array arr in resultFile)
                    {
                        foreach(object el in arr)
                        {
                            tw.Write(el);
                            tw.Write(";");
                        }
                        tw.WriteLine();
                    }
                }
            }
        }

        private void TestThreadAsync(List<Array> resultFile)
        {
            EmptyLine(resultFile, "AsyncThreads");
            resultFile.Add(new string[] { "1", "Core", "Java", "deltaCore", "deltaJava" });
            AddDataFullByTime(resultFile, "AsyncThreads", "Time:Thread 1");
            resultFile.Add(new string[] { "2", "Core", "Java", "deltaCore", "deltaJava" });
            AddDataFullByTime(resultFile, "AsyncThreads", "Time:Thread 2");
            resultFile.Add(new string[] { "3", "Core", "Java", "deltaCore", "deltaJava" });
            AddDataFullByTime(resultFile, "AsyncThreads", "Time:Thread 3");
            resultFile.Add(new string[] { "4", "Core", "Java", "deltaCore", "deltaJava" });
            AddDataFullByTime(resultFile, "AsyncThreads", "Time:Thread 4");
        }

        private void TestThreadSync(List<Array> resultFile)
        {
            EmptyLine(resultFile, "SyncThreads");
            resultFile.Add(new string[] { "1", "Core", "Java", "deltaCore", "deltaJava" });
            AddDataFullByTime(resultFile, "SyncThreads", "Time:Thread 1");
            resultFile.Add(new string[] { "2", "Core", "Java", "deltaCore", "deltaJava" });
            AddDataFullByTime(resultFile, "SyncThreads", "Time:Thread 2");
            resultFile.Add(new string[] { "3", "Core", "Java", "deltaCore", "deltaJava" });
            AddDataFullByTime(resultFile, "SyncThreads", "Time:Thread 3");
            resultFile.Add(new string[] { "4", "Core", "Java", "deltaCore", "deltaJava" });
            AddDataFullByTime(resultFile, "SyncThreads", "Time:Thread 4");

        }

        private void TestXML(List<Array> resultFile)
        {
            EmptyLine(resultFile, "XmlGame");
            resultFile.Add(new string[] { "", "Core", "Java", "deltaCore", "deltaJava" });
            AddDataFullByTime(resultFile, "XmlGame", "Time:xml");
        }

        private void TestDeterminant(List<Array> resultFile)
        {
            EmptyLine(resultFile, "DeterminantGame");
            resultFile.Add(new string[] { "", "Core", "Java", "deltaCore", "deltaJava" });
            AddDataFullByTime(resultFile, "DeterminantGame", "Time:Matrix 3", "Time:Matrix 4", "Time:Matrix 5", "Time:Matrix 6");
        }

        private void TestTaskASync(List<Array> resultFile)
        {
            EmptyLine(resultFile, "AsyncTasks");
            resultFile.Add(new string[] { "", "Core", "Java", "deltaCore", "deltaJava" });
            AddDataFullByTime(resultFile, "AsyncTasks", "Time:Count 500000");
        }

        private void TestTaskSync(List<Array> resultFile)
        {
            EmptyLine(resultFile, "SyncTasks");
            resultFile.Add(new string[] { "", "Core", "Java", "deltaCore", "deltaJava" });
            AddDataFullByTime(resultFile, "SyncTasks", "Time:Count 500000");
        }

        private void TestStack(List<Array> resultFile)
        {
            EmptyLine(resultFile, "Stack");
            resultFile.Add(new string[] { "", "Core", "Java", "deltaCore", "deltaJava" });
            AddDataFullByTime(resultFile, "Collections",
                "Time:Stack 1000000 object Create", "Time:Stack 1000000 object Reverse",
                "Time:Stack 1000000 int Create", "Time:Stack 1000000 int Reverse",
                "Time:Stack 1000000 long Create", "Time:Stack 1000000 long Reverse",
                "Time:Stack 1000000 string Create", "Time:Stack 1000000 string Reverse"
                );
        }

        private void TestQueue(List<Array> resultFile)
        {
            EmptyLine(resultFile, "Queue");
            resultFile.Add(new string[] { "", "Core", "Java", "deltaCore", "deltaJava" });
            AddDataFullByTime(resultFile, "Collections",
                "Time:Queue 1000000 object Create", "Time:Queue 1000000 object dequeue to queue", "Time:Queue 1000000 object dequeue",
                "Time:Queue 1000000 int Create", "Time:Queue 1000000 int dequeue to queue", "Time:Queue 1000000 int dequeue",
                "Time:Queue 1000000 long Create", "Time:Queue 1000000 long dequeue to queue", "Time:Queue 1000000 long dequeue",
                "Time:Queue 1000000 string Create", "Time:Queue 1000000 string dequeue to queue", "Time:Queue 1000000 string dequeue"
                );
        }

        private void TestHashMap(List<Array> resultFile)
        {

            EmptyLine(resultFile, "HashMap");
            resultFile.Add(new string[] { "", "Core", "Java", "deltaCore", "deltaJava" });
            AddDataFullByTime(resultFile, "Collections",
                "Time:Map 1000000 object Create", "Time:Map 1000000 object Replace odd and even",
                "Time:Map 1000000 int Create", "Time:Map 1000000 int Replace odd and even",
                "Time:Map 1000000 long Create", "Time:Map 1000000 long Replace odd and even",
                "Time:Map 1000000 string Create", "Time:Map 1000000 string Replace odd and even"
                );
        }

        private void TestArrays(List<Array> resultFile)
        {
            EmptyLine(resultFile, "Arrays");
            resultFile.Add(new string[] { "", "Core", "Java", "deltaCore", "deltaJava" });
            AddDataFullByTime(resultFile, "Collections", 
                "Time:Array 1000000 object Create", "Time:Array 1000000 object Reverse", "Time:Array 1000000 object Reverse by 10", "Time:Array 1000000 object Delete 7/8 + Resize x1/8", "Time:Array 1000000 object Replace odd and even",
                "Time:Array 1000000 int Create", "Time:Array 1000000 int Reverse", "Time:Array 1000000 int Reverse by 10", "Time:Array 1000000 int Delete 7/8 + Resize x1/8", "Time:Array 1000000 int Replace odd and even",
                "Time:Array 1000000 long Create", "Time:Array 1000000 long Reverse", "Time:Array 1000000 long Reverse by 10",  "Time:Array 1000000 long Delete 7/8 + Resize x1/8", "Time:Array 1000000 long Replace odd and even"
                //skip string tests
                );
        }

        private void TestRegex(List<Array> resultFile)
        {
            EmptyLine(resultFile, "Regex");
            resultFile.Add(new string[] { "", "Core", "Java", "deltaCore", "deltaJava" });
            AddDataFullByTime(resultFile, "Regulars", "Time:URI", "T    ime:Email", "Time:Date", "Time:URI|Email");
        }

        private void TestAkkerman(List<Array> resultFile)
        {
            EmptyLine(resultFile, "Recursive Ack");
            resultFile.Add(new string[] { "", "Core", "Java", "deltaCore", "deltaJava" });
            AddDataFullByTime(resultFile, "Recursive", "Time:4 1 Ackermann");
        }

        private void TestDice(List<Array> resultFile)
        {
            EmptyLine(resultFile, "Recursive Dice");
            resultFile.Add(new string[] { "", "Core", "Java", "deltaCore", "deltaJava" });
            AddDataFullByTime(resultFile, "Recursive", "Time:30 Dice");
        }

        private void TestRecursive(List<Array> resultFile)
        {
            
            EmptyLine(resultFile, "Recursive");
            resultFile.Add(new string[] { "", "Core", "Java", "deltaCore", "deltaJava" });
            AddDataFullByTime(resultFile, "Recursive", "Time:10000 Simple");
        }

        private void TestFullMath(List<Array> resultFile)
        {
            EmptyLine(resultFile, "SuperMath");
            resultFile.Add(new string[] { "", "Core", "Java", "deltaCore", "deltaJava" });
            AddDataFullByTime(resultFile, "VarMath", "Time:10000000 strange math");
        }

        private void TestMath(List<Array> resultFile)
        {
            EmptyLine(resultFile, "Math");
            resultFile.Add(new string[] { "", "Core", "Java", "deltaCore", "deltaJava" });
            AddDataFullByTime(resultFile, "VarMath", "Time:10000000 pow", "Time:10000000 sqrt", "Time:10000000 arctg", "Time:10000000 round");
        }

        private void TestRandom(List<Array> resultFile)
        {
            EmptyLine(resultFile, "Random");
            resultFile.Add(new string[] { "", "Core", "Java", "deltaCore", "deltaJava" });
            AddDataFullByTime(resultFile, "VarMath", "Time:10000000 random next", "Time:10000000 random double", "Time:10000000 random 1kb");
        }

        private void TestVariables(List<Array> resultFile)
        {
            EmptyLine(resultFile, "Переменные");
            resultFile.Add(new string[] { "", "Core", "Java", "deltaCore", "deltaJava" });
            AddDataFullByTime(resultFile, "VarMath", "Time:10000000 float", "Time:10000000 double", "Time:10000000 int", "Time:10000000 long");
        }

        private void TestMemotyStream(List<Array> resultFile)
        {
            EmptyLine(resultFile, "MemoryStream");
            resultFile.Add(new string[] { "", "Core", "Java", "deltaCore", "deltaJava" });
            AddDataFullByTime(resultFile, "MStream", "Time:10mb Create byte by byte", "Time:10mb Create 1kb by 1kb", "Time:10mb Create stream full", "Time:10mb Copy stream to another byte by byte", "Time:10mb Copy stream to another 1kb by 1kb", "Time:10mb Copy stream to another", "Time:10mb Generate byte array from stream", "Time:10mb Read all int by binary reader");
        }

        private void TestObjects(List<Array> resultFile)
        {
            EmptyLine(resultFile, "Объекты");
            resultFile.Add(new string[] { "", "Core", "Java", "deltaCore", "deltaJava" });
            AddDataFullByTime(resultFile, "Objects", "Time:Count 100000", "Time:Count 100000 repl layer 4", "Time:Count 100000 repl every 3", "Time:Count 100000 remove every 2", "Time:Count 100000 fill to full 2", "Time:Count 100000 reverse");
        }

        private void TestStringBuilder(List<Array> resultFile)
        {
            EmptyLine(resultFile, "StringBuilder");
            resultFile.Add(new string[] { "", "Core", "Java", "deltaCore", "deltaJava" });
            AddDataFullByTime(resultFile, "StringBuilder", "Time:Count 250000");
        }

        private void TestStringWithoutCon(List<Array> resultFile)
        {
            EmptyLine(resultFile, "Строки без конкатенации");
            resultFile.Add(new string[] { "", "Core", "Java", "deltaCore", "deltaJava" });
            AddDataFullByTime(resultFile, "String", "Time:Count 2500 full reverse", "Time:Count 2500 reverse 32 char words", "Time:Count 2500 count substring", "Time:Count 2500 split", "Time:Count 2500 to upper", "Time:Count 2500 to lower");
        }

        private void TestStringCon(List<Array> resultFile)
        {
            EmptyLine(resultFile, "Строки с конкатенацией");
            resultFile.Add(new string[] { "", "Core", "Java", "deltaCore", "deltaJava" });
            AddDataFullByTime(resultFile, "String", "Time:Count 2500", "Time:Count 2500 remove substring");
        }

        private void EmptyLine(List<Array> resultFile,string title= "")
        {
            resultFile.Add(new object[5] { title, "", "", "", "" });
        }

        private void AddData(string text, List<Array> resultFile, string file, params string[] columns)
        {
            float valCore = 0, valJava = 0, tempCore = 0, tempJava = 0;
            BigInteger sdCore = new BigInteger(1);
            BigInteger sdJava = new BigInteger(1);
            foreach (string column in columns)
            {
                valCore += (float)GetData(allDataCore, file, column, out tempCore) / 1000f / 1000f;
                valJava += (float)GetData(allDataJava, file, column, out tempJava) / 1000f / 1000f;
                sdCore = BigInteger.Multiply(sdCore, new BigInteger(tempCore));
                sdJava = BigInteger.Multiply(sdJava, new BigInteger(tempJava));
            }
            resultFile.Add(new object[5] 
            {
                text,
                valCore,
                valJava,
                (float)(Math.Exp(BigInteger.Log(sdCore) / columns.Length) / 1000 / 1000),
                (float)(Math.Exp(BigInteger.Log(sdJava) / columns.Length) / 1000 / 1000)
            });
        }

        private void AddDataFullByTime(List<Array> resultFile, string file, params string[] columns)
        {
            string[] newColumns = new string[columns.Length];
            for (int i = 0; i < columns.Length; i++)
                newColumns[i] = columns[i].Replace("Time:", "Memory:");
            AddData("Время", resultFile, file, columns);
            AddData("Память", resultFile, file, newColumns);
        }

        private void AddDataTime(List<Array> resultFile, string file, params string[] columns)
        {
            AddData("Время", resultFile, file, columns);
        }

        private void AddDataMemory(List<Array> resultFile, string file, params string[] columns)
        {
            AddData("Память", resultFile, file, columns);
        }
    }

}
