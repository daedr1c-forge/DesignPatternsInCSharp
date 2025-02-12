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

    public static void Run()
    {

        Console.WriteLine("Start -> Adapter");

        Draw();
        Draw();

        Console.WriteLine();
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
