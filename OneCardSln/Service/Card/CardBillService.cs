using DapperExtensions;
using OneCardSln.Model;
using OneCardSln.Repository.Card;
using OneCardSln.Repository.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.Service.Card
{
    public class CardBillService : BaseService<CardBill>
    {
        //常量
        const string Msg_QueryByPage = "分页获取流水记录";

        //私有变量
        private CardBillRepository _cardBillRep;

        public CardBillService(IDbSession session, CardBillRepository cardBillRep)
            : base(session, cardBillRep)
        {
            _cardBillRep = cardBillRep;
        }

        public OptResult GetBillsByPage(PageQuery page)
        {
            OptResult rst = null;
            if (page == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, Msg_QueryByPage + "，分页参数不能为空！");
                return rst;
            }
            page.Verify();

            //1、过滤条件
            PredicateGroup pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            if (page.conditions != null && page.conditions.Count > 0)
            {
                if (page.conditions.ContainsKey("idcard"))
                {
                    pg.Predicates.Add(Predicates.Field<CardBill>(b => b.bill_idcard, Operator.Eq, page.conditions["idcard"]));
                }
                if (page.conditions.ContainsKey("type"))
                {
                    pg.Predicates.Add(Predicates.Field<CardBill>(b => b.bill_type, Operator.Eq, page.conditions["type"]));
                }
                if (page.conditions.ContainsKey("order"))
                {
                    pg.Predicates.Add(Predicates.Field<CardBill>(b => b.bill_order, Operator.Like, "%" + page.conditions["order"] + "%"));
                }
                if (page.conditions.ContainsKey("time_begin"))
                {
                    pg.Predicates.Add(Predicates.Field<CardBill>(b => b.bill_time, Operator.Gt, page.conditions["time_begin"]));//＞查询起始时间
                }
                if (page.conditions.ContainsKey("time_end"))
                {
                    pg.Predicates.Add(Predicates.Field<CardBill>(r => r.bill_time, Operator.Le, page.conditions["time_end"]));//≤查询结束时间
                }
            }
            //2、排序
            long total = 0;
            IList<ISort> sort = new[]
            {
                new Sort{PropertyName="bill_time",Ascending=false}//按时间降序
            };

            try
            {
                var recs = _cardBillRep.GetPageList(page.pageIndex, page.pageSize, out total, sort, pg);
                rst = OptResult.Build(ResultCode.Success, Msg_QueryByPage, new
                {
                    total = total,
                    rows = recs
                });
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_QueryByPage, ex);
                rst = OptResult.Build(ResultCode.DbError, Msg_QueryByPage);
            }
            return rst;
        }
    }
}
