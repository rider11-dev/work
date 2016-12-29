using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders;
using System.Web.Http.ValueProviders.Providers;

namespace MyNet.WebHostService.Extension
{
    public class StaticValueProviderFactory : ValueProviderFactory
    {
        private static List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>();

        public override IValueProvider GetValueProvider(HttpActionContext actionContext)
        {
            return new NameValuePairsValueProvider(values, CultureInfo.InvariantCulture);
        }

        public static void Clear()
        {
            values.Clear();
        }

        public static void Add(string key, string value)
        {
            values.Add(new KeyValuePair<string, string>(key, value));
        }
    }
}
