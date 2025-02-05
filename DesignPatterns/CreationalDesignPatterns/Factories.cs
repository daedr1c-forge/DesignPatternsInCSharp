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
        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }

        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }

        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
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

    public static void Run()
    {
        Console.WriteLine("Start -> Factories");

        var point = Point.NewCartesianPoint(1.0, Math.PI / 2);
        Console.WriteLine(point);

        Console.WriteLine("Finish -> Factories");
    }
}
