using MyNet.Model.Card;
using MyNet.Service.Card;
using MyNet.WebApi.Filters;
using MyNet.WebApi.Models.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MyNet.WebApi.Extensions;
using MyNet.Components.Mapper;
using MyNet.Components.Result;
using MyNet.Model;

namespace MyNet.WebApi.Controllers.Card
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
                rst = OptResult.Build(ResultCode.ParamError, "参数不能为空");
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

        [HttpPost]
        [Route("makeup")]
        public OptResult Makeup(MakeupCardViewModel vmMakeup)
        {
            OptResult rst = null;
            if (vmMakeup == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, "参数不能为空");
                return rst;
            }
            if (ModelState.IsValid == false)
            {
                rst = OptResult.Build(ResultCode.ParamError, ModelState.Parse());
                return rst;
            }

            var token = base.ParseToken(ActionContext);

            rst = _cardSrv.Makeup(vmMakeup.idcard, vmMakeup.number, token.iss);

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

            var token = base.ParseToken(ActionContext);

            rst = _cardSrv.ChangePhone(vmChangePhone.idcard, vmChangePhone.phone, token.iss);

            return rst;
        }

        [HttpPost]
        [Route("closedown")]
        public OptResult CloseDown(EditByIdcardsViewModel vm)
        {
            OptResult rst = null;
            if (vm == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, "参数不能为空");
                return rst;
            }
            if (ModelState.IsValid == false)
            {
                rst = OptResult.Build(ResultCode.ParamError, ModelState.Parse());
                return rst;
            }

            var token = base.ParseToken(ActionContext);

            rst = _cardSrv.CloseDownBatch(vm.idcards, token.iss);

            return rst;
        }

        [HttpPost]
        [Route("reportloss")]
        public OptResult ReportLoss(EditByIdcardViewModel vm)
        {
            OptResult rst = null;
            if (vm == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, "参数不能为空");
                return rst;
            }
            if (ModelState.IsValid == false)
            {
                rst = OptResult.Build(ResultCode.ParamError, ModelState.Parse());
                return rst;
            }

            var token = base.ParseToken(ActionContext);

            rst = _cardSrv.ReportLoss(vm.idcard, token.iss);

            return rst;
        }

        [HttpPost]
        [Route("recover")]
        public OptResult Recover(EditByIdcardsViewModel vm)
        {
            OptResult rst = null;
            if (vm == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, "参数不能为空");
                return rst;
            }
            if (ModelState.IsValid == false)
            {
                rst = OptResult.Build(ResultCode.ParamError, ModelState.Parse());
                return rst;
            }

            var token = base.ParseToken(ActionContext);

            rst = _cardSrv.RecoverBatch(vm.idcards, token.iss);

            return rst;
        }

        [HttpGet]
        [Route("getstates")]
        public OptResult GetCardStates()
        {
            return _cardSrv.GetCardStates();
        }

        [HttpGet]
        [Route("getopts")]
        public OptResult GetCardOpts()
        {
            return _cardSrv.GetCardOperations();
        }

        [HttpPost]
        [Route("getpage")]
        public OptResult GetCardInfoByPage(PageQuery page)
        {
            OptResult rst = null;

            rst = _cardSrv.GetCardInfoByPage(page);

            return rst;
        }
    }
}