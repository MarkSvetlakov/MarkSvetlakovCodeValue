using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeLib
{
    interface IPersist
    {
        void Write(StringBuilder sb);
    }
}
