using MyNet.Model;
using MyNet.Service.Auth;
using MyNet.WebApi.Filters;
using MyNet.ViewModel.Auth.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MyNet.WebApi.Extensions;
using MyNet.Components.Mapper;
using MyNet.Model.Auth;
using MyNet.WebApi.Models;
using MyNet.Components.Result;

namespace MyNet.WebApi.Controllers.Auth
{
    [RoutePrefix("api/auth/permission")]
    [TokenValidateFilter]
    [ValidateModelFilter]
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
        public OptResult Add(PermDetailVM vmAddPer)
        {
            OptResult rst = null;
            if (vmAddPer == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, "参数不能为空");
                return rst;
            }

            //
            var token = base.ParseToken(ActionContext);
            var per = OOMapper.Map<PermDetailVM, Permission>(vmAddPer);
            rst = _perSrv.Add(per);

            return rst;
        }

        [HttpPost]
        [Route("update")]
        public OptResult Update(EditPermissionViewModel vmEditPer)
        {
            OptResult rst = null;

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

            var token = base.ParseToken(ActionContext);

            rst = _perSrv.Delete(vmDel.pk);

            return rst;
        }

        [HttpPost]
        [Route("multidelete")]
        public OptResult Delete(DelByIdsViewModel vmDels)
        {
            OptResult rst = null;

            var token = base.ParseToken(ActionContext);

            rst = _perSrv.DeleteBatch(vmDels.pks);

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

        
        [Route("getallfuncs")]
        public OptResult GetAllFuncs()
        {
            OptResult rst = null;
            rst = _perSrv.GetAllFuncs();

            return rst;
        }
        
        [Route("getallopts")]
        public OptResult GetAllOpts()
        {
            OptResult rst = null;
            rst = _perSrv.GetAllOpts();

            return rst;
        }
    }
}