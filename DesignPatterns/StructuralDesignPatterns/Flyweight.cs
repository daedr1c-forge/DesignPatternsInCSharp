﻿using JetBrains.dotMemoryUnit;
using NUnit.Framework;
using System.Text;

namespace DesignPatterns.StructuralDesignPatterns;

[TestFixture]
public class Flyweight
{
    public class User
    {
        private string fullName;

        public User(string fullName)
        {
            this.fullName = fullName;
        }
    }

    public class User2
    {
        static List<string> strings = new List<string>();
        private int[] names;

        public User2(string fullName)
        {
            int getOrAdd(string s)
            {
                int idx = strings.IndexOf(s);
                if (idx != -1) return idx;
                else
                {
                    strings.Add(s);
                    return strings.Count - 1;
                }
            }

            names = fullName.Split(' ').Select(getOrAdd).ToArray();
        }

        public string FullName => string.Join(" ", names);
    }

    public void ForceGC()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
    }

    public static string RandomString()
    {
        Random rand = new Random();
        return new string(
          Enumerable.Range(0, 10).Select(i => (char)('a' + rand.Next(26))).ToArray());
    }

    [Test]
    public void TestUser()
    {
        var users = new List<User>();

        var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
        var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());

        foreach (var firstName in firstNames)
            foreach (var lastName in lastNames)
                users.Add(new User($"{firstName} {lastName}"));

        ForceGC();

        dotMemory.Check(memory =>
        {
            Console.WriteLine(memory.SizeInBytes);
        });
    }

    [Test]
    public void TestUser2()
    {
        var users = new List<User2>();

        var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
        var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());

        foreach (var firstName in firstNames)
            foreach (var lastName in lastNames)
                users.Add(new User2($"{firstName} {lastName}"));

        ForceGC();

        dotMemory.Check(memory =>
        {
            Console.WriteLine(memory.SizeInBytes);
        });
    }


    public class FormattedText
    {
        private string plainText;

        public FormattedText(string plainText)
        {
            this.plainText = plainText;
            capitalize = new bool[plainText.Length];
        }

        public void Capitalize(int start, int end)
        {
            for (int i = start; i <= end; ++i)
                capitalize[i] = true;
        }

        private bool[] capitalize;

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < plainText.Length; i++)
            {
                var c = plainText[i];
                sb.Append(capitalize[i] ? char.ToUpper(c) : c);
            }
            return sb.ToString();
        }
    }

    //#################################################################################################################

    public class BetterFormattedText
    {
        private string plainText;
        private List<TextRange> formatting = new List<TextRange>();

        public BetterFormattedText(string plainText)
        {
            this.plainText = plainText;
        }

        public TextRange GetRange(int start, int end)
        {
            var range = new TextRange { Start = start, End = end };
            formatting.Add(range);
            return range;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (var i = 0; i < plainText.Length; i++)
            {
                var c = plainText[i];
                foreach (var range in formatting)
                    if (range.Covers(i) && range.Capitalize)
                        c = char.ToUpperInvariant(c);
                sb.Append(c);
            }

            return sb.ToString();
        }

        public class TextRange
        {
            public int Start, End;
            public bool Capitalize, Bold, Italic;

            public bool Covers(int position)
            {
                return position >= Start && position <= End;
            }
        }
    }

    public static void Run()
    {
        Console.WriteLine("Start -> Flyweight");

        var ft = new FormattedText("This is a brave new world");
        ft.Capitalize(10, 15);
        Console.WriteLine(ft);

        var bft = new BetterFormattedText("This is a brave new world");
        bft.GetRange(10, 15).Capitalize = true;
        Console.WriteLine(bft);

        Console.WriteLine("Finish -> Flyweight");
    }
}