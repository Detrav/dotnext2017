package detrav;

import java.util.concurrent.CountDownLatch;

public class TaskRunner implements Runnable{
    private final CountDownLatch finished;
    private final int l;

    public  TaskRunner(CountDownLatch countDownLatch,int l)
    {
        finished = countDownLatch;
        this.l = l;
    }
    @Override
    public void run() {
        if (l % 3 == 0) {
            synchronized (MainClass._locker) {
                MainClass.ends++;
            }
        } else MainClass.ends++;
        finished.countDown();
    }
}