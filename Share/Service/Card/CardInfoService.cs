using DapperExtensions;
using MyNet.Components.Extensions;
using MyNet.Components.Result;
using MyNet.Model.Card;
using MyNet.Model.Base;
using MyNet.Repository.Base;
using MyNet.Repository.Card;
using MyNet.Repository.Db;
using MyNet.Service.Card.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNet.Model;

namespace MyNet.Service.Card
{
    public class CardInfoService : BaseService<CardInfo>
    {
        //常量
        const string Msg_GetStates = "获取一卡通状态列表";
        const string Msg_GetOpts = "获取一卡通操作类型列表";
        const string Msg_QueryByPage = "分页获取一卡通信息";

        protected const string SqlName_Update = "update";

        //私有变量
        protected CardInfoRepository _cardInfoRep;
        protected CardRecordRepository _cardRecordRep;
        protected CardBillRepository _cardBillRep;
        protected CardOperationRepository _optRep = new CardOperationRepository();

        public CardInfoService(IDbSession session, CardInfoRepository cardInfoRep,
            CardRecordRepository cardRecordRep,
            CardBillRepository cardBillRep)
            : base(session, cardInfoRep)
        {
            _cardInfoRep = cardInfoRep;
            _cardRecordRep = cardRecordRep;
            _cardBillRep = cardBillRep;
        }

        //公共方法

        /// <summary>
        /// 注册一卡通账户
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public OptResult Register(CardInfo newCard)
        {
            OptResult rst = null;
            var operation = CardOperation.Reg;
            var optDesc = operation.GetDescription();

            PredicateGroup pg = new PredicateGroup { Operator = GroupOperator.Or, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<CardInfo>(c => c.card_idcard, Operator.Eq, newCard.card_idcard));
            pg.Predicates.Add(Predicates.Field<CardInfo>(c => c.card_number, Operator.Eq, newCard.card_number));
            pg.Predicates.Add(Predicates.Field<CardInfo>(c => c.card_phone, Operator.Eq, newCard.card_phone));
            var card = _cardInfoRep.GetList(pg).FirstOrDefault();
            if (card != null)
            {
                //1、身份证号是否已存在
                if (card.card_idcard.Equals(newCard.card_idcard))
                {
                    rst = OptResult.Build(ResultCode.DataRepeat, string.Format("{0}——创建本地一卡通账户失败，身份证号已存在！", optDesc));
                    return rst;
                }
                //2、一卡通号是否已存在
                if (card.card_number.Equals(newCard.card_number))
                {
                    rst = OptResult.Build(ResultCode.DataRepeat, string.Format("{0}——创建本地一卡通账户失败，一卡通号已存在！", optDesc));
                    return rst;
                }
                //3、手机号号是否已存在
                if (card.card_phone.Equals(newCard.card_phone))
                {
                    rst = OptResult.Build(ResultCode.DataRepeat, string.Format("{0}——创建本地一卡通账户失败，手机号已存在！", optDesc));
                    return rst;
                }
            }

            //4、创建
            //主键
            if (string.IsNullOrEmpty(newCard.card_id))
            {
                newCard.card_id = GuidExtension.GetOne();
            }
            //默认状态
            newCard.card_state = CardState.Normal.ToString();

            //新增一卡通数据
            var cardRecord = new CardRecord
            {
                rec_id = GuidExtension.GetOne(),
                rec_number = newCard.card_number,
                rec_username = newCard.card_username,
                rec_idcard = newCard.card_idcard,
                rec_type = operation.ToString(),
                rec_time = DateTime.Now,
                rec_operator = newCard.card_creator,
                rec_remark = optDesc
            };
            var tran = _cardInfoRep.Begin();
            try
            {
                var val = _cardInfoRep.Insert(newCard, tran);
                //新增操作记录
                val = _cardRecordRep.Insert(cardRecord, tran);

                tran.Commit();
                rst = OptResult.Build(ResultCode.Success, optDesc);
            }
            catch (Exception ex)
            {
                //这里不用rollback了，因为仓储层已处理
                //tran.Rollback();
                LogHelper.LogError(optDesc, ex);
                rst = OptResult.Build(ResultCode.DbError, optDesc);
            }

            return rst;
        }
        /// <summary>
        /// 补卡
        /// </summary>
        /// <param name="idcard"></param>
        /// <param name="number"></param>
        /// <param name="opt"></param>
        /// <returns></returns>
        public OptResult Makeup(string idcard, string number, string opt)
        {
            OptResult rst = null;
            var operation = CardOperation.Makeup;
            var optDesc = operation.GetDescription();

            //1、指定身份证号账户是否存在
            var card = GetByIdcard(idcard);
            if (card == null)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, string.Format("{0}失败，指定身份证号{1}的账户不存在！", optDesc, idcard));
                return rst;
            }
            //2、一卡通是否已注册
            var count = _cardInfoRep.Count(Predicates.Field<CardInfo>(c => c.card_number, Operator.Eq, number));
            if (count > 0)
            {
                rst = OptResult.Build(ResultCode.DataRepeat, string.Format("{0}失败，一卡通号{1}已登记！", optDesc, number));
                return rst;
            }

            //3、数据库操作
            var optTime = DateTime.Now;
            var cardRecord = new CardRecord
            {
                rec_id = GuidExtension.GetOne(),
                rec_number = number,
                rec_username = card.card_username,
                rec_idcard = idcard,
                rec_type = operation.ToString(),
                rec_time = optTime,
                rec_operator = opt,
                rec_remark = string.Format("{0}，变更前：{1}，变更后：{2}", optDesc, card.card_number, number)
            };
            var tran = _cardInfoRep.Begin();
            try
            {
                count = _cardInfoRep.UpdateBySqlName(SqlName_Update,
                    new { card_number = number, card_idcard = idcard, card_modifier = opt, card_modifytime = optTime },
                    new string[] { "card_number", "card_modifier", "card_modifytime" }, tran);
                if (count < 1)
                {
                    tran.Rollback();
                    rst = OptResult.Build(ResultCode.Fail, optDesc + "失败，未知错误！");
                    return rst;
                }
                //添加操作记录
                var val = _cardRecordRep.Insert(cardRecord, tran);
                rst = OptResult.Build(ResultCode.Success, optDesc);
                tran.Commit();
            }
            catch (Exception ex)
            {
                LogHelper.LogError(optDesc, ex);
                rst = OptResult.Build(ResultCode.DbError, optDesc);
            }

            return rst;
        }
        /// <summary>
        ///  变更手机号
        /// </summary>
        /// <param name="idcard"></param>
        /// <param name="newPhone"></param>
        /// <param name="opt"></param>
        /// <returns></returns>
        public OptResult ChangePhone(string idcard, string newPhone, string opt)
        {
            OptResult rst = null;
            var operation = CardOperation.ChgPhone;
            var optDesc = operation.GetDescription();

            //1、指定身份证号账户是否存在
            var card = GetByIdcard(idcard);
            if (card == null)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, string.Format("{0}——指定身份证号{1}的账户不存在！", optDesc, idcard));
                return rst;
            }
            //2、手机号是否已存在
            var count = _cardInfoRep.Count(Predicates.Field<CardInfo>(c => c.card_phone, Operator.Eq, newPhone));
            if (count > 0)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, string.Format("{0}——手机号{1}已存在！", optDesc, newPhone));
                return rst;
            }
            //3、数据库操作
            var optTime = DateTime.Now;
            var cardRecord = new CardRecord
            {
                rec_id = GuidExtension.GetOne(),
                rec_number = card.card_number,
                rec_username = card.card_username,
                rec_idcard = idcard,
                rec_type = operation.ToString(),
                rec_time = optTime,
                rec_operator = opt,
                rec_remark = string.Format("手机号，变更前：{0}，变更后：{1}", card.card_phone, newPhone)
            };
            var tran = _cardInfoRep.Begin();
            try
            {
                count = _cardInfoRep.UpdateBySqlName(SqlName_Update,
                    new { card_phone = newPhone, card_idcard = idcard, card_modifier = opt, card_modifytime = optTime },
                    new string[] { "card_phone", "card_modifier", "card_modifytime" }, tran);
                if (count < 1)
                {
                    tran.Rollback();
                    rst = OptResult.Build(ResultCode.Fail, optDesc + "——未知错误！");
                    return rst;
                }
                //添加操作记录
                var val = _cardRecordRep.Insert(cardRecord, tran);
                rst = OptResult.Build(ResultCode.Success, optDesc);
                tran.Commit();
            }
            catch (Exception ex)
            {
                LogHelper.LogError(optDesc, ex);
                rst = OptResult.Build(ResultCode.DbError, optDesc);
            }

            return rst;
        }

        /// <summary>
        /// 注销账户——批量
        /// </summary>
        /// <param name="idcards"></param>
        /// <param name="opt"></param>
        /// <returns></returns>
        public OptResult CloseDownBatch(IEnumerable<string> idcards, string opt)
        {
            return BatchProcess(idcards, opt, CardOperation.CloseDwn.GetDescription() + "(批量)",
                (param) =>
                {
                    return CloseDownSingle(param);
                });
        }

        /// <summary>
        /// 注销账户——单账户
        /// </summary>
        /// <param name="idcard"></param>
        /// <param name="opt"></param>
        /// <returns></returns>
        public OptResult CloseDownSingle(string idcard, string opt)
        {
            OptResult rst = null;
            //1、账户是否存在
            var card = GetByIdcard(idcard);
            if (card == null)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, string.Format("{0}——指定身份证号{1}的账户不存在！", CardOperation.CloseDwn.GetDescription() + "(单账户)", idcard));
                return rst;
            }

            return CloseDownSingle(new SingleProcessParam { card = card, opt = opt });
        }

        /// <summary>
        /// 挂失
        /// </summary>
        /// <param name="idcard"></param>
        /// <param name="opt"></param>
        /// <returns></returns>
        public OptResult ReportLoss(string idcard, string opt)
        {
            OptResult rst = null;

            var operation = CardOperation.RepLoss;
            var optDesc = operation.GetDescription();

            //1、账户是否存在
            var card = GetByIdcard(idcard);
            if (card == null)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, string.Format("{0}——指定身份证号{1}的账户不存在！", optDesc, idcard));
                return rst;
            }
            //2、状态是否“正常”
            if (card.State != CardState.Normal)
            {
                rst = OptResult.Build(ResultCode.Fail, string.Format("{0}——一卡通账户状态异常[{1}]！", optDesc, card.State.GetDescription()));
                return rst;
            }
            var optTime = DateTime.Now;
            var cardRecord = new CardRecord
            {
                rec_id = GuidExtension.GetOne(),
                rec_number = card.card_number,
                rec_username = card.card_username,
                rec_idcard = idcard,
                rec_type = operation.ToString(),
                rec_time = optTime,
                rec_operator = opt,
                rec_remark = optDesc
            };
            //
            var tran = base.Begin();
            try
            {
                var count = _cardInfoRep.UpdateBySqlName(SqlName_Update,
                    new { card_state = CardState.Loss.ToString(), card_idcard = idcard, card_modifier = opt, card_modifytime = optTime },
                    new string[] { "card_state", "card_modifier", "card_modifytime" }, tran);
                if (count < 1)
                {
                    tran.Rollback();
                    rst = OptResult.Build(ResultCode.Fail, optDesc + "失败，未知错误！");
                    return rst;
                }

                _cardRecordRep.Insert(cardRecord, tran);

                tran.Commit();
                rst = OptResult.Build(ResultCode.Success, optDesc);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(optDesc, ex);
                rst = OptResult.Build(ResultCode.DbError, optDesc);
            }

            return rst;
        }

        /// <summary>
        /// 恢复账户——批量
        /// </summary>
        /// <param name="idcards"></param>
        /// <param name="opt"></param>
        /// <returns></returns>
        public OptResult RecoverBatch(IEnumerable<string> idcards, string opt)
        {
            return BatchProcess(idcards, opt, CardOperation.Recover.GetDescription() + "(批量)",
                (param) =>
                {
                    return RecoverSingle(param);
                });
        }

        /// <summary>
        /// 恢复账户——单账户
        /// </summary>
        /// <param name="idcard"></param>
        /// <param name="opt"></param>
        /// <returns></returns>
        public OptResult RecoverSingle(string idcard, string opt)
        {
            OptResult rst = null;
            //1、账户是否存在
            var card = GetByIdcard(idcard);
            if (card == null)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, string.Format("{0}——指定身份证号{1}的账户不存在！", CardOperation.Recover.GetDescription() + "(单账户)", idcard));
                return rst;
            }

            return RecoverSingle(new SingleProcessParam { card = card, opt = opt });
        }

        /// <summary>
        /// 获取一卡通操作类型列表
        /// </summary>
        /// <returns></returns>
        public OptResult GetCardOperations()
        {
            return OptResult.Build(ResultCode.Success, Msg_GetOpts, new
            {
                total = _optRep.DataSrc.Count,
                rows = _optRep.DataSrc
            });
        }

        /// <summary>
        /// 分页获取一卡通信息
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public OptResult GetCardInfoByPage(PageQuery page)
        {
            OptResult rst = null;
            if (page == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, Msg_QueryByPage + "，分页参数不能为空！");
                return rst;
            }
            
            //1、过滤条件
            PredicateGroup pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            if (page.conditions != null && page.conditions.Count > 0)
            {
                if (page.conditions.ContainsKey("idcard") && page.conditions["idcard"].IsNotEmpty())
                {
                    pg.Predicates.Add(Predicates.Field<CardInfo>(c => c.card_idcard, Operator.Eq, page.conditions["idcard"]));
                }
                if (page.conditions.ContainsKey("state") && page.conditions["state"].IsNotEmpty())
                {
                    pg.Predicates.Add(Predicates.Field<CardInfo>(c => c.card_state, Operator.Eq, page.conditions["state"]));
                }
                if (page.conditions.ContainsKey("username") && page.conditions["username"].IsNotEmpty())
                {
                    pg.Predicates.Add(Predicates.Field<CardInfo>(c => c.card_username, Operator.Like, "%" + page.conditions["username"] + "%"));
                }
            }
            //2、排序
            long total = 0;
            IList<ISort> sort = new[]
            {
                new Sort{PropertyName="card_createtime",Ascending=false}
            };
            try
            {
                var pers = _cardInfoRep.GetPageList(page.pageIndex, page.pageSize, out total, sort, pg);
                rst = OptResult.Build(ResultCode.Success, Msg_QueryByPage, new
                {
                    total = total,
                    rows = pers
                });
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_QueryByPage, ex);
                rst = OptResult.Build(ResultCode.DbError, Msg_QueryByPage);
            }
            return rst;
        }

        //私有方法

        private OptResult CloseDownSingle(SingleProcessParam param)
        {
            OptResult rst = null;
            var operation = CardOperation.CloseDwn;
            var optDesc = operation.GetDescription() + "(单账户)";

            if (param.card == null)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, string.Format("{0}——一卡通账户不存在！", optDesc));
                return rst;
            }
            if (param.card.State != CardState.Normal)//暂定只有正常状态的账户才能注销
            {
                rst = OptResult.Build(ResultCode.Fail,
                    string.Format("{0}——身份证号为{1}的账户状态为{2}，不能注销！", optDesc, param.card.card_idcard, param.card.State.GetDescription()));
                return rst;
            }
            //2、数据库处理
            /*
           * 流程：
           * 1）更新card_info
           * 2）新增操作记录，type：注销
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
                rec_remark = string.Format("{0}——本次变动额：{1}", optDesc, param.card.card_govmoney + param.card.card_mymoney)
            };

            var cardBill = new CardBill
            {
                bill_id = Guid.NewGuid().ToString("N"),
                bill_number = param.card.card_number,
                bill_idcard = param.card.card_idcard,
                bill_agoall = param.card.card_govmoney + param.card.card_mymoney,
                bill_agogov = param.card.card_govmoney,
                bill_agomy = param.card.card_mymoney,
                bill_changegov = -param.card.card_govmoney,
                bill_changemy = -param.card.card_mymoney,
                bill_nowall = 0,
                bill_nowgov = 0,
                bill_nowmy = 0,
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
                //1
                var count = _cardInfoRep.UpdateBySqlName(SqlName_Update,
                        new { card_govmoney = 0, card_mymoney = 0, card_idcard = param.card.card_idcard, card_state = CardState.Off.ToString(), card_modifier = param.opt, card_modifytime = optTime },
                        new string[] { "card_govmoney", "card_mymoney", "card_state", "card_modifier", "card_modifytime" },
                        innerTran);
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

        private OptResult RecoverSingle(SingleProcessParam param)
        {
            OptResult rst = null;
            var operation = CardOperation.Recover;
            var optDesc = operation.GetDescription() + "(单账户)";

            if (param.card == null)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, string.Format("{0}——一卡通账户不存在！", optDesc));
                return rst;
            }
            if (param.card.State == CardState.Normal)//"正常"状态的账户无需恢复
            {
                rst = OptResult.Build(ResultCode.Success,
                    string.Format("{0}——身份证号为{1}的账户状态为{2}，无需恢复！", optDesc, param.card.card_idcard, param.card.State.GetDescription()));
                return rst;
            }
            //2、数据库处理
            /*
           * 流程：
           * 1）更新card_info
           * 2）新增操作记录，type：注销
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
                rec_remark = optDesc
            };

            var innerTran = param.tran;
            if (innerTran == null)
            {
                innerTran = _cardInfoRep.Begin();
            }
            try
            {
                //1
                var count = _cardInfoRep.UpdateBySqlName(SqlName_Update,
                        new { card_idcard = param.card.card_idcard, card_state = CardState.Normal.ToString(), card_modifier = param.opt, card_modifytime = optTime },
                        new string[] { "card_state", "card_modifier", "card_modifytime" },
                        innerTran);
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

        protected CardInfo GetByIdcard(string idcard)
        {
            CardInfo card = null;
            if (string.IsNullOrEmpty(idcard))
            {
                return card;
            }

            card = _cardInfoRep.GetList(Predicates.Field<CardInfo>(c => c.card_idcard, Operator.Eq, idcard)).FirstOrDefault();

            return card;
        }

        /// <summary>
        /// 通用批量处理方法
        /// </summary>
        /// <param name="idcards">身份证号列表</param>
        /// <param name="opt">操作员</param>
        /// <param name="processDes">本次操作描述</param>
        /// <param name="funcSingle">单次操作委托</param>
        /// <param name="funcSingle">单次操作委托所需参数的构造器</param>
        /// <returns></returns>
        protected OptResult BatchProcess(IEnumerable<string> idcards, string opt, string processDes, Func<SingleProcessParam, OptResult> funcSingle, Func<SingleProcessParam> paramGetter = null)
        {
            OptResult rst = null;
            if (idcards == null || idcards.Count() < 1)
            {
                rst = OptResult.Build(ResultCode.ParamError, string.Format("{0}——身份证号列表不能为空！", processDes));
                return rst;
            }
            if (funcSingle == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, string.Format("{0}——未指定funcSingle参数！", processDes));
                return rst;
            }
            var discinctIdcards = idcards.Distinct();
            var where = Predicates.Field<CardInfo>(c => c.card_idcard, Operator.Eq, discinctIdcards);//这里sql查询条件会转换为in
            var cards = _cardInfoRep.GetList(where);
            if (cards == null || cards.Count() < 1)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, string.Format("{0}——未找到指定身份证号的一卡通账户！", processDes));
                return rst;
            }

            var tran = base.Begin();
            try
            {
                bool succeed = true;
                SingleProcessParam param = null;
                foreach (var card in cards)
                {
                    if (paramGetter != null)
                    {
                        param = paramGetter();
                        param.card = card;
                        param.opt = opt;
                        param.tran = tran;
                    }
                    else
                    {
                        param = new SingleProcessParam { card = card, opt = opt, tran = tran };
                    }
                    rst = funcSingle(param);
                    if (rst.code != ResultCode.Success)
                    {
                        try
                        {
                            tran.Rollback();
                        }
                        catch { }
                        succeed = false;
                        break;
                    }
                }
                if (succeed)
                {
                    tran.Commit();
                    rst = OptResult.Build(ResultCode.Success, string.Format("{0}", processDes));
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError(processDes, ex);
                rst = OptResult.Build(ResultCode.DbError, string.Format("{0}", processDes));
            }
            return rst;
        }
    }
}
