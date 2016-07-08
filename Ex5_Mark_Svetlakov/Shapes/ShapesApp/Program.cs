using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShapeLib;

namespace ShapesApp
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Values is:");
            ShapeManager sm = new ShapeManager();
            Shape r1 = new Rectangle(4,6, ConsoleColor.Red);
            Shape e1 = new Ellipse(3,6, ConsoleColor.Blue);
            Shape c1 = new Circle(6,6, ConsoleColor.Green);
            sm.Add(r1);
            sm.Add(e1);
            sm.Add(c1);
            sm.DisplayAll();
        }
    }
}
