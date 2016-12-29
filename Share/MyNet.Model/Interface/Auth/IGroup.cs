using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Model.Interface.Auth
{
    public interface IGroup
    {
        string gp_id { get; set; }
        string gp_code { get; set; }
        string gp_name { get; set; }
        bool gp_system { get; set; }
        /// <summary>
        /// 上级组织编号
        /// </summary>
        string gp_parent { get; set; }

        string gp_sort { get; set; }
    }
}
