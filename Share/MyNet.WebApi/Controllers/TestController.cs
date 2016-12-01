using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class TestController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        public string Get()
        {
            return "hello";
        }
    }
}