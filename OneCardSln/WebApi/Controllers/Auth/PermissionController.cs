using OneCardSln.Model;
using OneCardSln.Service.Auth;
using OneCardSln.WebApi.Filters;
using OneCardSln.WebApi.Models.Auth.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using OneCardSln.WebApi.Extensions;
using OneCardSln.Components.Mapper;
using OneCardSln.Model.Auth;
using OneCardSln.WebApi.Models;

namespace OneCardSln.WebApi.Controllers.Auth
{
    [RoutePrefix("api/auth/permission")]
    [TokenValidateFilter]
    public class PermissionController : BaseController
    {
        //私有变量
        PermissionService _perSrv;

        public PermissionController(PermissionService perSrv)
        {
            _perSrv = perSrv;
        }

        [HttpPost]
        [Route("add")]
        public OptResult Add(AddPermissionViewModel vmAddPer)
        {
            OptResult rst = null;
            if (!ModelState.IsValid)
            {
                rst = OptResult.Build(ResultCode.ParamError, ModelState.Parse());
                return rst;
            }
            //
            var token = base.ParseToken(ActionContext);
            var per = OOMapper.Map<AddPermissionViewModel, Permission>(vmAddPer);
            rst = _perSrv.Add(per);

            return rst;
        }

        [HttpPost]
        [Route("update")]
        public OptResult Update(EditPermissionViewModel vmEditPer)
        {
            OptResult rst = null;
            if (!ModelState.IsValid)
            {
                rst = OptResult.Build(ResultCode.ParamError, ModelState.Parse());
                return rst;
            }
            //
            var token = base.ParseToken(ActionContext);

            var per = OOMapper.Map<EditPermissionViewModel, Permission>(vmEditPer);
            rst = _perSrv.Update(per);

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

            rst = _perSrv.Delete(vmDel.pk);

            return rst;
        }

        [HttpPost]
        [Route("querybypage")]
        public OptResult QueryByPage(PageQuery page)
        {
            OptResult rst = null;

            rst = _perSrv.QueryByPage(page);

            return rst;
        }

        [HttpGet]
        [Route("gettypes")]
        public OptResult GetPermTypes()
        {
            return _perSrv.GetPermTypes();
        }
    }
}