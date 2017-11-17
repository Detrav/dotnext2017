package detrav;

import java.util.concurrent.CountDownLatch;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.TimeUnit;

public class MainClass {
    public static void main(String[] args) throws InterruptedException {
        ForJIT();
        //JIT
        System.out.println("Start Benchmark");
        ForJIT();
    }

    private static void ForJIT() throws InterruptedException {
        Watcher watch = new Watcher(new String[]
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
    static final Object _locker = new Object();

    private static void TasksTest(int count, int column, Watcher watch) throws InterruptedException {
        ExecutorService service = Executors.newFixedThreadPool(2);
        for (int j = 0; j < 100; j++) {
            ends = 0;
            watch.ReStart();
            CountDownLatch countDownLatch = new CountDownLatch(count);
            for (int i = 0; i < count; i++) {
                service.execute(new TaskRunner(countDownLatch, i));
            }
            countDownLatch.await();
            watch.AddAndReset(column, j,ends);
        }
        service.shutdown();
    }
}
