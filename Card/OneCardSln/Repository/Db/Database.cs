using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Repository.Db
{
    /// <summary>
    /// 数据库类对象
    /// </summary>
    public class Database : IDatabase
    {
        public IDbConnection Connection { get; private set; }

        public DatabaseType DbType { get; private set; }

        public string ConnKey { get; set; }

        public Database(string connKey = DbUtils.DefaultConnectionKey)
        {
            this.ConnKey = connKey;
            this.DbType = DbUtils.GetDbTypeByConnKey(connKey);
            this.Connection = DbUtils.CreateDbConnection(connKey);
        }
    }

    /// <summary>
    /// 数据库接口
    /// </summary>
    public interface IDatabase
    {
        IDbConnection Connection { get; }

        DatabaseType DbType { get; }

        string ConnKey { get; }
    }
}
