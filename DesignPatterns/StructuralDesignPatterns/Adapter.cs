using Autofac;
using Autofac.Features.Metadata;
using MoreLinq.Extensions;
using System.Collections;
using System.Collections.ObjectModel;

namespace DesignPatterns.StructuralDesignPatterns;

public static class Adapter
{
    public class Point
    {
        public int X, Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object? obj)
        {
            return obj is Point point &&
                   X == point.X &&
                   Y == point.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }

    public class Line
    {
        public Point Start, End;

        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
        }

        public override bool Equals(object? obj)
        {
            return obj is Line line &&
                   EqualityComparer<Point>.Default.Equals(Start, line.Start) &&
                   EqualityComparer<Point>.Default.Equals(End, line.End);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Start, End);
        }
    }

    public class VectorObject : Collection<Line>
    {

    }

    public class VectorRectangle : VectorObject
    {
        public VectorRectangle(int x, int y, int widht, int height)
        {
            Add(new Line(new Point(x, y), new Point(x + widht, y)));
            Add(new Line(new Point(x + widht, y), new Point(x + widht, y + height)));
            Add(new Line(new Point(x, y), new Point(x, y + height)));
            Add(new Line(new Point(x, y + height), new Point(x + widht, y + height)));
        }
    }

    private static readonly List<VectorObject> objects = new List<VectorObject>()
    {
        new VectorRectangle(1,1,10,10),
        new VectorRectangle(3,3,6,6),
    };

    public class LineToPointAdapter : IEnumerable<Point>
    {
        private static int _count;
        private static Dictionary<int, List<Point>> _cache = new Dictionary<int, List<Point>>();

        public LineToPointAdapter(Line line)
        {
            var hash = line.GetHashCode();

            if (_cache.ContainsKey(hash)) return;

            Console.WriteLine($"{++_count}: Generation points for line " +
                $"[{line.Start.X},{line.Start.Y}]-[{line.End.X},{line.End.Y}]");

            var points = new List<Point>();

            int left = Math.Min(line.Start.X, line.End.X);
            int right = Math.Max(line.Start.X, line.End.X);
            int top = Math.Min(line.Start.Y, line.End.Y);
            int bottom = Math.Max(line.Start.Y, line.End.Y);
            int dx = right - left;
            int dy = line.End.Y - line.Start.Y;

            if (dx == 0)
            {
                for (int y = top; y <= bottom; ++y)
                {
                    points.Add(new Point(left, y));
                }
            }
            else if (dy == 0)
            {
                for (int x = left; x <= right; ++x)
                {
                    points.Add(new Point(x, top));
                }
            }

            _cache.Add(hash, points);
        }

        public IEnumerator<Point> GetEnumerator()
        {
            return _cache.Values.SelectMany(x => x).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public static void DrawPoint(Point point)
    {
        Console.Write(".");
    }

    //################################################################################################################
    //###########################################  Generic Value Adapter  ############################################
    //################################################################################################################

    // Vector2f, Vector3i

    public interface IInteger
    {
        int Value { get; }
    }

    public static class Dimensions
    {
        public class Two : IInteger
        {
            public int Value => 2;
        }

        public class Three : IInteger
        {
            public int Value => 3;
        }
    }

    public class Vector<TSelf, T, D>
      where D : IInteger, new()
      where TSelf : Vector<TSelf, T, D>, new()
    {
        protected T[] data;

        public Vector()
        {
            data = new T[new D().Value];
        }

        public Vector(params T[] values)
        {
            var requiredSize = new D().Value;
            data = new T[requiredSize];

            var providedSize = values.Length;

            for (int i = 0; i < Math.Min(requiredSize, providedSize); ++i)
                data[i] = values[i];
        }

        public static TSelf Create(params T[] values)
        {
            var result = new TSelf();
            var requiredSize = new D().Value;
            result.data = new T[requiredSize];

            var providedSize = values.Length;

            for (int i = 0; i < Math.Min(requiredSize, providedSize); ++i)
                result.data[i] = values[i];

            return result;
        }

        public T this[int index]
        {
            get => data[index];
            set => data[index] = value;
        }

        public T X
        {
            get => data[0];
            set => data[0] = value;
        }
    }

    public class VectorOfFloat<TSelf, D>
      : Vector<TSelf, float, D>
      where D : IInteger, new()
      where TSelf : Vector<TSelf, float, D>, new()
    {
    }

    public class VectorOfInt<D> : Vector<VectorOfInt<D>, int, D>
      where D : IInteger, new()
    {
        public VectorOfInt()
        {
        }

        public VectorOfInt(params int[] values) : base(values)
        {
        }

        public static VectorOfInt<D> operator +
          (VectorOfInt<D> lhs, VectorOfInt<D> rhs)
        {
            var result = new VectorOfInt<D>();
            var dim = new D().Value;
            for (int i = 0; i < dim; i++)
            {
                result[i] = lhs[i] + rhs[i];
            }

            return result;
        }
    }

    public class Vector2i : VectorOfInt<Dimensions.Two>
    {
        public Vector2i()
        {
        }

        public Vector2i(params int[] values) : base(values)
        {
        }
    }

    public class Vector3f
      : VectorOfFloat<Vector3f, Dimensions.Three>
    {
        public override string ToString()
        {
            return $"{string.Join(",", data)}";
        }
    }

    //################################################################################################################
    //######################################  Adapter in Dependency Injection  #######################################
    //################################################################################################################

    public interface ICommand
    {
        void Execute();
    }

    public class SaveCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Saving current file");
        }
    }

    public class OpenCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Opening a file");
        }
    }

    public class Button
    {
        private ICommand command;
        private string name;

        public Button(ICommand command, string name)
        {
            if (command == null)
            {
                throw new ArgumentNullException(paramName: nameof(command));
            }
            this.command = command;
            this.name = name;
        }

        public void Click()
        {
            command.Execute();
        }

        public void PrintMe()
        {
            Console.WriteLine($"I am a button called {name}");
        }
    }

    public class Editor
    {
        private readonly IEnumerable<Button> buttons;

        public IEnumerable<Button> Buttons => buttons;

        public Editor(IEnumerable<Button> buttons)
        {
            this.buttons = buttons;
        }

        public void ClickAll()
        {
            foreach (var btn in buttons)
            {
                btn.Click();
            }
        }
    }

    public static void Run()
    {

        Console.WriteLine("Start -> Adapter");

        Draw();
        Draw();

        Console.WriteLine();

        ///////////////////////////////////

        var v = new Vector2i(1, 2);
        v[0] = 0;

        var vv = new Vector2i(3, 2);

        var result = v + vv;

        Vector3f u = Vector3f.Create(3.5f, 2.2f, 1);


        // for each ICommand, a ToolbarButton is created to wrap it, and all
        // are passed to the editor
        var b = new ContainerBuilder();
        b.RegisterType<OpenCommand>().As<ICommand>().WithMetadata("Name", "Open");
        b.RegisterType<SaveCommand>().As<ICommand>().WithMetadata("Name", "Save");

        //b.RegisterType<Button>();
        //b.RegisterAdapter<ICommand, Button>(cmd => new Button(cmd, ""));
        b.RegisterAdapter<Meta<ICommand>, Button>(cmd => new Button(cmd.Value, (string)cmd.Metadata["Name"]));

        b.RegisterType<Editor>();

        using (var c = b.Build())
        {
            var editor = c.Resolve<Editor>();
            //editor.ClickAll();

            // problem: only one button

            foreach (var btn in editor.Buttons)
                btn.PrintMe();
        }

        Console.WriteLine("Finish -> Adapter");
    }
    private static void Draw()
    {
        foreach (var vo in objects)
        {
            foreach (var line in vo)
            {
                var adapter = new LineToPointAdapter(line);
                adapter.ForEach(DrawPoint);
            }
        }
    }
}
