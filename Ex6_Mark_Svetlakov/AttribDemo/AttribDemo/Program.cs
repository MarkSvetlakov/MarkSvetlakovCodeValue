using System;
using System.Linq;
using System.Reflection;
using System.Text;


namespace AttribDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Analyzer analyzer = new Analyzer();
            Console.WriteLine(analyzer.AnalayzeAssembly(Assembly.GetExecutingAssembly()));

            // What happens if you pass a reference to some other Assembly to the AnalyzeAssembly method? Try it.

            //If "CodeReview" doesnt exist, AnalayzeAssembly returns "No attributes found!" message:
            //Assembly assembly = Assembly.Load("mscorlib");
            //Console.WriteLine(analyzer.AnalayzeAssembly(assembly));

        }
    }
}
