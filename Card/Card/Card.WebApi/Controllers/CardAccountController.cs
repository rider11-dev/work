using Card.Service;
using Card.ViewModel;
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

namespace Card.WebApi
{
    [RoutePrefix("api/card/account")]
    //[TokenValidateFilter]
    [ValidateModelFilter]
    public class CardAccountController : BaseController
    {
        CardAccountService _acntSrv;
        CardInfoService _cardSrv;
        public CardAccountController(CardAccountService acntSrv, CardInfoService cardSrv)
        {
            _acntSrv = acntSrv;
            _cardSrv = cardSrv;
        }

        [HttpPost]
        [Route("get")]
        public OptResult GetAccount(GetByIdcardVM vm)
        {
            OptResult rst = null;
            if (vm == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, "参数不能为空");
                return rst;
            }
            rst = _acntSrv.GetAccountByIdcard(vm.idcard);
            return rst;
        }

        [HttpPost]
        [Route("cards")]
        public OptResult GetCards(GetByIdcardVM vm)
        {
            OptResult rst = null;
            if (vm == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, "参数不能为空");
                return rst;
            }
            rst = _cardSrv.QueryByPage(new PageQuery { pageIndex = 1, pageSize = 20, conditions = new Dictionary<string, object> { { "idcard", vm.idcard } } });
            return rst;
        }
    }
}
