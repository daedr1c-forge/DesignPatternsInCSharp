namespace DesignPatterns.CreationalDesignPatterns;

public static class Prototype
{
    public class Person
    {
        public string[] Names;
        public Address Address;

        public Person(string[] names, Address address)
        {
            Names = names ?? throw new ArgumentNullException(nameof(names));
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        public Person(Person other)
        {
            Names = other.Names;
            Address = new Address(other.Address); 
        }

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }
    }

    public class Address
    {
        public string StreetName;
        public int HouseNumber;

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName ?? throw new ArgumentNullException(nameof(streetName));
            HouseNumber = houseNumber;
        }

        public Address(Address other)
        {
            StreetName = other.StreetName;
            HouseNumber = other.HouseNumber;
        }

        public override string ToString()
        {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }
    }

    public static void Run()
    {
        Console.WriteLine("Start -> Prototype");

        var john = new Person(new[] { "John", "Smith" }, new Address("London Road", 123));

        var jane = new Person(john);
        jane.Names[0] = "Jane";
        jane.Address.HouseNumber = 321;

        Console.WriteLine(john);
        Console.WriteLine(jane);

        Console.WriteLine("Finish -> Prototype");
    }
}
