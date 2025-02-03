namespace DesignPatterns.SOLID;

/// <summary>
/// LSP (Liskov Substitution Principle)
/// </summary>
/// Barbara Liskov, who introduced this principle, states:
/// Objects of a superclass should be replaceable with objects of a subclass without affecting the functionality of the program
/// Or with another word “If S is a subtype of T, then objects of type T in a program
/// can be replaced with objects of type S without altering the desirable properties of the program."
/// In simpler words, if you have a base class and its derived classes,
/// you should be able to use the derived classes without unexpected behavior or errors.
/// 
/// -> Ensures Substitutability
///    -> LSP states that subtypes must be substitutable for their base 
///       without altering the correctness of the program.This ensures that derived
///       classes can replace their base classes seamlessly in any part of the program.
///       
///    -> Why it’s important: Without LSP, code could break when using a subclass,
///       leading to runtime errors and violating the expectations of polymorphism.
///       
/// -> Improves Code Reusability
///     -> By adhering to LSP, you can design base classes with clear,
///        generic functionality that derived classes extend or modify without breaking existing code.
///        
///     -> Why it’s important: This promotes reuse of both base and derived classes in a broader range of contexts.
///     
/// -> Encourages Correct Design
///     -> Following LSP often reveals flaws in your inheritance hierarchy or design logic.
///        It forces you to ensure that a subclass does not violate the expected behavior of its parent class.
///        
///     -> Why it’s important: It guides you toward designing classes with clear, specific responsibilities,
///        minimizing potential side effects of modifications.
///        
///  -> Enhances Maintainability
///     -> When LSP is upheld, future changes or extensions to a class hierarchy are less likely to introduce bugs.
///     
///     -> Why it’s important: Teams can confidently extend functionality without fear of breaking existing implementations.
///     
/// -> Supports Open/Closed Principle
///     -> LSP directly supports the Open/Closed Principle by ensuring that new subclasses can be added without modifying existing code.
///     
///     -> Why it’s important: This helps in creating systems that are both extensible and stable.
///     
/// -> Minimizes Unexpected Behavior
///     -> Violating LSP often leads to unexpected behavior because subclasses behave differently than what the base class promises.
///     
///     -> Why it’s important: This can confuse developers and cause unexpected issues in applications, especially in large-scale systems.
///     
/// The Liskov Substitution Principle is crucial for creating reliable and maintainable code.
/// By ensuring that subclasses can replace their superclasses without unexpected behavior,
/// we maintain the integrity of our class hierarchies. Adhering to LSP leads to more robust and predictable software,
/// which is easier to understand and extend.

public static class LSP
{
    public static int Area(Rectangle r) => r.Width * r.Height;

    public class Rectangle
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public Rectangle()
        {
        }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }

    public class Square : Rectangle
    {
        public override int Width
        {
            set { base.Width = base.Height = value; }
        }

        public override int Height
        {
            set { base.Width = base.Width = value; }
        }
    }

    public static void Run()
    {
        Console.WriteLine("Start -> LSP (Liskov Substitution Principle)");

        Rectangle rc = new Rectangle(2, 3);
        Console.WriteLine($"{rc} has area {Area(rc)}");

        Square sq = new Square();
        sq.Width = 4;
        Console.WriteLine($"{sq} has are {Area(sq)}");

        Console.WriteLine("Finish -> LSP (Liskov Substitution Principle)");
    }
}
