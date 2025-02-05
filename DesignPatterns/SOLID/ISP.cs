using static DesignPatterns.SOLID.ISP;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace DesignPatterns.SOLID;

/// <summary>
/// ISP (Interface Segregation Principle)
/// </summary>
/// 
/// The Interface Segregation Principle (ISP) is one of the five SOLID principles that help create modular,
/// scalable, and maintainable software. It focuses on designing interfaces to avoid excessive coupling and unnecessary dependencies.
/// 
/// The Interface Segregation Principle states that:
/// -> No class should be forced to implement methods it does not use.
/// In other words, interfaces should be small, cohesive, and designed to meet specific responsibilities.
/// When an interface is too large(i.e., contains methods not needed by all implementations),
/// it becomes a fat interface. This can lead to violations of other SOLID principles, 
/// such as high cohesion and low coupling, and increase maintenance effort.
/// 
/// In C#, implementing the ISP involves designing interfaces that are narrowly focused on specific functionalities,
/// rather than a single generalized interface. This ensures that classes implement only the interfaces whose methods
/// are relevant to their functionality, thus adhering to the ISP.
/// 
/// Benefits of Implementing ISP in C#
/// 
/// -> Implementing the Interface Segregation Principle in C# projects brings numerous benefits:
/// 
/// -> Improved Maintainability: Smaller, well-defined interfaces are easier to maintain and extend over time.
/// 
/// -> Enhanced Flexibility: By segregating interfaces, classes can choose to implement only
///    the interfaces that are relevant to their functionality, promoting a more flexible design.
///    
/// -> Reduced Coupling: ISP leads to a design that minimizes dependencies between classes,
///    thereby reducing coupling and enhancing the system’s robustness.
///    
/// The Interface Segregation Principle is a fundamental tenet of the SOLID principles that
/// advocate for minimalistic and focused interface design. In the realm of C# programming, 
/// adhering to ISP can dramatically improve software design by making it more adaptable,
/// maintainable, and easy to understand. By following the guidelines and examples provided,
/// developers can effectively implement ISP in their C# projects, clearly demonstrating the
/// principle’s value in real-world software development.

public class ISP
{
    public class Document
    {
    }

    public interface IMachine
    {
        void Print(Document d);
        void Scan(Document d);
        void Fax(Document d);
    }

    public class MultiFunctionPrinter : IMachine
    {
        public void Fax(Document d)
        {
            Console.WriteLine("Fax...");
        }

        public void Print(Document d)
        {
            Console.WriteLine("Print..");
        }

        public void Scan(Document d)
        {
            Console.WriteLine("Scan...");
        }
    }

    public class OldFunctionPrinter : IMachine
    {
        public void Fax(Document d)
        {
            Console.WriteLine("Fax...");
        }

        public void Print(Document d)
        {
            //  throw new NotImplementedException();
        }

        public void Scan(Document d)
        {
            // throw new NotImplementedException();
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////

    public interface IPrinter
    {
        void Print(Document d);
    }

    public interface IScanner
    {
        void Scan(Document d);
    }

    public class Photocopier : IPrinter, IScanner
    {
        public void Print(Document d)
        {
            Console.WriteLine("Print...");
        }

        public void Scan(Document d)
        {
            Console.WriteLine("Scan...");
        }
    }

    public interface IMultiFunctionDevice : IPrinter, IScanner ///...
    {
    }

    public class MultiFunctionMachine : IMultiFunctionDevice
    {
        private IPrinter _printer;
        private IScanner _scanner;

        public MultiFunctionMachine(IPrinter printer, IScanner scanner)
        {
            _printer = printer ?? throw new ArgumentNullException(paramName: nameof(printer));
            _scanner = scanner ?? throw new ArgumentNullException(paramName: nameof(printer));
        }

        public void Print(Document d)
        {
            _printer.Print(d);
        }

        public void Scan(Document d)
        {
            _scanner.Scan(d);
            // decorator
        }
    }
    public static void Run()
    {
        Console.WriteLine("Start -> ISP (Interface Segregation Principle)");

        Console.WriteLine("Finish -> ISP (Interface Segregation Principle)");
    }
}

// Interface Segregation Principle(ISP)
// The Interface Segregation Principle states that clients should not be forced to depend
// on methods they do not use.Instead of creating large interfaces with multiple responsibilities, break them into smaller, more focused interfaces.

// Example: Violating ISP
// Imagine we are building a printer/scanner/fax machine system.
//    Step 1: Creating a Large Interface

// public interface IMachine
// {
//    void Print(Document d);
//    void Scan(Document d);
//    void Fax(Document d);
// }
// Step 2: Implementing a Multifunction Printer
// A Multifunction Printer can handle all three tasks.

// public class MultiFunctionPrinter : IMachine
// {
//    public void Print(Document d)
//    {
//        Console.WriteLine("Printing document...");
//    }

//    public void Scan(Document d)
//    {
//        Console.WriteLine("Scanning document...");
//    }

//    public void Fax(Document d)
//    {
//        Console.WriteLine("Faxing document...");
//    }
// }
// Step 3: Implementing a Simple Printer
// A basic printer cannot scan or fax, but it still must implement the full IMachine interface.

// public class OldFashionedPrinter : IMachine
// {
//    public void Print(Document d)
//    {
//        Console.WriteLine("Printing document...");
//    }

//    public void Scan(Document d)
//    {
//        throw new NotImplementedException(); // Violates ISP
//    }

//    public void Fax(Document d)
//    {
//        throw new NotImplementedException(); // Violates ISP
//    }
// }
// Why Does This Violate ISP?
// OldFashionedPrinter is forced to implement Scan and Fax even though it doesn’t support them.
// We break the principle by making clients dependent on unused methods.
// Fixing ISP: Creating Smaller Interfaces
// Instead of one large interface, we split it into smaller, focused interfaces.

// Step 1: Creating Separate Interfaces

// public interface IPrinter
// {
//    void Print(Document d);
// }

// public interface IScanner
// {
//    void Scan(Document d);
// }

// public interface IFax
// {
//    void Fax(Document d);
// }
// Step 2: Implementing Focused Devices
// Now each class only implements what it needs.

// Basic Printer (Only Prints)

// public class SimplePrinter : IPrinter
//  {
//    public void Print(Document d)
//    {
//        Console.WriteLine("Printing document...");
//    }
// }
// Scanner(Only Scans)

// public class SimpleScanner : IScanner
// {
//    public void Scan(Document d)
//    {
//        Console.WriteLine("Scanning document...");
//    }
// }
// Multifunction Printer(Implements Multiple Interfaces)
// A multifunction printer can implement multiple interfaces.

// public class MultiFunctionDevice : IPrinter, IScanner, IFax
// {
//    public void Print(Document d)
//    {
//        Console.WriteLine("Printing document...");
//    }

//    public void Scan(Document d)
//    {
//        Console.WriteLine("Scanning document...");
//    }

//    public void Fax(Document d)
//    {
//        Console.WriteLine("Faxing document...");
//    }
// }
// Advanced: Using the Decorator Pattern
// We can compose a multifunction device by combining existing implementations.

// public class MultiFunctionMachine : IPrinter, IScanner
// {
//    private readonly IPrinter _printer;
//    private readonly IScanner _scanner;

//    public MultiFunctionMachine(IPrinter printer, IScanner scanner)
//    {
//        _printer = printer;
//        _scanner = scanner;
//    }

//    public void Print(Document d) => _printer.Print(d);
//    public void Scan(Document d) => _scanner.Scan(d);
// }
// Using the Decorator Pattern

// public static void Main()
// {
//    IPrinter printer = new SimplePrinter();
//    IScanner scanner = new SimpleScanner();

//    MultiFunctionMachine mfd = new MultiFunctionMachine(printer, scanner);

//    Document doc = new Document();
//    mfd.Print(doc); // Uses SimplePrinter
//    mfd.Scan(doc);  // Uses SimpleScanner
// }
// Key Takeaways
// ✅ Large interfaces should be split into smaller, focused ones.
// ✅ Clients should not be forced to implement methods they don’t use.
// ✅ Use multiple interfaces or composition (Decorator Pattern) instead of bloated base interfaces.
// This approach preserves the Interface Segregation Principle, making the code cleaner, modular, and scalable. 