using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.CustomQuery.Model
{
    public class Table
    {
        public Table()
        {
            fields = new List<Field>();
        }

        public string id { get; set; }
        /// <summary>
        /// 不可修改
        /// </summary>
        public string tbname { get; set; }
        public string alias { get; set; }
        public string comment { get; set; }
        public IList<Field> fields { get; set; }
    }
}
