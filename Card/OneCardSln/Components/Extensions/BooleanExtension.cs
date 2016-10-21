using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyNet.Components.Extensions
{
    public static class BooleanExtension
    {
        public static bool ToBool(this object obj)
        {
            bool rst = false;
            if (obj == null)
            {
                return rst;
            }
            rst = obj.ToString().ToBool();
            return rst;
        }

        public static bool ToBool(this string txt)
        {
            bool rst = false;
            Boolean.TryParse(txt, out rst);
            return rst;
        }
    }
}
