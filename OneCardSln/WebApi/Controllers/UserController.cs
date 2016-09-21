using OneCardSln.Model;
using OneCardSln.Service.Auth;
using OneCardSln.WebApi.Extensions;
using OneCardSln.WebApi.Filters;
using OneCardSln.WebApi.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace OneCardSln.WebApi.Controllers
{
    [CustomExceptionFilter]
    public class UserController : ApiController
    {
        //常量
        const string Msg_Login = "用户登录";

        //私有变量
        UserService _usrSrv;

        public UserController(UserService usrSrv)
        {
            _usrSrv = usrSrv;
        }

        [HttpPost]
        public async Task<OptResult> Login()
        {
            var vmLogin = await HttpUtils.ParseRequest<LoginViewModel>(this.Request);
            OptResult rst = new OptResult { code = ResultCode.Unknown };
            if (vmLogin == null || string.IsNullOrEmpty(vmLogin.username) || string.IsNullOrEmpty(vmLogin.pwd))
            {
                rst.code = ResultCode.ParamError;
                rst.msg = Msg_Login + "失败，用户名或密码不能为空";
                return rst;
            }
            rst = _usrSrv.Login(vmLogin.username, vmLogin.pwd);
            return rst;
        }
    }
}