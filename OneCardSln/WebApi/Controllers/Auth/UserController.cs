using OneCardSln.Components.Extensions;
using OneCardSln.Components.Mapper;
using OneCardSln.Model;
using OneCardSln.Model.Auth;
using OneCardSln.Service.Auth;
using OneCardSln.WebApi.Extensions;
using OneCardSln.WebApi.Filters;
using OneCardSln.WebApi.Models;
using OneCardSln.WebApi.Models.Auth.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace OneCardSln.WebApi.Controllers.Auth
{
    [RoutePrefix("api/auth/user")]
    [TokenValidateFilter]
    public class UserController : BaseController
    {

        //私有变量
        UserService _usrSrv;
        UserPermissionRelService _usrPerRelSrv;

        public UserController(UserService usrSrv, UserPermissionRelService usrPerRelSrv)
        {
            _usrSrv = usrSrv;
            _usrPerRelSrv = usrPerRelSrv;
        }

        [HttpPost]
        [Route("login")]
        [TokenValidateFilter(true)]
        public OptResult Login(LoginViewModel vmLogin)
        {
            OptResult rst = null;
            if (!ModelState.IsValid)
            {
                rst = OptResult.Build(ResultCode.ParamError, ModelState.Parse());
                return rst;
            }

            rst = _usrSrv.Login(vmLogin.username, vmLogin.pwd);

            if (rst.code == ResultCode.Success)
            {
                //生成JWT
                var payload = new TokenData
                {
                    iss = rst.data.user_id,
                    iat = (int)(DateTime.UtcNow - DateTimeExtension.GetMinUtcTime()).TotalSeconds
                };

                string token = JWT.JsonWebToken.Encode(payload, ApiContext.JwtSecretKey, JWT.JwtHashAlgorithm.HS256);
                rst = OptResult.Build(ResultCode.Success, "用户登录成功，并已生成token", new { token = token });
            }

            return rst;
        }

        [HttpPost]
        [Route("add")]
        public OptResult Add(AddUserViewModel vmUsr)
        {
            OptResult rst = null;
            if (!ModelState.IsValid)
            {
                rst = OptResult.Build(ResultCode.ParamError, ModelState.Parse());
                return rst;
            }

            //
            var token = base.ParseToken(ActionContext);

            var usr = OOMapper.Map<AddUserViewModel, User>(vmUsr);
            usr.user_creator = token.iss;
            rst = _usrSrv.Add(usr);
            return rst;
        }

        [HttpPost]
        [Route("update")]
        public OptResult Update(EditUserViewModel vmUsr)
        {
            OptResult rst = null;
            if (!ModelState.IsValid)
            {
                rst = OptResult.Build(ResultCode.ParamError, ModelState.Parse());
                return rst;
            }

            var token = base.ParseToken(ActionContext);
            var usr = OOMapper.Map<EditUserViewModel, User>(vmUsr);

            rst = _usrSrv.Update(usr);

            return rst;
        }

        [HttpPost]
        [Route("delete")]
        public OptResult Delete(DelByPkViewModel vmDel)
        {
            OptResult rst = null;
            if (!ModelState.IsValid)
            {
                rst = OptResult.Build(ResultCode.ParamError, ModelState.Parse());
                return rst;
            }

            var token = base.ParseToken(ActionContext);

            rst = _usrSrv.Delete(vmDel.pk);

            return rst;
        }

        [HttpPost]
        [Route("changepwd")]
        public OptResult ChangePwd(ChangePwdViewModel vmChangePwd)
        {
            OptResult rst = null;
            if (!ModelState.IsValid)
            {
                rst = OptResult.Build(ResultCode.ParamError, ModelState.Parse());
                return rst;
            }
            rst = _usrSrv.ChangePwd(vmChangePwd.userid, vmChangePwd.oldpwd, vmChangePwd.newpwd);

            return rst;
        }

        [HttpPost]
        [Route("querybypage")]
        public OptResult QueryByPage(PageQuery page)
        {
            OptResult rst = null;

            rst = _usrSrv.QueryByPage(page);

            return rst;
        }

        [HttpPost]
        [Route("assign")]
        public OptResult AssignPermissions(AssignPermissionViewModel vmAssignPer)
        {
            OptResult rst = null;
            if (vmAssignPer == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, "参数不能为空或格式不正确");
                return rst;
            }
            if (!ModelState.IsValid)
            {
                rst = OptResult.Build(ResultCode.ParamError, ModelState.Parse());
                return rst;
            }

            //
            rst = _usrPerRelSrv.AssignPermissions(vmAssignPer.userId, vmAssignPer.perIds, vmAssignPer.assignAll);

            return rst;
        }

        [HttpPost]
        [Route("getper")]
        public OptResult GetPermissions(GetByIdViewModel vmGetByPk)
        {
            OptResult rst = null;
            if (vmGetByPk == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, "参数不能为空或格式不正确");
                return rst;
            }
            if (!ModelState.IsValid)
            {
                rst = OptResult.Build(ResultCode.ParamError, ModelState.Parse());
                return rst;
            }
            rst = _usrPerRelSrv.GetPermissions(vmGetByPk.pk);

            return rst;
        }
    }
}