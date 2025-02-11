using MoreLinq;
using NUnit.Framework;
using System.Text;

namespace DesignPatterns.CreationalDesignPatterns;

public static class Singleton
{
    public interface IDatabase
    {
        int GetPopulation(string name);
    }

    public class SingletonDatabase : IDatabase
    {
        private Dictionary<string, int> _capitals;
        private static int instanceCount; // 0
        public static int Count => instanceCount;

        private static Lazy<SingletonDatabase> _instance = new Lazy<SingletonDatabase>(() => new SingletonDatabase());

        private SingletonDatabase()
        {
            instanceCount++;

            Console.WriteLine("Initializing database");

            _capitals = File.ReadAllLines("capitals.txt")
                .Batch(2)
                .ToDictionary(
                list => list.ElementAt(0).Trim(),
                list => int.Parse(list.ElementAt(1)));
        }

        public int GetPopulation(string name)
        {
            return _capitals[name];
        }

        public static SingletonDatabase Instance => _instance.Value;
    }

    public class ConfigurableRecordFinder
    {
        private IDatabase _database;

        public ConfigurableRecordFinder(IDatabase database)
        {
            _database = database;
        }

        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int total = 0;

            foreach (var name in names)
                total += _database.GetPopulation(name);

            return total;
        }
    }

    public class DummyDatabese : IDatabase
    {
        public int GetPopulation(string name)
        {
            return new Dictionary<string, int>
            {
                ["alpha"] = 1,
                ["beta"] = 2,
                ["gamma"] = 3
            }[name];
        }
    }

    [TestFixture]
    public class SingletonTests
    {
        [Test]
        public void IsSingletonTest()
        {
            var db = SingletonDatabase.Instance;
            var db1 = SingletonDatabase.Instance;

            Assert.That(db, Is.SameAs(db1));
            Assert.That(SingletonDatabase.Count, Is.EqualTo(1));
        }

        [Test]
        public void ConfigurablePopulationTest()
        {
            var rf = new ConfigurableRecordFinder(new DummyDatabese());
            var names = new[] { "alpha", "gamma" };
            int tp = rf.GetTotalPopulation(names);

            Assert.That(tp, Is.EqualTo(4));
        }
    }

    public sealed class PerThreadSingleton
    {
        private static ThreadLocal<PerThreadSingleton> threadInstance
          = new ThreadLocal<PerThreadSingleton>(
            () => new PerThreadSingleton());

        public int Id;

        private PerThreadSingleton()
        {
            Id = Thread.CurrentThread.ManagedThreadId;
        }

        public static PerThreadSingleton Instance => threadInstance.Value;
    }

    //################################################################################################################
    //##############################################  Ambient Context  ###############################################
    //################################################################################################################

    // non-thread-safe global context
    public sealed class BuildingContext : IDisposable
    {
        public int WallHeight = 0;
        public int WallThickness = 300; // etc.
        private static Stack<BuildingContext> stack
          = new Stack<BuildingContext>();

        static BuildingContext()
        {
            // ensure there's at least one state
            stack.Push(new BuildingContext(0));
        }

        public BuildingContext(int wallHeight)
        {
            WallHeight = wallHeight;
            stack.Push(this);
        }

        public static BuildingContext Current => stack.Peek();

        public void Dispose()
        {
            // not strictly necessary
            if (stack.Count > 1)
                stack.Pop();
        }
    }

    public class Building
    {
        public readonly List<Wall> Walls = new List<Wall>();

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var wall in Walls)
                sb.AppendLine(wall.ToString());
            return sb.ToString();
        }
    }

    public struct Point
    {
        private int X, Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}";
        }
    }

    public class Wall
    {
        public Point Start, End;
        public int Height;

        public const int UseAmbient = Int32.MinValue;

        // public Wall(Point start, Point end, int elevation = UseAmbient)
        // {
        //   Start = start;
        //   End = end;
        //   Elevation = elevation;
        // }

        public Wall(Point start, Point end)
        {
            Start = start;
            End = end;
            //Elevation = BuildingContext.Elevation;
            Height = BuildingContext.Current.WallHeight;
        }

        public override string ToString()
        {
            return $"{nameof(Start)}: {Start}, {nameof(End)}: {End}, " +
                   $"{nameof(Height)}: {Height}";
        }
    }


    public static void Run()
    {
        Console.WriteLine("Start -> Singleton");

        var db = SingletonDatabase.Instance;
        var city = "Tokyo";
        Console.WriteLine($"{city} has population {db.GetPopulation(city)}");


        var t1 = Task.Factory.StartNew(() =>
        {
            Console.WriteLine($"t1: " + PerThreadSingleton.Instance.Id);
        });
        var t2 = Task.Factory.StartNew(() =>
        {
            Console.WriteLine($"t2: " + PerThreadSingleton.Instance.Id);
            Console.WriteLine($"t2 again: " + PerThreadSingleton.Instance.Id);
        });
        Task.WaitAll(t1, t2);


        var house = new Building();
        using (new BuildingContext(3000))
        {
            // ground floor
            //var e = 0;
            house.Walls.Add(new Wall(new Point(0, 0), new Point(5000, 0)/*, e*/));
            house.Walls.Add(new Wall(new Point(0, 0), new Point(0, 4000)/*, e*/));

            // first floor
            //e = 3500;
            using (new BuildingContext(3500))
            {
                house.Walls.Add(new Wall(new Point(0, 0), new Point(5000, 0) /*, e*/));
                house.Walls.Add(new Wall(new Point(0, 0), new Point(0, 4000) /*, e*/));
            }

            // back to ground again
            // e = 0;
            house.Walls.Add(new Wall(new Point(5000, 0), new Point(5000, 4000)/*, e*/));
        }

        Console.WriteLine(house);

        Console.WriteLine("Finish -> Singleton");
    }
}