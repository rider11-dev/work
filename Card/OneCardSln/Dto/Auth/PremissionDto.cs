using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Dto.Auth
{
    public class PremissionDto
    {
        public string per_id { get; set; }
        public string per_code { get; set; }
        public string per_name { get; set; }
        public string per_type { get; set; }
        public string per_uri { get; set; }
        public string per_method { get; set; }
        public string per_type_name { get; set; }
        /// <summary>
        /// 上级权限编号
        /// </summary>
        public string per_parent { get; set; }
        public string per_sort { get; set; }
    }
}
