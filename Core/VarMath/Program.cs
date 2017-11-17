using System;

namespace VarMath
{
    class Program
    {
        static void Main(string[] args)
        {

            Watcher watch = new Watcher(new string[] {
                "1000000 float",
                "1000000 double",
                "1000000 int",
                "1000000 long",
                "1000000 random next",
                "1000000 random double",
                "1000000 random 1kb",
                "1000000 pow",
                "1000000 sqrt",
                "1000000 arctg",
                "1000000 round",
                "1000000 strange math",
                "10000000 float",
                "10000000 double",
                "10000000 int",
                "10000000 long",
                "10000000 random next",
                "10000000 random double",
                "10000000 random 1kb",
                "10000000 pow",
                "10000000 sqrt",
                "10000000 arctg",
                "10000000 round",
                "10000000 strange math",
            }, 100);
            double[] buffer;
            VarTest(1000000, 0, watch);
            buffer = RandomTest(1000000, 4, watch);
            MathTest(1000000, buffer, 7, watch);
            StrangeTest(1000000, 11, watch);
            VarTest(10000000, 12, watch);
            buffer = RandomTest(10000000, 16, watch);
            MathTest(10000000, buffer, 19, watch);
            StrangeTest(10000000, 23, watch);
            watch.Stop();
        }

        private static void VarTest(int count, int column, Watcher watch)
        {
            for (int j = 0; j < 100; j++)
            {
                watch.ReStart();
                float floatVar = 0.5f;
                for (int i = 0; i < count; i++)
                    floatVar = 3.7f * floatVar * (1.0f - floatVar);
                watch.AddAndReset(column, j, floatVar);
                double doubleVar = 0.5;
                for (int i = 0; i < count; i++)
                    doubleVar = 3.7 * doubleVar * (1.0 - doubleVar);
                watch.AddAndReset(column + 1, j, doubleVar);
                int intVar = 1;
                for (int i = 0; i < count; i++)
                    intVar = (5987 * intVar + 5987) / 5981;
                watch.AddAndReset(column + 2, j, intVar);
                long longVar = 1;
                for (int i = 0; i < count; i++)
                    longVar = (5987L * longVar + 5987L) / 5981L;
                watch.AddAndReset(column + 3, j, longVar);
            }
        }

        private static double[] RandomTest(int count, int column, Watcher watch)
        {
            int j;
            Random r;
            for (j = 0; j < 100; j++)
            {
                r = new Random((j + 1) * 5981);
                watch.ReStart();
                float varF = 0;
                for (int i = 0; i < count; i++)
                    varF = r.Next();
                watch.AddAndReset(column, j, varF);
                double varD = 0;
                for (int i = 0; i < count; i++)
                    varD = r.NextDouble();
                watch.AddAndReset(column + 1, j, varD);
                byte[] buffer = new byte[1024];
                for (int i = 0; i < count / 1024; i++)
                    r.NextBytes(buffer);
                watch.AddAndReset(column + 2, j, buffer[buffer.Length-1]);
            }
            r = new Random((j + 1) * 5981);
            double[] result = new double[count];
            for (int i = 0; i < count; i++)
                result[i] = r.NextDouble();
            return result;
        }

        private static void MathTest(int count, double[] array, int column, Watcher watch)
        {
            for (int j = 0; j < 100; j++)
            {
                watch.ReStart();
                double result = 0;
                for (int i = 0; i < count; i++)
                    result += Math.Pow(array[i], array[count - i - 1]);
                watch.AddAndReset(column, j, result);
                result = 0;
                for (int i = 0; i < count; i++)
                    result += Math.Sqrt(array[i]);
                result = 0;
                watch.AddAndReset(column + 1, j, result);
                result = 0;
                for (int i = 0; i < count; i++)
                    result += Math.Atan2(array[i], array[count - i - 1]);
                watch.AddAndReset(column + 2, j, result);
                result = 0;
                for (int i = 0; i < count; i++)
                    result += Math.Round(array[i]);
                watch.AddAndReset(column + 3, j, result);
            }
        }

        private static void StrangeTest(int count, int column, Watcher watch)
        {
            for (int j = 0; j < 100; j++)
            {
                watch.ReStart();
                double doubleVar = 0.5;
                double resultVar = 1;
                for (int i = 0; i < count; i++)
                {
                    doubleVar = 3.7 * doubleVar * (1.0 - doubleVar);
                    resultVar = Math.Atan(Math.Sqrt(
                        Math.Pow(Math.Sin((Math.PI * doubleVar + Math.E * doubleVar + resultVar)), 2.0) -
                        Math.Pow(Math.Cos((Math.PI * doubleVar + Math.E * doubleVar + resultVar)), 2.0)));
                }
                watch.AddAndReset(column, j, resultVar);
            }
        }
    }
}