using OneCardSln.Components.Extensions;
using OneCardSln.Components.Logger;
using OneCardSln.Model;
using OneCardSln.Service.Auth;
using OneCardSln.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Autofac;
using System.Web.Http;
using OneCardSln.Components.Result;

namespace OneCardSln.WebApi.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class TokenValidateFilterAttribute : ActionFilterAttribute
    {
        public bool Ignore { get; private set; }

        public UserService UserSrv;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ignore">是否忽略token验证，默认false</param>
        public TokenValidateFilterAttribute(bool ignore = false)
            : base()
        {
            Ignore = ignore;
        }

        ILogHelper<TokenValidateFilterAttribute> _logHelper = LogHelperFactory.GetLogHelper<TokenValidateFilterAttribute>();
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (Ignore)
            {
                return;
            }
            var rst = ValidateToken(actionContext);
            if (rst.code != ResultCode.Success)
            {
                actionContext.Response = actionContext.Request.CreateResponse<OptResult>(rst);
            }

            base.OnActionExecuting(actionContext);
        }

        OptResult ValidateToken(HttpActionContext actionContext)
        {
            OptResult rst = null;
            var tokenHeader = actionContext.Request.Headers.Where(kvp => kvp.Key == "token").FirstOrDefault();
            if (string.IsNullOrEmpty(tokenHeader.Key) || tokenHeader.Value == null || tokenHeader.Value.Count() < 1)
            {
                rst = OptResult.Build(ResultCode.Tokenless);
                return rst;
            }
            var tokenString = tokenHeader.Value.First();
            if (string.IsNullOrEmpty(tokenString))
            {
                rst = OptResult.Build(ResultCode.Tokenless);
                return rst;
            }

            try
            {
                var tokenObj = JWT.JsonWebToken.DecodeToObject<TokenData>(tokenString, ApiContext.JwtSecretKey);
                int expires = ApiContext.TokenExpire * 60;//失效时间
                if ((DateTime.UtcNow - DateTimeExtension.GetMinUtcTime()).TotalSeconds - tokenObj.iat > expires)
                {
                    rst = OptResult.Build(ResultCode.TokenExpired);
                    return rst;
                }
                //TODO
                //////这里应该校验一下token所指用户是否还存在，并从数据库获取token指定用户详细信息；暂未实现
                ////rst = _usrSrv.Find(tokenObj.iss);
                ////if (rst.code != ResultCode.Success)
                ////{
                ////    return rst;
                ////}
                //var usr = rst.data;

                //token校验成功后，把token信息写入HttpActionContext
                actionContext.ActionArguments.Add("token", tokenObj);
            }
            catch (Exception ex)
            {
                //记录日志——异步
                Task.Run(() =>
                {
                    _logHelper.LogInfo(string.Format("{0}token验证失败：{0}\ttoken：{1}{0}\texception：{2}", Environment.NewLine, tokenString, ex.ToString()));
                });

                rst = OptResult.Build(ResultCode.TokenIllegal);
                return rst;
            }

            rst = OptResult.Build(ResultCode.Success);

            return rst;
        }
    }
}