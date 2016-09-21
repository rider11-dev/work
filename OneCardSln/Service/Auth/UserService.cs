using OneCardSln.Model;
using OneCardSln.Repository.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions;
using OneCardSln.Model.Auth;
using OneCardSln.Components;

namespace OneCardSln.Service.Auth
{
    public class UserService
    {
        //常量
        const string Msg_Login = "用户登录";
        //私有变量
        private UserRepository _usrRep;

        public UserService(UserRepository usrRep)
        {
            _usrRep = usrRep;
        }

        public OptResult Login(string username, string pwd)
        {
            OptResult rst = new OptResult();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(pwd))
            {
                rst.code = ResultCode.ParamError;
                rst.msg = Msg_Login + "失败,用户名或密码不能为空";
                return rst;
            }
            //用户名
            var where = Predicates.Field<User>(u => u.user_name, Operator.Eq, username);
            var usr = _usrRep.GetList(where).FirstOrDefault();
            if (usr == null)
            {
                rst.code = ResultCode.Fail;
                rst.msg = Msg_Login + "失败,用户不存在";
                return rst;
            }
            //密码
            var pwdHash = EncryptionHelper.GetMd5Hash(pwd);
            if (string.Equals(pwdHash, usr.user_pwd))
            {
                rst.code = ResultCode.Success;
                rst.data = usr;
            }
            else
            {
                rst.code = ResultCode.Fail;
                rst.msg = Msg_Login + "失败,密码不正确";
            }

            return rst;
        }
    }
}
