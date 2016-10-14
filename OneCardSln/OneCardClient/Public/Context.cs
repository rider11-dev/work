using OneCardSln.Components.Extensions;
using OneCardSln.Components.Logger;
using OneCardSln.Model.Auth;
using OneCardSln.OneCardClient.Models;
using OneCardSln.OneCardClient.Models.Auth;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OneCardSln.OneCardClient.Public
{
    public class Context
    {
        static string _tokenTest = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJhZG1pbiIsImlhdCI6MTQ3NjE2NTg2NywidXNlciI6bnVsbH0.vGbRZRUXqWMzHwapI1-C6iMu1d8KEGUte28ZgdVcqjw";
        static string _token;
        public static string Token
        {
            get
            {
#if DEBUG
                return _tokenTest;
#endif
                return _token;
            }
            set
            {
                _token = value;
            }
        }
        public static UserViewModel CurrentUser { get; set; }
        public static string ServerRoot
        {
            get
            {
                return ConfigurationManager.AppSettings["srvroot"];
            }
        }
        public static string BaseDirectory
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        public static IEnumerable<Permission> Pers { get; set; }
        static Context()
        {

        }

    }
}
