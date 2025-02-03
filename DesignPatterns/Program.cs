using DesignPatterns.SOLID;

namespace DesignPatterns;

internal class Program
{
    static void Main(string[] args)
    {
        SectionSOLID();
    }

    /// <summary>
    /// SOLID Design Principles
    /// SOLID principles make it easy for a developer to write easily extendable code and avoid common coding errors.
    /// These principles were introduced by Robert C.Martin, and they have become a fundamental part of object-oriented programming.
    /// In the context of .NET development, adhering to SOLID principles can lead to more modular, flexible, and maintainable code.
    /// </summary>
    private static void SectionSOLID()
    {   
        SRP.Run();
        OCP.Run();
        LSP.Run();
        ISP.Run();
        DIP.Run();
    }
}
