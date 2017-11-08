using System;
using System.Collections.Generic;
using System.Text;

namespace AsyncThreads
{
    class CountWatcher
    {
        private string[] columns;
        private int count;
        private long[,] results;

        public CountWatcher(string[] columns, int count)
        {
            this.columns = columns;
            this.count = count;
            results = new long[columns.Length, count];
        }

        public void Add(long count, int column, int testNumber)
        {
            if (column < columns.Length && testNumber < count)
            {
                results[column , testNumber] = count;
            }
        }

        public void Stop()
        {
            Console.WriteLine("#CSV_START");
            bool notFirst = false;
            foreach (string column in columns)
            {
                if (notFirst)
                    Console.Write(";");
                Console.Write("Count:");
                Console.Write(column);
                notFirst = true;
            }
            Console.WriteLine();

            for (int j = 0; j < count; j++)
            {
                notFirst = false;
                for (int i = 0; i < columns.Length; i++)
                {
                    if (notFirst)
                        Console.Write(";");
                    Console.Write(results[i, j]);
                    notFirst = true;
                }
                Console.WriteLine();
            }

        }
    }
}
