using Newtonsoft.Json;
using OneCardSln.Components;
using OneCardSln.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.Service.Card
{
    /// <summary>
    /// 商城账户服务类
    /// </summary>
    public class MallAccountService : BaseService<dynamic>
    {
        const string Key_Api_GetAccountInfo = "getAccountInfo";
        const string Key_Api_CreateAccount = "createAccount";
        const string Key_Api_CloseDownAccount = "closeDownAccount";
        const string Key_Api_ChangePhone = "changePhone";

        const string Key_Provider = "mall";

        const string Msg_GetAccount = "获取商城账户信息";
        const string Msg_CreateAccount = "创建商城账户";
        const string Msg_CloseDownAccount = "封停商城账户";
        const string Msg_ChangePhone = "商城账户手机号变更";


        public MallAccountService()
            : base(null, null)
        {

        }

        /// <summary>
        /// 调用商城接口——根据身份证号获取商城账户信息
        /// </summary>
        /// <param name="idcard"></param>
        /// <returns></returns>
        public OptResult GetAccount(string idcard)
        {
            OptResult rst = null;
            string msg = string.Empty;
            var apiUrl = GetMallApiUrl(Key_Api_GetAccountInfo, ref msg);
            if (string.IsNullOrEmpty(apiUrl))
            {
                rst = OptResult.Build(ResultCode.Fail, Msg_GetAccount + "失败，" + msg);
                return rst;
            }
            //调用商城接口：查询账户信息
            try
            {
                var data = HttpHelper.Post(apiUrl, new { idcard = idcard });
                rst = JsonConvert.DeserializeObject<OptResult>(data);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_GetAccount, ex);
                rst = OptResult.Build(ResultCode.Fail, Msg_GetAccount + "失败，" + ex.Message);
            }

            return rst;
        }

        /// <summary>
        /// 调用商城接口——创建商城账户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public OptResult CreateAccount(CreateMallAccount entity)
        {
            OptResult rst = null;

            if (entity == null)
            {
                rst = OptResult.Build(ResultCode.Fail, Msg_CreateAccount + "失败，请求参数不能为空");
                return rst;
            }
            var msg = entity.Check();
            if (!string.IsNullOrEmpty(msg))
            {
                rst = OptResult.Build(ResultCode.Fail, Msg_CreateAccount + "失败，" + msg);
                return rst;
            }

            //调用商城接口：创建账户
            var apiUrl = GetMallApiUrl(Key_Api_CreateAccount, ref msg);
            if (string.IsNullOrEmpty(apiUrl))
            {
                rst = OptResult.Build(ResultCode.Fail, Msg_CreateAccount + "失败，" + msg);
                return rst;
            }
            try
            {
                var data = HttpHelper.Post(apiUrl, entity);
                rst = JsonConvert.DeserializeObject<OptResult>(data);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_CreateAccount, ex);
                rst = OptResult.Build(ResultCode.Fail, Msg_CreateAccount + "失败，" + ex.Message);
            }
            return rst;
        }

        /// <summary>
        /// 调用商城接口——封停指定身份证号的商城账户
        /// </summary>
        /// <param name="idcard"></param>
        /// <returns></returns>
        public OptResult CloseDownAccount(string idcard)
        {
            OptResult rst;

            string msg = string.Empty;
            var apiUrl = GetMallApiUrl(Key_Api_CloseDownAccount, ref msg);
            if (string.IsNullOrEmpty(apiUrl))
            {
                rst = OptResult.Build(ResultCode.Fail, Msg_CloseDownAccount + "失败，" + msg);
                return rst;
            }
            //调用商城接口：封停账户
            try
            {
                var data = HttpHelper.Post(apiUrl, new { idcard = idcard });
                rst = JsonConvert.DeserializeObject<OptResult>(data);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_CloseDownAccount, ex);
                rst = OptResult.Build(ResultCode.Fail, Msg_CloseDownAccount + "失败，" + ex.Message);
            }

            return rst;
        }

        /// <summary>
        /// 调用商城接口——修改手机号
        /// </summary>
        /// <param name="idcard"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public OptResult ChangePhone(string idcard, string phone)
        {
            OptResult rst;

            string msg = string.Empty;
            var apiUrl = GetMallApiUrl(Key_Api_ChangePhone, ref msg);
            if (string.IsNullOrEmpty(apiUrl))
            {
                rst = OptResult.Build(ResultCode.Fail, Msg_ChangePhone + "失败，" + msg);
                return rst;
            }
            //调用商城接口：修改手机号
            try
            {
                var data = HttpHelper.Post(apiUrl, new { idcard = idcard, phone = phone });
                rst = JsonConvert.DeserializeObject<OptResult>(data);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_ChangePhone, ex);
                rst = OptResult.Build(ResultCode.Fail, Msg_ChangePhone + "失败，" + ex.Message);
            }

            return rst;
        }

        string GetMallApiUrl(string apiName, ref string msg)
        {
            msg = string.Empty;
            var api = Context.Apis.Find(a =>
                string.Equals(a.Name, apiName, StringComparison.CurrentCultureIgnoreCase) &&
                string.Equals(a.Provider, Key_Provider, StringComparison.CurrentCultureIgnoreCase));
            if (api == null || string.IsNullOrEmpty(api.Url))
            {
                msg = "未找到api接口信息或接口url不存在，请检查配置是否正确";
                return string.Empty;
            }
            return api.Url;
        }

        public class CreateMallAccount
        {
            public string idcard { get; set; }
            public string username { get; set; }
            public string pwd { get; set; }

            public string Check()
            {
                if (string.IsNullOrEmpty(idcard))
                {
                    return "idcard不能为空";
                }
                if (string.IsNullOrEmpty(username))
                {
                    return "username不能为空";
                }
                if (string.IsNullOrEmpty(pwd))
                {
                    return "pwd不能为空";
                }

                return string.Empty;
            }
        }
    }
}
