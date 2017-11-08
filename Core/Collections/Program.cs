using System;
using System.Collections.Generic;

namespace Collections
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> tests = new List<string>();
            tests.AddRange(GetArrayTests());
            tests.AddRange(GetMapTests());
            tests.AddRange(GetQueueTests());
            tests.AddRange(GetStackTests());
            Watcher watch = new Watcher(tests.ToArray(), 100);
            int column = 0;
            for (int i = 10000; i <= 1000000; i *= 10)
            {
                ArrayObjectTest(i, column, watch);
                column += 7;
                ArrayIntTest(i, column, watch);
                column += 7;
                ArrayLongTest(i, column, watch);
                column += 7;
                ArrayStringTest(i, column, watch);
                column += 7;
            }
            for (int i = 10000; i <= 1000000; i *= 10)
            {
                MapObjectTest(i, column, watch);
                column += 2;
                MapIntTest(i, column, watch);
                column += 2;
                MapLongTest(i, column, watch);
                column += 2;
                MapStringTest(i, column, watch);
                column += 2;
            }


            for (int i = 10000; i <= 1000000; i *= 10)
            {
                QueueObjectTest(i, column, watch);
                column += 3;
                QueueIntTest(i, column, watch);
                column += 3;
                QueueLongTest(i, column, watch);
                column += 3;
                QueueStringTest(i, column, watch);
                column += 3;
            }

            for (int i = 10000; i <= 1000000; i *= 10)
            {
                StackObjectTest(i, column, watch);
                column += 2;
                StackIntTest(i, column, watch);
                column += 2;
                StackLongTest(i, column, watch);
                column += 2;
                StackStringTest(i, column, watch);
                column += 2;
            }

            watch.Stop();
        }

        private static string[] GetArrayTests()
        {
            List<string> list = new List<string>();
            string[] types = new string[] { "object", "int", "long", "string" };
            for (int i = 10000; i <= 1000000; i *= 10)
            {
                for (int j = 0; j < types.Length; j++)
                {
                    list.Add("Array " + i.ToString() + " " + types[j] + " Create");
                    list.Add("Array " + i.ToString() + " " + types[j] + " Reverse");
                    list.Add("Array " + i.ToString() + " " + types[j] + " Reverse by 10");
                    list.Add("Array " + i.ToString() + " " + types[j] + " Resize x2");
                    list.Add("Array " + i.ToString() + " " + types[j] + " Resize x4");
                    list.Add("Array " + i.ToString() + " " + types[j] + " Delete 7/8 + Resize x1/8");
                    list.Add("Array " + i.ToString() + " " + types[j] + " Replace odd and even");
                }
            }
            return list.ToArray();
        }

        private static string[] GetMapTests()
        {
            List<string> list = new List<string>();
            string[] types = new string[] { "object", "int", "long", "string" };
            for (int i = 10000; i <= 1000000; i *= 10)
            {
                for (int j = 0; j < types.Length; j++)
                {
                    list.Add("Map " + i.ToString() + " " + types[j] + " Create");
                    list.Add("Map " + i.ToString() + " " + types[j] + " Replace odd and even");
                }
            }
            return list.ToArray();
        }

        private static string[] GetStackTests()
        {
            List<string> list = new List<string>();
            string[] types = new string[] { "object", "int", "long", "string" };
            for (int i = 10000; i <= 1000000; i *= 10)
            {
                for (int j = 0; j < types.Length; j++)
                {
                    list.Add("Stack " + i.ToString() + " " + types[j] + " Create");
                    list.Add("Stack " + i.ToString() + " " + types[j] + " Reverse");
                }
            }
            return list.ToArray();
        }

        private static string[] GetQueueTests()
        {
            List<string> list = new List<string>();
            string[] types = new string[] { "object", "int", "long", "string" };
            for (int i = 10000; i <= 1000000; i *= 10)
            {
                for (int j = 0; j < types.Length; j++)
                {
                    list.Add("Queue " + i.ToString() + " " + types[j] + " Create");
                    list.Add("Queue " + i.ToString() + " " + types[j] + " dequeue to queue");
                    list.Add("Queue " + i.ToString() + " " + types[j] + " dequeue");
                }
            }
            return list.ToArray();
        }

        private static void ArrayObjectTest(int count, int column, Watcher watch)
        {
            for (int j = 0; j < 100; j++)
            {
                watch.ReStart();
                object[] arr = new object[count];
                for (int i = 0; i < arr.Length; i++)
                    arr[i] = new object();
                watch.AddAndReset(column, j, arr[arr.Length - 1]);
                object[] tempArr = new object[arr.Length];
                for (int i = 0; i < arr.Length; i++)
                    tempArr[tempArr.Length - i - 1] = arr[i];
                arr = tempArr;
                watch.AddAndReset(column + 1, j, arr[arr.Length - 1]);
                tempArr = new object[arr.Length];
                for (int i = 0; i < arr.Length; i += 10)
                    for (int k = 0; k < 10; k++)
                        tempArr[i + 9 - k] = arr[i + k];
                arr = tempArr;
                watch.AddAndReset(column + 2, j, arr[arr.Length - 1]);
                Array.Resize(ref arr, arr.Length * 2);
                for (int i = arr.Length - 1; i >= 0; i -= 2)
                    arr[i] = arr[(i + 1) / 2];
                watch.AddAndReset(column + 3, j, arr[arr.Length - 1]);
                Array.Resize(ref arr, arr.Length * 4);
                for (int i = arr.Length - 1; i >= 0; i -= 4)
                    arr[i] = arr[(i + 1) / 4];
                watch.AddAndReset(column + 4, j, arr[arr.Length - 1]);
                for (int i = 0; i < arr.Length; i += 8)
                    arr[(i + 1) / 8] = arr[i];
                Array.Resize(ref arr, arr.Length / 8);
                watch.AddAndReset(column + 5, j, arr[arr.Length - 1]);
                for (int i = 0; i < arr.Length; i += 2)
                {
                    object temp = arr[i];
                    arr[i] = arr[i + 1];
                    arr[i + 1] = temp;
                }
                watch.AddAndReset(column + 6, j, arr[arr.Length - 1]);
            }
        }

        private static void ArrayIntTest(int count, int column, Watcher watch)
        {
            for (int j = 0; j < 100; j++)
            {
                watch.ReStart();
                int[] arr = new int[count];
                for (int i = 0; i < arr.Length; i++)
                    arr[i] = i;
                watch.AddAndReset(column, j, arr[arr.Length - 1]);
                int[] tempArr = new int[arr.Length];
                for (int i = 0; i < arr.Length; i++)
                    tempArr[tempArr.Length - i - 1] = arr[i];
                arr = tempArr;
                watch.AddAndReset(column + 1, j, arr[arr.Length - 1]);
                tempArr = new int[arr.Length];
                for (int i = 0; i < arr.Length; i += 10)
                    for (int k = 0; k < 10; k++)
                        tempArr[i + 9 - k] = arr[i + k];
                arr = tempArr;
                watch.AddAndReset(column + 2, j, arr[arr.Length - 1]);
                Array.Resize(ref arr, arr.Length * 2);
                for (int i = arr.Length - 1; i >= 0; i -= 2)
                    arr[i] = arr[(i + 1) / 2];
                watch.AddAndReset(column + 3, j, arr[arr.Length - 1]);
                Array.Resize(ref arr, arr.Length * 4);
                for (int i = arr.Length - 1; i >= 0; i -= 4)
                    arr[i] = arr[(i + 1) / 4];
                watch.AddAndReset(column + 4, j, arr[arr.Length - 1]);
                for (int i = 0; i < arr.Length; i += 8)
                    arr[(i + 1) / 8] = arr[i];
                Array.Resize(ref arr, arr.Length / 8);
                watch.AddAndReset(column + 5, j, arr[arr.Length - 1]);
                for (int i = 0; i < arr.Length; i += 2)
                {
                    int temp = arr[i];
                    arr[i] = arr[i + 1];
                    arr[i + 1] = temp;
                }
                watch.AddAndReset(column + 6, j, arr[arr.Length - 1]);
            }
        }

        private static void ArrayLongTest(int count, int column, Watcher watch)
        {
            for (int j = 0; j < 100; j++)
            {
                watch.ReStart();
                long[] arr = new long[count];
                for (long i = 0; i < arr.Length; i++)
                    arr[i] = i;
                watch.AddAndReset(column, j, arr[arr.Length - 1]);
                long[] tempArr = new long[arr.Length];
                for (int i = 0; i < arr.Length; i++)
                    tempArr[tempArr.Length - i - 1] = arr[i];
                arr = tempArr;
                watch.AddAndReset(column + 1, j, arr[arr.Length - 1]);
                tempArr = new long[arr.Length];
                for (int i = 0; i < arr.Length; i += 10)
                    for (int k = 0; k < 10; k++)
                        tempArr[i + 9 - k] = arr[i + k];
                arr = tempArr;
                watch.AddAndReset(column + 2, j, arr[arr.Length - 1]);
                Array.Resize(ref arr, arr.Length * 2);
                for (int i = arr.Length - 1; i >= 0; i -= 2)
                    arr[i] = arr[(i + 1) / 2];
                watch.AddAndReset(column + 3, j, arr[arr.Length - 1]);
                Array.Resize(ref arr, arr.Length * 4);
                for (int i = arr.Length - 1; i >= 0; i -= 4)
                    arr[i] = arr[(i + 1) / 4];
                watch.AddAndReset(column + 4, j, arr[arr.Length - 1]);
                for (int i = 0; i < arr.Length; i += 8)
                    arr[(i + 1) / 8] = arr[i];
                Array.Resize(ref arr, arr.Length / 8);
                watch.AddAndReset(column + 5, j, arr[arr.Length - 1]);
                for (int i = 0; i < arr.Length; i += 2)
                {
                    long temp = arr[i];
                    arr[i] = arr[i + 1];
                    arr[i + 1] = temp;
                }
                watch.AddAndReset(column + 6, j, arr[arr.Length - 1]);
            }
        }
        private static void ArrayStringTest(int count, int column, Watcher watch)
        {
            for (int j = 0; j < 100; j++)
            {
                watch.ReStart();
                string[] arr = new string[count];
                for (int i = 0; i < arr.Length; i++)
                    arr[i] = i.ToString();
                watch.AddAndReset(column, j, arr[arr.Length - 1]);
                string[] tempArr = new string[arr.Length];
                for (int i = 0; i < arr.Length; i++)
                    tempArr[tempArr.Length - i - 1] = arr[i];
                arr = tempArr;
                watch.AddAndReset(column + 1, j, arr[arr.Length - 1]);
                tempArr = new string[arr.Length];
                for (int i = 0; i < arr.Length; i += 10)
                    for (int k = 0; k < 10; k++)
                        tempArr[i + 9 - k] = arr[i + k];
                arr = tempArr;
                watch.AddAndReset(column + 2, j, arr[arr.Length - 1]);
                Array.Resize(ref arr, arr.Length * 2);
                for (int i = arr.Length - 1; i >= 0; i -= 2)
                    arr[i] = arr[(i + 1) / 2];
                watch.AddAndReset(column + 3, j, arr[arr.Length - 1]);
                Array.Resize(ref arr, arr.Length * 4);
                for (int i = arr.Length - 1; i >= 0; i -= 4)
                    arr[i] = arr[(i + 1) / 4];
                watch.AddAndReset(column + 4, j, arr[arr.Length - 1]);
                for (int i = 0; i < arr.Length; i += 8)
                    arr[(i + 1) / 8] = arr[i];
                Array.Resize(ref arr, arr.Length / 8);
                watch.AddAndReset(column + 5, j, arr[arr.Length - 1]);
                for (int i = 0; i < arr.Length; i += 2)
                {
                    string temp = arr[i];
                    arr[i] = arr[i + 1];
                    arr[i + 1] = temp;
                }
                watch.AddAndReset(column + 6, j, arr[arr.Length - 1]);
            }
        }

        private static void MapObjectTest(int count, int column, Watcher watch)
        {
            for (int j = 0; j < 100; j++)
            {
                watch.ReStart();
                Dictionary<object, object> arr = new Dictionary<object, object>();
                for (int i = 0; i < count; i++)
                    arr[new object()] = new object();
                watch.AddAndReset(column, j, arr);
                object lastKey = null;
                object[] keys = new object[arr.Keys.Count]; int keyIndex = 0; foreach (object key in arr.Keys) keys[keyIndex++] = key;
                foreach (object key in keys)
                {
                    if (lastKey != null)
                    {
                        object temp = arr[key];
                        arr[key] = arr[lastKey];
                        arr[lastKey] = temp;
                        lastKey = null;
                    }
                    else
                        lastKey = key;
                }
                watch.AddAndReset(column + 1, j, arr.Count);
            }
        }

        private static void MapIntTest(int count, int column, Watcher watch)
        {
            for (int j = 0; j < 100; j++)
            {
                watch.ReStart();
                Dictionary<int, object> arr = new Dictionary<int, object>();
                for (int i = 0; i < count; i++)
                    arr[i] = new object();
                watch.AddAndReset(column, j, arr.Count);
                int lastKey = 0;
                int[] keys = new int[arr.Keys.Count]; int keyIndex = 0; foreach (int key in arr.Keys) keys[keyIndex++] = key;
                foreach (var key in keys)
                {
                    if (lastKey >= 0)
                    {
                        object temp = arr[key];
                        arr[key] = arr[lastKey];
                        arr[lastKey] = temp;
                        lastKey = -1;
                    }
                    else
                        lastKey = key;
                }
                watch.AddAndReset(column + 1, j, arr.Count);
            }
        }

        private static void MapLongTest(int count, int column, Watcher watch)
        {
            for (int j = 0; j < 100; j++)
            {
                watch.ReStart();
                Dictionary<long, object> arr = new Dictionary<long, object>();
                for (long i = 0; i < count; i++)
                    arr[i] = new object();
                watch.AddAndReset(column, j, arr);
                long lastKey = -1;
                long[] keys = new long[arr.Keys.Count]; long keyIndex = 0; foreach (long key in arr.Keys) keys[keyIndex++] = key;
                foreach (long key in keys)
                {
                    if (lastKey >= 0)
                    {
                        object temp = arr[key];
                        arr[key] = arr[lastKey];
                        arr[lastKey] = temp;
                        lastKey = -1;
                    }
                    else
                        lastKey = key;
                }
                watch.AddAndReset(column + 1, j, arr.Count);
            }
        }

        private static void MapStringTest(int count, int column, Watcher watch)
        {
            for (int j = 0; j < 100; j++)
            {
                watch.ReStart();
                Dictionary<string, object> arr = new Dictionary<string, object>();
                for (int i = 0; i < count; i++)
                    arr[i.ToString()] = new object();
                watch.AddAndReset(column, j, arr);
                string lastKey = null;
                string[] keys = new string[arr.Keys.Count]; long keyIndex = 0; foreach (string key in arr.Keys) keys[keyIndex++] = key;
                foreach (string key in keys)
                {
                    if (lastKey != null)
                    {
                        object temp = arr[key];
                        arr[key] = arr[lastKey];
                        arr[lastKey] = temp;
                        lastKey = null;
                    }
                    else
                        lastKey = key;
                }
                watch.AddAndReset(column + 1, j, arr.Count);
            }
        }

        private static void QueueObjectTest(int count, int column, Watcher watch)
        {
            for (int j = 0; j < 100; j++)
            {
                watch.ReStart();
                Queue<object> arr = new Queue<object>();
                for (int i = 0; i < count; i++)
                    arr.Enqueue(new object());
                watch.AddAndReset(column, j, arr);
                Queue<object> tempArr = new Queue<object>();
                object obj = null;
                while (arr.TryDequeue(out obj))
                    tempArr.Enqueue(obj);
                tempArr = arr;
                watch.AddAndReset(column + 1, j, arr);
                while (arr.TryDequeue(out obj)) ;
                watch.AddAndReset(column + 2, j, arr);
            }
        }

        private static void QueueIntTest(int count, int column, Watcher watch)
        {
            for (int j = 0; j < 100; j++)
            {
                watch.ReStart();
                Queue<int> arr = new Queue<int>();
                for (int i = 0; i < count; i++)
                    arr.Enqueue(i);
                watch.AddAndReset(column, j, arr.Count);
                Queue<int> tempArr = new Queue<int>();
                int obj = -1;
                while (arr.TryDequeue(out obj))
                    tempArr.Enqueue(obj);
                tempArr = arr;
                watch.AddAndReset(column + 1, j, arr.Count);
                while (arr.TryDequeue(out obj)) ;
                watch.AddAndReset(column + 2, j, arr.Count);
            }
        }
        private static void QueueLongTest(int count, int column, Watcher watch)
        {
            for (int j = 0; j < 100; j++)
            {
                watch.ReStart();
                Queue<long> arr = new Queue<long>();
                for (long i = 0; i < count; i++)
                    arr.Enqueue(i);
                watch.AddAndReset(column, j, arr.Count);
                Queue<long> tempArr = new Queue<long>();
                long obj = -1;
                while (arr.TryDequeue(out obj))
                    tempArr.Enqueue(obj);
                tempArr = arr;
                watch.AddAndReset(column + 1, j, arr.Count);
                while (arr.TryDequeue(out obj)) ;
                watch.AddAndReset(column + 2, j, arr.Count);
            }
        }
        private static void QueueStringTest(int count, int column, Watcher watch)
        {
            for (int j = 0; j < 100; j++)
            {
                watch.ReStart();
                Queue<string> arr = new Queue<string>();
                for (int i = 0; i < count; i++)
                    arr.Enqueue(i.ToString());
                watch.AddAndReset(column, j, arr.Count);
                Queue<string> tempArr = new Queue<string>();
                string obj = null;
                while (arr.TryDequeue(out obj))
                    tempArr.Enqueue(obj);
                tempArr = arr;
                watch.AddAndReset(column + 1, j, arr.Count);
                while (arr.TryDequeue(out obj)) ;
                watch.AddAndReset(column + 2, j, arr.Count);
            }
        }

        private static void StackObjectTest(int count, int column, Watcher watch)
        {
            for (int j = 0; j < 100; j++)
            {
                watch.ReStart();
                Stack<object> arr = new Stack<object>();
                for (int i = 0; i < count; i++)
                    arr.Push(new object());
                watch.AddAndReset(column, j, arr.Count);
                Stack<object> tempArr = new Stack<object>();
                object obj = null;
                while (arr.TryPop(out obj))
                    tempArr.Push(obj);
                tempArr = arr;
                watch.AddAndReset(column + 1, j, arr.Count);
            }
        }

        private static void StackIntTest(int count, int column, Watcher watch)
        {
            for (int j = 0; j < 100; j++)
            {
                watch.ReStart();
                Stack<int> arr = new Stack<int>();
                for (int i = 0; i < count; i++)
                    arr.Push(i);
                watch.AddAndReset(column, j, arr.Count);
                Stack<int> tempArr = new Stack<int>();
                int obj = -1;
                while (arr.TryPop(out obj))
                    tempArr.Push(obj);
                tempArr = arr;
                watch.AddAndReset(column + 1, j, arr.Count);
            }
        }

        private static void StackLongTest(int count, int column, Watcher watch)
        {
            for (int j = 0; j < 100; j++)
            {
                watch.ReStart();
                Stack<long> arr = new Stack<long>();
                for (long i = 0; i < count; i++)
                    arr.Push(i);
                watch.AddAndReset(column, j, arr.Count);
                Stack<long> tempArr = new Stack<long>();
                long obj = -1;
                while (arr.TryPop(out obj))
                    tempArr.Push(obj);
                tempArr = arr;
                watch.AddAndReset(column + 1, j, arr.Count);
            }
        }

        private static void StackStringTest(int count, int column, Watcher watch)
        {
            for (int j = 0; j < 100; j++)
            {
                watch.ReStart();
                Stack<string> arr = new Stack<string>();
                for (int i = 0; i < count; i++)
                    arr.Push(i.ToString());
                watch.AddAndReset(column, j, arr.Count);
                Stack<string> tempArr = new Stack<string>();
                string obj = null;
                while (arr.TryPop(out obj))
                    tempArr.Push(obj);
                tempArr = arr;
                watch.AddAndReset(column + 1, j, arr.Count);
            }
        }
    }
}