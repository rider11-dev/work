using OneCardSln.Components;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.Repository.Db
{
    public class DbConfigure
    {
        const string Key_ConnectionStrings = "dbconn";

        static ConnectionStringSettings _connectionStringSettings = null;

        static DbConfigure()
        {
            //初始化
        }

        public static string ConnectString
        {
            get
            {
                return ConnectionStrings.ConnectionString;
            }
        }

        public static string Provider
        {
            get
            {
                return ConnectionStrings.ProviderName;
            }
        }

        public static ConnectionStringSettings ConnectionStrings
        {
            get
            {
                if (_connectionStringSettings == null)
                {
                    _connectionStringSettings = ConfigurationManager.ConnectionStrings[Key_ConnectionStrings];
                }
                return _connectionStringSettings;
            }
        }
    }
}
