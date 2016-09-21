using Autofac;
using Autofac.Integration.WebApi;
using OneCardSln.Repository.Auth;
using OneCardSln.Repository.Db;
using OneCardSln.Service.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace OneCardSln.WebApi
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

            RegisterControllers(builder);
            RegisterBussinessModules(builder);

            var config = GlobalConfiguration.Configuration;
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        static void RegisterControllers(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
        }

        static void RegisterBussinessModules(ContainerBuilder builder)
        {
            builder.RegisterType<DbSession>().As<IDbSession>().InstancePerRequest();

            builder.RegisterType(typeof(UserRepository)).InstancePerRequest();
            builder.RegisterType(typeof(UserService)).InstancePerRequest();
        }

        static void InitDapper()
        {
            DapperExtensions.DapperExtensions.SqlDialect = DbUtils.GetSqlDialect();
            DapperExtensions.DapperExtensions.SetMappingAssemblies(new[] { Assembly.Load("OneCardSln.Repository") });
        }
    }
}