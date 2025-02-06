using System.Reflection.PortableExecutable;
using System.Text;
using static DesignPatterns.CreationalDesignPatterns.Factories.HotDrinkMachine;

namespace DesignPatterns.CreationalDesignPatterns;

/// <summary>
/// Factories
/// </summary>
/// This pattern is typically helpful when it’s necessary to separate 
/// the construction of an object from its implementation. With the use of this design pattern,
/// objects can be produced without having to define the exact class of object to be created.
/// Below is when to use Factory Method Design Pattern:
/// 
///   -> A class can't anticipate the class of objects it must create.
///   -> A class wants its subclass to specify the objects it creates.
///   -> Classes delegate responsibility to one of several helper subclasses,
///      and you want to localize the knowledge of which helper subclass is the delegate.

public static class Factories
{
    //################################################################################################################
    //################################################ Factory Method ################################################
    //################################################################################################################
    public enum CoordinateSystem
    {
        Cartesiean,
        Polar
    }

    public class Point
    {
        private double x, y;

        //factory method
        //public static Point NewCartesianPoint(double x, double y)
        //{
        //    return new Point(x, y);
        //}

        //public static Point NewPolarPoint(double rho, double theta)
        //{
        //    return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        //}

        // for factory method private
        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }

        //################################################################################################################
        //#################################################  Inner Factory  ##############################################
        //################################################################################################################

        public static Point Origin = new Point(0, 0);

        public static class Factory
        {
            public static Point NewCartesianPoint(double x, double y)
            {
                return new Point(x, y);
            }

            public static Point NewPolarPoint(double rho, double theta)
            {
                return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
            }
        }
    }

    //################################################################################################################
    //########################################## Asynchronous Factory Method #########################################
    //################################################################################################################

    public class Foo
    {
        private Foo()
        {
            //
        }

        private async Task<Foo> InitAsync()
        {
            await Task.Delay(1000);
            return this;
        }

        public static Task<Foo> CreateAsync()
        {
            var result = new Foo();
            return result.InitAsync();
        }
    }

    //################################################################################################################
    //#####################################################  Factory  ################################################
    //################################################################################################################

    public static class PointFactory
    {
        //constructor of Point -> public
        //public static Point NewCartesianPoint(double x, double y)
        //{
        //    return new Point(x, y);
        //}

        //public static Point NewPolarPoint(double rho, double theta)
        //{
        //    return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        //}
    }

    //################################################################################################################
    //#######################################  Object Tracking and Bulk Replacement  #################################
    //################################################################################################################


    public interface ITheme
    {
        string TextColor { get; }
        string BgrColor { get; }
    }

    public class LightTheme : ITheme
    {
        public string TextColor => "black";
        public string BgrColor => "white";
    }

    public class DarkTheme : ITheme
    {
        public string TextColor => "white";
        public string BgrColor => "dark gray";
    }

    public class TrackingThemeFactory
    {
        private readonly List<WeakReference<ITheme>> _themes = new();

        public ITheme CreateTheme(bool dark)
        {
            ITheme theme = dark ? new DarkTheme() : new LightTheme();
            _themes.Add(new WeakReference<ITheme>(theme));
            return theme;
        }

        public string Info
        {
            get
            {
                var sb = new StringBuilder();

                foreach (var referense in _themes)
                {
                    if (referense.TryGetTarget(out var theme))
                    {
                        bool dark = theme is DarkTheme;
                        sb.Append(dark ? "Dark" : "Light")
                          .AppendLine(" theme");
                    }
                }

                return sb.ToString();
            }
        }
    }

    public class ReplaceableThemeFactry
    {
        private readonly List<WeakReference<Ref<ITheme>>> _themes = new();

        private ITheme CreateThemeImpl(bool dark)
        {
            return dark ? new DarkTheme() : new LightTheme();
        }

        public Ref<ITheme> CreateTheme(bool dark)
        {
            var r = new Ref<ITheme>(CreateThemeImpl(dark));
            _themes.Add(new(r));
            return r;
        }

        public void ReplaceTheme(bool dark)
        {
            foreach (var wr in _themes)
            {
                if (wr.TryGetTarget(out var reference))
                {
                    reference.Value = CreateThemeImpl(dark);
                }
            }
        }
    }

    public class Ref<T> where T : class
    {
        public T Value;

        public Ref(T value)
        {
            Value = value;
        }
    }

    //################################################################################################################
    //###############################################  Abstract Factory  #############################################
    //################################################################################################################

    public interface IHotDrink
    {
        void Consume();
    }

    internal class Tea : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("This tea is nice but I'd prefer it with milk.");
        }
    }

    internal class Coffe : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("This coffe is sensational");
        }
    }

    public interface IHotDrinkFactory
    {
        IHotDrink Prepare(int amount);
    }

    internal class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Put in a tea bag, boil water, pour {amount} ml, add lemon, enjoy!");
            return new Tea();
        }
    }

    internal class CoffeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Grind some beans, boil water, pour {amount} ml, add cream and suger, enjoy!");
            return new Coffe();
        }
    }

    public class HotDrinkMachine
    {
        // Abstract Factory
        //public enum AvailableDrink
        //{
        //    Coffe, Tea
        //}

        //private Dictionary<AvailableDrink, IHotDrinkFactory> _factories
        //    = new Dictionary<AvailableDrink, IHotDrinkFactory>();

        //public HotDrinkMachine()
        //{
        //    foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
        //    {
        //        string typeName = "DesignPatterns.CreationalDesignPatterns.Factories."
        //            + Enum.GetName(typeof(AvailableDrink), drink) + "Factory";
        //        Console.WriteLine($"Trying to get type: {typeName}");

        //        var type = Type.GetType(typeName);

        //        var factory = (IHotDrinkFactory)Activator.CreateInstance(type);

        //        _factories.Add(drink, factory);
        //    }
        //}

        //public IHotDrink MakeDrink(AvailableDrink drink, int amount)
        //{
        //    return _factories[drink].Prepare(amount);
        //}

        // Abstract Factory and OCP

        private List<Tuple<string, IHotDrinkFactory>> _factories = new List<Tuple<string, IHotDrinkFactory>>();

        public HotDrinkMachine()
        {
            foreach (var type in typeof(HotDrinkMachine).Assembly.GetTypes())
            {
                if (typeof(IHotDrinkFactory).IsAssignableFrom(type) && !type.IsInterface)
                {
                    _factories.Add(Tuple.Create(type.Name.Replace("Factory", string.Empty),
                        (IHotDrinkFactory)Activator.CreateInstance(type)));
                }
            }
        }

        public IHotDrink MakeDrink()
        {
            Console.WriteLine("Availeble drinks :");
            for (var index = 0; index < _factories.Count; index++)
            {
                var tuple = _factories[index];
                Console.WriteLine($"{index}: {tuple.Item1}");
            }

            while (true)
            {
                string s;
                if ((s = Console.ReadLine()) != null && int.TryParse(s, out int i) && i >= 0 && i < _factories.Count)
                {
                    Console.Write("Specify amount: ");
                    s = Console.ReadLine();
                    if (s != null && int.TryParse(s, out int amount) && amount > 0)
                    {
                        return _factories[i].Item2.Prepare(amount);
                    }
                }

                Console.WriteLine("Incorrent input, try again!");
            }
        }
    }

    public static void Run()
    {
        Console.WriteLine("Start -> Factories");

        //var point = Point.NewCartesianPoint(1.0, Math.PI / 2);
        //Console.WriteLine(point);

        ////////////////////////////////////////

        //var pointFactory = PointFactory.NewCartesianPoint(1.0, Math.PI / 2);
        //Console.WriteLine(pointFactory);

        ////////////////////////////////////////

        var factory = new TrackingThemeFactory();
        var theme1 = factory.CreateTheme(false);
        var theme2 = factory.CreateTheme(true);

        Console.WriteLine(factory.Info);

        var factory1 = new ReplaceableThemeFactry();
        var magicTheme = factory1.CreateTheme(true);
        Console.WriteLine(magicTheme.Value.BgrColor);

        factory1.ReplaceTheme(false);
        Console.WriteLine(magicTheme.Value.BgrColor);

        ////////////////////////////////////////

        var p = Point.Factory.NewPolarPoint(1, 2);

        ////////////////////////////////////////

        //var machine = new HotDrinkMachine();
        //var drink = machine.MakeDrink(AvailableDrink.Tea, 100);
        //drink.Consume();

        var machine = new HotDrinkMachine();
        var drink = machine.MakeDrink();
        drink.Consume();

        Console.WriteLine("Finish -> Factories");
    }
}
