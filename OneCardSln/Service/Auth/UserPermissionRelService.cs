using DapperExtensions;
using OneCardSln.Components.Extensions;
using OneCardSln.Components.Result;
using OneCardSln.Model;
using OneCardSln.Model.Auth;
using OneCardSln.Repository.Auth;
using OneCardSln.Repository.Db;
using OneCardSln.Service.Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.Service.Auth
{
    public class UserPermissionRelService : BaseService<UserPermissionRel>
    {
        //常量
        const string Msg_AssignPer = "给用户分配权限";
        const string Msg_GetPer = "获取用户权限";

        //私有变量
        private UserRepository _usrRep;
        private UserPermissionRelRepository _usrPerRelRep;

        public UserPermissionRelService(IDbSession session, UserPermissionRelRepository usrPerRelRep, UserRepository usrRep)
            : base(session, usrPerRelRep)
        {
            _usrPerRelRep = usrPerRelRep;
            _usrRep = usrRep;
        }

        /// <summary>
        /// 给用户分配权限
        /// </summary>
        /// <param name="usrId"></param>
        /// <param name="perIds">新的权限id列表</param>
        /// <param name="assignAll">是否分配所有权限，如果true，则忽略perIds</param>
        /// <returns></returns>
        public OptResult AssignPermissions(string usrId, List<string> perIds = null, bool assignAll = false)
        {
            OptResult rst = null;
            //1、用户是否存在
            var usr = _usrRep.GetById(usrId);
            if (usr == null)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, string.Format("{0}失败，用户不存在！", Msg_AssignPer));
                return rst;
            }
            //新权限（非数据库操作尽量放到事务外）
            var newRels = new List<UserPermissionRel>();
            if (!assignAll && perIds != null && perIds.Count > 0)
            {
                //去重
                perIds.Distinct().ToList()
                .ForEach(newPerId =>
                {
                    newRels.Add(new UserPermissionRel { rel_id = GuidExtension.GetOne(), rel_userid = usrId, rel_permissionid = newPerId });
                });
            }
            if (assignAll)
            {
                newRels.Add(new UserPermissionRel { rel_id = GuidExtension.GetOne(), rel_userid = usrId, rel_permissionid = "*" });
            }
            var tran = _usrPerRelRep.Begin();
            try
            {
                //2、清空用户当前所有权限
                bool val = _usrPerRelRep.Delete(Predicates.Field<UserPermissionRel>(r => r.rel_userid, Operator.Eq, usrId), tran);
                //3、添加新权限
                if (newRels.Count > 0)
                {
                    _usrPerRelRep.InsertBatch(newRels, tran);
                }
                tran.Commit();
                rst = OptResult.Build(ResultCode.Success, Msg_AssignPer);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_AssignPer, ex);
                rst = OptResult.Build(ResultCode.DbError, Msg_AssignPer);
            }

            return rst;
        }

        public OptResult GetPermissions(string usrId)
        {
            OptResult rst = null;
            //1、用户名是否存在
            var usr = _usrRep.GetById(usrId);
            if (usr == null)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, string.Format("{0}失败，用户不存在！", Msg_GetPer));
                return rst;
            }
            //2、执行查询
            try
            {
                var pers = _usrPerRelRep.QueryBySqlName(typeof(UserPremissionDto), "getper", new { user_id = usrId });

                rst = OptResult.Build(ResultCode.Success, Msg_GetPer, new { user_id = usrId, pers = pers });
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_GetPer, ex);
                rst = OptResult.Build(ResultCode.DbError, Msg_GetPer);
            }
            return rst;
        }
    }
}
