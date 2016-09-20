using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneCardSln.Components.Extensions
{
    public static class NumberExtension
    {
        public static decimal ToDecimal(this object obj)
        {
            decimal rst = 0;
            if (obj == null)
            {
                return rst;
            }
            return obj.ToString().ToDecimal();
        }

        public static decimal ToDecimal(this string txt)
        {
            decimal rst = 0;
            Decimal.TryParse(txt, out rst);
            return rst;
        }

        public static int ToInt(this object obj)
        {
            int rst = 0;
            if (obj == null)
            {
                return rst;
            }
            return obj.ToString().ToInt();
        }

        public static int ToInt(this string txt)
        {
            int rst = 0;
            Int32.TryParse(txt, out rst);
            return rst;
        }
    }
}
