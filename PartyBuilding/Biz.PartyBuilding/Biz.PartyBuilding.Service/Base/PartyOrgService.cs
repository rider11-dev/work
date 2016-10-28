using Biz.PartyBuilding.Model;
using Biz.PartyBuilding.Repository;
using MyNet.Components.Result;
using MyNet.Model;
using MyNet.Repository.Auth;
using MyNet.Repository.Db;
using MyNet.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNet.Components.Extensions;
using MyNet.Dto.Auth;

namespace Biz.PartyBuilding.Service
{
    public class PartyOrgService : BaseService<PartyOrgDto>
    {
        //常量
        const string Msg_Update = "更新党组织信息";
        const string Msg_PageQuery = "分页查询党组织信息";

        const string SqlName_PageQuery = "pagequery";

        //私有变量
        private PartyOrgRepository _poRep;
        private GroupRepository _gpRep;

        public PartyOrgService(IDbSession session, PartyOrgRepository poRep, GroupRepository gpRep)
            : base(session, poRep)
        {
            _poRep = poRep;
            _gpRep = gpRep;
        }

        public OptResult Update(PartyOrgDto po)
        {
            OptResult rst = null;
            //3、更新：获取旧对象，赋值，更新
            //TODO
            try
            {
                var oldPo = _poRep.GetById(po.po_id);
                oldPo.po_type = po.po_type;
                oldPo.po_chg_num = po.po_chg_num;
                oldPo.po_chg_date = po.po_chg_date;
                oldPo.po_expire_date = po.po_expire_date;
                oldPo.po_chg_remind = po.po_chg_remind;
                oldPo.po_mem_normal = po.po_mem_normal;
                oldPo.po_mem_potential = po.po_mem_potential;
                oldPo.po_mem_activists = po.po_mem_activists;
                oldPo.po_remark = po.po_remark;

                bool val = _poRep.Update(oldPo);
                rst = OptResult.Build(val ? ResultCode.Success : ResultCode.Fail, Msg_Update);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_Update, ex);
                rst = OptResult.Build(ResultCode.DbError, Msg_Update);
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

            PageQuerySqlEntity sqlEntity = _poRep.GetPageQuerySql(SqlName_PageQuery);
            if (sqlEntity == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, Msg_PageQuery + "，未能获取sql配置！");
                return rst;
            }
            //构造where
            #region where条件
            if (page.conditions != null && page.conditions.Count > 0)
            {
                if (page.conditions.ContainsKey("gp_name") && !page.conditions["gp_name"].IsEmpty())
                {
                    sqlEntity.where.AppendFormat(" and gp.gp_name like '%{0}%' ", page.conditions["gp_name"]);
                }

                if (page.conditions.ContainsKey("gp_parent") && !page.conditions["gp_parent"].IsEmpty())
                {
                    sqlEntity.where.AppendFormat(" and gp.gp_parent = '{0}' ", page.conditions["gp_parent"]);
                }
                else if (page.conditions.ContainsKey("gp_parent_name") && !page.conditions["gp_parent_name"].IsEmpty())
                {
                    sqlEntity.where.AppendFormat(" and gpp.gp_name like '%{0}%' ", page.conditions["gp_parent_name"]);
                }
            }
            #endregion
            try
            {
                var pos = _poRep.PageQueryBySp<PartyOrgDto, GroupDto, PartyOrgDto>(sqlEntity: sqlEntity, page: page,
                    map: (po, gp) =>
                    {
                        po.po_group = gp;
                        return po;
                    }, splitOn: "po_id,gp_id");
                rst = OptResult.Build(ResultCode.Success, Msg_PageQuery, new
                {
                    total = page.total,
                    pagecount = page.pageTotal,
                    rows = pos
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
