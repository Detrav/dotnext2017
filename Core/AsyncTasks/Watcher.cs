using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AsyncTasks
{
    class Watcher
    {
        Stopwatch watch;
        long memory = 0;
        string[] columns;
        long[,] results;
        int count;

        public Watcher(string[] columns, int count)
        {
            this.columns = columns;
            this.count = count;
            results = new long[columns.Length * 2, count];
            GC.Collect();
            memory = GC.GetAllocatedBytesForCurrentThread();
            watch = Stopwatch.StartNew();
        }

        public void ReStart()
        {
            watch = Stopwatch.StartNew();
        }

        public void Stop()
        {
            Console.WriteLine("#CSV_START");
            bool notFirst = false;
            foreach (string column in columns)
            {
                if (notFirst)
                    Console.Write(";");
                Console.Write("Time:");
                Console.Write(column);
                Console.Write(";");
                Console.Write("Memory:");
                Console.Write(column);
                notFirst = true;
            }
            Console.WriteLine();

            for (int j = 0; j < count; j++)
            {
                notFirst = false;
                for (int i = 0; i < columns.Length * 2; i++)
                {
                    if (notFirst)
                        Console.Write(";");
                    Console.Write(results[i, j]);
                    notFirst = true;
                }
                Console.WriteLine();
            }

        }

        public void AddAndReset(int column, int testNumber, object resultObject)
        {
            watch.Stop();
            long ms = (long)((double)watch.ElapsedTicks / Stopwatch.Frequency * 1000000000);
            long mem = GC.GetAllocatedBytesForCurrentThread() - memory;
            if (column < columns.Length && testNumber < count)
            {
                results[column * 2, testNumber] = ms;
                results[column * 2 + 1, testNumber] = mem;
            }
            Console.WriteLine(resultObject);
            memory = GC.GetAllocatedBytesForCurrentThread();
            watch = Stopwatch.StartNew();
        }

        public void Reset()
        {
            memory = GC.GetAllocatedBytesForCurrentThread();
            watch = Stopwatch.StartNew();
        }
    }
}
