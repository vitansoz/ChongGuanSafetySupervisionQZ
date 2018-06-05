using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanDotNetUtils.Helpers
{
    public class ReflectionHelper
    {
        public static void CopyProperties<T>(T src, T dest, IEnumerable<string> exception)
        {
            Type t = typeof(T);
            var properties = t.GetProperties();
            foreach (var p in properties)
            {
                if (exception != null && exception.Contains(p.Name, StringComparer.InvariantCultureIgnoreCase))
                {
                    continue;
                }
                p.SetValue(dest, p.GetValue(src));
            }
        }
    }
}
