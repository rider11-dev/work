using Autofac;
using Autofac.Integration.WebApi;
using OneCardSln.Repository.Auth;
using OneCardSln.Repository.Base;
using OneCardSln.Repository.Card;
using OneCardSln.Repository.Db;
using OneCardSln.Service.Auth;
using OneCardSln.Service.Base;
using OneCardSln.Service.Card;
using OneCardSln.WebApi.Filters;
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
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired(PropertyWiringOptions.None);
        }

        static void RegisterBussinessModules(ContainerBuilder builder)
        {
            builder.RegisterType<DbSession>().As<IDbSession>().InstancePerRequest();
            //
            builder.RegisterType(typeof(DictTypeRepository)).InstancePerRequest();
            builder.RegisterType(typeof(DictTypeService)).InstancePerRequest();
            //
            builder.RegisterType(typeof(DictRepository)).InstancePerRequest();
            builder.RegisterType(typeof(DictService)).InstancePerRequest();

            //
            builder.RegisterType(typeof(UserRepository)).InstancePerRequest();
            builder.RegisterType(typeof(UserService)).InstancePerRequest();
            //
            builder.RegisterType(typeof(PermissionRepository)).InstancePerRequest();
            builder.RegisterType(typeof(PermissionService)).InstancePerRequest();
            //
            builder.RegisterType(typeof(UserPermissionRelRepository)).InstancePerRequest();
            builder.RegisterType(typeof(UserPermissionRelService)).InstancePerRequest();
            //
            builder.RegisterType(typeof(CardInfoRepository)).InstancePerRequest();
            builder.RegisterType(typeof(CardInfoService)).InstancePerRequest();
            //
            builder.RegisterType(typeof(CardRecordRepository)).InstancePerRequest();
            //
            builder.RegisterType(typeof(MallAccountService)).InstancePerRequest();
            //
            builder.RegisterType(typeof(CardRecordRepository)).InstancePerRequest();
            //
            builder.RegisterType(typeof(CardBillRepository)).InstancePerRequest();
            //
            builder.RegisterType(typeof(CardMoneyService)).InstancePerRequest();
            //
            builder.RegisterType(typeof(CardRecordService)).InstancePerRequest();
            //
            builder.RegisterType(typeof(CardBillService)).InstancePerRequest();

        }

        static void InitDapper()
        {
            DapperExtensions.DapperExtensions.SqlDialect = DbUtils.GetSqlDialect();
            DapperExtensions.DapperExtensions.SetMappingAssemblies(new[] { Assembly.Load("OneCardSln.Repository"), Assembly.Load("OneCardSln.Service") });
        }
    }
}