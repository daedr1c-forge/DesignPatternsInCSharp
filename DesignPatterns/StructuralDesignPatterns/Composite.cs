using System.Text;

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
                .AppendLine (Name);

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

        Console.WriteLine("Finish -> Composite");
    }
}
