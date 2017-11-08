using System.IO;
using System.Text.RegularExpressions;

namespace Regulars
{
    class Program
    {
        
        static void Main(string[] args)
        {

            string text = File.ReadAllText("BigFileText.txt");

            Watcher watch = new Watcher(new string[]
                {
                        "URI",
                        "Email",
                        "Date",
                        "URI|Email",
                }, 100);

            int count = 0;
            Regex regexURI = new Regex("([a-zA-Z][a-zA-Z0-9]*):\\/\\/([^ /]+)(\\/[\\S]*)?");
            Regex regexEmail = new Regex("\\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,6}\\b");
            Regex regexDate = new Regex("([0-9][0-9]?)/([0-9][0-9]?)/([0-9][0-9]([0-9][0-9])?)");
            Regex regexURIEmail = new Regex("([a-zA-Z][a-zA-Z0-9]*):\\/\\/([^ /]+)(\\/[\\S]*)?|\\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,6}\\b");
            for (int j = 0; j< 100; j++)
            {
                watch.ReStart();
                count = regexURI.Matches(text).Count;
                watch.AddAndReset(0, j, count);
                count = regexEmail.Matches(text).Count;
                watch.AddAndReset(1, j, count);
                count = regexDate.Matches(text).Count;
                watch.AddAndReset(2, j, count);
                count = regexURIEmail.Matches(text).Count;
                watch.AddAndReset(3, j, count);
            }
            watch.Stop();
        }
    }
}
