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
using MyNet.Components.Mapper;
using MyNet.Repository.Base;
using DapperExtensions;
using MyNet.Model.Base;
using Biz.PartyBuilding.Model.Base;

namespace Biz.PartyBuilding.Service
{
    public class PartyOrgService : BaseService<PartyOrg>
    {
        //常量
        const string Msg_Save = "保存党组织信息";
        const string Msg_PageQuery = "分页查询党组织信息";
        const string Msg_FindOrgByGroupId = "查找党组织信息";

        const string SqlName_PageQuery = "pagequery";
        const string SqlName_FindOrgByGroupId = "get";

        //私有变量
        private PartyOrgRepository _poRep;
        private DictRepository _dictRep;

        public PartyOrgService(IDbSession session, PartyOrgRepository poRep, DictRepository dictRep)
            : base(session, poRep)
        {
            _poRep = poRep;
            _dictRep = dictRep;
        }

        public OptResult FindOrgByGroupId(string gpid)
        {
            OptResult rst = null;
            try
            {
                var pod = _poRep.QueryBySqlName<PartyOrgDto>(SqlName_FindOrgByGroupId, new { gpid = gpid }).FirstOrDefault();
                rst = OptResult.Build(ResultCode.Success, Msg_FindOrgByGroupId, pod);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_FindOrgByGroupId, ex);
                rst = OptResult.Build(ResultCode.DbError, Msg_FindOrgByGroupId);
            }
            return rst;
        }

        public OptResult Save(PartyOrg po, string po_gp_id)
        {
            OptResult rst = null;
            try
            {
                //1、党组织类型是否存在
                string msg = "";
                if (!ValidateOrgType(po.po_type, out msg))
                {
                    rst = OptResult.Build(ResultCode.ParamError, Msg_Save + "，" + msg);
                    return rst;
                }
                //2、保存
                var oldPo = _poRep.GetById(po_gp_id);
                if (oldPo == null)
                {
                    po.po_id = po_gp_id;
                    //新增
                    _poRep.Insert(po);
                }
                else
                {
                    //修改
                    oldPo.po_type = po.po_type;
                    oldPo.po_chg_num = po.po_chg_num;
                    oldPo.po_chg_date = po.po_chg_date;
                    oldPo.po_expire_date = po.po_expire_date;
                    oldPo.po_chg_remind = po.po_chg_remind;
                    oldPo.po_mem_normal = po.po_mem_normal;
                    oldPo.po_mem_potential = po.po_mem_potential;
                    oldPo.po_mem_activists = po.po_mem_activists;
                    oldPo.po_remark = po.po_remark;
                    _poRep.Update(oldPo);
                }
                rst = OptResult.Build(ResultCode.Success, Msg_Save);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_Save, ex);
                rst = OptResult.Build(ResultCode.DbError, Msg_Save);
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

        private bool ValidateOrgType(string orgType, out string msg)
        {
            msg = "";
            var val = _dictRep.Count(Predicates.Field<Dict>(d => d.dict_type, Operator.Eq, PartyDictType.PartyOrg.type_code)) > 0;

            msg = val ? "" : "党组织类型不存在！";

            return val;
        }


    }
}
