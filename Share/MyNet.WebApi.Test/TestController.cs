using MyNet.Components.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MyNet.WebApi.Test
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {
        public TestController()
        {

        }

        [HttpGet]
        [Route("data")]
        public dynamic Get()
        {
            var file = Assembly.GetExecutingAssembly().GetAssemblyDirectory() + "/data.json";
            if (!File.Exists(file))
            {
                return "file not found:" + file;
            }
            var str = File.ReadAllText(file);
            try
            {
                return JsonConvert.DeserializeObject(str);
            }
            catch (Exception ex)
            {
                return "服务端json解析错误：" + ex.Message + ",data:" + str;
            }
        }
    }
}
