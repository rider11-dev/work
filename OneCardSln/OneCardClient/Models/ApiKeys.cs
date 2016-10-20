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
        public const string GetUsr = "usr_get";

        /*-------------------------------------我的账户------------------------------*/
        //修改密码
        public const string ChangePwd = "changepwd";
        public const string EditUsr = "usr_edit";

        /*-------------------------------------权限管理——用户管理------------------------------*/
        public const string GetUsrByPage = "usr_pagequery";
        public const string AddUsr = "usr_add";
        public const string DeleteUsr = "usr_delete";
        public const string MultiDeleteUsr = "usr_multidelete";

        /*-------------------------------------权限管理——权限管理------------------------------*/
        public const string GetPerByPage = "per_pagequery";
        public const string AddPer = "per_add";
        public const string EditPer = "per_edit";
        public const string DeletePer = "per_delete";
        public const string MultiDeletePer = "per_multidelete";

    }
}
