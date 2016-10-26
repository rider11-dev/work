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

namespace Biz.PartyBuilding.Service
{
    public class PartyOrgService : BaseService<PartyOrg>
    {
        //常量
        const string Msg_SyncOrg = "同步组织信息";
        const string Msg_Update = "更新党组织信息";
        const string Msg_GetById = "根据主键查询党组织信息";
        const string Msg_PageQuery = "分页查询党组织信息";
        const string SqlName_Sync = "sync";
        const string SqlName_GetById = "getbyid";

        //私有变量
        private PartyOrgRepository _poRep;
        private GroupRepository _gpRep;

        public PartyOrgService(IDbSession session, PartyOrgRepository poRep, GroupRepository gpRep)
            : base(session, poRep)
        {
            _poRep = poRep;
            _gpRep = gpRep;
        }

        /// <summary>
        /// 同步党组织，从auth_group表拉取数据
        /// </summary>
        /// <returns></returns>
        public OptResult Sync()
        {
            OptResult rst = null;
            try
            {
                var val = _poRep.UpdateBySqlName(SqlName_Sync);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_SyncOrg, ex);
                rst = OptResult.Build(ResultCode.DbError, Msg_SyncOrg);
            }
            return rst;
        }

        public OptResult Update(PartyOrg po)
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

        public OptResult GetById(string pkId)
        {
            OptResult rst = null;
            try
            {
                var val = _poRep.QueryBySqlName(typeof(PartyOrg), SqlName_GetById, new { pkid = pkId });
                rst = (val == null || val.Count() < 1) ?
                    OptResult.Build(ResultCode.DataNotFound, Msg_GetById) :
                    OptResult.Build(ResultCode.Success, Msg_GetById, val.First());
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_GetById, ex);
                rst = OptResult.Build(ResultCode.DbError, Msg_GetById);
            }
            return rst;
        }

        public OptResult PageQuery(PageQuery page)
        {
            return null;
        }
    }
}
