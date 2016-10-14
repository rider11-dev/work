using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.OneCardClient.Models
{
    public struct ApiKeys
    {
        //登录、主界面初始化
        public const string Login = "login";
        public const string VerifyCode = "verifycode";
        public const string GetPer = "getper";
        public const string GetUsr = "getusr";

        /*-------------------------------------我的账户------------------------------*/
        //修改密码
        public const string ChangePwd = "changepwd";
        public const string EditUsr = "editusr";

        /*-------------------------------------权限管理——用户管理------------------------------*/
        public const string GetUsrByPage = "querybypage";


    }
}
