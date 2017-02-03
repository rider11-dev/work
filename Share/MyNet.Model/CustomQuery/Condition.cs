using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Model.CustomQuery
{
    /// <summary>
    /// 查询条件
    /// </summary>
    public class Condition
    {
        /// <summary>
        /// 如,ag.gp_id
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// 组合类型
        /// </summary>
        public CompositeType CmpType { get; set; }
        public ConditionType ConditionType { get; set; }
        public FieldType FieldType { get; set; }
        public bool Not { get; set; }
        /// <summary>
        /// 值字符串（json），边界（Between）比较时，Value为BoundaryValue类型（JObject）；范围（In）比较时，Value为数组（JArray）
        /// </summary>
        public dynamic Value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isFirst">是否第一个查询条件，如果true，则忽略CompositeType</param>
        /// <returns></returns>
        public string Parse(bool isFirst = false)
        {
            //条件模板：and/or ag.gp_name like '%asdfasd%'
            string sql = string.Format(" {0} {1} ", (isFirst || CmpType == CompositeType.None) ? "" : CmpType.ToString(), Field);
            switch (ConditionType)
            {
                //字符串
                case ConditionType.Contain:
                    sql += string.Format("{0} like '%{1}%'", Not ? "not" : "", (string)Value);
                    break;
                case ConditionType.StartWith:
                    sql += string.Format("{0} like '{1}%'", Not ? "not" : "", (string)Value);
                    break;
                case ConditionType.EndWith:
                    sql += string.Format("{0} like '%{1}'", Not ? "not" : "", (string)Value);
                    break;
                case ConditionType.GreaterThan:
                    sql += ParseNumberAndDateTime(ConditionType.GreaterThan);
                    break;
                case ConditionType.GreaterOrEqual:
                    sql += ParseNumberAndDateTime(ConditionType.GreaterOrEqual);
                    break;
                case ConditionType.LessThan:
                    sql += ParseNumberAndDateTime(ConditionType.LessThan);
                    break;
                case ConditionType.LessOrEqual:
                    sql += ParseNumberAndDateTime(ConditionType.LessOrEqual);
                    break;
                case ConditionType.Equal:
                    sql += string.Format("{0} {1}", Not ? "<>" : "=", FieldType == FieldType.Number ? (string)Value : ("'" + (string)Value + "'"));
                    break;
                case ConditionType.In:
                    sql += ParseIn();
                    break;
                case ConditionType.IsEmpty:
                    sql += string.Format("is {0} null", Not ? "not" : "");
                    break;
                case ConditionType.Between:
                    sql += ParseBetween();
                    break;
            }

            return sql;
        }
        /// <summary>
        /// 针对：GreaterThan、GreaterOrEqual、LessThan、LessOrEqual
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string ParseNumberAndDateTime(ConditionType type)
        {
            string opt = string.Empty;
            switch (type)
            {
                case ConditionType.GreaterThan:
                    opt = Not ? "<=" : ">";
                    break;
                case ConditionType.GreaterOrEqual:
                    opt = Not ? "<" : ">=";
                    break;
                case ConditionType.LessThan:
                    opt = Not ? ">=" : "<";
                    break;
                case ConditionType.LessOrEqual:
                    opt = Not ? ">" : "<=";
                    break;
            }
            string sql = "";
            string val = (string)Value;
            switch (FieldType)
            {
                case FieldType.String:
                case FieldType.Date:
                case FieldType.Time:
                case FieldType.Boolean://布尔类型的value，取"1"或"0"
                    sql = string.Format("{0} '{1}'", opt, val);
                    break;
                case FieldType.Number:
                    sql = string.Format("{0} {1}", opt, val);
                    break;
            }
            return sql;
        }

        private string ParseIn()
        {
            var valList = JsonConvert.DeserializeObject<IEnumerable<string>>((Value as JArray).ToString());
            if (FieldType == FieldType.Number)
            {
                return string.Format("{0} in ({1})", Not ? "not" : "", string.Join(",", valList));
            }
            else
            {
                return string.Format("{0} in ('{1}')", Not ? "not" : "", string.Join("','", valList));
            }
        }

        private string ParseBetween()
        {
            var val = JsonConvert.DeserializeObject<BoundaryValue>((Value as JObject).ToString());
            if (FieldType == FieldType.Number)
            {
                return string.Format("{0} between {1} and {2}", Not ? "not" : "", val.Min, val.Max);
            }
            else
            {
                return string.Format("{0} between '{1}' and '{2}'", Not ? "not" : "", val.Min, val.Max);
            }
        }
    }

    /// <summary>
    /// 查询条件类型
    /// </summary>
    public enum ConditionType
    {
        [Description("包含")]
        Contain,
        [Description("开头是")]
        StartWith,
        [Description("结尾是")]
        EndWith,
        [Description("大于")]
        GreaterThan,
        [Description("大于等于")]
        GreaterOrEqual,
        [Description("小于")]
        LessThan,
        [Description("小于等于")]
        LessOrEqual,
        [Description("等于")]
        Equal,
        [Description("范围")]
        In,
        [Description("为空")]
        IsEmpty,
        [Description("边界")]
        Between
    }
    /// <summary>
    /// 查询条件组合类型
    /// </summary>
    public enum CompositeType
    {
        [Description("并且")]
        And,
        [Description("或者")]
        Or,
        [Description("无")]
        None
    }
}
