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
    class XMLInfo
    {

        public StringBuilder TypesWithoutProperties(XElement element)
        {
            StringBuilder strBuilder = new StringBuilder();
            var types = element?.Descendants("Type")
                .Where(x => x.Element("Properties").IsEmpty).OrderBy(x => x.Name);

            if (types != null)
            {
                foreach (var item in types)
                {
                    strBuilder.AppendLine(item.ToString());
                }
            }
            else
            {
                strBuilder.AppendLine("No data");
            }
            return strBuilder;
        }


        public StringBuilder TypesWithoutPropertiesCount(XElement element)
        {
            StringBuilder strBuilder = new StringBuilder();
            if (element != null)
            {
                strBuilder.AppendLine($"Count of Types without properties - {element.Descendants("Type").Where(x => x.Element("Properties").IsEmpty).Count()}");
            }
            else
            {
                strBuilder.AppendLine("No data");
            }
            return strBuilder;
        }


        public StringBuilder MethodsCount(XElement element)
        {
            StringBuilder strBuilder = new StringBuilder();
            if (element != null)
            {
                strBuilder.AppendLine($"Count of Methods - {element.Descendants("Method").Count()}");
            }
            else
            {
                strBuilder.AppendLine("No data");
            }
            return strBuilder;
        }


        public StringBuilder PropertiesCount(XElement element)
        {
            StringBuilder strBuilder = new StringBuilder();
            if (element != null)
            {
                strBuilder.AppendLine($"Count of Properties - {element.Descendants("Property").Count()}");
            }
            else
            {
                strBuilder.AppendLine("No data");
            }
            return strBuilder;
        }


        public StringBuilder ParametersCount(XElement element)
        {
            StringBuilder strBuilder = new StringBuilder();
            if (element != null)
            {
                strBuilder.AppendLine($"Count of Parameters - {element.Descendants("Parameter").Count()}");
            }
            else
            {
                strBuilder.AppendLine("No data");
            }
            return strBuilder;
        }


        public StringBuilder MostType(XElement element)
        {
            StringBuilder strBuilder = new StringBuilder();

            var resutl = element?.Descendants("Parameters").Elements("Parameter").Attributes("Type")
               .GroupBy(x => x.Value).Select(group => new
               {
                   value = group.Key,
                   count = group.Count()
               }
               ).OrderByDescending(x => x.count).FirstOrDefault();

            if (resutl != null)
            {
                strBuilder.AppendLine($"Most common type - {resutl.value}, appears {resutl.count} times");
            }
            else
            {
                strBuilder.AppendLine("No data");
            }

            return strBuilder;
        }


        public StringBuilder GroupByNumberOfMethods(XElement element)
        {
            StringBuilder strBuilder = new StringBuilder();

            if (element != null)
            {

                var result = from p in element?.Descendants("Type")
                         .OrderByDescending(x => x.Descendants("Method").Count())
                             group p by p.Attribute("Full_Name")
                         into groups
                             select new
                             {
                                 key = groups.FirstOrDefault(),
                             };

                foreach (var item in result)
                {
                    strBuilder.AppendLine($"{item.key}");
                }
                
            }
            else
            {
                strBuilder.AppendLine("No data");
            }

            return strBuilder;
        }

    }
}
