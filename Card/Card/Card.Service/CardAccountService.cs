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
    public class CardAccountService : BaseService<CardAccount>
    {
        const string Msg_GetAccountByIdcard = "获取一卡通账户信息";
        const string Msg_PageQuery = "分页获取一卡通账户信息";
        protected const string SqlName_PageQuery = "pagequery";
        private CardAccountRepository _acntRep;
        CardInfoRepository _cardRep;
        public CardAccountService(IDbSession session, CardAccountRepository acntRep, CardInfoRepository cardRep) : base(session, acntRep)
        {
            _acntRep = acntRep;
            _cardRep = cardRep;
        }

        public OptResult GetAccountByIdcard(string idcard)
        {
            OptResult rst = null;
            if (idcard.IsEmpty())
            {
                rst = OptResult.Build(ResultCode.ParamError, Msg_GetAccountByIdcard + "，身份证号不能为空！");
                return rst;
            }
            try
            {
                var account = _acntRep.GetList(Predicates.Field<CardAccount>(a => a.idcard, Operator.Eq, idcard));
                if (account.IsEmpty())
                {
                    rst = OptResult.Build(ResultCode.DataNotFound);
                    return rst;
                }
                rst = OptResult.Build(ResultCode.Success, Msg_GetAccountByIdcard, account.First());
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_GetAccountByIdcard, ex);
                rst = OptResult.Build(ResultCode.DbError, Msg_GetAccountByIdcard);
            }
            return rst;
        }

        public OptResult QueryByPage(PageQuery page)
        {
            OptResult rst = null;
            if (page == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, Msg_PageQuery + "，分页参数不能为空！");
                return rst;
            }

            PageQuerySqlEntity sqlEntity = _acntRep.GetPageQuerySql(SqlName_PageQuery);
            if (sqlEntity == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, Msg_PageQuery + "，未能获取sql配置！");
                return rst;
            }
            if (page.conditions != null && page.conditions.Count > 0)
            {
                if (page.conditions.ContainsKey("idcard") && page.conditions["idcard"].IsNotEmpty())
                {
                    sqlEntity.where.AppendFormat(" and ci.idcard='{0}' ", page.conditions["idcard"]);
                }
            }
            try
            {
                var accounts = _acntRep.PageQueryBySp<dynamic>(sqlEntity: sqlEntity, page: page);
                rst = OptResult.Build(ResultCode.Success, Msg_PageQuery, new
                {
                    total = page.total,
                    pagecount = page.pageTotal,
                    rows = accounts
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
