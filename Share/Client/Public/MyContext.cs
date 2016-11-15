using MyNet.Components.Extensions;
using MyNet.Components.Logger;
using MyNet.Components.WPF.Models;
using MyNet.Model.Auth;
using MyNet.Client.Models;
using MyNet.Client.Models.Auth;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;
using MyNet.Components;
using MyNet.Client.Command;

namespace MyNet.Client.Public
{
    public class MyContext
    {
        public static string Token { get; set; }
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

        static string _sysName;
        public static string SysName
        {
            get
            {
                if (string.IsNullOrEmpty(_sysName))
                {
                    _sysName = AppSettingHelper.Get("sysname");
                }
                return _sysName;
            }
        }

        static MyContext()
        {

        }

    }
}
