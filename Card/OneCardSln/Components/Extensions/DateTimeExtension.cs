using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Components.Extensions
{
    public class DateTimeExtension
    {
        public static DateTime GetMinTime(DateTimeKind dtKind)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, dtKind);
        }

        public static DateTime GetMinUtcTime()
        {
            return GetMinTime(DateTimeKind.Utc);
        }
    }
}
