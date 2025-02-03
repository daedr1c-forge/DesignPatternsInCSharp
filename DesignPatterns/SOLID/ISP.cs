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
