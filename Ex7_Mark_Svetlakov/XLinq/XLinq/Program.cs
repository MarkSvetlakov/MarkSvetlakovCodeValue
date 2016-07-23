using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            XMLBuilder xmlBuilder = new XMLBuilder();
            XMLInfo xmlInfo = new XMLInfo();

            xmlBuilder.SaveToXML(xmlBuilder.XMLElement, "mscorlib");

            Console.WriteLine(xmlInfo.TypesWithoutProperties(xmlBuilder.XMLElement));

            Console.WriteLine(xmlInfo.TypesWithoutPropertiesCount(xmlBuilder.XMLElement));

            Console.WriteLine(xmlInfo.MethodsCount(xmlBuilder.XMLElement));

            Console.WriteLine(xmlInfo.PropertiesCount(xmlBuilder.XMLElement));

            Console.WriteLine(xmlInfo.ParametersCount(xmlBuilder.XMLElement));

            Console.WriteLine(xmlInfo.MostType(xmlBuilder.XMLElement));

            Console.WriteLine(xmlBuilder.BuildSortedXML(xmlBuilder.XMLElement));

            xmlBuilder.SaveToXML(xmlBuilder.SortedXMLElement, "sorted");

            Console.WriteLine(xmlInfo.GroupByNumberOfMethods(xmlBuilder.XMLElement));

        }
    }
}
