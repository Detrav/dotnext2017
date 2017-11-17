package detrav;

public class Watcher{
    long watch = 0;
    long memory = 0;
    String[] columns;
    int count;

    public Watcher(String[] columns, int count) {
        this.columns = columns;
        this.count = count;
        Runtime.getRuntime().gc();
        memory = getMemory();
        watch = System.nanoTime();
    }

    public void ReStart() {
        memory = getMemory();
        System.out.println("ResetMemory: " + memory);
        watch = System.nanoTime();
    }

    public void Stop() {

    }

    private long getMemory()
    {
        return Runtime.getRuntime().totalMemory() - Runtime.getRuntime().freeMemory();
    }

    public void AddAndReset(int column, int testNumber, Object resultObject) {
        long ms =  System.nanoTime() - watch;
        long mem = getMemory();
        System.out.println(resultObject);
        System.out.println(
                "Watcher: ["
                        + (column < columns.length? columns[column] : "")
                        + "] Time: "
                        + ms
                        + " Memory: "
                        + mem);
        memory = getMemory();
        System.out.println("ResetMemory: " + memory);
        watch = System.nanoTime();
    }

    public void Reset() {
        memory = getMemory();
        System.out.println("ResetMemory: " + memory);
        watch = System.nanoTime();
    }
}