package detrav;



import java.io.*;
import java.nio.file.Files;
import java.nio.file.Paths;

public class MainClass {
    public static void main(String[] args) throws IOException {
        byte[] bytes = Files.readAllBytes(Paths.get("stream.dat"));
        ForJIT(bytes);
        //JIT
        System.out.println("Start Benchmark");
        ForJIT(bytes);
    }

    private static void ForJIT(byte[] bytes) throws IOException{
        Watcher watch = new Watcher(new String[]
                {
                        "1mb Create byte by byte",
                        "1mb Create 1kb by 1kb",
                        "1mb Create stream full",
                        "1mb Copy stream to another byte by byte",
                        "1mb Copy stream to another 1kb by 1kb",
                        "1mb Copy stream to another",
                        "1mb Generate byte array from stream",
                        "1mb Read all int by binary reader",
                        "5mb Create byte by byte",
                        "5mb Create 1kb by 1kb",
                        "5mb Create stream full",
                        "5mb Copy stream to another byte by byte",
                        "5mb Copy stream to another 1kb by 1kb",
                        "5mb Copy stream to another",
                        "5mb Generate byte array from stream",
                        "5mb Read all int by binary reader",
                        "10mb Create byte by byte",
                        "10mb Create 1kb by 1kb",
                        "10mb Create stream full",
                        "10mb Copy stream to another byte by byte",
                        "10mb Copy stream to another 1kb by 1kb",
                        "10mb Copy stream to another",
                        "10mb Generate byte array from stream",
                        "10mb Read all int by binary reader"

                }, 100);
        TestMemoryStream(1, 0, bytes, watch);
        TestMemoryStream(5, 8, bytes, watch);
        TestMemoryStream(10, 16, bytes, watch);
        watch.Stop();
    }


    private static void TestMemoryStream(int count, int column, byte[] bytes, Watcher watch) throws IOException {
        for (int j = 0; j < 100; j++) {
            try (ByteArrayOutputStream writeMemoryStream = new ByteArrayOutputStream()) {
                watch.ReStart();
                //1
                for (int k = 0; k < count; k++)
                    for (int i = 0; i < bytes.length; i++)
                        writeMemoryStream.write(bytes[i]);
                watch.AddAndReset(column, j, writeMemoryStream.size());

                //2
                for (int k = 0; k < count; k++)
                    for (int i = 0; i < bytes.length; i += 1024)
                        writeMemoryStream.write(bytes, i, 1024);
                watch.AddAndReset(column + 1, j, writeMemoryStream.size());
                //3
                for (int k = 0; k < count; k++)
                    writeMemoryStream.write(bytes, 0, bytes.length);
                watch.AddAndReset(column + 2, j, writeMemoryStream.size());
                //4

                for (int k = 0; k < count; k++)
                    try (ByteArrayInputStream readMemoryStream = new ByteArrayInputStream(bytes)) {
                        int value = readMemoryStream.read();
                        while (value >= 0) {
                            writeMemoryStream.write((byte) value);
                            value = readMemoryStream.read();
                        }
                    }
                watch.AddAndReset(column + 3, j,writeMemoryStream.size());
                //5

                try (ByteArrayInputStream readMemoryStream = new ByteArrayInputStream(bytes)) {
                    for (int k = 0; k < count; k++) {
                        int lenght = 0;
                        byte[] buffer = new byte[1024];
                        int position = 0;
                        while ((lenght = readMemoryStream.read(buffer, position, buffer.length)) > 0)
                            writeMemoryStream.write(buffer, position, lenght);
                    }
                    watch.AddAndReset(column + 4, j, writeMemoryStream.size());
                }

                try (ByteArrayInputStream readMemoryStream = new ByteArrayInputStream(bytes)) {
                    //6
                    for (int k = 0; k < count; k++)
                        copyLarge(readMemoryStream, writeMemoryStream);
                    watch.AddAndReset(column + 5, j, writeMemoryStream.size());
                }
                //7
                long val = 0;
                for (int k = 0; k < count; k++) {
                    byte[] tempBytes = writeMemoryStream.toByteArray();
                    val += tempBytes.length;
                }
                watch.AddAndReset(column + 6, j,val);
                //8
                try (DataOutputStream writer = new DataOutputStream(writeMemoryStream)) {
                    for (int k = 0; k < count; k++) {
                        int tempValue = 0;
                        try (DataInputStream reader = new DataInputStream(new ByteArrayInputStream(bytes))) {
                            for (int i = 0; i < bytes.length; i += 4) {
                                tempValue = reader.readInt();
                                writer.writeInt(tempValue);
                            }
                        }
                    }
                    watch.AddAndReset(column + 7, j,writer.size());
                }

            }
        }
    }

    //from org.apache.commons.io.IOUtils
    private static final int DEFAULT_BUFFER_SIZE = 1024 * 4;

    private static long copyLarge(InputStream input, OutputStream output)
            throws IOException {
        byte[] buffer = new byte[DEFAULT_BUFFER_SIZE];
        long count = 0;
        int n = 0;
        while (-1 != (n = input.read(buffer))) {
            output.write(buffer, 0, n);
            count += n;
        }
        return count;
    }
}