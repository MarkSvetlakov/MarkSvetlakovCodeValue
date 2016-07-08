using ShapeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeLib
{
    public class Rectangle : Shape , IPersist, IComparable
    {
        
        public int Wight { get; private set; }
        public int Height { get; private set; }
        public ConsoleColor Color { get; private set; }
        public override double Area
        {
            get
            {
                return this.Wight * this.Height;
            }
        }


        public Rectangle(int width, int height, ConsoleColor color) : base (color)
        {
            this.Wight = width;
            this.Height = height;
            this.Color = color;
        }

        public override void Display()
        {
            Console.ForegroundColor = this.Color;
            Console.WriteLine(string.Format("Rectangle Wight is - {0}, Height - {1}", this.Wight, this.Height));
            Console.WriteLine("Area is: "+Area);
            Console.ResetColor();
        }


        public void Write(StringBuilder sb)
        {
            sb.AppendLine("" + this.Wight);
            sb.AppendLine("" + this.Height);
        }

        public int CompareTo(object obj)
        {
            return Area.CompareTo(obj);
        }
    }
}
