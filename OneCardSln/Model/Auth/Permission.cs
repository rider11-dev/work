using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneCardSln.Components.Extensions;

namespace OneCardSln.Model.Auth
{
    /// <summary>
    /// 权限控制——权限列表
    /// </summary>
    public class Permission
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
        public string per_parent { get; set; }
        public string per_sort { get; set; }
        public bool per_system { get; set; }
        public string per_remark { get; set; }
    }
}
