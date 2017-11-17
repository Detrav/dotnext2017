package detrav;





public class MainClass {
    public static void main(String[] args) {

        ForJIT();
        //JIT
        System.out.println("Start Benchmark");
        ForJIT();
    }

    private static void ForJIT() {
        Watcher watch = new Watcher(new String[]
                {
                        "100 Simple",
                        "1000 Simple",
                        "10000 Simple",
                        "10 Dice",
                        "20 Dice",
                        "30 Dice",
                        "3 3 Ackermann",
                        "3 6 Ackermann",
                        "3 12 Ackermann",
                        "4 0 Ackermann",
                        "4 1 Ackermann",

                }, 100);
        SimpleTest(100, 0, watch);
        SimpleTest(1000, 1, watch);
        SimpleTest(10000, 2, watch);
        DiceTest(10, 3, watch);
        DiceTest(20, 4, watch);
        DiceTest(30, 5, watch);
        AckermannTest(3, 3, 6, watch);
        AckermannTest(3, 6, 7, watch);
        AckermannTest(3, 12, 8, watch);
        AckermannTest(4, 0, 9, watch);
        AckermannTest(4, 1, 10, watch);
        watch.Stop();
    }

    private static void SimpleTest(int count, int column, Watcher watch) {
        for (int j = 0; j < 100; j++) {
            watch.ReStart();
            SimpleRecursive(count);
            watch.AddAndReset(column, j, count);
        }
    }

    private static void SimpleRecursive(int left) {
        if (left < 0)
            return;
        SimpleRecursive(left - 1);
    }

    private static void DiceTest(int size, int column, Watcher watch) {
        for (int j = 0; j < 100; j++) {
            watch.ReStart();
            long count = 0;
            count = Dice(0, size);
            watch.AddAndReset(column, j,count);

        }
    }

    private static long Dice(long current, long size) {
        if (current == size)
            return 1;
        long count = 0;
        if (current < size) {
            count += Dice(current + 1, size);
            count += Dice(current + 2, size);
            count += Dice(current + 3, size);
            count += Dice(current + 4, size);
            count += Dice(current + 5, size);
            count += Dice(current + 6, size);
        }
        return count;

    }


    private static void AckermannTest(int count1, int count2, int column, Watcher watch) {

        for (int j = 0; j < 100; j++) {

            watch.ReStart();
            long results = Ackermann(count1, count2);
            watch.AddAndReset(column, j,results);
        }
    }

    private static long Ackermann(long m, long n) {
        if (m == 0)
            return n + 1;
        if (n == 0)
            return Ackermann(m - 1, 1);
        return Ackermann(m - 1, Ackermann(m, n - 1));
    }
}