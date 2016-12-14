using MyNet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.CustomQuery.Model
{
    public class QueryModel
    {
        public IList<string> Fields { get; set; }
        public TableRelation TableRelation { get; set; }
        public IList<Condition> Conditions { get; set; }
        public IList<Sort> Sorts { get; set; }
        public PageQuery Page { get; set; }

        public QueryModel()
        {
            Fields = new List<string>();
            TableRelation = new TableRelation();
            Conditions = new List<Condition>();
            Sorts = new List<Sort>();
            Page = new PageQuery { pageIndex = 1, pageSize = 20 };
        }
    }
}
