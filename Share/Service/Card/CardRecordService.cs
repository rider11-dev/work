using DapperExtensions;
using MyNet.Components.Result;
using MyNet.Model.Card;
using MyNet.Repository.Card;
using MyNet.Repository.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNet.Components.Extensions;
using MyNet.Model;

namespace MyNet.Service.Card
{
    public class CardRecordService : BaseService<CardRecord>
    {
        //常量
        const string Msg_QueryByPage = "分页获取操作记录";

        //私有变量
        private CardRecordRepository _cardRecordRep;

        public CardRecordService(IDbSession session, CardRecordRepository cardRecordRep)
            : base(session, cardRecordRep)
        {
            _cardRecordRep = cardRecordRep;
        }

        public OptResult GetRecordsByPage(PageQuery page)
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
                if (page.conditions.ContainsKey("idcard") && page.conditions["idcard"].IsNotEmpty())
                {
                    pg.Predicates.Add(Predicates.Field<CardRecord>(r => r.rec_idcard, Operator.Eq, page.conditions["idcard"]));
                }
                if (page.conditions.ContainsKey("username") && page.conditions["username"].IsNotEmpty())
                {
                    pg.Predicates.Add(Predicates.Field<CardRecord>(r => r.rec_username, Operator.Like, "%" + page.conditions["username"] + "%"));
                }
                if (page.conditions.ContainsKey("type") && page.conditions["type"].IsNotEmpty())
                {
                    pg.Predicates.Add(Predicates.Field<CardRecord>(r => r.rec_type, Operator.Eq, page.conditions["type"]));
                }
                if (page.conditions.ContainsKey("time_begin") && page.conditions["time_begin"].IsNotEmpty())
                {
                    pg.Predicates.Add(Predicates.Field<CardRecord>(r => r.rec_time, Operator.Gt, page.conditions["time_begin"]));//＞查询起始时间
                }
                if (page.conditions.ContainsKey("time_end") && page.conditions["time_end"].IsNotEmpty())
                {
                    pg.Predicates.Add(Predicates.Field<CardRecord>(r => r.rec_time, Operator.Le, page.conditions["time_end"]));//≤查询结束时间
                }
            }
            //2、排序
            long total = 0;
            IList<ISort> sort = new[]
            {
                new Sort{PropertyName="rec_time",Ascending=false}//按时间降序
            };

            try
            {
                var recs = _cardRecordRep.GetPageList(page.pageIndex, page.pageSize, out total, sort, pg);
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
