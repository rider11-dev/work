using OneCardSln.Components.Result;
using OneCardSln.Model;
using OneCardSln.Service.Card;
using OneCardSln.WebApi.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace OneCardSln.WebApi.Controllers.Card
{
    [RoutePrefix("api/card/bill")]
    [TokenValidateFilter]
    public class CardBillController : BaseController
    {
        private CardBillService _cardBillSrv;

        public CardBillController(CardBillService cardBillSrv)
        {
            _cardBillSrv = cardBillSrv;
        }

        [HttpPost]
        [Route("getpage")]
        public OptResult QueryByPage(PageQuery page)
        {
            OptResult rst = null;

            rst = _cardBillSrv.GetBillsByPage(page);

            return rst;
        }
    }
}