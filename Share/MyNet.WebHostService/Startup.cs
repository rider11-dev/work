using Autofac;
using Autofac.Integration.WebApi;
using MyNet.Components.Extensions;
using MyNet.Components.Logger;
using MyNet.Components.Misc;
using MyNet.WebHostService.Extension;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Owin;
using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Dispatcher;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;
using System.Web.Http.ValueProviders;

namespace MyNet.WebHostService
{
    public class Startup
    {
        ILogHelper<Startup> _logHelper = LogHelperFactory.GetLogHelper<Startup>();
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host.  
            HostContext.Configration = new HttpConfiguration();
            //1、加载所有扩展api相关dll
            HostContext.Configration.Services.Replace(typeof(IAssembliesResolver), new ExtendedAssembliesResolver());

            //2、自定义路由
            //defaults: new { opt = RouteParameter.Optional }
            HostContext.Configration.MapHttpAttributeRoutes();//映射RouteAttribute属性
            HostContext.Configration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );
            HostContext.Configration.Routes.MapHttpRoute(
                name: "ModuleApi",
                routeTemplate: "api/{module}/{controller}/{action}"
                );
            //3、序列化器
            //干掉xml序列化器
            var jsonFormatter = HostContext.Configration.Formatters.JsonFormatter;
            //解决json序列化时的循环引用问题
            jsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //json序列化日期格式
            jsonFormatter.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            HostContext.Configration.Formatters.Remove(HostContext.Configration.Formatters.XmlFormatter);
            //4、自定义消息处理程序
            HostContext.Configration.MessageHandlers.Add(new CustomMessageHandler());
            //5、容器注册
            IocRegister();
            //6、跨域
            HostContext.Configration.EnableCors(new EnableCorsAttribute("", "*", "*") { SupportsCredentials = true });

            appBuilder.UseWebApi(HostContext.Configration);
        }

        private void IocRegister()
        {
            var builder = new ContainerBuilder();
            //注册当前应用程序域中指定程序集的类型
            var assDomain = AppDomain.CurrentDomain.GetAssemblies();
            builder.RegisterApiControllers(assDomain.Where(ass => ass.FullName.Contains("WebApi")).ToArray())
                .PropertiesAutowired(PropertyWiringOptions.None);//ApiController
            builder.RegisterAssemblyTypes(assDomain)
                .Where(t => t.Name.EndsWith("Repository"));//Repository
            builder.RegisterAssemblyTypes(assDomain)
                .Where(t => t.Name.EndsWith("Service"));//Service

            Init(builder);

            var container = builder.Build();
            HostContext.Configration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private void Init(ContainerBuilder builder)
        {
            var allTypes = AppDomain.CurrentDomain.GetLoadedTypes();
            var iname = typeof(Iinit).Name;
            var initTypes = allTypes.Where(t => t.GetInterface(iname) != null);
            if (initTypes.IsNotEmpty())
            {
                //_logHelper.LogInfo(string.Join("\r\n", initTypes.Select(a => a.FullName)));
                initTypes.ToList().ForEach(t => (t.Assembly.CreateInstance(t.FullName, false) as Iinit).Init(builder));
            }

        }
    }
}
