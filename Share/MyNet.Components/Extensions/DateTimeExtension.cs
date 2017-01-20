using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Components.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime GetMinTime(DateTimeKind dtKind)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, dtKind);
        }

        public static DateTime GetMinUtcTime()
        {
            return GetMinTime(DateTimeKind.Utc);
        }

        public static DateTime ParseUnixTimeStamp(double unixTimeStamp)
        {
            DateTime start = GetStart();
            DateTime dt = start.AddSeconds(unixTimeStamp);
            return dt;
        }

        public static double ToUnitTimeStamp(this DateTime dt)
        {
            DateTime start = GetStart();
            double timeStamp = (dt - start).TotalSeconds;
            return timeStamp;
        }

        private static DateTime GetStart()
        {
            return TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        }
    }
}
