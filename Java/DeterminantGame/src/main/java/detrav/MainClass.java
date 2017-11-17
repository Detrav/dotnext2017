package detrav;


import java.util.Random;

public class MainClass {
    public static void main(String[] args) {
        ForJIT();
        //JIT
        System.out.println("Start Benchmark");
        ForJIT();
    }

    private static void ForJIT() {
        String[] strings = new String[4];
        for (int i = 0; i < 4; i++)
            strings[i] = "Matrix " + ( 3 + i );
        Watcher watcher = new Watcher(strings, 100);
        for (int i = 0; i < 4; i++)
            DeterminantGame(3 * i , i, watcher);
        watcher.Stop();
    }

    static void DeterminantGame(int size,int column, Watcher watch)
    {
        for (int j = 0; j < 100; j++) {
            watch.ReStart();
            Matrix m = GetRandomMatrix(3571 * j, size, size);
            double result = m.Determinant();
            watch.AddAndReset(column, j, result);
        }
    }

    static Matrix GetRandomMatrix(int seed,int width, int height)
    {
        Random r = new Random(seed);
        Matrix m = new Matrix(width, height);
        for (int i = 0; i < m.getWidth(); i++)
            for (int j = 0; j < m.getHeight(); j++)
                m.set(i, j,r.nextDouble());
        return m;
    }
}