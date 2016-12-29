using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Model.Interface.Auth
{
    /// <summary>
    /// 权限控制——权限列表
    /// </summary>
    public interface IPermission
    {
        string per_id { get; set; }
        string per_code { get; set; }
        string per_name { get; set; }
        string per_type { get; set; }

        PermType PermType { get; }
        string per_uri { get; set; }
        string per_method { get; set; }
        /// <summary>
        /// 上级权限编号
        /// </summary>
        string per_parent { get; set; }
        string per_sort { get; set; }
        bool per_system { get; set; }
        string per_remark { get; set; }
        string per_icon { get; set; }
        string per_halign { get; set; }

    }
}
