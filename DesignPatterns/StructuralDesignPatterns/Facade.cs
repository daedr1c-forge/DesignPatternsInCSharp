namespace DesignPatterns.StructuralDesignPatterns;

public static class Facade
{
    // Subsystem A
    class SubsystemA
    {
        public void OperationA()
        {
            Console.WriteLine("SubsystemA: Performing operation A");
        }
    }

    // Subsystem B
    class SubsystemB
    {
        public void OperationB()
        {
            Console.WriteLine("SubsystemB: Performing operation B");
        }
    }

    // Subsystem C
    class SubsystemC
    {
        public void OperationC()
        {
            Console.WriteLine("SubsystemC: Performing operation C");
        }
    }

    // Facade class to simplify interaction with subsystems
    public class FacadeSystem
    {
        private readonly SubsystemA _subsystemA;
        private readonly SubsystemB _subsystemB;
        private readonly SubsystemC _subsystemC;

        public FacadeSystem()
        {
            _subsystemA = new SubsystemA();
            _subsystemB = new SubsystemB();
            _subsystemC = new SubsystemC();
        }

        // Simplified method to call multiple subsystems
        public void Operation()
        {
            Console.WriteLine("Facade: Coordinating subsystems...");
            _subsystemA.OperationA();
            _subsystemB.OperationB();
            _subsystemC.OperationC();
            Console.WriteLine("Facade: Operation completed");
        }
    }

    public static void Run()
    {
        Console.WriteLine("Start -> Facade");

        FacadeSystem facade = new FacadeSystem();
        facade.Operation();

        Console.WriteLine("Finish -> Facade");
    }
}