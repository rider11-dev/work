using MyNet.Model.Interface.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Model.Auth
{
    public class Group : IGroup
    {
        public string gp_id { get; set; }
        public string gp_code { get; set; }
        public string gp_name { get; set; }
        public bool gp_system { get; set; }
        /// <summary>
        /// 上级组织编号
        /// </summary>
        public string gp_parent { get; set; }

        public string gp_sort { get; set; }
    }
}
