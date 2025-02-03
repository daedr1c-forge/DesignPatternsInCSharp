namespace DesignPatterns.SOLID;

/// <summary>
/// DIP (Dependency Inversion Principle)
/// </summary>
/// 
/// Dependency Inversion is a principle proposed by Robert C. Martin as part of the SOLID principles.
/// It suggests that high-level modules should not depend on low-level modules; both should depend on abstractions.
/// Abstractions should not depend on details; details should depend on abstractions.
/// 
/// Let us first explain why the word “inversion” is used here. In the old, procedural design,
/// high-level modules (which generally contain the core business logic) would naturally
/// depend on low-level modules (which handle specific tasks, like data access).
/// This means that the business core, the identity of the application, the policy classes,
/// depends on low level details. This creates tight coupling, which means that changes in low-level 
/// modules cause changes in high-level modules and so the low-level details direct the core, the very 
/// identity of the application. For example, if the data storage mechanism changes, all modules using it,
/// including the high-level business services, would need to be updated.
/// 
/// DIP states two main rules:
///  
/// -> High-level modules should not depend on low-level modules.Both should depend on abstractions.
/// 
/// -> Abstractions should not depend on details. Details should depend on abstractions.

public class DIP
{
    public DIP() { }

    public enum Relationship
    {
        Parent, Child, Sibling
    }

    public class Person
    {
        public string Name { get; set; }
    }

    public interface IRelationshipBrowser
    {
        IEnumerable<Person> FindAllChildrenOf(string name);
    }

    // low-level
    public class Relationships : IRelationshipBrowser
    {
        private List<(Person, Relationship, Person)> _relations
            = new List<(Person, Relationship, Person)>();

        public void AddParentAndChild(Person parent, Person child)
        {
            _relations.Add((parent, Relationship.Parent, child));
            _relations.Add((child, Relationship.Child, parent));
        }

        public IEnumerable<Person> FindAllChildrenOf(string name)
        {
            return _relations.Where(x => x.Item1.Name == name && x.Item2 == Relationship.Parent).Select(rel => rel.Item3);

            //foreach (var rel in _relations.Where(x => x.Item1.Name == name && x.Item2 == Relationship.Parent))
            //{
            //    yield return rel.Item3;
            //}
        }

        //public List<(Person, Relationship, Person)> Relations => _relations;
    }

    //public DIP(Relationships relationships)
    //{
    //    var relations = relationships.Relations;
    //    foreach (var rel in relations.Where(x => x.Item1.Name == "Jonh" && x.Item2 == Relationship.Parent))
    //    {
    //        Console.WriteLine($"John has a child called {rel.Item3.Name}");
    //    }
    //}

    public DIP(IRelationshipBrowser browser)
    {
        foreach (var p in browser.FindAllChildrenOf("John"))
            Console.WriteLine($"John has a child called {p.Name}");
    }

    public static void Run()
    {
        Console.WriteLine("Start ->  DIP (Dependency Inversion Principle)");

        var parent = new Person() { Name = "John" };
        var child1 = new Person() { Name = "Chris" };
        var child2 = new Person() { Name = "Mary" };

        var relationships = new Relationships();
        relationships.AddParentAndChild(parent, child1);
        relationships.AddParentAndChild(parent, child2);

        new DIP(relationships);

        Console.WriteLine("Finish ->  DIP (Dependency Inversion Principle)");
    }
}
