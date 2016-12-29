using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Model.Interface.Auth
{
    /// <summary>
    /// 权限控制——用户
    /// </summary>
    public interface IUser
    {
        string user_id { get; set; }
        string user_name { get; set; }
        string user_pwd { get; set; }
        string user_idcard { get; set; }
        string user_truename { get; set; }
        string user_regioncode { get; set; }
        string user_group { get; set; }
        string user_remark { get; set; }
        string user_creator { get; set; }
    }
}
