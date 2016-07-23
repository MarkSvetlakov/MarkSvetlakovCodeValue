using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XLinq
{
    class XMLBuilder
    {
        public XElement XMLElement { get; private set; }
        public XElement SortedXMLElement { get; private set; }
        public Assembly AssemblyToLoad { get; private set; }


        public XMLBuilder()
        {
            AssemblyToLoad = Assembly.Load("mscorlib");
            BuildMscorlibXML();
        }


        private void BuildMscorlibXML()
        {
            XMLElement = new XElement("mscorlib",
            from classes in AssemblyToLoad.GetTypes()
            where classes.IsClass && classes.IsPublic
            select new XElement("Type",
            new XAttribute("Full_Name", classes.FullName),

            new XElement("Properties",
            from properties in classes.GetProperties()
            select new XElement("Property",
            new XAttribute("Name", properties.Name),
            new XAttribute("Type", properties.PropertyType))),

            new XElement("Methods",
            from methods in classes.GetMethods()
            where methods.DeclaringType == classes
            select new XElement("Method",
            new XAttribute("Name", methods.Name),
            new XAttribute("Return_type", methods.ReturnType),

            new XElement("Parameters",
            from parameters in methods.GetParameters()
            select new XElement("Parameter",
            new XAttribute("Name", parameters.Name),
            new XAttribute("Type", parameters.ParameterType)
            ))))));
        }


        public StringBuilder BuildSortedXML(XElement element)
        {
            int propCount = 0;
            int methodCount = 0;
            StringBuilder strBuilder = new StringBuilder();

            var sortedElements = element?.Descendants("Type").OrderByDescending(x => x.Descendants("Method").Count());


            if (element != null)
            {
                SortedXMLElement = new XElement("Sorted_By_Methods");

                foreach (XElement itemElement in sortedElements)
                {
                    propCount = itemElement.Descendants("Property").Count();
                    methodCount = itemElement.Descendants("Method").Count();
                    SortedXMLElement.Add(new XElement(itemElement));
                    SortedXMLElement.Add(new XElement("Prop_count", propCount));
                    SortedXMLElement.Add(new XElement("Methods_count", methodCount));
                }
                strBuilder.AppendLine("Build sorted XML element complete.");
            }
            else
            {
                strBuilder.AppendLine("Can't build with this element");
            }
            return strBuilder;
        }


        public void SaveToXML(XElement element, string elementName)
        {
            if (string.IsNullOrEmpty(elementName))
            {
                element?.Save("noNamed.xml");
            }
            else
            {
                element?.Save($"{elementName}.xml");
            }
        }
    }
}
