package detrav;




import java.io.*;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class MainClass {
    public static void main(String[] args) throws IOException {
        String text = String.join("\n\r",Files.readAllLines(Paths.get("BigFileText.txt")));
        ForJIT(text);
        //JIT
        System.out.println("Start Benchmark");
        ForJIT(text);
    }

    private static void ForJIT(String text) {
        Watcher watch = new Watcher(new String[]
                {
                        "URI",
                        "Email",
                        "Date",
                        "URI|Email",
                }, 100);

        int count = 0;
        Pattern regexURI = Pattern.compile("([a-zA-Z][a-zA-Z0-9]*):\\/\\/([^ /]+)(\\/[\\S]*)?");
        Pattern regexEmail = Pattern.compile("\\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,6}\\b");
        Pattern regexDate = Pattern.compile("([0-9][0-9]?)/([0-9][0-9]?)/([0-9][0-9]([0-9][0-9])?)");
        Pattern regexURIEmail = Pattern.compile("([a-zA-Z][a-zA-Z0-9]*):\\/\\/([^ /]+)(\\/[\\S]*)?|\\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,6}\\b");
        Matcher matcher;
        for (int j = 0; j< 100; j++) {
            watch.ReStart();


            count = 0;
            matcher = regexURI.matcher(text);
            while (matcher.find())
                count++;
            watch.AddAndReset(0, j,count);
            count = 0;
            matcher = regexEmail.matcher(text);
            while (matcher.find())
                count++;
            watch.AddAndReset(1, j,count);
            count = 0;
            matcher = regexDate.matcher(text);
            while (matcher.find())
                count++;
            watch.AddAndReset(2, j,count);
            count = 0;
            matcher = regexURIEmail.matcher(text);
            while (matcher.find())
                count++;
            watch.AddAndReset(3, j,count);
        }
        watch.Stop();
    }
}