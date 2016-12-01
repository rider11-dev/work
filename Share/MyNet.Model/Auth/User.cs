using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Model.Auth
{
    /// <summary>
    /// 权限控制——用户
    /// </summary>
    public class User
    {
        public string user_id { get; set; }
        public string user_name { get; set; }
        public string user_pwd { get; set; }
        public string user_idcard { get; set; }
        public string user_truename { get; set; }
        public string user_regioncode { get; set; }
        public string user_group { get; set; }
        public string user_remark { get; set; }
        public string user_creator { get; set; }
    }
}
