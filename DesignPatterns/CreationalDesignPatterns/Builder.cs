using System.Text;

namespace DesignPatterns.CreationalDesignPatterns;

/// <summary>
/// Builder
/// </summary>
/// 
/// Builder Design Pattern is used to Separate the construction of a complex object
/// from its representation so that the same construction process can create different
/// representations. It helps in constructing a complex object step by step and the final
/// step will return the object. Use It when:
/// 
///     -> The algorithm for creating a complex object should be independent of the parts that make up the object and how they're assembled.
/// 
///     -> The construction process must allow different representations for the object that's constructed.

public static class Builder
{
    public class HtmlElement
    {
        public string Name, Text;
        public List<HtmlElement> Elements = new List<HtmlElement>();
        private const int _indentSize = 2;

        public HtmlElement()
        {
        }

        public HtmlElement(string name, string text)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }

        private string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', _indentSize * indent);
            sb.AppendLine($"{i}<{Name}>");

            if (!string.IsNullOrWhiteSpace(Text))
            {
                sb.Append(new string(' ', _indentSize * (indent + 1)));
                sb.AppendLine(Text);
            }

            foreach (var element in Elements)
            {
                sb.Append(element.ToStringImpl(indent + 1));
            }

            sb.AppendLine($"{i}</{Name}>");

            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }
    }

    public class HtmlBuider
    {
        private readonly string _rootName;
        HtmlElement root = new HtmlElement();


        public HtmlBuider(string rootName)
        {
            _rootName = rootName;
            root.Name = rootName;
        }

        // Fluent Builder
        public HtmlBuider AddChild(string childName, string childText)
        {
            var e = new HtmlElement(childName, childText);
            root.Elements.Add(e);

            return this;
        }

        //public void AddChild(string childName, string childText)
        //{
        //    var e = new HtmlElement(childName, childText);
        //    root.Elements.Add(e);
        //}

        public override string ToString()
        {
            return root.ToString();
        }

        public void Clear()
        {
            root = new HtmlElement { Name = _rootName };
        }
    }

    //////////////////////////////////////////////////////////
    /// Fluent Builder Inheritance with Recursive Generics ///
    //////////////////////////////////////////////////////////

    public class Person
    {
        public string Name { get; set; }
        public string Postion { get; set; }

        public class Builder : PersoneJobBuilder<Builder>
        {
        }
        public static Builder New => new Builder();

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Postion)}: {Postion}";
        }
    }

    public abstract class PersoneBuilder
    {
        protected Person person = new Person();

        public Person Build()
        {
            return person;
        }
    }

    public class PersoneInfoBuilder<T> : PersoneBuilder where T : PersoneInfoBuilder<T>
    {
        public T Called(string name)
        {
            person.Name = name;
            return (T)this;
        }
    }

    public class PersoneJobBuilder<T> : PersoneInfoBuilder<PersoneJobBuilder<T>> where T : PersoneJobBuilder<T>
    {
        public T WorksAsA(string position)
        {
            person.Postion = position;
            return (T)this;
        }
    }

    /////////////////////////
    /// Stepwise Builder ////
    /////////////////////////

    public enum CarType
    {
        Sedan,
        Crossover
    }

    public class Car
    {
        public CarType Type;
        public int WheelSize;
    }

    public interface ISpecifyCarType
    {
        ISpecifyWheelSize OfType(CarType type);
    }

    public interface ISpecifyWheelSize
    {
        IBuildCar WithWheels(int size);
    }

    public interface IBuildCar
    {
        public Car Build();
    }

    public class CarBuilder
    {
        private class Impl : ISpecifyCarType, ISpecifyWheelSize, IBuildCar
        {
            private Car car = new Car();

            public ISpecifyWheelSize OfType(CarType type)
            {
                car.Type = type;
                return this;
            }

            public IBuildCar WithWheels(int size)
            {
                switch (car.Type)
                {
                    case CarType.Crossover when size < 17 || size > 20:
                    case CarType.Sedan when size < 15 || size > 17:
                        throw new ArgumentException($"Wrong size of wheel of {car.Type}.");
                }

                car.WheelSize = size;
                return this;
            }

            public Car Build()
            {
                return car;
            }
        }

        public static ISpecifyCarType Create()
        {
            return new Impl();
        }
    }


    public static void Run()
    {
        Console.WriteLine("Start -> Builder");

        var builder = new HtmlBuider("ul");

        builder.AddChild("li", "hello")
            .AddChild("li", "world");

        Console.WriteLine(builder.ToString());

        //////////////////////////////////////

        var me = Person.New
            .Called("Mykola")
            .WorksAsA("quant")
            .Build();

        Console.WriteLine(me);

        ////////////////////////////////////////

        var car = CarBuilder.Create()   //ISpecifyCarType
            .OfType(CarType.Crossover)  //ISpecifyWheelSize
            .WithWheels(18)             //IBuildCar
            .Build();

        Console.WriteLine("Start -> Builder");
    }
}
