using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.CustomQuery.Model.Criteria
{
    public abstract class BaseCriteria : ICriteria
    {
        public string FieldName { get; set; }
        public bool IsNot { get; set; }
        public abstract string Parse();
    }
}
