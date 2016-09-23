using OneCardSln.Components.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OneCardSln.Repository.Db
{
    public class SqlTextProvider
    {
        static XDocument SqlConfigure = null;

        static SqlTextProvider()
        {
            string sqlFile = GetSqlFile();
            SqlConfigure = XDocument.Load(sqlFile);
            if (SqlConfigure == null)
            {
                throw new Exception("加载sql配置文件失败！");
            }
        }

        static string GetSqlFile()
        {
            string filePrefix = "sql.config.";
            string fileName = string.Empty;
            var dbType = DbUtils.GetDbTypeByConnKey();
            switch (dbType)
            {
                case DatabaseType.MySql:
                    fileName = filePrefix + "mysql";
                    break;
                case DatabaseType.Sqlite:
                    fileName = filePrefix + "sqlite";
                    break;
                case DatabaseType.SqlServer:
                default:
                    fileName = filePrefix + "sqlserver";
                    break;
            }
            string fileFullPath = "";
            bool find = FileExtension.GetFileFullPath(AppDomain.CurrentDomain.BaseDirectory, fileName, out fileFullPath);
            if (find == false)
            {
                throw new FileNotFoundException("未找到sql配置文件：", fileName);
            }
            return fileFullPath;
        }

        public static string GetSql(SqlConfEntity conf)
        {
            if (!conf.Check())
            {
                return string.Empty;
            }

            var sqlNode = SqlConfigure
                .Descendants("sqlarea").Where(e => e.Attribute("name").Value == conf.area)
                .Descendants("sqlgroup").Where(e => e.Attribute("name").Value == conf.group)
                .Descendants("sql").Where(e => e.Attribute("name").Value == conf.name)
                .FirstOrDefault();
            if (sqlNode == null)
            {
                return string.Empty;
            }

            return sqlNode.Value.Replace("\r\n", "").Replace(" ", "").Replace("\t", "");
        }
    }
}
