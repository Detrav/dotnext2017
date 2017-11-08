using System;

namespace DeterminantGame
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] strings = new string[4];
            for (int i = 0; i < 4; i++)
                strings[i] = "Matrix " + (3+ i ).ToString();
            Watcher watcher = new Watcher(strings, 100);
            for (int i = 0; i < 4; i++)
                DeterminantGame(3 * i , i, watcher);
            watcher.Stop();
        }

        static void DeterminantGame(int size,int column, Watcher watch)
        {
            for (int j = 0; j < 100; j++)
            {
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
            for (int i = 0; i < m.Width; i++)
                for (int j = 0; j < m.Height; j++)
                    m[i, j] = r.NextDouble() ;
            return m;
        }
    }
}
