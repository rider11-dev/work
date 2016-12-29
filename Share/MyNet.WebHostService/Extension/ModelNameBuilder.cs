using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.WebHostService.Extension
{
    public static class ModelNameBuilder
    {
        public static string CreatePropertyModelName(string prefix, string propertyName)
        {
            if (string.IsNullOrEmpty(prefix))
            {
                return (propertyName ?? string.Empty);
            }
            if (!string.IsNullOrEmpty(propertyName))
            {
                return (prefix + "." + propertyName);
            }
            return prefix ?? string.Empty;
        }
    }
}
