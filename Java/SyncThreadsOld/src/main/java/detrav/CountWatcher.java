package detrav;

public class CountWatcher {
    private String[] columns;
    private int count;

    public CountWatcher(String[] columns, int count)
    {
        Runtime.getRuntime().gc();
        this.columns = columns;
        this.count = count;
    }

    public void Add(long count, int column, int testNumber)
    {
        System.out.println(
                "CountWatcher: ["
                        + (column < columns.length? columns[column] : "")
                        + "] Count: "
                        + count);
    }

    public void Stop()
    {

    }
}
