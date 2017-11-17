using System;

namespace StringBuilder
{
    class Program
    {
        static void Main(string[] args)
        {

            Watcher watch = new Watcher(new string[]
                {
                    "Count 10000",
                    "Count 10000 full reverse",
                    "Count 10000 reverse 32 char words",
                    "Count 10000 count substring",
                    "Count 10000 remove substring",
                    "Count 10000 split",
                    "Count 10000 to upper",
                    "Count 10000 to lower",
                    "Count 50000",
                    "Count 50000 full reverse",
                    "Count 50000 reverse 32 char words",
                    "Count 50000 count substring",
                    "Count 50000 remove substring",
                    "Count 50000 split",
                    "Count 50000 to upper",
                    "Count 50000 to lower",
                    "Count 250000",
                    "Count 250000 full reverse",
                    "Count 250000 reverse 32 char words",
                    "Count 250000 count substring",
                    "Count 250000 remove substring",
                    "Count 250000 split",
                    "Count 250000 to upper",
                    "Count 250000 to lower",
                }, 100);
            TestString(0, 10000, watch);
            TestString(8, 50000, watch);
            TestString(16, 250000, watch);
            watch.Stop();

#if DEBUG
            Console.ReadLine();
#endif
        }

        private static string TestString(int column,int count, Watcher watcher)
        {
            string result = "";
            for (int j = 0; j < 100; j++)
            {
                watcher.ReStart();
                string str = "";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < count; i++)
                {
                    sb.Append("b165579034d5538683ed211ede1e9d64f68143eb59258763ff11d7a908775a76e86eb7155379fb568efab478ce0cb65a2c7f1787dbcaf1a421f7bca91d2c75aedd72455067c08d9b4b997de52de34b071dc1a70cce130914378bc070f10671ba74fa080cbdd8ca48ca0d34b06b18794a37cba63a6b0e3e44c7ad0e628f4468528255e8e565684dc77dcfe794093b2e4e6d608f63956a7eeb2cd9b5308145532718267cf878680ec2b8851a20a577a1e62f9fc746d423fa0942adad5651c247d6a0615d63328d8f1f4e3860c592aab2257c9f4e46c69758e60cd545f930833b81025c61a6acc074f0349e4c02cfb46030d762310e16b88e2f08fe29c413f66609");
                }
                result = str = sb.ToString();
                watcher.AddAndReset(column, j, result[result.Length-1]);
                char[] chars = new char[str.Length];
                for(int i =0; i< str.Length; i++)
                    chars[i] = str[str.Length - 1 - i];
                result = new string(chars);
                watcher.AddAndReset(column + 1, j, result[result.Length-1]);
                chars = new char[str.Length];
                for (int i = 0; i < str.Length; i += 32)
                    for(int k =0; k < 32; k++)
                        chars[i + k] = str[i + 31 - k];
                result = new string(chars);
                watcher.AddAndReset(column + 2, j, result[result.Length-1]);
                int subStrCount = 0;
                for(int i =0; i<str.Length && i>=0; i++)
                {
                    if ((i = str.IndexOf("165579034d5538683ed211ede1e9d64f", i)) > 0)
                        subStrCount++;
                    else break;
                }
                watcher.AddAndReset(column + 3, j, subStrCount);
                result = str;
                sb = new System.Text.StringBuilder();
                int lastIndex = 0;
                for (int i = 0; i < result.Length; i++)
                {
                    i = result.IndexOf("165579034d5538683ed211ede1e9d64f", i);
                    if (i >= 0)
                    {
                        sb.Append(result.Substring(lastIndex, i - lastIndex));
                        i += 32;
                        lastIndex = i;
                    }
                    else
                    {
                        sb.Append(result.Substring(lastIndex));
                        break;
                    }
                }
                result = sb.ToString();
                watcher.AddAndReset(column + 4, j, result[result.Length-1]);
                string[] results = str.Split("165579034d5538683ed211ede1e9d64f");
                watcher.AddAndReset(column + 5, j, results[results.Length-1]);
                result = str.ToUpper();
                watcher.AddAndReset(column + 6, j, result[result.Length-1]);
                result = str.ToLower();
                watcher.AddAndReset(column + 7, j, result[result.Length-1]);
            }
            return result;
        }
    }
}
