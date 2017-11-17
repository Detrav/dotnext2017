package detrav;

import java.util.concurrent.CountDownLatch;

public class TaskRunner implements Runnable{
    private final CountDownLatch finished;

    public  TaskRunner(CountDownLatch countDownLatch)
    {
        finished = countDownLatch;
    }
    @Override
    public void run() {
        MainClass.ends++;
        finished.countDown();
    }
}