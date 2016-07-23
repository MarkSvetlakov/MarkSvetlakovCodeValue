using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OLinq
{
    class MscorlibInfo
    {
        public Assembly AssemblyToLoad { get; }
        public Type[] Types { get; }
        public StringBuilder strBuilder { get; private set; }

        public MscorlibInfo()
        {
            AssemblyToLoad = Assembly.Load("mscorlib");
            Types = AssemblyToLoad.GetTypes();
            strBuilder = new StringBuilder();
        }


        public StringBuilder GetAssemblyInfo()
        {
            strBuilder.AppendLine("Assembly info:");
            var result = Types.Where(x => x.IsPublic).Where(x => x.IsInterface).OrderBy(x => x.Name);
            foreach (var item in result)
            {
                MethodInfo[] resMethods = item.GetMethods();
                var methodsCount = resMethods.Count();
                strBuilder.AppendLine($"Interface name: {item.Name} Number of methods: {methodsCount}");
            }
            return strBuilder;
        }
    }
}
