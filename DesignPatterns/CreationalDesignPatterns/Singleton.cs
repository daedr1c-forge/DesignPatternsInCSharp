using MoreLinq;

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

        private static Lazy<SingletonDatabase> _instance = new Lazy<SingletonDatabase>(() => new SingletonDatabase());

        private SingletonDatabase()
        {
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

    public static void Run()
    {
        Console.WriteLine("Start -> Singleton");

        var db = SingletonDatabase.Instance;
        var city = "Tokyo";
        Console.WriteLine($"{city} has population {db.GetPopulation(city)}");

        Console.WriteLine("Finish -> Singleton");
    }
}
