using System;
using System.Diagnostics;
using static DesignPatterns.SOLID.SRP;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Metadata;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Channels;

namespace DesignPatterns.SOLID;

/// <summary>
/// SRP (Single Responsibility Principle)
/// </summary>
/// 
///     The Single Responsibility Principle is one of the SOLID principles of object-oriented design.
/// At its core, SRP advocates that a class should have only one reason to change. In simpler terms,
/// a class should encapsulate a single responsibility or task. This principle helps in keeping classes focused,
/// improving code maintainability, and reducing unintended consequences during code modifications.
/// 
/// Applying SRP: A Step-by-Step Guide
/// To effectively apply the Single Responsibility Principle, follow these steps:
/// 
/// -> 1. Identify Responsibilities: Begin by identifying the distinct responsibilities that a class currently handles.
///       For instance, in our previous example, the Employee class has two responsibilities: salary calculation and employee record updates.
///       
/// -> 2. Split Responsibilities: For each responsibility, create a new class. By isolating each responsibility,
///       you prevent a class from becoming bloated and ensure that changes in one area don’t affect unrelated parts.
///       
/// -> 3. Create New Classes: Implement the new classes with a singular focus on their respective responsibilities.
///       This approach enhances code readability, understandability, and maintainability.
///       
/// -> 4. Collaboration: If the responsibilities need to interact, establish collaboration between the newly created classes.
///       This can be achieved through interfaces, method calls, or events, promoting loose coupling and encapsulation.
/// 
/// Advantages of Embracing SRP
/// 
/// Adhering to the Single Responsibility Principle offers several noteworthy advantages:
/// 
/// -> Modularity: Your codebase becomes a collection of focused, modular components, simplifying maintenance and updates.
/// 
/// -> Testability: With individual responsibilities isolated, you can write targeted tests for each class,
///    ensuring thorough test coverage and easy debugging.
/// 
/// -> Flexibility: Modifications to a specific responsibility won’t affect other unrelated functionalities, enhancing code flexibility.
/// 
/// -> Collaboration: The collaborative nature of classes adhering to SRP is more intuitive,
///    as each class serves a clear purpose in the grand scheme.
///
///     The Single Responsibility Principle is a cornerstone of effective software design.
/// By ensuring that each class has a single reason to change, you pave the way for a codebase that is modular,
/// maintainable, and adaptable. By following a step-by-step approach to apply SRP,
/// you can break down complex classes into focused components that collaborate seamlessly.
/// Embracing SRP empowers you to create software that is not only functional but also robust, 
/// testable, and scalable, setting the stage for successful application development.
/// As you continue to embrace the principles of SRP, you’ll find your coding practices elevated to a new level of excellence.

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

        try
        {
            p.SaveToFile(j, fileName);
            Process.Start(fileName);
        }
        catch
        {
            Console.WriteLine("Folder not found");
        }

        Console.WriteLine("Finish -> SRP (Single Responsibility Principle)");
    }
}

// Single Responsibility Principle(SRP) in Software Design
// Introduction
// The Single Responsibility Principle(SRP) is a fundamental concept in software design that states:

// A class should have only one reason to change.

// This means that each class should only be responsible for a single functionality within
// a system.By following SRP, we create code that is easier to maintain, test, and extend.

// Example: A Simple Journal Class
// Imagine we are developing a journal where users can write down their thoughts.We might
// start by creating a simple Journal class with methods to add and remove entries:

// public class Journal
// {
//    private readonly List<string> entries = new();
//    private static int count = 0;

//    public int AddEntry(string entry)
//    {
//        entries.Add($"{++count}: {entry}");
//        return count; // Return the index of the entry
//    }

//    public void RemoveEntry(int index)
//    {
//        if (index >= 0 && index < entries.Count)
//            entries.RemoveAt(index);
//    }

//    public override string ToString()
//    {
//        return string.Join(Environment.NewLine, entries);
//    }
// }
// Here, the Journal class manages journal entries by:
// ✅ Adding new entries
// ✅ Removing entries
// ✅ Converting the journal entries into a string

// Violation of SRP: Adding Persistence Methods
// Suppose we now want to save the journal entries to a file. A common mistake would be to
// add persistence methods directly into the Journal class:


// public void Save(string fileName)
// {
//    File.WriteAllText(fileName, ToString());
// }

//  public static Journal Load(string fileName)
//{
//    var journal = new Journal();
//    var entries = File.ReadAllLines(fileName);
//    foreach (var entry in entries)
//        journal.AddEntry(entry);
//    return journal;
//}
// Why This Violates SRP?
// The Journal class now has two responsibilities:

// Managing journal entries(Core functionality)
// Handling file storage(Persistence)
// If the way we store data changes(e.g., saving to a database instead of a file), we would need to modify
// the Journal class. This goes against SRP because the class should change for only one reason—modifying journal entries.

// Solution: Creating a Separate Persistence Class
// To fix this, we create a separate Persistence class that handles saving and loading journals:


// public class Persistence
// {
//    public void SaveToFile(Journal journal, string fileName, bool overwrite = false)
//    {
//        if (overwrite || !File.Exists(fileName))
//            File.WriteAllText(fileName, journal.ToString());
//    }
// }
// Now, our Journal class is only responsible for managing journal entries, while Persistence handles storage.

// Usage Example
// var journal = new Journal();
// journal.AddEntry("I cried today.");
// journal.AddEntry("I ate a bug.");

// Console.WriteLine(journal);

// Save to file
// var persistence = new Persistence();
// string filePath = @"C:\temp\journal.txt";
// persistence.SaveToFile(journal, filePath, true);
// Benefits of Following SRP
// ✅ Code Maintainability – Changes to journal logic don’t affect persistence logic.
// ✅ Reusability – The Persistence class can be used for different objects, not just journals.
// ✅ Testability – We can unit-test journal functionality separately from persistence logic.

// Conclusion
// The Single Responsibility Principle helps build scalable and maintainable systems by ensuring each class
// has one and only one reason to change.By separating concerns, we create modular and reusable code, making future modifications easier.