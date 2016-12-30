using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Components.Extensions
{
    public static class TypeExtension
    {
        public static bool IsA<TParent>(this Type type)
        {
            var parentType = typeof(TParent);
            if (parentType.IsInterface)
            {
                return type.GetInterface(parentType.Name) != null;
            }
            return type.IsAssignableFrom(parentType);
        }
    }
}
