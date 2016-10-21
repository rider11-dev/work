using MyNet.Model;
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
using MyNet.Service.Card.Models;
using MyNet.Components.Result;
using MyNet.Model.Card;

namespace MyNet.WebApi.Controllers.Card
{
    [RoutePrefix("api/card/money")]
    [TokenValidateFilter]
    public class CardMoneyController : BaseController
    {
        private CardMoneyService _cardMoneySrv;

        public CardMoneyController(CardMoneyService cardMoneySrv)
        {
            _cardMoneySrv = cardMoneySrv;
        }


        [HttpPost]
        [Route("setgov")]
        public OptResult SetGovMoney(SetMoneyViewModel vmSetMoney)
        {
            return SetMoneyCore(vmSetMoney, MoneyEnum.gov);
        }

        [HttpPost]
        [Route("setmy")]
        public OptResult SetMyMoney(SetMoneyViewModel vmSetMoney)
        {
            return SetMoneyCore(vmSetMoney, MoneyEnum.my);
        }

        [HttpPost]
        [Route("pay")]
        public OptResult Pay(PayViewModel vmPay)
        {
            OptResult rst = null;
            if (vmPay == null)
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

            if (string.IsNullOrEmpty(vmPay.opt))
            {
                vmPay.opt = token.iss;
            }

            var payEntity = OOMapper.Map<PayViewModel, PayEntity>(vmPay);
            rst = _cardMoneySrv.Pay(payEntity);

            return rst;
        }

        [HttpPost]
        [Route("refund")]
        public OptResult Refund(RefundViewModel vmRefund)
        {
            OptResult rst = null;
            if (vmRefund == null)
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

            if (string.IsNullOrEmpty(vmRefund.opt))
            {
                vmRefund.opt = token.iss;
            }

            var refundEntity = OOMapper.Map<RefundViewModel, RefundEntity>(vmRefund);

            rst = _cardMoneySrv.Refound(refundEntity);

            return rst;
        }

        //私有方法
        private OptResult SetMoneyCore(SetMoneyViewModel vmSetMoney, MoneyEnum moneyType)
        {
            OptResult rst = null;
            if (vmSetMoney == null)
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

            rst = _cardMoneySrv.SetMoneyBatch(vmSetMoney.idcards, vmSetMoney.money, moneyType, token.iss);

            return rst;
        }
    }
}