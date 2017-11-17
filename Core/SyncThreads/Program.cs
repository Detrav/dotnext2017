using System;
using System.Threading;

namespace SyncThreads
{
    class Program
    {
        const int COUNT = 12000000;
        static void Main(string[] args)
        {
            Watcher watch = new Watcher(new string[] {
                "Thread 1",
                "Thread 2",
                "Thread 3",
                "Thread 4",
            }
        , 100);
            ThreadTests(1, 0, watch);
            ThreadTests(2, 1, watch);
            ThreadTests(3, 2, watch);
            ThreadTests(4, 3, watch);
            watch.Stop();
        }

     
        private static void ThreadTests(int number, int column, Watcher watch)
        {
            Thread[] threads = new Thread[number];
            VarTest[] tests = new VarTest[number];
            CountdownEvent e = new CountdownEvent(number);
            AutoResetEvent[] events = new AutoResetEvent[number];
            for (int i = 0; i < number; i++)
            {
                events[i] = new AutoResetEvent(false);
                tests[i] = new VarTest(e, events[i], COUNT / number);
                threads[i] = new Thread(new ThreadStart(tests[i].Test));
                threads[i].Start();
            }

            for (int j = 0; j < 100; j++)
            {
                e.Reset();
                watch.ReStart();
                for (int i = 0; i < number; i++)
                    events[i].Set();
                e.Wait();
                double result = 0;
                for (int i = 0; i < number; i++)
                    result += tests[i].Results;
                watch.AddAndReset(column, j, result);
            }
            for (int i = 0; i < number; i++)
                events[i].Set();
        }
    }
}
