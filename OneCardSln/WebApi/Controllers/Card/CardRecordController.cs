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
    [RoutePrefix("api/card/record")]
    [TokenValidateFilter]
    public class CardRecordController : BaseController
    {
        private CardRecordService _cardRecSrv;

        public CardRecordController(CardRecordService cardRecSrv)
        {
            _cardRecSrv = cardRecSrv;
        }

        [HttpPost]
        [Route("getpage")]
        public OptResult QueryByPage(PageQuery page)
        {
            OptResult rst = null;

            rst = _cardRecSrv.GetRecordsByPage(page);

            return rst;
        }
    }
}