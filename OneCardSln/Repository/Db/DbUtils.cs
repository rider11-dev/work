using DapperExtensions.Sql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.Repository.Db
{
    public class DbUtils
    {
        public const string DefaultConnectionKey = "default";
        public static DatabaseType GetDbTypeByConnKey(string strKey = DefaultConnectionKey)
        {
            DatabaseType dbType = DatabaseType.SqlServer;
            //1、获取数据库连接配置集合
            if (ConfigurationManager.ConnectionStrings == null || ConfigurationManager.ConnectionStrings.Count < 1)
            {
                throw new Exception("未找到数据库连接配置！");
            }
            //2、获取指定key的数据库连接配置
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings[strKey];
            if (connectionStringSettings == null)
            {
                throw new Exception(string.Format("未找到name为{0}的连接字符串！", strKey));
            }

            if (string.IsNullOrEmpty(connectionStringSettings.ProviderName))
            {
                throw new Exception(strKey + "连接字符串未定义 ProviderName");
            }

            switch (connectionStringSettings.ProviderName)
            {
                case "System.Data.SqlClient":
                    dbType = DatabaseType.SqlServer;
                    break;
                //case "Oracle.DataAccess.Client":
                //    dbType = DatabaseType.Oracle;
                //    break;
                case "MySql.Data.MySqlClient":
                    dbType = DatabaseType.MySql;
                    break;
                case "System.Data.SQLite":
                    dbType = DatabaseType.Sqlite;
                    break;
                //case "System.Data.OleDb":
                //    dbType = DatabaseType.Aceess;
                //    break;
                default:
                    throw new Exception(strKey + "连接字符串未识别 ProviderName:" + connectionStringSettings.ProviderName);
            }

            return dbType;
        }

        static DbProviderFactory _dbProviderFactory = null;
        public static IDbConnection CreateDbConnection(string strKey = DbUtils.DefaultConnectionKey)
        {
            //1、获取数据库连接配置集合
            if (ConfigurationManager.ConnectionStrings == null || ConfigurationManager.ConnectionStrings.Count < 1)
            {
                throw new Exception("未找到数据库连接配置！");
            }

            //2、获取指定key的数据库连接配置
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings[strKey];
            if (connectionStringSettings == null)
            {
                throw new Exception(string.Format("未找到name为{0}的连接字符串！", strKey));
            }

            //3、获取指定数据库提供器工厂
            if (_dbProviderFactory == null)
            {
                try
                {
                    _dbProviderFactory = DbProviderFactories.GetFactory(connectionStringSettings.ProviderName);
                }
                catch (Exception ex)
                {
                    throw new Exception("无法创建指定类型的DbProviderFactory实例", ex);
                }
                if (_dbProviderFactory == null)
                {
                    throw new Exception("无法创建指定类型的DbProviderFactory实例");
                }
            }
            //4、创建数据库连接
            try
            {
                IDbConnection conn = _dbProviderFactory.CreateConnection();
                conn.ConnectionString = connectionStringSettings.ConnectionString;
                return conn;
            }
            catch (Exception ex)
            {
                throw new Exception("创建数据库连接失败！", ex);
            }
        }

        public static ISqlDialect GetSqlDialect()
        {
            var dbType = GetDbTypeByConnKey();
            switch (dbType)
            {
                case DatabaseType.MySql:
                    return new MySqlDialect();
                case DatabaseType.Sqlite:
                    return new SqliteDialect();
                case DatabaseType.SqlServer:
                default:
                    return new SqlServerDialect();
            }
        }

    }
}
