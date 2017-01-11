using Card.Model;
using Card.Repository;
using DapperExtensions;
using MyNet.Components.Extensions;
using MyNet.Components.Result;
using MyNet.Model;
using MyNet.Repository;
using MyNet.Repository.Db;
using MyNet.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Service
{
    public class CardInfoService : BaseService<CardInfo>
    {
        const string Msg_PageQuery = "分页获取卡信息";
        protected const string SqlName_PageQuery = "pagequery";
        CardInfoRepository _cardRep;
        public CardInfoService(IDbSession session, CardInfoRepository cardRep) : base(session, cardRep)
        {
            _cardRep = cardRep;
        }

        public OptResult QueryByPage(PageQuery page)
        {
            OptResult rst = null;
            if (page == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, Msg_PageQuery + "，分页参数不能为空！");
                return rst;
            }

            PageQuerySqlEntity sqlEntity = _cardRep.GetPageQuerySql(SqlName_PageQuery);
            if (sqlEntity == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, Msg_PageQuery + "，未能获取sql配置！");
                return rst;
            }
            if (page.conditions != null && page.conditions.Count > 0)
            {
                if (page.conditions.ContainsKey("idcard") && page.conditions["idcard"].IsNotEmpty())
                {
                    sqlEntity.where.AppendFormat(" and cn.idcard='{0}' ", page.conditions["idcard"]);
                }
            }
            try
            {
                var cards = _cardRep.PageQueryBySp<dynamic>(sqlEntity: sqlEntity, page: page);
                rst = OptResult.Build(ResultCode.Success, Msg_PageQuery, new
                {
                    total = page.total,
                    pagecount = page.pageTotal,
                    rows = cards
                });
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_PageQuery, ex);
                rst = OptResult.Build(ResultCode.DbError, Msg_PageQuery);
            }
            return rst;
        }
    }
}
