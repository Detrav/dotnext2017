package detrav;

import java.util.concurrent.CountDownLatch;
import java.util.concurrent.TimeUnit;


public class MainClass {
    static final int COUNT = 12000000;

    static final Object locker = new Object();

    public static void main(String[] args) throws InterruptedException {
        ForJIT();
        //JIT
        System.out.println("Start Benchmark");
        ForJIT();
    }

    private static void ForJIT() throws InterruptedException {
        Watcher watch = new Watcher(new String[]{
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

    volatile static boolean ends = false;


    private static void ThreadTests(int number, int column, Watcher watch) {
        try {
            Thread[] threads = new Thread[number];
            VarTest[] tests = new VarTest[number];
            CountDownLatch e = new CountDownLatch(number);
            Object[] events = new Object[number];
            for (int i = 0; i < number; i++) {
                events[i] = new Object();
                tests[i] = new VarTest(e, events[i], COUNT / number);
                threads[i] = new Thread(tests[i]);
                threads[i].start();
            }

            for (int j = 0; j < 100; j++) {
                e = new CountDownLatch(number);
                for (int i = 0; i < number; i++)
                    tests[i].cdEvent = e;
                watch.ReStart();
                for (int i = 0; i < number; i++)
                    synchronized (tests[i].arEvent) {
                        tests[i].notified = true;
                        tests[i].arEvent.notifyAll();
                    }
                e.await();
                double result = 0;
                for (int i = 0; i < number; i++)
                    result += tests[i].results;
                watch.AddAndReset(column, j, result);
            }
            for (int i = 0; i < number; i++)
                synchronized (tests[i].arEvent) {
                    tests[i].notified = true;
                    tests[i].arEvent.notifyAll();
                    //threads[i].join();
                }
        } catch (InterruptedException ex) {
            System.out.println(ex.getMessage());
        }
    }
}
