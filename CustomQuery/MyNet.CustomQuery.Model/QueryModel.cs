using MyNet.CustomQuery.Model.Criteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.CustomQuery.Model
{
    /// <summary>
    /// 通用查询模型
    /// </summary>
    public class QueryModel
    {
        public IList<ICriteria> Criterias { get; set; }

        public IList<Table> Tables { get; set; }

        public IList<Field> Fields { get; set; }
    }
}
