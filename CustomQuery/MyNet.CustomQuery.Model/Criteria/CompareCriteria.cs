using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.CustomQuery.Model.Criteria
{
    /// <summary>
    /// 简单比较查询条件
    /// </summary>
    public class CompareCriteria : BaseCriteria
    {
        public string ParamName { get; set; }
        public Relation Relation { get; set; }

        public override string Parse()
        {
            string sqlCriteria = string.Empty;
            switch (Relation)
            {
                case Relation.Contain:
                case Relation.StartWith:
                case Relation.EndWith:
                    sqlCriteria += string.Format("{0} {1} like {2} ", FieldName, IsNot ? "not" : "", ParamName);
                    break;
                case Relation.GreaterThan:
                    sqlCriteria += string.Format("{0} {1} {2} ", FieldName, IsNot ? "<=" : ">", ParamName);
                    break;
                case Relation.GreaterOrEqual:
                    sqlCriteria += string.Format("{0} {1} {2} ", FieldName, IsNot ? "<" : ">=", ParamName);
                    break;
                case Relation.LessThan:
                    sqlCriteria += string.Format("{0} {1} {2} ", FieldName, IsNot ? ">=" : "<", ParamName);
                    break;
                case Relation.LessOrEqual:
                    sqlCriteria += string.Format("{0} {1} {2} ", FieldName, IsNot ? ">" : "<=", ParamName);
                    break;
                case Relation.Equal:
                    sqlCriteria += string.Format("{0} {1} {2} ", FieldName, IsNot ? "<>" : "=", ParamName);
                    break;
                case Relation.In:
                    sqlCriteria += string.Format("{0} {1} in {2}", FieldName, IsNot ? "not" : "", ParamName);
                    break;
                case Relation.IsEmpty:
                    sqlCriteria += string.Format("{0} is {1} null ", FieldName, IsNot ? "not" : "");
                    break;
                default:
                    break;
            }
            return sqlCriteria;
        }
    }
}
