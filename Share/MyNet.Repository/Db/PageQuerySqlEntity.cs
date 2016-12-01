using MyNet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Repository.Db
{
    /// <summary>
    /// 分页存储过程sql配置实体
    /// </summary>
    public class PageQuerySqlEntity
    {
        public string sp_name { get; set; }
        public string fields { get; set; }
        public string tables { get; set; }
        public StringBuilder where { get; set; }
        public StringBuilder order { get; set; }
    }
}
