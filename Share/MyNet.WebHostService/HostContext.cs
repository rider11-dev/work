using MyNet.Components;
using MyNet.Components.Extensions;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace MyNet.WebHostService
{
    public class HostContext
    {
        public static string HostUrl
        {
            get
            {
                var url = AppSettingUtils.Get("host");
                return url;
            }
        }

        public static bool IsDebug
        {
            get
            {
                bool debug = false;
                bool.TryParse(AppSettingUtils.Get("debug"), out debug);
                return debug;
            }
        }

        public static HttpConfiguration Configration
        {
            get; set;
        }

        static string _defaultSvrName = "WebHostService";
        static string _svrName = "";
        public static string SvrName
        {
            get
            {
                if (_svrName.IsEmpty())
                {
                    var val = AppSettingUtils.Get("svrname");
                    _svrName = val.IsEmpty() ? _defaultSvrName : val;
                }

                return _svrName;
            }
        }
    }
}
