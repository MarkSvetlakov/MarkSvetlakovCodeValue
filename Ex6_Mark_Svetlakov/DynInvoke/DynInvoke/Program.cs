using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DynInvoke
{
    class Program
    {
        static void Main(string[] args)
        {
            A a = new A();
            B b = new B();
            C c = new C();

            Console.WriteLine(InvokeHello(a, "from A"));
            Console.WriteLine(InvokeHello(b, "from B"));
            Console.WriteLine(InvokeHello(c, "from C"));
        }

        public static StringBuilder InvokeHello(object obj, string parameter)
        {
            StringBuilder stBuilder = new StringBuilder();
            string[] parameters = { parameter };

            MethodInfo[] methods = obj.GetType().GetMethods();

            foreach (MethodInfo info in methods)
            {
                if (info.Name == "Hello")
                {
                    stBuilder.Append(info.Invoke(obj, parameters).ToString());
                }
            }

            if (stBuilder.Length<1)
            {
                stBuilder.AppendLine("No matches found!");
            }
            return stBuilder;
        }
    }
}
