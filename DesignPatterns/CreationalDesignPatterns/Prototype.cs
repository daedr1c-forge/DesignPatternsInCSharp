using static DesignPatterns.CreationalDesignPatterns.Prototype;

namespace DesignPatterns.CreationalDesignPatterns;

public static class Prototype
{
    public interface IPrototype<T>
    {
        T DeepCopy();
    }

    public class Person : IPrototype<Person>
    {
        public string[] Names;
        public Address Address;

        public Person(string[] names, Address address)
        {
            Names = names ?? throw new ArgumentNullException(nameof(names));
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        //public Person(Person other)
        //{
        //    Names = other.Names;
        //    Address = new Address(other.Address);
        //}

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }

        public Person DeepCopy()
        {
            return new Person(Names, Address.DeepCopy());
        }
    }

    public class Address : IPrototype<Address>
    {
        public string StreetName;
        public int HouseNumber;

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName ?? throw new ArgumentNullException(nameof(streetName));
            HouseNumber = houseNumber;
        }

        //public Address(Address other)
        //{
        //    StreetName = other.StreetName;
        //    HouseNumber = other.HouseNumber;
        //}

        public override string ToString()
        {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }

        public Address DeepCopy()
        {
            return new Address(StreetName, HouseNumber);
        }
    }

    //################################################################################################################
    //#############################################  Prototype Inheritance  ##########################################
    //################################################################################################################

    public interface IDeepCopyable<T> where T : new()
    {
        void CopyTo(T target);

        public T DeepCopy()
        {
            T t = new T();
            CopyTo(t);
            return t;
        }
    }

    public class Address1 : IDeepCopyable<Address1>
    {
        public string StreetName;
        public int HouseNumber;

        public Address1()
        {
        }

        public Address1(Address other)
        {
            StreetName = other.StreetName;
            HouseNumber = other.HouseNumber;
        }

        public void CopyTo(Address1 target)
        {
            StreetName = target.StreetName;
            HouseNumber = target.HouseNumber;
        }

        public override string ToString()
        {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }
    }

    public class Person1 : IDeepCopyable<Person1>
    {
        public string[] Names;
        public Address1 Address;

        public Person1()
        {
        }

        public Person1(string[] names, Address1 address)
        {
            Names = names ?? throw new ArgumentNullException(nameof(names));
            Address = address ?? throw new ArgumentNullException(nameof(address));

        }

        public void CopyTo(Person1 target)
        {
            target.Names = (string[])Names.Clone();
            target.Address = Address.DeepCopy();
        }

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(",", Names)}, {nameof(Address)}: {Address}";
        }
    }

    public class Employee : Person1, IDeepCopyable<Employee>
    {
        public int Salary;

        public Employee()
        {
        }

        public Employee(string[] names, Address1 address, int salary)
            : base(names, address)
        {
            Salary = salary;
        }

        public void CopyTo(Employee target)
        {
            base.CopyTo(target);
            target.Salary = Salary;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, {nameof(Salary)}: {Salary}";
        }
    }

    public static void Run()
    {
        Console.WriteLine("Start -> Prototype");

        var john = new Person(new[] { "John", "Smith" }, new Address("London Road", 123));

        var jane = john.DeepCopy();
        jane.Names[0] = "Jane";
        jane.Address.HouseNumber = 321;

        Console.WriteLine(john);
        Console.WriteLine(jane);

        /////////////////////////////////////////

        var john1 = new Employee();
        john1.Names = new string[] { "John", "Doe" };
        john1.Address = new Address1
        {
            HouseNumber = 123,
            StreetName = "London Road"
        };
        john1.Salary = 320000;

        var copy = john1.DeepCopy();
        copy.Names[1] = "Smith";
        copy.Address.HouseNumber++;
        copy.Salary = 120000;

        Console.WriteLine(john1);
        Console.WriteLine(copy);

        Console.WriteLine("Finish -> Prototype");
    }
}

public static class ExtensionMethods
{
    public static T DeepCopy<T>(this IDeepCopyable<T> item) where T : new()
    {
        return item.DeepCopy();
    }

    public static T DeepCopy<T>(this T person) where T : Person1, new()
    {
        return ((IDeepCopyable<T>)person).DeepCopy();
    }
}