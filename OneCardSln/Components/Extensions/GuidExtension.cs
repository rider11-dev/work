using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.Components.Extensions
{
    public class GuidExtension
    {
        public static string GetOne()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
