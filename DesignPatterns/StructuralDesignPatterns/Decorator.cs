using System.Runtime.Serialization;
using System.Text;

namespace DesignPatterns.StructuralDesignPatterns;

public static class Decorator
{
    public class CodeBuilder
    {
        private StringBuilder _builder = new StringBuilder();

        public override string ToString()
        {
            return _builder.ToString();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ((ISerializable)_builder).GetObjectData(info, context);
        }

        public int EnsureCapacity(int capacity)
        {
            return _builder.EnsureCapacity(capacity);
        }

        public string ToString(int startIndex, int length)
        {
            return _builder.ToString(startIndex, length);
        }

        public CodeBuilder Clear()
        {
            _builder.Clear();
            return this;
        }

        public CodeBuilder Append(char value, int repeatCount)
        {
            _builder.Append(value, repeatCount);
            return this;
        }

        public CodeBuilder Append(char[] value, int startIndex, int charCount)
        {
            _builder.Append(value, startIndex, charCount);
            return this;
        }

        public CodeBuilder Append(string value)
        {
            _builder.Append(value);
            return this;
        }

        public CodeBuilder Append(string value, int startIndex, int count)
        {
            _builder.Append(value, startIndex, count);
            return this;
        }

        public CodeBuilder AppendLine()
        {
            _builder.AppendLine();
            return this;
        }

        public CodeBuilder AppendLine(string value)
        {
            _builder.AppendLine(value);
            return this;
        }

        public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
        {
            _builder.CopyTo(sourceIndex, destination, destinationIndex, count);
        }

        public CodeBuilder Insert(int index, string value, int count)
        {
            _builder.Insert(index, value, count);
            return this;
        }

        public CodeBuilder Remove(int startIndex, int length)
        {
            _builder.Remove(startIndex, length);
            return this;
        }

        public CodeBuilder Append(bool value)
        {
            _builder.Append(value);
            return this;
        }

        public CodeBuilder Append(sbyte value)
        {
            _builder.Append(value);
            return this;
        }

        public CodeBuilder Append(byte value)
        {
            _builder.Append(value);
            return this;
        }

        public CodeBuilder Append(char value)
        {
            _builder.Append(value);
            return this;
        }

        public CodeBuilder Append(short value)
        {
            _builder.Append(value);
            return this;
        }

        public CodeBuilder Append(int value)
        {
            _builder.Append(value);
            return this;
        }

        public CodeBuilder Append(long value)
        {
            _builder.Append(value);
            return this;
        }

        public CodeBuilder Append(float value)
        {
            _builder.Append(value);
            return this;
        }

        public CodeBuilder Append(double value)
        {
            _builder.Append(value);
            return this;
        }

        public CodeBuilder Append(decimal value)
        {
            _builder.Append(value);
            return this;
        }

        public CodeBuilder Append(ushort value)
        {
            _builder.Append(value);
            return this;
        }

        public CodeBuilder Append(uint value)
        {
            _builder.Append(value);
            return this;
        }

        public CodeBuilder Append(ulong value)
        {
            _builder.Append(value);
            return this;
        }

        public CodeBuilder Append(object value)
        {
            _builder.Append(value);
            return this;
        }

        public CodeBuilder Append(char[] value)
        {
            _builder.Append(value);
            return this;
        }

        public CodeBuilder Insert(int index, string value)
        {
            _builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, bool value)
        {
            _builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, sbyte value)
        {
            _builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, byte value)
        {
            _builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, short value)
        {
            _builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, char value)
        {
            _builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, char[] value)
        {
            _builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, char[] value, int startIndex, int charCount)
        {
            _builder.Insert(index, value, startIndex, charCount);
            return this;
        }

        public CodeBuilder Insert(int index, int value)
        {
            _builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, long value)
        {
            _builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, float value)
        {
            _builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, double value)
        {
            _builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, decimal value)
        {
            _builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, ushort value)
        {
            _builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, uint value)
        {
            _builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, ulong value)
        {
            _builder.Insert(index, value);
            return this;
        }

        public CodeBuilder Insert(int index, object value)
        {
            _builder.Insert(index, value);
            return this;
        }

        public CodeBuilder AppendFormat(string format, object arg0)
        {
            _builder.AppendFormat(format, arg0);
            return this;
        }

        public CodeBuilder AppendFormat(string format, object arg0, object arg1)
        {
            _builder.AppendFormat(format, arg0, arg1);
            return this;
        }

        public CodeBuilder AppendFormat(string format, object arg0, object arg1, object arg2)
        {
            _builder.AppendFormat(format, arg0, arg1, arg2);
            return this;
        }

        public CodeBuilder AppendFormat(string format, params object[] args)
        {
            _builder.AppendFormat(format, args);
            return this;
        }

        public CodeBuilder AppendFormat(IFormatProvider provider, string format, params object[] args)
        {
            _builder.AppendFormat(provider, format, args);
            return this;
        }

        public CodeBuilder Replace(string oldValue, string newValue)
        {
            _builder.Replace(oldValue, newValue);
            return this;
        }

        public bool Equals(CodeBuilder sb)
        {
            return _builder.Equals(sb);
        }

        public CodeBuilder Replace(string oldValue, string newValue, int startIndex, int count)
        {
            _builder.Replace(oldValue, newValue, startIndex, count);
            return this;
        }

        public CodeBuilder Replace(char oldChar, char newChar)
        {
            _builder.Replace(oldChar, newChar);
            return this;
        }

        public CodeBuilder Replace(char oldChar, char newChar, int startIndex, int count)
        {
            _builder.Replace(oldChar, newChar, startIndex, count);
            return this;
        }

        public int Capacity
        {
            get => _builder.Capacity;
            set => _builder.Capacity = value;
        }

        public int MaxCapacity => _builder.MaxCapacity;

        public int Length
        {
            get => _builder.Length;
            set => _builder.Length = value;
        }

        public char this[int index]
        {
            get => _builder[index];
            set => _builder[index] = value;
        }
    }

    //################################################################################################################
    //#############################################  Adapter-Decorator  ##############################################
    //################################################################################################################

    public class MyStringBuilder
    {
        StringBuilder sb = new StringBuilder();

        //=============================================

        public static implicit operator MyStringBuilder(string s)
        {
            var msb = new MyStringBuilder();
            msb.sb.Append(s);
            return msb;
        }

        public static MyStringBuilder operator +(MyStringBuilder msb, string s)
        {
            msb.Append(s);
            return msb;
        }

        public override string ToString()
        {
            return sb.ToString();
        }

        //=============================================

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ((ISerializable)sb).GetObjectData(info, context);
        }

        public int EnsureCapacity(int capacity)
        {
            return sb.EnsureCapacity(capacity);
        }

        public string ToString(int startIndex, int length)
        {
            return sb.ToString(startIndex, length);
        }

        public StringBuilder Clear()
        {
            return sb.Clear();
        }

        public StringBuilder Append(char value, int repeatCount)
        {
            return sb.Append(value, repeatCount);
        }

        public StringBuilder Append(char[] value, int startIndex, int charCount)
        {
            return sb.Append(value, startIndex, charCount);
        }

        public StringBuilder Append(string value)
        {
            return sb.Append(value);
        }

        public StringBuilder Append(string value, int startIndex, int count)
        {
            return sb.Append(value, startIndex, count);
        }

        public StringBuilder AppendLine()
        {
            return sb.AppendLine();
        }

        public StringBuilder AppendLine(string value)
        {
            return sb.AppendLine(value);
        }

        public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
        {
            sb.CopyTo(sourceIndex, destination, destinationIndex, count);
        }

        public StringBuilder Insert(int index, string value, int count)
        {
            return sb.Insert(index, value, count);
        }

        public StringBuilder Remove(int startIndex, int length)
        {
            return sb.Remove(startIndex, length);
        }

        public StringBuilder Append(bool value)
        {
            return sb.Append(value);
        }

        public StringBuilder Append(sbyte value)
        {
            return sb.Append(value);
        }

        public StringBuilder Append(byte value)
        {
            return sb.Append(value);
        }

        public StringBuilder Append(char value)
        {
            return sb.Append(value);
        }

        public StringBuilder Append(short value)
        {
            return sb.Append(value);
        }

        public StringBuilder Append(int value)
        {
            return sb.Append(value);
        }

        public StringBuilder Append(long value)
        {
            return sb.Append(value);
        }

        public StringBuilder Append(float value)
        {
            return sb.Append(value);
        }

        public StringBuilder Append(double value)
        {
            return sb.Append(value);
        }

        public StringBuilder Append(decimal value)
        {
            return sb.Append(value);
        }

        public StringBuilder Append(ushort value)
        {
            return sb.Append(value);
        }

        public StringBuilder Append(uint value)
        {
            return sb.Append(value);
        }

        public StringBuilder Append(ulong value)
        {
            return sb.Append(value);
        }

        public StringBuilder Append(object value)
        {
            return sb.Append(value);
        }

        public StringBuilder Append(char[] value)
        {
            return sb.Append(value);
        }

        public StringBuilder Insert(int index, string value)
        {
            return sb.Insert(index, value);
        }

        public StringBuilder Insert(int index, bool value)
        {
            return sb.Insert(index, value);
        }

        public StringBuilder Insert(int index, sbyte value)
        {
            return sb.Insert(index, value);
        }

        public StringBuilder Insert(int index, byte value)
        {
            return sb.Insert(index, value);
        }

        public StringBuilder Insert(int index, short value)
        {
            return sb.Insert(index, value);
        }

        public StringBuilder Insert(int index, char value)
        {
            return sb.Insert(index, value);
        }

        public StringBuilder Insert(int index, char[] value)
        {
            return sb.Insert(index, value);
        }

        public StringBuilder Insert(int index, char[] value, int startIndex, int charCount)
        {
            return sb.Insert(index, value, startIndex, charCount);
        }

        public StringBuilder Insert(int index, int value)
        {
            return sb.Insert(index, value);
        }

        public StringBuilder Insert(int index, long value)
        {
            return sb.Insert(index, value);
        }

        public StringBuilder Insert(int index, float value)
        {
            return sb.Insert(index, value);
        }

        public StringBuilder Insert(int index, double value)
        {
            return sb.Insert(index, value);
        }

        public StringBuilder Insert(int index, decimal value)
        {
            return sb.Insert(index, value);
        }

        public StringBuilder Insert(int index, ushort value)
        {
            return sb.Insert(index, value);
        }

        public StringBuilder Insert(int index, uint value)
        {
            return sb.Insert(index, value);
        }

        public StringBuilder Insert(int index, ulong value)
        {
            return sb.Insert(index, value);
        }

        public StringBuilder Insert(int index, object value)
        {
            return sb.Insert(index, value);
        }

        public StringBuilder AppendFormat(string format, object arg0)
        {
            return sb.AppendFormat(format, arg0);
        }

        public StringBuilder AppendFormat(string format, object arg0, object arg1)
        {
            return sb.AppendFormat(format, arg0, arg1);
        }

        public StringBuilder AppendFormat(string format, object arg0, object arg1, object arg2)
        {
            return sb.AppendFormat(format, arg0, arg1, arg2);
        }

        public StringBuilder AppendFormat(string format, params object[] args)
        {
            return sb.AppendFormat(format, args);
        }

        public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0)
        {
            return sb.AppendFormat(provider, format, arg0);
        }

        public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0,
          object arg1)
        {
            return sb.AppendFormat(provider, format, arg0, arg1);
        }

        public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0,
          object arg1, object arg2)
        {
            return sb.AppendFormat(provider, format, arg0, arg1, arg2);
        }

        public StringBuilder AppendFormat(IFormatProvider provider, string format, params object[] args)
        {
            return sb.AppendFormat(provider, format, args);
        }

        public StringBuilder Replace(string oldValue, string newValue)
        {
            return sb.Replace(oldValue, newValue);
        }

        public bool Equals(StringBuilder sb)
        {
            return this.sb.Equals(sb);
        }

        public StringBuilder Replace(string oldValue, string newValue, int startIndex, int count)
        {
            return sb.Replace(oldValue, newValue, startIndex, count);
        }

        public StringBuilder Replace(char oldChar, char newChar)
        {
            return sb.Replace(oldChar, newChar);
        }

        public StringBuilder Replace(char oldChar, char newChar, int startIndex, int count)
        {
            return sb.Replace(oldChar, newChar, startIndex, count);
        }

        public int Capacity
        {
            get => sb.Capacity;
            set => sb.Capacity = value;
        }

        public int MaxCapacity => sb.MaxCapacity;

        public int Length
        {
            get => sb.Length;
            set => sb.Length = value;
        }

        public char this[int index]
        {
            get => sb[index];
            set => sb[index] = value;
        }
    }

    //################################################################################################################
    //####################################  Multiple Inheritance with Interfaces  ####################################
    //################################################################################################################

    public interface IBirth
    {
        void Fly();
        int Weight { get; set; }
    }

    public class Birth : IBirth
    {
        public int Weight { get; set; }

        public void Fly()
        {
            Console.WriteLine($"Soaring in the sky with weight {Weight}");
        }
    }

    public interface ILizard
    {
        void Crawl();
        int Weight { get; set; }
    }

    public class Lizard : ILizard
    {
        public int Weight { get; set; }

        public void Crawl()
        {
            Console.WriteLine($"Crawling in the dirt with weight {Weight}");
        }
    }

    public class Dragon : IBirth, ILizard
    {
        private Birth birth = new Birth();
        private Lizard lizard = new Lizard();
        private int weight;

        public void Crawl()
        {
            lizard.Crawl();
        }

        public void Fly()
        {
            birth.Fly();
        }

        public int Weight
        {
            get { return weight; }
            set
            {
                weight = value;
                birth.Weight = value;
                lizard.Weight = weight;
            }
        }
    }

    //################################################################################################################
    //########################################  Dynamic Decorator Composition  #######################################
    //################################################################################################################

    public interface IShape
    {
        string AsString();
    }

    public class Circle : IShape
    {
        private float _radius;

        public Circle(float radius)
        {
            _radius = radius;
        }

        public void Resize(float factor)
        {
            _radius *= factor;
        }

        public string AsString() => $"A circle with radius {_radius}";
    }

    public class Squere : IShape
    {
        private float _side;

        public Squere(float side)
        {
            _side = side;
        }

        public string AsString() => $"A square with side {_side}";
    }

    public class ColoredShape : IShape
    {
        private IShape _shape;
        private string _color;

        public ColoredShape(IShape shape, string color)
        {
            _shape = shape;
            _color = color;
        }

        public string AsString() => $"{_shape.AsString()} has the color {_color}";
    }

    public class TransparentShape : IShape
    {
        private IShape _shape;
        private float _transpaerncy;

        public TransparentShape(IShape shape, float transpaerncy)
        {
            _shape = shape;
            _transpaerncy = transpaerncy;
        }

        public string AsString() => $"{_shape.AsString()} has {_transpaerncy * 100.0}% transpaerncy";
    }

    public static void Run()
    {
        Console.WriteLine("Start -> Decorator");

        var cb = new CodeBuilder();
        cb.AppendLine("class Foo")
            .AppendLine("{")
            .AppendLine("}");

        Console.WriteLine(cb);

        ////////////////////////////////////////

        MyStringBuilder s = "Hello ";
        s += "world";
        Console.WriteLine(s);

        ////////////////////////////////////////

        var d = new Dragon();
        d.Weight = 123;
        d.Fly();
        d.Crawl();

        ////////////////////////////////////////

        var square = new Squere(1.23f);
        Console.WriteLine(square.AsString());
        var redSquere = new ColoredShape(square, "red");
        Console.WriteLine(redSquere.AsString());
        var redHalfTransparentSquare = new TransparentShape(redSquere, 0.5f);
        Console.WriteLine(redHalfTransparentSquare.AsString());

        Console.WriteLine("Finish -> Decorator");
    }
}
