using DesignPatterns.BehavioralDesignPatterns;
using DesignPatterns.CreationalDesignPatterns;
using DesignPatterns.SOLID;
using DesignPatterns.StructuralDesignPatterns;

namespace DesignPatterns;

internal class Program
{
    static void Main(string[] args)
    {
        //SectionSOLID();
        //CreationalDesignPatterns();
        //StructuralDesignPatterns();
        //BehavioralDesignPatterns();
    }

    /// <summary>
    /// SOLID Design Principles
    /// SOLID principles make it easy for a developer to write easily extendable code and avoid common coding errors.
    /// These principles were introduced by Robert C.Martin, and they have become a fundamental part of object-oriented programming.
    /// In the context of .NET development, adhering to SOLID principles can lead to more modular, flexible, and maintainable code.
    /// </summary>
    private static void SectionSOLID()
    {
        SRP.Run();
        OCP.Run();
        LSP.Run();
        ISP.Run();
        DIP.Run();
    }

    /// <summary>
    /// Creational Design Patterns focus on the process of object creation or problems related to object creation.
    /// They help in making a system independent of how its objects are created, composed, and represented.
    /// Creational patterns give a lot of flexibility in what gets created, who creates it, and how it gets created.
    /// There are two main themes in these patterns:
    /// -> They keep information about the specific classes used in the system hidden.
    /// -> They hide the details of how instances of these classes are created and assembled.
    /// Types of Creational Design Patterns :
    ///     -> Factory Method Design Patterns
    ///     -> Singleton Method Design Pattern
    ///     -> Prototype Method Design Patterns
    ///     -> Builder Method Design Patterns
    /// </summary>
    private static void CreationalDesignPatterns()
    {
        Builder.Run();
        Factories.Run();
        Prototype.Run();
        Singleton.Run();
    }

    private static void StructuralDesignPatterns()
    {
        Adapter.Run();
        Bridge.Run();
        Composite.Run();
        Decorator.Run();
        Facade.Run();
        Flyweight.Run();
        Proxy.Run();
    }

    private static void BehavioralDesignPatterns()
    {
        ChainOfResponsibility.Run();
        Command.Run();
    }
}
