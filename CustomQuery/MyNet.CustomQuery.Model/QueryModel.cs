using MyNet.Model;
using MyNet.Repository.Db;
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
    }
}
