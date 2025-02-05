namespace DesignPatterns.SOLID;

/// <summary>
/// LSP (Liskov Substitution Principle)
/// </summary>
/// 
/// Barbara Liskov, who introduced this principle, states:
///     Objects of a superclass should be replaceable with objects of a subclass without affecting the functionality of the program
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

// Liskov Substitution Principle(LSP)
// The Liskov Substitution Principle states that a subclass should be able to replace its base class
// without affecting the correctness of the program.This principle ensures that derived classes extend base classes
// without altering their expected behavior.

// Example: Violating LSP
// A common example to demonstrate LSP violations is using Rectangles and Squares.

// Step 1: Creating the Rectangle Class

// public class Rectangle
// {
//    public virtual int Width { get; set; }
//    public virtual int Height { get; set; }

//    public Rectangle() { }

//    public Rectangle(int width, int height)
//    {
//        Width = width;
//        Height = height;
//    }

//    public int Area() => Width * Height;

//    public override string ToString() => $"Rectangle [Width={Width}, Height={Height}]";
// }
// Step 2: Creating the Square Class
// A Square is a special case of a Rectangle where both width and height must be the same.

// public class Square : Rectangle
// {
//    public override int Width
//    {
//        set { base.Width = base.Height = value; }
//    }

//    public override int Height
//    {
//        set { base.Width = base.Height = value; }
//    }
// }

//Step 3: Testing the Code

// public static void Main()
// {
//    Rectangle rc = new Rectangle(2, 3);
//    Console.WriteLine($"{rc} has area {rc.Area()}"); // Expected: 6

//    Rectangle sq = new Square();
//    sq.Width = 4;
//    Console.WriteLine($"{sq} has area {sq.Area()}"); // Expected: 16, but might fail!
// }
// Why Does This Violate LSP?
// When treating a Square as a Rectangle, setting the Width or Height independently breaks expectations:

// A Rectangle assumes width and height can be set independently.
// A Square enforces width = height, overriding independent assignments.
// If an algorithm expects a Rectangle but gets a Square, unexpected behavior occurs.
// Fixing the LSP Violation
// Instead of using inheritance, we can use composition.

// Step 1: Define a More General Shape

// public abstract class Shape
// {
//    public abstract int Area();
// }
// Step 2: Separate Rectangle and Square

// public class Rectangle : Shape
// {
//    public int Width { get; set; }
//    public int Height { get; set; }

//    public Rectangle(int width, int height)
//    {
//        Width = width;
//        Height = height;
//    }

//    public override int Area() => Width * Height;

//    public override string ToString() => $"Rectangle [Width={Width}, Height={Height}]";
// }

// public class Square : Shape
// {
//    public int Side { get; set; }

//    public Square(int side)
//    {
//        Side = side;
//    }

//    public override int Area() => Side * Side;

//    public override string ToString() => $"Square [Side={Side}]";
// }

// Step 3: Test the Solution

// public static void Main()
// {
//    Shape rect = new Rectangle(2, 3);
//    Console.WriteLine($"{rect} has area {rect.Area()}"); // Correct: 6

//    Shape square = new Square(4);
//    Console.WriteLine($"{square} has area {square.Area()}"); // Correct: 16
// }
// Key Takeaways
// ✅ Avoid overriding behaviors in a way that changes base class expectations.
// ✅ Use composition instead of inheritance when a subclass does not perfectly fit the parent class model.
// ✅ Ensure that derived classes can be substituted without altering program correctness.

// This approach preserves Liskov Substitution Principle, ensuring that a Rectangle and Square can function
// independently while maintaining correctness.