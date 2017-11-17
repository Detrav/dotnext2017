using System;
using System.IO;

namespace MStream
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] bytes = File.ReadAllBytes("stream.dat");
            Watcher watch = new Watcher(new string[]
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
#if DEBUG
            Console.ReadLine();
#endif
        }

        private static void TestMemoryStream(int count, int column, byte[] bytes, Watcher watch)
        {
            for (int j = 0; j < 100; j++)
            {
                using (MemoryStream writeMemoryStream = new MemoryStream())
                {
                    watch.ReStart();
                    //1
                    for (int k = 0; k < count; k++)
                        for (int i = 0; i < bytes.Length; i++)
                            writeMemoryStream.WriteByte(bytes[i]);
                    watch.AddAndReset(column, j, writeMemoryStream.Length);

                    //2
                    for (int k = 0; k < count; k++)
                        for (int i = 0; i < bytes.Length; i += 1024)
                            writeMemoryStream.Write(bytes, i, 1024);
                    watch.AddAndReset(column + 1, j, writeMemoryStream.Length);
                    //3
                    for (int k = 0; k < count; k++)
                        writeMemoryStream.Write(bytes, 0, bytes.Length);
                    watch.AddAndReset(column + 2, j, writeMemoryStream.Length);
                    //4

                    for (int k = 0; k < count; k++)
                        using (MemoryStream readMemoryStream = new MemoryStream(bytes))
                        {
                            int value = readMemoryStream.ReadByte();
                            while (value >= 0)
                            {
                                writeMemoryStream.WriteByte((byte)value);
                                value = readMemoryStream.ReadByte();
                            }
                        }
                    watch.AddAndReset(column + 3, j, writeMemoryStream.Length);
                    //5

                    using (MemoryStream readMemoryStream = new MemoryStream(bytes))
                    {
                        for (int k = 0; k < count; k++)
                        {
                            int lenght = 0;
                            byte[] buffer = new byte[1024];
                            int position = 0;
                            while ((lenght = readMemoryStream.Read(buffer, position, buffer.Length)) > 0)
                                writeMemoryStream.Write(buffer, position, lenght);
                        }
                        watch.AddAndReset(column + 4, j, writeMemoryStream.Length);
                    }

                    using (MemoryStream readMemoryStream = new MemoryStream(bytes))
                    {
                        //6
                        for (int k = 0; k < count; k++)
                            readMemoryStream.CopyTo(writeMemoryStream);
                        watch.AddAndReset(column + 5, j, writeMemoryStream.Length);
                    }
                    //7
                    long len = 0;
                    for (int k = 0; k < count; k++)
                    {
                        byte[] tempBytes = writeMemoryStream.ToArray();
                        len += tempBytes.Length;
                    }
                    watch.AddAndReset(column + 6, j, len);
                    //8
                    using (BinaryWriter writer = new BinaryWriter(writeMemoryStream))
                    {
                        for (int k = 0; k < count; k++)
                        {
                            int tempValue = 0;
                            using (BinaryReader reader = new BinaryReader(new MemoryStream(bytes)))
                            {
                                for (int i = 0; i < bytes.Length; i += 4)
                                {
                                    tempValue = reader.ReadInt32();
                                    writer.Write(tempValue);
                                }
                            }
                        }
                        watch.AddAndReset(column + 7, j, writer.BaseStream.Length);
                    }
                }
            }
        }
    }
}