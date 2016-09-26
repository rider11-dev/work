using OneCardSln.Model;
using OneCardSln.Service.Card;
using OneCardSln.WebApi.Filters;
using OneCardSln.WebApi.Models.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using OneCardSln.WebApi.Extensions;
using OneCardSln.Components.Mapper;

namespace OneCardSln.WebApi.Controllers.Card
{
    [RoutePrefix("api/card/info")]
    [TokenValidateFilter]
    public class CardInfoController : BaseController
    {
        private CardInfoService _cardSrv;

        public CardInfoController(CardInfoService cardSrv)
        {
            _cardSrv = cardSrv;
        }

        [HttpPost]
        [Route("register")]
        public OptResult Register(RegisterCardViewModel vmCard)
        {
            OptResult rst = null;
            if (vmCard == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, "一卡通参数不能为空");
                return rst;
            }
            if (ModelState.IsValid == false)
            {
                rst = OptResult.Build(ResultCode.ParamError, ModelState.Parse());
                return rst;
            }

            var token = base.ParseToken(ActionContext);

            var card = OOMapper.Map<RegisterCardViewModel, CardInfo>(vmCard);
            card.card_creator = token.iss;
            card.card_createtime = DateTime.Now;

            rst = _cardSrv.Register(card);

            return rst;

        }
    }
}