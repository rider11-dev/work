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
using System.Reflection;
using System.IO;
using Newtonsoft.Json;
using MyNet.ViewModel.Auth.User;

namespace MyNet.Client.Public
{
    public class ClientContext
    {
        public static string Token { get; set; }
        public static UserDetailVM CurrentUser { get; set; }
        /// <summary>
        /// dll所在目录
        /// </summary>
        public static string BaseDirectory
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetAssemblyDirectory();
            }
        }

        public static string SysName { get { return Conf.sysname; } }

        public static IEnumerable<Permission> Pers { get; set; }

        public static ClientConf Conf { get; private set; }

        static ClientContext()
        {
            LoadClientConf();
        }

        private static void LoadClientConf()
        {
            var confFile = BaseDirectory + "/clientconf.json";
            if (!File.Exists(confFile))
            {
                Conf = new ClientConf();
                return;
            }
            var data = File.ReadAllText(confFile);
            Conf = JsonConvert.DeserializeObject<ClientConf>(data);
        }

        public static string GetFullPath(string filename)
        {
            return BaseDirectory + "/" + filename;
        }
    }

    public struct ClientConf
    {
        public string footer { get; set; }
        public string header { get; set; }
        public string icon { get; set; }
        public string maincontent { get; set; }
        public string mainpage { get; set; }
        public string srvroot { get; set; }
        public string sysname { get; set; }
        public string title { get; set; }
    }
}
