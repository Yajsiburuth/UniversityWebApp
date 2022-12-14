using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Helper
{
    public static class SqlHelper
    {
        public static string GetColumnNames(this Type type, string alias = null, HashSet<string> excludedProp = null, bool parameter = false)
        {
            var props = type.GetProperties();
            var cols = new List<string>();

            foreach (var prop in props)
            {
                if (prop.CustomAttributes.Any(z => z.AttributeType == typeof(IgnoreMappingAttribute)) || (excludedProp?.Contains(prop.Name) ?? false))
                    continue;
                if (alias != null)
                    cols.Add($"{alias}.{prop.Name}");
                else if (parameter)
                    cols.Add($"@{prop.Name}");
                else
                    cols.Add($"{prop.Name}");
            }

            return string.Join(" ,", cols);
        }

        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
        public class IgnoreMappingAttribute : Attribute { }
    }
}
