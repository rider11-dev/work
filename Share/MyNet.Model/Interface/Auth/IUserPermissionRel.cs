using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Model.Interface.Auth
{
    /// <summary>
    /// 权限控制——用户权限关联
    /// </summary>
    public interface IUserPermissionRel
    {
        string rel_id { get; set; }
        string rel_userid { get; set; }
        string rel_permissionid { get; set; }
    }
}
