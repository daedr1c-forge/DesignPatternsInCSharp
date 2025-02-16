using System.Collections;
using System.Collections.ObjectModel;
using System.Text;
using static DesignPatterns.StructuralDesignPatterns.Composite;

namespace DesignPatterns.StructuralDesignPatterns;

public static class Composite
{
    public class GraphicObject
    {
        public virtual string Name { get; set; } = "Group";
        public string Color;

        private Lazy<List<GraphicObject>> _children = new Lazy<List<GraphicObject>>();
        public List<GraphicObject> Children => _children.Value;

        private void Print(StringBuilder sb, int depth)
        {
            sb.Append(new string('*', depth))
                .Append(string.IsNullOrWhiteSpace(Color) ? string.Empty : $"{Color} ")
                .AppendLine(Name);

            foreach (var child in Children)
            {
                child.Print(sb, depth + 1);
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            Print(sb, 0);
            return sb.ToString();
        }
    }

    public class Circle : GraphicObject
    {
        public override string Name => "Cirle";
    }

    public class Square : GraphicObject
    {
        public override string Name => "Square";
    }


    public class Neuron : IEnumerable<Neuron>
    {
        public float Value;
        public List<Neuron> In, Out;

        public IEnumerator<Neuron> GetEnumerator()
        {
            yield return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class NeuronLayer : Collection<Neuron>
    {

    }


    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    public abstract class ISpecification<T>
    {
        public abstract bool IsSatisfied(T p);

        public static ISpecification<T> operator &(ISpecification<T> first, ISpecification<T> second)
        {
            return new AndSpecification<T>(first, second);
        }
    }

    public abstract class CompositeSpecification<T> : ISpecification<T>
    {
        protected readonly ISpecification<T>[] _items;

        public CompositeSpecification(params ISpecification<T>[] items)
        {
            _items = items;
        }
    }

    //combinator
    public class AndSpecification<T> : CompositeSpecification<T>
    {
        public AndSpecification(params ISpecification<T>[] items) : base(items)
        {
        }

        public override bool IsSatisfied(T t)
        {
            return _items.All(i => i.IsSatisfied(t));
        }
    }

    public class ColorSpecification : ISpecification<Product>
    {
        private Color color;

        public ColorSpecification(Color color)
        {

        }

        public override bool IsSatisfied(Product p)
        {
            return true;
        }
    }

    public class Color
    {
    }

    public class Product
    {
        public int Size { get; set; }
    }

    public static void Run()
    {
        Console.WriteLine("Start -> Composites");

        var drawing = new GraphicObject { Name = "My Drawing" };
        drawing.Children.Add(new Square { Color = "Red" });
        drawing.Children.Add(new Circle { Color = "Yellow" });

        var group = new GraphicObject();
        group.Children.Add(new Square { Color = "Blue" });
        group.Children.Add(new Circle { Color = "Blue" });
        drawing.Children.Add(group);

        Console.WriteLine(drawing);

        var neuron1 = new Neuron();
        var neuron2 = new Neuron();
        var layer1 = new NeuronLayer();
        var layer2 = new NeuronLayer();

        neuron1.ConnectTo(neuron2);
        neuron1.ConnectTo(layer1);
        layer1.ConnectTo(layer2);

        Console.WriteLine("Finish -> Composite");
    }
}

public static class ExtensionMethods
{
    public static void ConnectTo(this IEnumerable<Neuron> self, IEnumerable<Neuron> other)
    {
        if (ReferenceEquals(self, other)) return;

        foreach (var from in self)
            foreach (var to in other)
            {
                from.Out.Add(to);
                to.In.Add(from);
            }
    }
}
