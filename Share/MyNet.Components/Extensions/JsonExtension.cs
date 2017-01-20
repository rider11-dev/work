using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Components.Extensions
{
    public class JsonExtension
    {
        public static readonly JsonSerializerSettings JsonSerializerDefaultSettings = new JsonSerializerSettings
        {
            DateTimeZoneHandling = DateTimeZoneHandling.Local,
            DateFormatString = "yyyy-MM-dd HH:mm:ss",
            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
        };
    }
}
