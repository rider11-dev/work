using Autofac;
using DapperExtensions;
using DapperExtensions.Mapper;
using DapperExtensions.Sql;
using MyNet.Components.Logger;
using MyNet.Components.Misc;
using MyNet.Repository.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Repository
{
    public class RepositoryInit : Iinit
    {
        ILogHelper<RepositoryInit> _logHelper = LogHelperFactory.GetLogHelper<RepositoryInit>();
        public void Init(ContainerBuilder builder)
        {
            //_logHelper.LogInfo("RepositoryInit.Init");
            builder.RegisterType<DbSession>().As<IDbSession>().InstancePerRequest();//DbSession

            DapperExtensions.DapperExtensions.SqlDialect = DbUtils.GetSqlDialect();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(ass => ass.FullName.Contains("Repository") || ass.FullName.Contains("Service"))
                .ToList();
            DapperExtensions.DapperExtensions.SetMappingAssemblies(assemblies);
            DbUtils.SqlGenerator = new SqlGeneratorImpl(
                new DapperExtensionsConfiguration(
                    typeof(AutoClassMapper<>),
                    assemblies,
                    DapperExtensions.DapperExtensions.SqlDialect));
        }
    }
}
