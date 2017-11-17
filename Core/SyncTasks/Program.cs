using System;
using System.Threading;
using System.Threading.Tasks;

namespace SyncTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            Watcher watch = new Watcher(new string[]
                {
                    "Count 10000",
                    "Count 100000",
                    "Count 500000",
                }, 100);
            TasksTest(10000, 0, watch);
            TasksTest(100000, 1, watch);
            TasksTest(500000, 2, watch);
            watch.Stop();
        }

        volatile static int ends = 0;
        static readonly Object _locker = new Object();

        private static void TasksTest(int count,int column, Watcher watch)
        {
            for (int j = 0; j < 100; j++)
            {
                ends = 0;
                watch.ReStart();
                Task[] tasks = new Task[count];
                for (int i = 0; i < count; i++)
                {
                    int l = i;
                    tasks[i] = new Task(() =>
                    {
                        if(l%3 == 0)
                            lock(_locker){
                                ends++;
                            }
                        else ends++;

                    });
                    tasks[i].Start();
                }
                Task.WaitAll(tasks, CancellationToken.None);
                watch.AddAndReset(column, j, ends);
            }
        }
    }
}
