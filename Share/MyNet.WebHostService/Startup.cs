using Autofac;
using Autofac.Integration.WebApi;
using MyNet.Components.Logger;
using MyNet.Repository.Db;
using Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;

namespace MyNet.WebHostService
{
    public class Startup
    {
        static HttpConfiguration _httpConfig;
        static ILogHelper<Startup> _logHelper = LogHelperFactory.GetLogHelper<Startup>();
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host.  
            _httpConfig = new HttpConfiguration();
            //1、加载所有扩展api相关dll
            _httpConfig.Services.Replace(typeof(IAssembliesResolver), new ExtendedAssembliesResolver());
            //if (HostContext.IsDebug)
            //{
            //    //打印所有加载的dll
            //    var asses = AppDomain.CurrentDomain.GetAssemblies();
            //    StringBuilder sb = new StringBuilder();
            //    foreach (var ass in asses)
            //    {
            //        sb.Append(ass.FullName + Environment.NewLine);
            //    }
            //    _logHelper.LogInfo("当前加载dll：" + Environment.NewLine + sb.ToString());
            //}
            //2、自定义路由
            //defaults: new { opt = RouteParameter.Optional }
            _httpConfig.MapHttpAttributeRoutes();//映射RouteAttribute属性
            _httpConfig.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );
            _httpConfig.Routes.MapHttpRoute(
                name: "ModuleApi",
                routeTemplate: "api/{module}/{controller}/{action}"
                );
            //3、序列化器
            //干掉xml序列化器
            var jsonFormatter = _httpConfig.Formatters.JsonFormatter;
            //解决json序列化时的循环引用问题
            jsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            HostContext.CurrentMediaTypeFormatter = jsonFormatter;
            _httpConfig.Formatters.Remove(_httpConfig.Formatters.XmlFormatter);
            //4、自定义消息处理程序
            _httpConfig.MessageHandlers.Add(new CustomMessageHandler());
            //5、容器注册
            IocRegister();
            //6、初始化dapper
            InitDapper();

            appBuilder.UseWebApi(_httpConfig);
        }

        private void IocRegister()
        {
            var builder = new ContainerBuilder();
            //注册当前应用程序域中指定程序集的类型
            var assDomain = AppDomain.CurrentDomain.GetAssemblies();
            builder.RegisterType<DbSession>().As<IDbSession>().InstancePerRequest();//DbSession
            builder.RegisterApiControllers(assDomain.Where(ass => ass.FullName.Contains("WebApi")).ToArray())
                .PropertiesAutowired(PropertyWiringOptions.None);//ApiController
            builder.RegisterAssemblyTypes(assDomain)
                .Where(t => t.Name.EndsWith("Repository"));//Repository
            builder.RegisterAssemblyTypes(assDomain)
                .Where(t => t.Name.EndsWith("Service"));//Service

            var container = builder.Build();
            _httpConfig.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private void InitDapper()
        {
            DapperExtensions.DapperExtensions.SqlDialect = DbUtils.GetSqlDialect();
            DapperExtensions.DapperExtensions.SetMappingAssemblies(
                AppDomain.CurrentDomain.GetAssemblies()
                .Where(ass => ass.FullName.Contains("Repository") || ass.FullName.Contains("Service"))
                .ToList()
                );
        }
    }
}
