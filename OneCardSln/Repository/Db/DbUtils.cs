using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
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
                case "Oracle.DataAccess.Client":
                    dbType = DatabaseType.Oracle;
                    break;
                case "MySql.Data.MySqlClient":
                    dbType = DatabaseType.MySql;
                    break;
                case "System.Data.OleDb":
                    dbType = DatabaseType.Aceess;
                    break;
                default:
                    throw new Exception(strKey + "连接字符串未识别 ProviderName:" + connectionStringSettings.ProviderName);
            }

            return dbType;
        }

        static DbProviderFactory _dbProviderFactory = null;
        public static IDbConnection CreateDbConnection(string strKey = DbUtils.DefaultConnectionKey)
        {
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings[strKey];
            if (connectionStringSettings == null)
            {
                throw new Exception(string.Format("未找到name为{0}的连接字符串！", strKey));
            }

            //获取指定数据库提供器工厂
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
            //获取数据库连接
            try
            {
                IDbConnection conn = _dbProviderFactory.CreateConnection();
                conn.ConnectionString = connectionStringSettings.ConnectionString;
                return conn;
            }
            catch (Exception ex)
            {
                throw new Exception("获取数据库连接失败！", ex);
            }
        }
    }
}
