using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;

namespace ShapesApp
{
    class ShapeManager
    {
        ArrayList shapesList = new ArrayList();

        public void Add(ShapeLib.Shape shape)
        {
            shapesList.Add(shape);
        }

        public void DisplayAll()
        {
            foreach (ShapeLib.Shape shape in shapesList)
            {
                shape.Display();
            }
        }

        public ShapeLib.Shape this[int index]
        {
            get
            {
                return shapesList[index] as ShapeLib.Shape;
            }
        }

        public int Count
        {
            get
            {
                return shapesList.Count;
            }
        }
    }
}
