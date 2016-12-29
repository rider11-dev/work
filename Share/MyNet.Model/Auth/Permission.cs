using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNet.Components.Extensions;
using MyNet.Model.Interface.Auth;

namespace MyNet.Model.Auth
{
    /// <summary>
    /// 权限控制——权限列表
    /// </summary>
    public class Permission : IPermission
    {
        public string per_id { get; set; }
        public string per_code { get; set; }
        public string per_name { get; set; }
        public string per_type { get; set; }

        public PermType PermType
        {
            get
            {
                return per_type.ToEnum<PermType>();
            }
        }
        public string per_uri { get; set; }
        public string per_method { get; set; }
        /// <summary>
        /// 上级权限编号
        /// </summary>
        public string per_parent { get; set; }
        public string per_sort { get; set; }
        public bool per_system { get; set; }
        public string per_remark { get; set; }
        public string per_icon { get; set; }
        public string per_halign { get; set; }
    }
}
