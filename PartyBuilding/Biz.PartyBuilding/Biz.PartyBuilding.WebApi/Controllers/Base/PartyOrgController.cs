using Biz.PartyBuilding.Service;
using MyNet.Components.Result;
using MyNet.Model;
using MyNet.WebApi.Controllers;
using MyNet.WebApi.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

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
    }
}
