using MyNet.WebApi.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Routing;

namespace MyNet.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //替换IAssembliesResolver，加载扩展程序集
            var config = GlobalConfiguration.Configuration;
            config.Services.Replace(typeof(IAssembliesResolver), new ExtendedAssembliesResolver());

            Bootstrapper.Init();

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
