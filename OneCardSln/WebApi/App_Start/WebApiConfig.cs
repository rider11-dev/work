using OneCardSln.WebApi.Extensions;
using OneCardSln.WebApi.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace OneCardSln.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //filters
            config.Filters.Add(new CustomExceptionFilterAttribute());

            //干掉xml序列化器
            var json = config.Formatters.JsonFormatter;
            //解决json序列化时的循环引用问题
            json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //添加自定义日志组件
            config.Services.Clear(typeof(IExceptionLogger));
            config.Services.Add(typeof(IExceptionLogger), new CustomExceptionLogger());

            //自定义消息处理程序
            config.MessageHandlers.Add(new CustomMessageHandler());
        }
    }
}
