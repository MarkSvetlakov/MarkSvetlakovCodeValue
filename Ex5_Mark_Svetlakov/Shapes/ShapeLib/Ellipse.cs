using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeLib
{
    public class Ellipse : Shape, IPersist, IComparable
    {
        public int LongAxle { get; private set; }
        public int ShortAxle { get; private set; }
        private ConsoleColor _color { get; }
        public Ellipse (int longAxle, int shortAxle, ConsoleColor color) : base (color)
        {
            this.LongAxle = longAxle;
            this.ShortAxle = shortAxle;
            this._color = color;
        }

        public override double Area
        {
            get
            {
                return Math.PI * (this.LongAxle * this.ShortAxle);
            }
        }

        public override void Display()
        {
            Console.ForegroundColor = this._color;
            Console.WriteLine(string.Format("Ellipse Long Axle is - {0}, Short Axle - {1}", this.LongAxle, this.ShortAxle));
            Console.WriteLine("Area is: " + Area);
            Console.ResetColor();
        }

        public void Write(StringBuilder sb)
        {
            sb.AppendLine("" + this.LongAxle);
            sb.AppendLine("" + this.ShortAxle);
        }

        public int CompareTo(object obj)
        {
            return Area.CompareTo(obj);
        }
    }
}
