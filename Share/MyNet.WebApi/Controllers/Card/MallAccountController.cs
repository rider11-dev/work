using MyNet.Model;
using MyNet.Service.Card;
using MyNet.WebApi.Filters;
using MyNet.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MyNet.WebApi.Extensions;
using MyNet.ViewModel.Card;
using MyNet.Components.Result;

namespace MyNet.WebApi.Controllers.Card
{
    /// <summary>
    /// 商城账户控制器
    /// </summary>
    [RoutePrefix("api/card/mall")]
    [TokenValidateFilter]
    public class MallAccountController : BaseController
    {
        MallAccountService _srv;
        public MallAccountController(MallAccountService srv)
        {
            _srv = srv;
        }

        [HttpPost]
        [Route("get")]
        public OptResult GetAccount(GetByIdViewModel vmGetById)
        {
            OptResult rst = null;
            if (vmGetById == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, "参数不能为空");
                return rst;
            }
            if (ModelState.IsValid == false)
            {
                rst = OptResult.Build(ResultCode.ParamError, ModelState.Parse());
                return rst;
            }

            rst = _srv.GetAccount(vmGetById.pk);

            return rst;
        }

        [HttpPost]
        [Route("create")]
        public OptResult CreateAccount(MallAccountService.CreateMallAccount entity)
        {
            OptResult rst = null;
            if (entity == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, "参数不能为空");
                return rst;
            }
            rst = _srv.CreateAccount(entity);

            return rst;
        }

        [HttpPost]
        [Route("closedown")]
        public OptResult CloseDownAccount(GetByIdViewModel vmGetById)
        {
            OptResult rst = null;
            if (vmGetById == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, "参数不能为空");
                return rst;
            }
            if (ModelState.IsValid == false)
            {
                rst = OptResult.Build(ResultCode.ParamError, ModelState.Parse());
                return rst;
            }

            rst = _srv.CloseDownAccount(vmGetById.pk);

            return rst;
        }

        [HttpPost]
        [Route("changephone")]
        public OptResult ChangePhone(ChangePhoneViewModel vmChangePhone)
        {
            OptResult rst = null;
            if (vmChangePhone == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, "参数不能为空");
                return rst;
            }
            if (ModelState.IsValid == false)
            {
                rst = OptResult.Build(ResultCode.ParamError, ModelState.Parse());
                return rst;
            }

            rst = _srv.ChangePhone(vmChangePhone.idcard, vmChangePhone.phone);

            return rst;
        }
    }
}