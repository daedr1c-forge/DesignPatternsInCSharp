namespace DesignPatterns.SOLID;

/// <summary>
/// OCP (Open-Closed Principle)
/// </summary>
/// 
///     The Open/Closed Principle (OCP) is the “O” in the SOLID principles and is one of the most fundamental guidelines for creating robust,
/// flexible software systems. The principle states that software entities (such as classes,
/// modules, and functions) should be open for extension but closed for modification.
/// In simple terms, you should be able to add new functionality to a class without changing its existing code.
/// This principle is essential because it protects existing code from bugs and ensures that the system remains
/// stable even as new features are added.
/// 
/// The Open/Closed Principle is crucial for several reasons:
/// 
/// -> Stability: By not modifying existing code, you reduce the risk of introducing new bugs into a stable system.
/// -> Scalability: OCP allows you to extend the system’s capabilities without altering its core behavior,
///    making it easier to scale and add features.
/// -> Maintainability: Following OCP leads to a codebase that is easier to maintain and understand,
///    as the original logic remains intact and changes are isolated to new extensions.

public static class OCP
{
    public enum Color
    {
        Red, Green, Blue
    }

    public enum Size
    {
        Small, Medium, Large
    }

    public class Product
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public Size Size { get; set; }

        public Product(string name, Color color, Size size)
        {
            if (name == null)
                throw new ArgumentNullException(paramName: nameof(name));

            Name = name;
            Color = color;
            Size = size;
        }
    }

    public class ProductFilter
    {
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
            foreach (var p in products)
                if (p.Size == size)
                    yield return p;
        }

        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {
            foreach (var p in products)
                if (p.Color == color)
                    yield return p;
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    public class ColorSpecification : ISpecification<Product>
    {
        private Color _color;

        public ColorSpecification(Color color) => _color = color;

        public bool IsSatisfied(Product t) => t.Color == _color;
    }

    public class SizeSpecification : ISpecification<Product>
    {
        private Size _size;

        public SizeSpecification(Size size) => _size = size;

        public bool IsSatisfied(Product t) => t.Size == _size;
    }

    public class AndSpecification<T> : ISpecification<T>
    {
        private ISpecification<T> _first, _second;

        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            _first = first ?? throw new ArgumentNullException(paramName: nameof(first));
            _second = second ?? throw new ArgumentNullException(paramName: nameof(second));
        }

        public bool IsSatisfied(T t) => _first.IsSatisfied(t) && _second.IsSatisfied(t);
    }

    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach (var i in items)
                if (spec.IsSatisfied(i))
                    yield return i;
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////

    public static void Run()
    {
        Console.WriteLine("Start -> OCP (Open-Closed Principle)");

        var apple = new Product("Apple", Color.Green, Size.Small);
        var tree = new Product("Tree", Color.Green, Size.Large);
        var house = new Product("House", Color.Blue, Size.Large);

        Product[] products = { apple, tree, house };

        var pf = new ProductFilter();

        Console.WriteLine("Green products (old): ");
        foreach (var p in pf.FilterByColor(products, Color.Green))
            Console.WriteLine($" - {p.Name} is green");

        /////////////////////////////////////////////////////////////////////////////////////////////////

        var bf = new BetterFilter();

        Console.WriteLine("Green products (new): ");
        foreach (var p in bf.Filter(products, new ColorSpecification(Color.Green)))
            Console.WriteLine($" - {p.Name} is green");

        Console.WriteLine("Large blue items");
        foreach (var p in bf.Filter(products,
            new AndSpecification<Product>(new ColorSpecification(Color.Blue), new SizeSpecification(Size.Large))))
        {
            Console.WriteLine($" - {p.Name} is big and blue");
        }

        Console.WriteLine("Finish -> OCP (Open-Closed Principle)");
    }
}

// Open-Closed Principle
// The Open-Closed Principle(OCP) states that classes should be open for extension but closed
// for modification.This means that you should be able to add new functionality without changing the existing code.

// Violation of OCP
// Initially, the filtering logic is implemented in a way that requires modifying the ProductFilter class
// whenever a new filtering criterion is needed.

// public enum Color { Red, Green, Blue }
// public enum Size { Small, Medium, Large }

// public class Product
// {
//    public string Name;
//    public Color Color;
//    public Size Size;

//    public Product(string name, Color color, Size size)
//    {
//        Name = name;
//        Color = color;
//        Size = size;
//    }
// }

// public class ProductFilter
// {
//    public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
//    {
//        foreach (var p in products)
//            if (p.Color == color)
//                yield return p;
//    }

//    public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
//    {
//        foreach (var p in products)
//            if (p.Size == size)
//                yield return p;
//    }

//    public IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Size size, Color color)
//    {
//        foreach (var p in products)
//            if (p.Size == size && p.Color == color)
//                yield return p;
//    }
// }

// Issue
// Every time a new filtering condition is needed, the ProductFilter class must be modified.
// This violates the Open-Closed Principle because existing code is being changed.
// Applying OCP Using Specification Pattern
// To adhere to the Open-Closed Principle, we introduce the Specification Pattern using interfaces.

// Step 1: Define the Specification Interface

// public interface ISpecification<T>
// {
//    bool IsSatisfied(T item);
// }
// Step 2: Create a Generic Filter

// public interface IFilter<T>
// {
//    IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
// }

// public class BetterFilter : IFilter<Product>
// {
//    public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
//    {
//        foreach (var item in items)
//            if (spec.IsSatisfied(item))
//                yield return item;
//    }
// }

// Step 3: Create Specific Filtering Criteria

// public class ColorSpecification : ISpecification<Product>
// {
//    private Color color;
//    public ColorSpecification(Color color) { this.color = color; }

//    public bool IsSatisfied(Product item) => item.Color == color;
// }

// public class SizeSpecification : ISpecification<Product>
// {
//    private Size size;
//    public SizeSpecification(Size size) { this.size = size; }

//    public bool IsSatisfied(Product item) => item.Size == size;
// }
// Step 4: Combine Specifications
// Using the AND condition for multiple filters:

// public class AndSpecification<T> : ISpecification<T>
// {
//    private ISpecification<T> first, second;

//    public AndSpecification(ISpecification<T> first, ISpecification<T> second)
//    {
//        this.first = first;
//        this.second = second;
//    }

//    public bool IsSatisfied(T item) => first.IsSatisfied(item) && second.IsSatisfied(item);
// }

//Step 5: Usage Example

// var apple = new Product("Apple", Color.Green, Size.Small);
// var tree = new Product("Tree", Color.Green, Size.Large);
// var house = new Product("House", Color.Blue, Size.Large);

// var products = new List<Product> { apple, tree, house };

// var bf = new BetterFilter();
// Console.WriteLine("Green products:");
// foreach (var p in bf.Filter(products, new ColorSpecification(Color.Green)))
//    Console.WriteLine($" - {p.Name} is green");

// Console.WriteLine("Large and Blue products:");
// foreach (var p in bf.Filter(products, new AndSpecification<Product>(
//    new SizeSpecification(Size.Large),
//    new ColorSpecification(Color.Blue))))
// {
//    Console.WriteLine($" - {p.Name} is large and blue");
// }
// Advantages
// ✅ New filtering criteria can be added without modifying existing code.
// ✅ Adheres to the Open-Closed Principle by enabling extensions through new classes rather than modifying the BetterFilter.
// This demonstrates how OCP is implemented effectively using the Specification Pattern. 