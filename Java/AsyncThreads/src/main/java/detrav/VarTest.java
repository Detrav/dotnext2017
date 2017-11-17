package detrav;

import java.util.concurrent.CountDownLatch;

class VarTest implements Runnable {

    public CountDownLatch cdEvent;
    public final Object arEvent;
    int count = 0;
    public double results;
    public boolean notified = false;


    public VarTest(CountDownLatch e1, Object e2, int c) {
        cdEvent = e1;
        arEvent = e2;
        count = c;
    }


    @Override
    public void run() {
        try {
            synchronized (arEvent) {
                while (!notified)
                  arEvent.wait();
                notified = false;
            }
            for (int j = 0; j < 100; j++) {
                float floatVar = 0.5f;
                for (int i = 0; i < count; i++)
                    floatVar = 3.7f * floatVar * (1.0f - floatVar);
                results = floatVar;
                double doubleVar = 0.5;
                for (int i = 0; i < count; i++)
                    doubleVar = 3.7 * doubleVar * (1.0 - doubleVar);
                results = doubleVar;
                int intVar = 1;
                for (int i = 0; i < count; i++)
                    intVar = (5987 * intVar + 5987) / 5981;
                results += intVar;
                long longVar = 1;
                for (int i = 0; i < count; i++)
                    longVar = (5987L * longVar + 5987L) / 5981L;
                results += longVar;
                cdEvent.countDown();
                synchronized (arEvent) {
                    while (!notified)
                        arEvent.wait();
                    notified = false;
                }
            }
        } catch (InterruptedException ex) {
            System.out.println(ex.getMessage());
        }
    }
}