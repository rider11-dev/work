using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.CustomQuery.Model.Criteria
{
    /// <summary>
    /// 范围查询条件
    /// </summary>
    public class BetweenCriteria : BaseCriteria
    {
        public string ParamNameMin { get; set; }
        public string ParamNameMax { get; set; }
        public override string Parse()
        {
            return string.Format("{0} {1} between {2} and {3}", FieldName, IsNot ? "not" : "", ParamNameMin, ParamNameMax);
        }
    }
}
