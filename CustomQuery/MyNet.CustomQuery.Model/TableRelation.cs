using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.CustomQuery.Model
{
    public class TableRelation
    {
        public string PrimeTable { get; set; }
        public IList<JoinTable> JoinTables { get; set; }
    }

    public class JoinTable
    {
        public string Table { get; set; }
        public TableJoinType JoinType { get; set; }
        /// <summary>
        /// 关联字段集合
        /// </summary>
        public IList<RelationField> RelFields { get; set; }
    }

    public enum TableJoinType
    {
        [Description("左连接")]
        Left,
        [Description("右连接")]
        Right,
        [Description("内连接")]
        Inner
    }

    public class RelationField
    {
        /// <summary>
        /// 关联字段1，如au.user_group
        /// </summary>
        public string Field1 { get; set; }
        /// <summary>
        /// 关联字段2，如ag.gp_id
        /// </summary>
        public string Field2 { get; set; }
    }
}
