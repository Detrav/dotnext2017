package detrav;


import java.util.Random;

public class MainClass {
    public static void main(String[] args) throws InterruptedException  {
        ForJIT();
        //JIT
        System.out.println("Start Benchmark");
        ForJIT();
    }
    private static void ForJIT() throws InterruptedException {
        CountWatcher watch = new CountWatcher(new String[] {
                "1 Thread Reference",
                "2 Thread 1",
                "2 Thread 2",
                "3 Thread 1",
                "3 Thread 2",
                "3 Thread 3",
                "4 Thread 1",
                "4 Thread 2",
                "4 Thread 3",
                "4 Thread 4"
        }
                , 100);
        ThreadTests(2, 1, watch);
        ThreadTests(3, 3, watch);
        ThreadTests(4, 6, watch);
        ThreadTests(1, 0, watch);
        watch.Stop();
    }

    volatile static boolean ends = false;
    static final Object _locker = new Object();

    private static void ThreadTests(int number, int column, CountWatcher watch) throws InterruptedException
    {
        for (int j = 0; j < 100; j++) {
            ends = false;
            Thread[] threads = new Thread[number];
            for (int k = 0; k < number; k++) {
                int l = k;
                int row = j;
                threads[k] = new Thread(new Runnable() {
                    @Override
                    public void run() {
                        long count = 1;
                        while (true) {
                            if (count % 3 == 0)
                                synchronized (_locker) {
                                    if (count++ % 10000 == 0)
                                        if (ends)
                                            break;
                                }
                            else if (count++ % 10000 == 0)
                                if (ends)
                                    break;
                        }

                        watch.Add(count, column + l, row);
                    }
                });
                threads[k].start();
            }
            Thread.sleep(1000);
            ends = true;
            for (int k = 0; k< number; k++)
            {
                threads[k].join();
            }
        }
    }
}
