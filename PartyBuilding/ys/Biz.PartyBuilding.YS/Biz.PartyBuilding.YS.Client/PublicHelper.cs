using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.PartyBuilding.YS.Client
{
    public class PublicHelper
    {
        public static List<TReturn> GetDataFromJsonFile<TReturn>(string filename)
        {
            var dllPath = Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            var file = dllPath + "/data/" + filename + ".json";
            if (!File.Exists(file))
            {
                return null;
            }
            string data = File.ReadAllText(file);
            var objs = JsonConvert.DeserializeObject<List<TReturn>>(data);

            return objs;
        }
    }
}
