using MyNet.Client.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.CustomQuery.Client
{
    public class CustomQueryOptDesc
    {
        public const string GetDbTables = "获取数据库表信息";
        public const string GetDbFields = "获取数据库字段信息";
        public const string InitTables = "初始化查询表信息";
        public const string InitFields = "初始化查询字段信息";

        public const string AddJoinTable = "添加关联表";
        public const string DelJoinTable = "删除关联表";

        public const string AddRelField = "添加关联字段";
        public const string DelRelField = "删除关联字段";

    }
}
