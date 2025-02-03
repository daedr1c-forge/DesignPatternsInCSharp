using System.Diagnostics;

namespace DesignPatterns.SOLID;

/// <summary>
/// SRP (Single Responsibility Principle)
/// </summary>
public static class SRP
{
    public class Journal
    {
        private readonly List<string> _entries = new List<string>();
        private static int _count = 0;

        public int AddEntry(string text)
        {
            _entries.Add($"{++_count} {text}");
            return _count; //momento
        }

        public void RemoveEntry(int index)
        {
            _entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, _entries);
        }
    }

    public class Persistence
    {
        public void SaveToFile(Journal j, string filename, bool overwrite = false)
        {
            if (overwrite || !File.Exists(filename))
                File.WriteAllText(filename, j.ToString());
        }
    }

    public static void Run()
    {
        Console.WriteLine("Start -> SRP (Single Responsibility Principle)");

        var j = new Journal();
        j.AddEntry("I cried today");
        j.AddEntry("I ate a bug");
        Console.WriteLine(j);

        var p = new Persistence();
        var fileName = @"c:\temp\journal.txt";

        p.SaveToFile(j, fileName);
        Process.Start(fileName);

        Console.WriteLine("Finish -> SRP (Single Responsibility Principle)");
    }
}