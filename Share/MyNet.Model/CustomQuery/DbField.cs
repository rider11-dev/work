using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Model.CustomQuery
{
    public class DbField
    {
        public string table_name { get; set; }
        public string column_name { get; set; }
        public string data_type { get; set; }
        public string field_type { get; set; }
        public string column_key { get; set; }
        public string column_comment { get; set; }
    }
}
