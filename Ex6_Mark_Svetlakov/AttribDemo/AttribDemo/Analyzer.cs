using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AttribDemo
{
    class Analyzer
    {
        public StringBuilder StrBuilder { get; }

        public Analyzer()
        {
            StrBuilder = new StringBuilder();
        }

        public StringBuilder AnalayzeAssembly(Assembly obj)
        {
            CodeReviewAttribute att;

            var types = obj.GetTypes().Where(x => x.IsDefined(typeof(CodeReviewAttribute)));

            foreach (Type item in types)
            {
                att = (CodeReviewAttribute)item.GetCustomAttribute(typeof(CodeReviewAttribute));
                StrBuilder.AppendLine($"Result from: {item.Name}");
                StrBuilder.AppendLine($"Reviewer Name: {att.ReviewerName}");
                StrBuilder.AppendLine($"Reviewer Date: {att.ReviewerDate}");
                StrBuilder.AppendLine($"Approved: {att.IsApproved}");
                StrBuilder.AppendLine("");
            }
            if (StrBuilder.Length < 1)
            {
                StrBuilder.AppendLine("No attributes found!");
            }
            return StrBuilder;
        }

    }
}
