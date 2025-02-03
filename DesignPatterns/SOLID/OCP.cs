namespace DesignPatterns.SOLID;

/// <summary>
/// OCP (Open-Closed Principle)
/// </summary>
/// The Open/Closed Principle (OCP) is the “O” in the SOLID principles and is one of the most fundamental guidelines for creating robust,
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