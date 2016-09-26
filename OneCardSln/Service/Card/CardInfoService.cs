using DapperExtensions;
using OneCardSln.Components.Extensions;
using OneCardSln.Model;
using OneCardSln.Model.Base;
using OneCardSln.Repository.Base;
using OneCardSln.Repository.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.Service.Card
{
    public class CardInfoService
    {
        //常量
        const string Msg_RegisterCard = "注册一卡通账户";

        //私有变量
        private CardInfoRepository _cardInfoRep;
        private DictRepository _dictRep;
        private CardRecordRepository _cardRecordRep;

        public CardInfoService(CardInfoRepository cardInfoRep, DictRepository dictRep, CardRecordRepository cardRecordRep)
        {
            _cardInfoRep = cardInfoRep;
            _dictRep = dictRep;
            _cardRecordRep = cardRecordRep;
        }

        /// <summary>
        /// 注册一卡通账户
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public OptResult Register(CardInfo newCard)
        {
            OptResult rst = null;

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
                    rst = OptResult.Build(ResultCode.DataRepeat, string.Format("{0}——创建本地一卡通账户失败，身份证号已存在", Msg_RegisterCard));
                    return rst;
                }
                //2、一卡通号是否已存在
                if (card.card_number.Equals(newCard.card_number))
                {
                    rst = OptResult.Build(ResultCode.DataRepeat, string.Format("{0}——创建本地一卡通账户失败，一卡通号已存在", Msg_RegisterCard));
                    return rst;
                }
                //3、手机号号是否已存在
                if (card.card_idcard.Equals(newCard.card_phone))
                {
                    rst = OptResult.Build(ResultCode.DataRepeat, string.Format("{0}——创建本地一卡通账户失败，手机号已存在", Msg_RegisterCard));
                    return rst;
                }
            }

            //4、创建
            //主键
            if (string.IsNullOrEmpty(newCard.card_id))
            {
                newCard.card_id = GuidExtension.GetOne();
            }
            //状态默认值：取default或排序后的第一个
            var states = _dictRep.GetList(Predicates.Field<Dict>(d => d.dict_type, Operator.Eq, "cardstate"));
            if (states == null || states.Count() < 1)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, string.Format("{0}——创建本地一卡通账户失败，未找到状态字典", Msg_RegisterCard));
                return rst;
            }
            var state = states.Where(d => d.dict_default == true).FirstOrDefault();
            newCard.card_state = (state != null) ? state.dict_id : states.OrderBy(d => d.dict_order).First().dict_id;

            //新增一卡通数据
            var cardRecord = new CardRecord
            {
                rec_id = GuidExtension.GetOne(),
                rec_number = newCard.card_number,
                rec_username = newCard.card_username,
                rec_idcard = newCard.card_idcard,
                rec_type = "发卡",
                rec_time = DateTime.Now,
                rec_operator = newCard.card_creator
            };
            var tran = _cardInfoRep.DbSession.Begin();
            try
            {
                var val = _cardInfoRep.Insert(newCard, tran);
                //新增操作记录
                val = _cardRecordRep.Insert(cardRecord, tran);

                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();

                throw;
            }

            rst = OptResult.Build(ResultCode.Success, Msg_RegisterCard);

            return rst;
        }


    }
}
