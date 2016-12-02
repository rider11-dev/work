using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MyNet.WebApi.Test
{
    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {
        public TestController()
        {

        }

        [HttpGet]
        [Route("hello")]
        public dynamic SayHello()
        {
            //throw new Exception("异常测试");
            return new { say = "hello,this is a test api!" };
        }
    }
}
