using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynInvoke
{
    class C
    {
        public C()
        {
            this.HelloWord = "Hello";
        }
        public string HelloWord { get; }
        public StringBuilder Hello(string str)
        {
            
        StringBuilder strBuilder = new StringBuilder();
            

        strBuilder.AppendLine($"{HelloWord} {str}");
            return strBuilder;
        }
    }
}
