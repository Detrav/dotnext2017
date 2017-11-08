using System;

namespace String
{
    class Program
    {
        static void Main(string[] args)
        {
            Watcher watch = new Watcher(new string[]
                {
                    "Count 100",
                    "Count 100 full reverse",
                    "Count 100 reverse 32 char words",
                    "Count 100 count substring",
                    "Count 100 remove substring",
                    "Count 100 split",
                    "Count 100 to upper",
                    "Count 100 to lower",
                    "Count 500",
                    "Count 500 full reverse",
                    "Count 500 reverse 32 char words",
                    "Count 500 count substring",
                    "Count 500 remove substring",
                    "Count 500 split",
                    "Count 500 to upper",
                    "Count 500 to lower",
                    "Count 2500",
                    "Count 2500 full reverse",
                    "Count 2500 reverse 32 char words",
                    "Count 2500 count substring",
                    "Count 2500 remove substring",
                    "Count 2500 split",
                    "Count 2500 to upper",
                    "Count 2500 to lower",
                }, 100);
            TestString(0, 100, watch);
            TestString(8, 500, watch);
            TestString(16, 2500, watch);
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
                for (int i = 0; i < count; i++)
                {
                    str += "b165579034d5538683ed211ede1e9d64f68143eb59258763ff11d7a908775a76e86eb7155379fb568efab478ce0cb65a2c7f1787dbcaf1a421f7bca91d2c75aedd72455067c08d9b4b997de52de34b071dc1a70cce130914378bc070f10671ba74fa080cbdd8ca48ca0d34b06b18794a37cba63a6b0e3e44c7ad0e628f4468528255e8e565684dc77dcfe794093b2e4e6d608f63956a7eeb2cd9b5308145532718267cf878680ec2b8851a20a577a1e62f9fc746d423fa0942adad5651c247d6a0615d63328d8f1f4e3860c592aab2257c9f4e46c69758e60cd545f930833b81025c61a6acc074f0349e4c02cfb46030d762310e16b88e2f08fe29c413f66609";
                }
                result = str;
                watcher.AddAndReset(column, j, result[result.Length - 1]);
                char[] chars = new char[str.Length];
                for (int i = 0; i < str.Length; i++)
                    chars[i] = str[str.Length - 1 - i];
                result = new string(chars);
                watcher.AddAndReset(column + 1, j, result[result.Length - 1]);
                chars = new char[str.Length];
                for (int i = 0; i < str.Length; i += 32)
                    for (int k = 0; k < 32; k++)
                        chars[i + k] = str[i + 31 - k];
                result = new string(chars);
                watcher.AddAndReset(column + 2, j, result[result.Length - 1]);
                int subStrCount = 0;
                for (int i = 0; i < str.Length && i >= 0; i++)
                {
                    if ((i = str.IndexOf("165579034d5538683ed211ede1e9d64f", i)) > 0)
                        subStrCount++;
                    else break;
                }
                watcher.AddAndReset(column + 3, j, subStrCount);
                result = str;
                for (int i = 0; i < result.Length; i++)
                {
                    i = result.IndexOf("165579034d5538683ed211ede1e9d64f", i);
                    if (i >= 0)
                        result = result.Substring(0, i)
                        + result.Substring(i + 32, result.Length - i - 32);
                    else break;
                }
                watcher.AddAndReset(column + 4, j, result[result.Length - 1]);
                string[] results = str.Split("165579034d5538683ed211ede1e9d64f");
                watcher.AddAndReset(column + 5, j, results[results.Length - 1]);
                result = str.ToUpper();
                watcher.AddAndReset(column + 6, j, result[result.Length - 1]);
                result = str.ToLower();
                watcher.AddAndReset(column + 7, j, result[result.Length - 1]);
            }
            return result;
        }
    }
}
