using Autofac;
using Autofac.Integration.WebApi;
using MyNet.Repository.Auth;
using MyNet.Repository.Base;
using MyNet.Repository.Card;
using MyNet.Repository.Db;
using MyNet.Service.Auth;
using MyNet.Service.Base;
using MyNet.Service.Card;
using MyNet.WebApi.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using MyNet.Components.Extensions;
using System.Web.Http.Dispatcher;
using MyNet.WebApi.Extensions;

namespace MyNet.WebApi
{
    public class Bootstrapper
    {
        public static void Init()
        {
            SetAutofacWebApi();

            InitDapper();
        }

        static void SetAutofacWebApi()
        {
            var builder = new ContainerBuilder();
            RegisterModules(builder);

            var container = builder.Build();
            var config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        }

        static void RegisterModules(ContainerBuilder builder)
        {
            //1、注册当前应用程序域中指定程序集的类型
            var assDomain = System.AppDomain.CurrentDomain.GetAssemblies();
            builder.RegisterType<DbSession>().As<IDbSession>().InstancePerRequest();//DbSession
            builder.RegisterApiControllers(assDomain).PropertiesAutowired(PropertyWiringOptions.None);//ApiController
            builder.RegisterAssemblyTypes(assDomain)
                .Where(t => t.Name.EndsWith("Repository"));//Repository
            builder.RegisterAssemblyTypes(assDomain)
                .Where(t => t.Name.EndsWith("Service"));//Service
        }

        static void InitDapper()
        {
            DapperExtensions.DapperExtensions.SqlDialect = DbUtils.GetSqlDialect();
            DapperExtensions.DapperExtensions.SetMappingAssemblies(
                System.AppDomain.CurrentDomain.GetAssemblies()
                .Where(ass => ass.FullName.Contains("Repository") || ass.FullName.Contains("Service"))
                .ToList()
                );
        }
    }
}