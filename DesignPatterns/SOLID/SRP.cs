using System.Diagnostics;

namespace DesignPatterns.SOLID;

/// <summary>
/// SRP (Single Responsibility Principle)
/// </summary>
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

        p.SaveToFile(j, fileName);
        Process.Start(fileName);

        Console.WriteLine("Finish -> SRP (Single Responsibility Principle)");
    }
}