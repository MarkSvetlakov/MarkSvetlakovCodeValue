using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AttribDemo
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = true)]
    public sealed class CodeReviewAttribute : Attribute
    {

        public string ReviewerName { get; }
        public string ReviewerDate { get; }
        public bool IsApproved { get; }

        public CodeReviewAttribute(string name, string date, bool approved)
        {
            this.ReviewerName = name;
            this.ReviewerDate = date;
            this.IsApproved = approved;
        }
    }
}
