package detrav;



import java.util.*;

public class MainClass {
    public static void main(String[] args)  {
        ForJIT();
        //JIT
        System.out.println("Start Benchmark");
        ForJIT();
    }

    private static void ForJIT() {
        ArrayList<String> tests = new ArrayList<String>();
        tests.addAll(GetArrayTests());
        tests.addAll(GetMapTests());
        tests.addAll(GetQueueTests());
        tests.addAll(GetStackTests());
        String[] testsArr = new String[tests.size()];
        testsArr = tests.toArray(testsArr);
        Watcher watch = new Watcher(testsArr, 100);
        int column = 0;
        for (int i = 10000; i <= 1000000; i *= 10) {
            ArrayObjectTest(i, column, watch);
            column += 7;
            ArrayIntTest(i, column, watch);
            column += 7;
            ArrayLongTest(i, column, watch);
            column += 7;
            ArrayStringTest(i, column, watch);
            column += 7;
        }

        for (int i = 10000; i <= 1000000; i *= 10) {
            MapObjectTest(i, column, watch);
            column += 2;
            MapIntTest(i, column, watch);
            column += 2;
            MapLongTest(i, column, watch);
            column += 2;
            MapStringTest(i, column, watch);
            column += 2;
        }


        for (int i = 10000; i <= 1000000; i *= 10) {
            QueueObjectTest(i, column, watch);
            column += 3;
            QueueIntTest(i, column, watch);
            column += 3;
            QueueLongTest(i, column, watch);
            column += 3;
            QueueStringTest(i, column, watch);
            column += 3;
        }

        for (int i = 10000; i <= 1000000; i *= 10) {
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


    private static ArrayList<String> GetArrayTests() {
        ArrayList<String> list = new ArrayList<String>();
        String[] types = new String[]{"object", "int", "long", "string"};
        for (int i = 10000; i <= 1000000; i *= 10) {
            for (int j = 0; j < types.length; j++) {
                list.add("Array " + i + " " + types[j] + " Create");
                list.add("Array " + i + " " + types[j] + " Reverse");
                list.add("Array " + i + " " + types[j] + " Reverse by 10");
                list.add("Array " + i + " " + types[j] + " Resize x2");
                list.add("Array " + i + " " + types[j] + " Resize x4");
                list.add("Array " + i + " " + types[j] + " Delete 7/8 + Resize x1/8");
                list.add("Array " + i + " " + types[j] + " Replace odd and even");
            }
        }
        return list;
    }

    private static ArrayList<String> GetMapTests() {
        ArrayList<String> list = new ArrayList<String>();
        String[] types = new String[]{"object", "int", "long", "string"};
        for (int i = 10000; i <= 1000000; i *= 10) {
            for (int j = 0; j < types.length; j++) {
                list.add("Map " + i + " " + types[j] + " Create");
                list.add("Map " + i + " " + types[j] + " Replace odd and even");
            }
        }
        return list;
    }

    private static ArrayList<String> GetStackTests() {
        ArrayList<String> list = new ArrayList<String>();
        String[] types = new String[]{"object", "int", "long", "string"};
        for (int i = 10000; i <= 1000000; i *= 10) {
            for (int j = 0; j < types.length; j++) {
                list.add("Stack " + i + " " + types[j] + " Create");
                list.add("Stack " + i + " " + types[j] + " Reverse");
            }
        }
        return list;
    }

    private static ArrayList<String> GetQueueTests() {
        ArrayList<String> list = new ArrayList<String>();
        String[] types = new String[]{"object", "int", "long", "string"};
        for (int i = 10000; i <= 1000000; i *= 10) {
            for (int j = 0; j < types.length; j++) {
                list.add("Queue " + i + " " + types[j] + " Create");
                list.add("Queue " + i + " " + types[j] + " dequeue to queue");
                list.add("Queue " + i + " " + types[j] + " dequeue");
            }
        }
        return list;
    }

    private static void ArrayObjectTest(int count, int column, Watcher watch) {
        for (int j = 0; j < 100; j++) {
            watch.ReStart();
            Object[] arr = new Object[count];
            for (int i = 0; i < arr.length; i++)
                arr[i] = new Object();
            watch.AddAndReset(column, j,arr[count-1]);
            Object[] tempArr = new Object[arr.length];
            for (int i = 0; i < arr.length; i++)
                tempArr[tempArr.length - i - 1] = arr[i];
            arr = tempArr;
            watch.AddAndReset(column + 1, j,arr[count-1]);
            tempArr = new Object[arr.length];
            for (int i = 0; i < arr.length; i += 10)
                for (int k = 0; k < 10; k++)
                    tempArr[i + 9 - k] = arr[i + k];
            arr = tempArr;
            watch.AddAndReset(column + 2, j,arr[count-1]);
            arr = Arrays.copyOf(arr,arr.length*2);
            for (int i = arr.length - 1; i >= 0; i -= 2)
                arr[i] = arr[(i + 1) / 2];
            watch.AddAndReset(column + 3, j,arr[count-1]);
            arr = Arrays.copyOf(arr,arr.length*4);
            for (int i = arr.length - 1; i >= 0; i -= 4)
                arr[i] = arr[(i + 1) / 4];
            watch.AddAndReset(column + 4, j,arr[count-1]);
            for (int i = 0; i < arr.length; i += 8)
                arr[(i + 1) / 8] = arr[i];
            arr = Arrays.copyOf(arr,arr.length/8);
            watch.AddAndReset(column + 5, j,arr[count-1]);
            for (int i = 0; i < arr.length; i += 2) {
                Object temp = arr[i];
                arr[i] = arr[i + 1];
                arr[i + 1] = temp;
            }
            watch.AddAndReset(column + 6, j,arr[count-1]);
        }
    }

    private static void ArrayIntTest(int count, int column, Watcher watch) {
        for (int j = 0; j < 100; j++) {
            watch.ReStart();
            int[] arr = new int[count];
            for (int i = 0; i < arr.length; i++)
                arr[i] = i;
            watch.AddAndReset(column, j,arr[count-1]);
            int[] tempArr = new int[arr.length];
            for (int i = 0; i < arr.length; i++)
                tempArr[tempArr.length - i - 1] = arr[i];
            arr = tempArr;
            watch.AddAndReset(column + 1, j,arr[count-1]);
            tempArr = new int[arr.length];
            for (int i = 0; i < arr.length; i += 10)
                for (int k = 0; k < 10; k++)
                    tempArr[i + 9 - k] = arr[i + k];
            arr = tempArr;
            watch.AddAndReset(column + 2, j,arr[count-1]);
            arr = Arrays.copyOf(arr,arr.length*2);
            for (int i = arr.length - 1; i >= 0; i -= 2)
                arr[i] = arr[(i + 1) / 2];
            watch.AddAndReset(column + 3, j,arr[count-1]);
            arr = Arrays.copyOf(arr,arr.length*4);
            for (int i = arr.length - 1; i >= 0; i -= 4)
                arr[i] = arr[(i + 1) / 4];
            watch.AddAndReset(column + 4, j,arr[count-1]);
            for (int i = 0; i < arr.length; i += 8)
                arr[(i + 1) / 8] = arr[i];
            arr = Arrays.copyOf(arr,arr.length/8);
            watch.AddAndReset(column + 5, j,arr[count-1]);
            for (int i = 0; i < arr.length; i += 2) {
                int temp = arr[i];
                arr[i] = arr[i + 1];
                arr[i + 1] = temp;
            }
            watch.AddAndReset(column + 6, j,arr[count-1]);
        }
    }

    private static void ArrayLongTest(int count, int column, Watcher watch) {
        for (int j = 0; j < 100; j++) {
            watch.ReStart();
            long[] arr = new long[count];
            for (int i = 0; i < arr.length; i++)
                arr[i] = (long)i;
            watch.AddAndReset(column, j,arr[count-1]);
            long[] tempArr = new long[arr.length];
            for (int i = 0; i < arr.length; i++)
                tempArr[tempArr.length - i - 1] = arr[i];
            arr = tempArr;
            watch.AddAndReset(column + 1, j,arr[count-1]);
            tempArr = new long[arr.length];
            for (int i = 0; i < arr.length; i += 10)
                for (int k = 0; k < 10; k++)
                    tempArr[i + 9 - k] = arr[i + k];
            arr = tempArr;
            watch.AddAndReset(column + 2, j,arr[count-1]);
            arr = Arrays.copyOf(arr,arr.length*2);
            for (int i = arr.length - 1; i >= 0; i -= 2)
                arr[i] = arr[(i + 1) / 2];
            watch.AddAndReset(column + 3, j,arr[count-1]);
            arr = Arrays.copyOf(arr,arr.length*4);
            for (int i = arr.length - 1; i >= 0; i -= 4)
                arr[i] = arr[(i + 1) / 4];
            watch.AddAndReset(column + 4, j,arr[count-1]);
            for (int i = 0; i < arr.length; i += 8)
                arr[(i + 1) / 8] = arr[i];
            arr = Arrays.copyOf(arr,arr.length/8);
            watch.AddAndReset(column + 5, j,arr[count-1]);
            for (int i = 0; i < arr.length; i += 2) {
                long temp = arr[i];
                arr[i] = arr[i + 1];
                arr[i + 1] = temp;
            }
            watch.AddAndReset(column + 6, j,arr[count-1]);
        }
    }

    private static void ArrayStringTest(int count, int column, Watcher watch) {
        for (int j = 0; j < 100; j++) {
            watch.ReStart();
            String[] arr = new String[count];
            for (int i = 0; i < arr.length; i++)
                arr[i] = "" + i;
            watch.AddAndReset(column, j,arr[count-1]);
            String[] tempArr = new String[arr.length];
            for (int i = 0; i < arr.length; i++)
                tempArr[tempArr.length - i - 1] = arr[i];
            arr = tempArr;
            watch.AddAndReset(column + 1, j,arr[count-1]);
            tempArr = new String[arr.length];
            for (int i = 0; i < arr.length; i += 10)
                for (int k = 0; k < 10; k++)
                    tempArr[i + 9 - k] = arr[i + k];
            arr = tempArr;
            watch.AddAndReset(column + 2, j,arr[count-1]);
            arr = Arrays.copyOf(arr,arr.length*2);
            for (int i = arr.length - 1; i >= 0; i -= 2)
                arr[i] = arr[(i + 1) / 2];
            watch.AddAndReset(column + 3, j,arr[count-1]);
            arr = Arrays.copyOf(arr,arr.length*4);
            for (int i = arr.length - 1; i >= 0; i -= 4)
                arr[i] = arr[(i + 1) / 4];
            watch.AddAndReset(column + 4, j,arr[count-1]);
            for (int i = 0; i < arr.length; i += 8)
                arr[(i + 1) / 8] = arr[i];
            arr = Arrays.copyOf(arr,arr.length/8);
            watch.AddAndReset(column + 5, j,arr[count-1]);
            for (int i = 0; i < arr.length; i += 2) {
                String temp = arr[i];
                arr[i] = arr[i + 1];
                arr[i + 1] = temp;
            }
            watch.AddAndReset(column + 6, j,arr[count-1]);
        }
    }



    private static void MapObjectTest(int count, int column, Watcher watch) {
        for (int j = 0; j < 100; j++) {
            watch.ReStart();
            HashMap<Object, Object> arr = new HashMap<Object, Object>();
            for (int i = 0; i < count; i++)
                arr.put(new Object(), new Object());
            watch.AddAndReset(column, j,arr.size());
            Object lastKey = null;
            for(Object key : arr.keySet())
            {
                if (lastKey != null) {
                    Object temp = arr.get(key);
                    arr.put(key, arr.get(lastKey));
                    arr.put(lastKey, temp);
                    lastKey = null;
                } else
                    lastKey = key;
            }
            watch.AddAndReset(column + 1, j,arr.size());
        }
    }

    private static void MapIntTest(int count, int column, Watcher watch) {
        for (int j = 0; j < 100; j++) {
            watch.ReStart();
            HashMap<Integer, Object> arr = new HashMap<Integer, Object>();
            for (int i = 0; i < count; i++)
                arr.put(i, new Object());
            watch.AddAndReset(column, j,arr.size());
            int lastKey = -1;
            for(int key : arr.keySet())
            {
                if (lastKey >= 0) {
                    Object temp = arr.get(key);
                    arr.put(key, arr.get(lastKey));
                    arr.put(lastKey, temp);
                    lastKey = -1;
                } else
                    lastKey = key;
            }
            watch.AddAndReset(column + 1, j,arr.size());
        }
    }

    private static void MapLongTest(int count, int column, Watcher watch) {
        for (int j = 0; j < 100; j++) {
            watch.ReStart();
            HashMap<Long, Object> arr = new HashMap<Long, Object>();
            for (long i = 0; i < count; i++)
                arr.put(i, new Object());
            watch.AddAndReset(column, j,arr.size());
            long lastKey = -1;
            for(long key : arr.keySet())
            {
                if (lastKey >= 0) {
                    Object temp = arr.get(key);
                    arr.put(key, arr.get(lastKey));
                    arr.put(lastKey, temp);
                    lastKey = -1;
                } else
                    lastKey = key;
            }
            watch.AddAndReset(column + 1, j,arr.size());
        }
    }

    private static void MapStringTest(int count, int column, Watcher watch) {
        for (int j = 0; j < 100; j++) {
            watch.ReStart();
            HashMap<String, Object> arr = new HashMap<String, Object>();
            for (int i = 0; i < count; i++)
                arr.put("" + i, new Object());
            watch.AddAndReset(column, j,arr.size());
            String lastKey = null;
            for(String key : arr.keySet())
            {
                if (lastKey != null) {
                    Object temp = arr.get(key);
                    arr.put(key, arr.get(lastKey));
                    arr.put(lastKey, temp);
                    lastKey = null;
                } else
                    lastKey = key;
            }
            watch.AddAndReset(column + 1, j,arr.size());
        }
    }

    private static void QueueObjectTest(int count, int column, Watcher watch) {
        for (int j = 0; j < 100; j++) {
            watch.ReStart();
            LinkedList<Object> arr = new LinkedList<Object>();
            for (int i = 0; i < count; i++)
                arr.add(new Object());
            watch.AddAndReset(column, j,arr.size());
            LinkedList<Object> tempArr = new LinkedList<Object>();
            Object obj = null;
            while ((obj = arr.poll())!=null)
                tempArr.add(obj);
            tempArr = arr;
            watch.AddAndReset(column + 1, j,arr.size());
            while ((obj = arr.poll())!=null);
            watch.AddAndReset(column + 2, j,arr.size());
        }
    }

    private static void QueueIntTest(int count, int column, Watcher watch) {
        for (int j = 0; j < 100; j++) {
            watch.ReStart();
            LinkedList<Integer> arr = new LinkedList<Integer>();
            for (int i = 0; i < count; i++)
                arr.add(i);
            watch.AddAndReset(column, j,arr.size());
            LinkedList<Integer> tempArr = new LinkedList<Integer>();
            Integer obj = null;
            while ((obj = arr.poll())!=null)
                tempArr.add(obj);
            tempArr = arr;
            watch.AddAndReset(column + 1, j,arr.size());
            while ((obj = arr.poll())!=null);
            watch.AddAndReset(column + 2, j,arr.size());
        }
    }

    private static void QueueLongTest(int count, int column, Watcher watch) {
        for (int j = 0; j < 100; j++) {
            watch.ReStart();
            LinkedList<Long> arr = new LinkedList<Long>();
            for (long i = 0; i < count; i++)
                arr.add(i);
            watch.AddAndReset(column, j,arr.size());
            LinkedList<Long> tempArr = new LinkedList<Long>();
            Long obj = null;
            while ((obj = arr.poll())!=null)
                tempArr.add(obj);
            tempArr = arr;
            watch.AddAndReset(column + 1, j,arr.size());
            while ((obj = arr.poll())!=null);
            watch.AddAndReset(column + 2, j,arr.size());
        }
    }

    private static void QueueStringTest(int count, int column, Watcher watch) {
        for (int j = 0; j < 100; j++) {
            watch.ReStart();
            LinkedList<String> arr = new LinkedList<String>();
            for (int i = 0; i < count; i++)
                arr.add("" + i);
            watch.AddAndReset(column, j,arr.size());
            LinkedList<String> tempArr = new LinkedList<String>();
            String obj = null;
            while ((obj = arr.poll())!=null)
                tempArr.add(obj);
            tempArr = arr;
            watch.AddAndReset(column + 1, j,arr.size());
            while ((obj = arr.poll())!=null);
            watch.AddAndReset(column + 2, j,arr.size());
        }
    }

    private static void StackObjectTest(int count, int column, Watcher watch) {
        for (int j = 0; j < 100; j++) {
            watch.ReStart();
            Stack<Object> arr = new Stack<Object>();
            for (int i = 0; i < count; i++)
                arr.push(new Object());
            watch.AddAndReset(column, j,arr.size());
            Stack<Object> tempArr = new Stack<Object>();
            Object obj = null;
            while (!arr.empty())
                tempArr.push(arr.pop());
            tempArr = arr;
            watch.AddAndReset(column + 1, j,arr.size());
        }
    }

    private static void StackIntTest(int count, int column, Watcher watch) {
        for (int j = 0; j < 100; j++) {
            watch.ReStart();
            Stack<Integer> arr = new Stack<Integer>();
            for (int i = 0; i < count; i++)
                arr.push(i);
            watch.AddAndReset(column, j,arr.size());
            Stack<Integer> tempArr = new Stack<Integer>();
            int obj = -1;
            while (!arr.empty())
                tempArr.push(arr.pop());
            tempArr = arr;
            watch.AddAndReset(column + 1, j,arr.size());
        }
    }

    private static void StackLongTest(int count, int column, Watcher watch) {
        for (int j = 0; j < 100; j++) {
            watch.ReStart();
            Stack<Long> arr = new Stack<Long>();
            for (long i = 0; i < count; i++)
                arr.push(i);
            watch.AddAndReset(column, j,arr.size());
            Stack<Long> tempArr = new Stack<Long>();
            long obj = -1;
            while (!arr.empty())
                tempArr.push(arr.pop());
            tempArr = arr;
            watch.AddAndReset(column + 1, j,arr.size());
        }
    }

    private static void StackStringTest(int count, int column, Watcher watch) {
        for (int j = 0; j < 100; j++) {
            watch.ReStart();
            Stack<String> arr = new Stack<String>();
            for (int i = 0; i < count; i++)
                arr.push("" + i);
            watch.AddAndReset(column, j,arr.size());
            Stack<String> tempArr = new Stack<String>();
            String obj = null;
            while (!arr.empty())
                tempArr.push(arr.pop());
            tempArr = arr;
            watch.AddAndReset(column + 1, j,arr.size());
        }
    }
}