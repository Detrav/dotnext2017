using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AsyncThreads
{
    class VarTest
    {

        CountdownEvent cdEvent;
        AutoResetEvent arEvent;
        int count = 0;
        public double Results { get; private set; }
        

        public VarTest(CountdownEvent e1, AutoResetEvent e2, int c)
        {
            cdEvent = e1;
            arEvent = e2;
            count = c;
        }


        public void Test()
        {
            arEvent.WaitOne();
            for (int j = 0; j < 100; j++)
            {
                float floatVar = 0.5f;
                for (int i = 0; i < count; i++)
                    floatVar = 3.7f * floatVar * (1.0f - floatVar);
                Results = floatVar;
                double doubleVar = 0.5;
                for (int i = 0; i < count; i++)
                    doubleVar = 3.7 * doubleVar * (1.0 - doubleVar);
                Results = doubleVar;
                int intVar = 1;
                for (int i = 0; i < count; i++)
                    intVar = (5987 * intVar + 5987) / 5981;
                Results += intVar;
                long longVar = 1;
                for (int i = 0; i < count; i++)
                    longVar = (5987L * longVar + 5987L) / 5981L;
                Results += longVar;
                cdEvent.Signal();
                arEvent.WaitOne();
            }
        }
    }
}
