using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.Model.Auth
{
    /// <summary>
    /// 权限控制——用户权限关联
    /// </summary>
    public class UserPermissionRel
    {
        public string rel_id { get; set; }
        public string rel_userid { get; set; }
        public string rel_permissionid { get; set; }
    }
}
