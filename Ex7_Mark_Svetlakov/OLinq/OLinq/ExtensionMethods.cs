using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace OLinq
{
    public static class ExtensionMethods
    {
        public static void CopyTo(this object source, object destination)
        {
            if (source != null && destination != null)
            {
                BindingFlags flags = BindingFlags.Instance | BindingFlags.Public;
                var properties = source?.GetType().GetProperties(flags).Where(x => x.CanRead && x.CanWrite);
                foreach (var item in properties)
                {
                    try
                    {
                        destination.GetType().GetProperty(item.Name).SetValue(destination, item.GetValue(source, null), null);
                    }
                    catch (ArgumentException ex)
                    {

                        Trace.TraceError(ex.Message);
                    }
                }
            }
        }
    }
}
