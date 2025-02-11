using MoreLinq;
using NUnit.Framework;
using static DesignPatterns.CreationalDesignPatterns.Singleton;

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


        Console.WriteLine("Finish -> Singleton");
    }
}