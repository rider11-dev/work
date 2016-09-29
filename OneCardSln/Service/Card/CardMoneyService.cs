using DapperExtensions;
using OneCardSln.Components.Extensions;
using OneCardSln.Model;
using OneCardSln.Repository.Card;
using OneCardSln.Repository.Db;
using OneCardSln.Service.Card.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.Service.Card
{
    /// <summary>
    /// 一卡通与钱相关的服务类
    /// </summary>
    public class CardMoneyService : CardInfoService
    {
        //常量

        public CardMoneyService(IDbSession session, CardInfoRepository cardInfoRep,
            CardRecordRepository cardRecordRep,
            CardBillRepository cardBillRep)
            : base(session, cardInfoRep, cardRecordRep, cardBillRep)
        {

        }

        //公共方法
        /// <summary>
        /// 批量设置一卡通账户金额
        /// </summary>
        /// <param name="idcards"></param>
        /// <param name="money"></param>
        /// <param name="moneyType"></param>
        /// <param name="opt"></param>
        /// <returns></returns>
        public OptResult SetMoneyBatch(IEnumerable<string> idcards, decimal money, MoneyEnum moneyType, string opt)
        {
            var desc = moneyType.GetDescription() + "(批量)";
            return base.BatchProcess(idcards, opt, desc,
                (param) =>
                {
                    var p = (SetMoneySingleProcessParam)param;
                    return SetMoneySingle(p);
                }, () => { return new SetMoneySingleProcessParam { money = money, moneyType = moneyType }; });

        }

        /// <summary>
        /// 设置单个一卡通账户的金额
        /// </summary>
        /// <param name="idcard"></param>
        /// <param name="money"></param>
        /// <param name="moneyType"></param>
        /// <param name="@operator"></param>
        /// <returns></returns>
        public OptResult SetMoneySingle(string idcard, decimal money, MoneyEnum moneyType, string opt)
        {
            OptResult rst = null;
            //1、账户是否存在
            var card = GetByIdcard(idcard);
            if (card == null)
            {
                var desc = moneyType.GetDescription() + "(单账户)";
                rst = OptResult.Build(ResultCode.DataNotFound, string.Format("{0}——指定身份证号{1}的账户不存在！", desc, idcard));
                return rst;
            }

            return SetMoneySingle(new SetMoneySingleProcessParam { card = card, money = money, moneyType = moneyType, opt = opt });
        }

        /// <summary>
        /// 付款
        /// </summary>
        /// <param name="pay"></param>
        /// <returns></returns>
        public OptResult Pay(PayEntity pay)
        {
            OptResult rst = null;

            var operation = CardOperation.Pay;
            var optDesc = operation.GetDescription();

            if (pay == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, optDesc + "——参数不能为空！");
                return rst;
            }
            string msg = string.Empty;
            if (!pay.Check(out msg))
            {
                rst = OptResult.Build(ResultCode.ParamError, string.Format("{0}——{1}！", optDesc, msg));
                return rst;
            }
            //1、账户是否存在
            var card = GetByIdcard(pay.idcard);
            if (card == null)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, string.Format("{0}——指定身份证号{1}的账户不存在！", optDesc, pay.idcard));
                return rst;
            }
            //2、如果一卡通号存在，看是否和身份证号对应
            if (!string.IsNullOrEmpty(pay.number) && !string.Equals(card.card_number, pay.number))
            {
                rst = OptResult.Build(ResultCode.ParamError, string.Format("{0}——身份证号{1}与一卡通号{2}不匹配！", optDesc, pay.idcard, pay.number));
                return rst;
            }
            //3、一卡通状态是否正常
            if (card.State != CardState.Normal)
            {
                rst = OptResult.Build(ResultCode.ParamError, string.Format("{0}——一卡通状态异常[{1}]！", optDesc, card.State.GetDescription()));
                return rst;
            }
            //4、余额是否充足
            if (pay.amount > card.card_govmoney + card.card_mymoney)
            {
                rst = OptResult.Build(ResultCode.ParamError, string.Format("{0}——余额不足！", optDesc));
                return rst;
            }
            //5、是否已经支付过（该一卡通和订单号下，存在"支付"流水或"退款"流水)
            var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<CardBill>(b => b.bill_idcard, Operator.Eq, card.card_idcard));
            pg.Predicates.Add(Predicates.Field<CardBill>(b => b.bill_order, Operator.Eq, pay.order));
            pg.Predicates.Add(Predicates.Field<CardBill>(b => b.bill_type, Operator.Eq, new string[] { CardOperation.Pay.ToString(), CardOperation.Refund.ToString() }));
            var count = _cardBillRep.Count(pg);
            if (count > 0)
            {
                //已付款或退款
                rst = OptResult.Build(ResultCode.OptRepeat, optDesc + "——已付款或退款，不能再次支付！");
                return rst;
            }
            //6、支付
            /*
           * 消费扣费逻辑：
             * 优先扣除gov：if pay > govnow,govchanged=govnow,govnew=0;mychanged=pay-govnow,mynew=mynow-mychanged;
             *                   else         govchanged=pay,govnew=govnow-govchanged;mychanged=0,mynew=mynow;
             *      优先扣除my： if pay > mynow, mychanged=mynow,mynew=0;govchanged=pay-mynow,govnew=govnow-govchanged;
             *                   else         mychanged=pay,mynew=mynow-mychanged;govchanged=0,govnew=govnow;
           * 1）更新card_info，
           * 2）新增操作记录，type：付款
           * 3）新增一卡通流水，type：付款
             * 注：扣费时，变动额为负数
           */
            decimal govnew, govchanged, mynew, mychanged;
            if (pay.priority == MoneyEnum.gov)
            {
                if (pay.amount > card.card_govmoney)
                {
                    govchanged = -card.card_govmoney;
                    govnew = 0;
                    mychanged = -(pay.amount - card.card_govmoney);
                    mynew = card.card_mymoney + mychanged;//因为mychanged是负数
                }
                else
                {
                    govchanged = -pay.amount;
                    govnew = card.card_govmoney - pay.amount;
                    mychanged = 0;
                    mynew = card.card_mymoney;
                }
            }
            else
            {
                if (pay.amount > card.card_mymoney)
                {
                    mychanged = -card.card_mymoney;
                    mynew = 0;
                    govchanged = -(pay.amount - card.card_mymoney);
                    govnew = card.card_govmoney + govchanged;//因为govchanged是负数
                }
                else
                {
                    mychanged = -pay.amount;
                    mynew = card.card_mymoney - pay.amount;
                    govchanged = 0;
                    govnew = card.card_govmoney;
                }
            }
            //
            var rec_id = GuidExtension.GetOne();//操作记录id
            DateTime optTime = DateTime.Now;
            var cardRecord = new CardRecord
            {
                rec_id = rec_id,
                rec_number = card.card_number,
                rec_idcard = card.card_idcard,
                rec_type = operation.ToString(),
                rec_time = optTime,
                rec_username = card.card_username,
                rec_remark = string.Format("付款金额：{0}", pay.amount),
                rec_operator = pay.opt
            };
            var cardBill = new CardBill
            {
                bill_id = GuidExtension.GetOne(),
                bill_number = card.card_number,
                bill_idcard = card.card_idcard,
                bill_agoall = card.card_govmoney + card.card_mymoney,
                bill_agogov = card.card_govmoney,
                bill_agomy = card.card_mymoney,
                bill_changegov = govchanged,
                bill_changemy = mychanged,
                bill_nowall = mynew + govnew,
                bill_nowgov = govnew,
                bill_nowmy = mynew,
                bill_type = operation.ToString(),
                bill_time = optTime,
                bill_order = pay.order,
                bill_src = pay.src,
                bill_record = rec_id,
                bill_remark = pay.remark
            };
            var tran = base.Begin();
            try
            {
                //
                count = _cardInfoRep.UpdateBySqlName(SqlName_Update,
                    new { card_govmoney = govnew, card_mymoney = mynew, card_modifier = pay.opt, card_modifytime = optTime, card_idcard = pay.idcard },
                    new string[] { "card_govmoney", "card_mymoney", "card_modifier", "card_modifytime" },
                    tran);
                if (count < 1)
                {
                    tran.Rollback();
                    rst = OptResult.Build(ResultCode.Fail, optDesc + "——未知错误！");
                    return rst;
                }
                //
                _cardRecordRep.Insert(cardRecord);
                //
                _cardBillRep.Insert(cardBill);

                tran.Commit();

                rst = OptResult.Build(ResultCode.Success, optDesc + "——idcard：" + pay.idcard);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(optDesc, ex);
                rst = OptResult.Build(ResultCode.DbError, optDesc);
            }

            return rst;
        }

        /// <summary>
        /// 退款
        /// </summary>
        /// <param name="refund"></param>
        /// <returns></returns>
        public OptResult Refound(RefundEntity refund)
        {
            OptResult rst = null;
            var operation = CardOperation.Refund;
            var optDesc = operation.GetDescription();

            if (refund == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, optDesc + "——参数不能为空！");
                return rst;
            }
            string msg = string.Empty;
            if (!refund.Check(out msg))
            {
                rst = OptResult.Build(ResultCode.ParamError, string.Format("{0}——{1}！", optDesc, msg));
                return rst;
            }

            //1、账户是否存在（获取账户信息）
            var card = GetByIdcard(refund.idcard);
            if (card == null)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, string.Format("{0}——指定身份证号{1}的账户不存在！", optDesc, refund.idcard));
                return rst;
            }
            //2、账户状态
            if (card.State != CardState.Normal)
            {
                rst = OptResult.Build(ResultCode.Fail, string.Format("{0}——一卡通状态异常[{1}]！", optDesc, card.State.GetDescription()));
                return rst;
            }
            //3、指定订单是否已付款并且未退款（获取付款流水信息）
            PredicateGroup pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<CardBill>(b => b.bill_idcard, Operator.Eq, card.card_idcard));
            pg.Predicates.Add(Predicates.Field<CardBill>(b => b.bill_order, Operator.Eq, refund.order));
            var payTypeWhere = Predicates.Field<CardBill>(b => b.bill_type, Operator.Eq, CardOperation.Pay.ToString());
            pg.Predicates.Add(payTypeWhere);
            var payBill = _cardBillRep.GetList(pg).FirstOrDefault();
            if (payBill == null)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, string.Format("{0}——指定身份证号{1}的订单{2}尚未支付！", optDesc, card.card_idcard, refund.order));
                return rst;
            }
            pg.Predicates.Remove(payTypeWhere);
            pg.Predicates.Add(Predicates.Field<CardBill>(b => b.bill_type, Operator.Eq, "退款"));
            var count = _cardBillRep.Count(pg);
            if (count > 0)
            {
                rst = OptResult.Build(ResultCode.OptRepeat, string.Format("{0}——指定身份证号{1}的订单{2}已退款！", optDesc, card.card_idcard, refund.order));
                return rst;
            }
            //4、数据库操作
            /*
            * 退款流程
            * 1）更新一卡通基本信息
            *      gov_new=gov_now+bill.gov_change,my_new=my_now+bill.my_change
            * 2）新增操作记录，type：退款
            * 3）新增流水记录，type：退款
            */
            decimal govchanged = -payBill.bill_changegov;//求反
            decimal mychanged = -payBill.bill_changemy;//求反
            decimal govnew = card.card_govmoney + govchanged;
            decimal mynew = card.card_mymoney + mychanged;
            var rec_id = GuidExtension.GetOne();
            var optTime = DateTime.Now;
            var cardRecord = new CardRecord
            {
                rec_id = rec_id,
                rec_number = card.card_number,
                rec_idcard = card.card_idcard,
                rec_type = operation.ToString(),
                rec_time = optTime,
                rec_username = card.card_username,
                rec_remark = string.Format("退款金额：{0}", govchanged + mychanged),
                rec_operator = refund.opt
            };
            var cardBill = new CardBill
            {
                bill_id = GuidExtension.GetOne(),
                bill_number = card.card_number,
                bill_idcard = card.card_idcard,
                bill_agoall = card.card_govmoney + card.card_mymoney,
                bill_agogov = card.card_govmoney,
                bill_agomy = card.card_mymoney,
                bill_changegov = govchanged,
                bill_changemy = mychanged,
                bill_nowall = govnew + mynew,
                bill_nowgov = govnew,
                bill_nowmy = mynew,
                bill_type = operation.ToString(),
                bill_time = optTime,
                bill_order = refund.order,
                bill_src = refund.src,
                bill_record = rec_id,
                bill_remark = refund.remark
            };
            //
            var tran = base.Begin();
            try
            {
                //
                count = _cardInfoRep.UpdateBySqlName(SqlName_Update,
                    new { card_govmoney = govnew, card_mymoney = mynew, card_modifier = refund.opt, card_modifytime = optTime, card_idcard = refund.idcard },
                    new string[] { "card_govmoney", "card_mymoney", "card_modifier", "card_modifytime" },
                    tran);
                if (count < 1)
                {
                    tran.Rollback();
                    rst = OptResult.Build(ResultCode.Fail, optDesc + "——未知错误！");
                    return rst;
                }
                //
                _cardRecordRep.Insert(cardRecord);
                //
                _cardBillRep.Insert(cardBill);

                tran.Commit();
                rst = OptResult.Build(ResultCode.Success, string.Format("{0}——身份证号{1}，订单号{2}", optDesc, refund.idcard, refund.order));
            }
            catch (Exception ex)
            {
                LogHelper.LogError(optDesc, ex);
                rst = OptResult.Build(ResultCode.DbError, optDesc);
            }

            return rst;
        }

        //私有方法

        /// <summary>
        /// 设置单个一卡通账户的金额
        /// 关于事务：
        ///  * 1、如果从外部传递事务，则事务操作也由调用方处理
        ///  * 2、如果外部没有传入事务，则内部起一个事务，并自行负责事务操作
        /// </summary>
        /// <param name="card"></param>
        /// </param>
        /// <returns></returns>
        private OptResult SetMoneySingle(SetMoneySingleProcessParam param)
        {
            OptResult rst = null;

            if (param == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, "补贴/充值——参数错误！");
                return rst;
            }
            var operation = param.moneyType == MoneyEnum.gov ? CardOperation.SetGov : CardOperation.SetMy;
            var optDesc = operation.GetDescription() + "(单账户)";

            if (param.card == null)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, string.Format("{0}[{1}]——一卡通账户不存在！", optDesc, param.moneyType.GetDescription()));
                return rst;
            }

            //2、数据库处理
            /*
           * 流程：
           * 1）更新card_info
           * 2）新增操作记录，type：补贴、充值
           * 3）新增一卡通流水
           */
            var rec_id = GuidExtension.GetOne();
            var optTime = DateTime.Now;
            var cardRecord = new CardRecord
            {
                rec_id = rec_id,
                rec_number = param.card.card_number,
                rec_username = param.card.card_username,
                rec_idcard = param.card.card_idcard,
                rec_type = operation.ToString(),
                rec_time = optTime,
                rec_operator = param.opt,
                rec_remark = string.Format("{0}，本次变动额：{1}", optDesc, param.money)
            };
            //政府补贴金时，不保留政府补贴现有余额，直接更新成新的金额
            var cardBill = new CardBill
            {
                bill_id = Guid.NewGuid().ToString("N"),
                bill_number = param.card.card_number,
                bill_idcard = param.card.card_idcard,
                bill_agoall = param.card.card_govmoney + param.card.card_mymoney,
                bill_agogov = param.card.card_govmoney,
                bill_agomy = param.card.card_mymoney,
                bill_changegov = param.moneyType == MoneyEnum.gov ? (param.money - param.card.card_govmoney) : 0,
                bill_changemy = param.moneyType == MoneyEnum.my ? param.money : 0,
                bill_nowall = param.moneyType == MoneyEnum.gov ? (param.money + param.card.card_mymoney) : (param.card.card_govmoney + param.card.card_mymoney + param.money),
                bill_nowgov = param.moneyType == MoneyEnum.gov ? param.money : param.card.card_govmoney,
                bill_nowmy = param.moneyType == MoneyEnum.my ? (param.card.card_mymoney + param.money) : param.card.card_mymoney,
                bill_type = operation.ToString(),
                bill_time = optTime,
                bill_record = rec_id,
                bill_remark = optDesc
            };


            var innerTran = param.tran;
            if (innerTran == null)
            {
                innerTran = _cardInfoRep.Begin();
            }
            try
            {
                var count = 0;
                //1
                if (param.moneyType == MoneyEnum.gov)
                {
                    count = _cardInfoRep.UpdateBySqlName(SqlName_Update,
                        new { card_govmoney = param.money, card_idcard = param.card.card_idcard, card_modifier = param.opt, card_modifytime = optTime },
                        new string[] { "card_govmoney", "card_modifier", "card_modifytime" },
                        innerTran);
                }
                else
                {
                    count = _cardInfoRep.UpdateBySqlName(SqlName_Update,
                        new { card_mymoney = param.card.card_mymoney + param.money, card_idcard = param.card.card_idcard, card_modifier = param.opt, card_modifytime = optTime },
                        new string[] { "card_mymoney", "card_modifier", "card_modifytime" },
                        innerTran);
                }
                if (count < 1)
                {
                    if (param.tran == null)
                    {
                        //外部事务为null，说明是本方法内部事务，这里自行回滚
                        innerTran.Rollback();
                    }
                    rst = OptResult.Build(ResultCode.Fail, string.Format("{0}——idcard={1}", optDesc, param.card.card_idcard));
                    return rst;
                }
                //2
                _cardRecordRep.Insert(cardRecord, innerTran);
                //3
                _cardBillRep.Insert(cardBill, innerTran);
                if (param.tran == null)
                {
                    //外部事务为null，说明是本方法内部事务，这里自行提交
                    innerTran.Commit();
                }
                rst = OptResult.Build(ResultCode.Success, string.Format("{0}——idcard={1}", optDesc, param.card.card_idcard));
            }
            catch (Exception ex)
            {
                LogHelper.LogError(optDesc, ex);
                rst = OptResult.Build(ResultCode.DbError, optDesc);
                return rst;
            }

            return rst;
        }

    }
}
