using MyNet.Components.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyNet.Repository.Db
{
    public class SqlProvider
    {
        static Dictionary<string, XDocument> SqlCfgs = new Dictionary<string, XDocument>();

        static SqlProvider()
        {
            LoadSqlCfgs();
        }

        private static void LoadSqlCfgs()
        {
            //1、构造sql配置文件正则表达式
            string searchPattern = "^sql.*.";
            string dbString = DbUtils.GetDbTypeByConnKey().ToString();
            searchPattern += dbString + "$";

            //2、加载sql配置文件
            try
            {
                var files = FileExtension.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "/bin", searchPattern);
                if (files != null && files.Count > 0)
                {
                    files.ForEach(f =>
                    {
                        var key = f.Name.Replace("sql.", "").Replace("." + dbString, "");
                        if (!SqlCfgs.ContainsKey(key))
                        {
                            SqlCfgs.Add(key, XDocument.Load(f.FullName));
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("加载sql配置文件失败！", ex);
            }
        }

        public static string GetTxtSql(SqlConfEntity conf)
        {
            if (!conf.Check())
            {
                return string.Empty;
            }

            var sqlNode = GetSqlNode(conf);
            if (sqlNode == null)
            {
                return string.Empty;
            }

            return sqlNode.Value.Replace("\r\n", " ").Replace("\n", " ").Replace("\t", " ").Trim();
        }

        public static PageQuerySqlEntity GetPageQuerySql(SqlConfEntity conf)
        {
            PageQuerySqlEntity entity = null;
            if (!conf.Check())
            {
                return entity;
            }
            var sqlNode = GetSqlNode(conf);
            if (sqlNode == null)
            {
                return entity;
            }
            entity = new PageQuerySqlEntity
            {
                sp_name = sqlNode.Attribute("sp_name").Value,
                fields = sqlNode.Attribute("fields").Value,
                tables = sqlNode.Attribute("tables").Value,
                where = new StringBuilder(sqlNode.Attribute("where").Value),
                order = new StringBuilder(sqlNode.Attribute("order").Value),
            };
            return entity;
        }

        static XElement GetSqlNode(SqlConfEntity conf)
        {
            var sqlNode = SqlCfgs[conf.area]
                .Descendants("sqlarea").Where(e => e.Attribute("name").Value == conf.area)
                .Descendants("sqlgroup").Where(e => e.Attribute("name").Value == conf.group)
                .Descendants("sql").Where(e => e.Attribute("name").Value == conf.name)
                .FirstOrDefault();
            return sqlNode;
        }
    }
}
