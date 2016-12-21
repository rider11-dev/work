using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MyNet.WebApi.Controllers.Client
{
    [RoutePrefix("api/client")]
    public class ClientApiController : BaseController
    {
        private static IEnumerable<UpgradeItem> _items;
        private static string _clientConfFile;
        private static string _upgradeConfFile;
        public ClientApiController()
        {
            _clientConfFile = new Uri(this.GetType().Assembly.CodeBase).AbsolutePath;

            var jsonData = File.ReadAllText("");
            _items = JsonConvert.DeserializeObject<IEnumerable<UpgradeItem>>(jsonData);
        }

        [Route("upgrade")]
        public HttpResponseMessage GetUpdateFile()
        {
            return null;
        }
    }

    public class Client
    {
        public string Version { get; set; }
    }

    public class UpgradeItem
    {
        //相对路径
        public string path { get; set; }
        public ItemType type { get; set; }
        public OptType opt { get; set; }
    }

    public enum OptType
    {
        update,
        delete
    }

    public enum ItemType
    {
        file,
        folder
    }
}