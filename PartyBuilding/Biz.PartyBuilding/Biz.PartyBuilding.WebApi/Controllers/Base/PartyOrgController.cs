using Biz.PartyBuilding.Service;
using MyNet.Components.Result;
using MyNet.Model;
using MyNet.WebApi.Controllers;
using MyNet.WebApi.Filters;
using MyNet.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using MyNet.WebApi.Extensions;
using Biz.PartyBuilding.WebApi.Models.Base;
using MyNet.Components.Mapper;
using Biz.PartyBuilding.Model;

namespace Biz.PartyBuilding.WebApi.Controllers.Base
{
    [RoutePrefix("api/party/org")]
    [TokenValidateFilter]
    public class PartyOrgController : BaseController
    {
        PartyOrgService _poSrv;
        public PartyOrgController(PartyOrgService poSrv)
        {
            _poSrv = poSrv;
        }

        [HttpPost]
        [Route("pagequery")]
        public OptResult PageQuery(PageQuery page)
        {
            OptResult rst = null;

            rst = _poSrv.QueryByPage(page);

            return rst;
        }

        [HttpPost]
        [Route("get")]
        public OptResult GetById(GetByIdViewModel vm)
        {
            OptResult rst = null;
            if (vm == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, "参数不能为空或格式不正确");
                return rst;
            }
            if (!ModelState.IsValid)
            {
                rst = OptResult.Build(ResultCode.ParamError, ModelState.Parse());
                return rst;
            }
            rst = _poSrv.FindOrgByGroupId(vm.pk);

            return rst;
        }
        [HttpPost]
        [Route("save")]
        public OptResult Save(SavePartyOrgViewModel vmPartyOrg)
        {
            OptResult rst = null;
            if (vmPartyOrg == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, "参数不能为空或格式不正确");
                return rst;
            }
            if (!ModelState.IsValid)
            {
                rst = OptResult.Build(ResultCode.ParamError, ModelState.Parse());
                return rst;
            }
            var po = OOMapper.Map<SavePartyOrgViewModel, PartyOrg>(vmPartyOrg);

            rst = _poSrv.Save(po, vmPartyOrg.po_gp_id);

            return rst;
        }
    }
}
