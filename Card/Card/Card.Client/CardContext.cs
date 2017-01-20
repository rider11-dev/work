using MyNet.Components.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Card.Client
{
    public class CardContext
    {
        static CardContext()
        {
            LoadClientConf();
        }

        public static CardConf Conf { get; private set; }
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
        private static void LoadClientConf()
        {
            var confFile = BaseDirectory + "/cardconf.json";
            if (!File.Exists(confFile))
            {
                Conf = new CardConf();
                return;
            }
            var data = File.ReadAllText(confFile);
            Conf = JsonConvert.DeserializeObject<CardConf>(data);
        }
    }
}
