using DapperExtensions;
using MyNet.Components.Extensions;
using MyNet.Components.Result;
using MyNet.Dto.Auth;
using MyNet.Model;
using MyNet.Model.Auth;
using MyNet.Repository.Auth;
using MyNet.Repository.Db;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Service.Auth
{
    public class GroupService : BaseService<Group>
    {
        //常量
        const string Msg_AddGroup = "新增组织";
        const string Msg_UpdateGroup = "修改组织";
        const string Msg_DeleteGroup = "删除组织";
        const string Msg_BatchDeleteGroup = "批量删除组织";
        const string Msg_QueryByPage = "分页查询组织信息";
        const string Msg_GetAll = "获取所有组织信息";

        const string SqlName_HasChild = "haschild";
        const string SqlName_PageQuery = "pagequery";

        //私有变量
        private GroupRepository _groupRep;
        private UserRepository _usrRep;

        public GroupService(IDbSession session, GroupRepository groupRep, UserRepository usrRep)
            : base(session, groupRep)
        {
            _groupRep = groupRep;
            _usrRep = usrRep;
        }

        public OptResult Add(Group group)
        {
            OptResult rst = null;

            //1、校验组织编号是否已存在
            var count = _groupRep.Count(Predicates.Field<Group>(gp => gp.gp_code, Operator.Eq, group.gp_code));
            if (count > 0)
            {
                rst = OptResult.Build(ResultCode.DataRepeat, string.Format("{0}，编号为{1}的组织已存在！", Msg_AddGroup, group.gp_code));
                return rst;
            }

            //2、上级组织是否存在
            string msg = "";
            if (!ValidateParent(group.gp_parent, out msg))
            {
                rst = OptResult.Build(ResultCode.ParamError, Msg_AddGroup + "，" + msg);
                return rst;
            }

            group.gp_id = GuidExtension.GetOne();
            try
            {
                _groupRep.Insert(group);

                rst = OptResult.Build(ResultCode.Success, Msg_AddGroup);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_AddGroup, ex);
                rst = OptResult.Build(ResultCode.DbError, Msg_AddGroup);
            }
            return rst;
        }

        public OptResult Update(Group group)
        {
            OptResult rst = null;
            //1、组织是否存在
            var count = _groupRep.Count(Predicates.Field<Group>(gp => gp.gp_id, Operator.Eq, group.gp_id));
            if (count < 1)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, Msg_UpdateGroup);
                return rst;
            }

            //1、父级组织
            string msg = "";
            if (!ValidateParent(group.gp_parent, out msg))
            {
                rst = OptResult.Build(ResultCode.ParamError, Msg_UpdateGroup + "，" + msg);
                return rst;
            }
            try
            {
                //2、更新
                //TODO（先查询，后更新，有点儿笨）
                var oldGroup = _groupRep.GetById(group.gp_id);
                oldGroup.gp_name = group.gp_name;
                oldGroup.gp_system = group.gp_system;
                oldGroup.gp_parent = group.gp_parent;
                oldGroup.gp_sort = group.gp_sort;

                bool val = _groupRep.Update(oldGroup);
                rst = OptResult.Build(val ? ResultCode.Success : ResultCode.Fail, Msg_UpdateGroup);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_UpdateGroup, ex);
                rst = OptResult.Build(ResultCode.DbError, Msg_UpdateGroup);
            }
            return rst;
        }

        public OptResult DeleteBatch(IEnumerable<string> ids)
        {
            OptResult rst = null;
            //1、组织是否存在
            var predicate1 = Predicates.Field<Group>(gp => gp.gp_id, Operator.Eq, ids);
            var count = _groupRep.Count(predicate1);
            if (count < ids.Count())
            {
                rst = OptResult.Build(ResultCode.DataNotFound, Msg_BatchDeleteGroup);
                return rst;
            }
            //2、系统预制不允许删除
            PredicateGroup pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(predicate1);
            var predicate2 = Predicates.Field<Group>(gp => gp.gp_system, Operator.Eq, true);
            pg.Predicates.Add(predicate2);
            count = _groupRep.Count(pg);
            if (count > 0)
            {
                rst = OptResult.Build(ResultCode.DataSystem, Msg_BatchDeleteGroup + "失败");
                return rst;
            }
            //3、admin不允许删除
            pg.Predicates.Remove(predicate2);
            predicate2 = Predicates.Field<Group>(gp => gp.gp_code, Operator.Eq, "admin");
            pg.Predicates.Add(predicate2);
            count = _groupRep.Count(pg);
            if (count > 0)
            {
                rst = OptResult.Build(ResultCode.DataAddmin, Msg_BatchDeleteGroup + "失败");
                return rst;
            }
            //3、是否包含下级组织
            var hasChild = HasChild(ids);
            if (hasChild)
            {
                rst = OptResult.Build(ResultCode.DataInUse, string.Format("{0}，存在下级组织", Msg_BatchDeleteGroup));
                return rst;
            }
            //4、是否已被分配
            var assigned = HasUser(ids);
            if (assigned)
            {
                rst = OptResult.Build(ResultCode.DataInUse, string.Format("{0}，已分配到用户", Msg_BatchDeleteGroup));
                return rst;
            }
            //删除
            try
            {
                bool val = _groupRep.Delete(predicate1);
                rst = OptResult.Build(val ? ResultCode.Success : ResultCode.Fail, Msg_BatchDeleteGroup);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_BatchDeleteGroup, ex);
                rst = OptResult.Build(ResultCode.DbError, Msg_BatchDeleteGroup);
            }
            return rst;
        }

        public OptResult QueryByPage(PageQuery page)
        {
            OptResult rst = null;
            if (page == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, Msg_QueryByPage + "，分页参数不能为空！");
                return rst;
            }

            PageQuerySqlEntity sqlEntity = _groupRep.GetPageQuerySql(SqlName_PageQuery);
            if (sqlEntity == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, Msg_QueryByPage + "，未能获取sql配置！");
                return rst;
            }
            //构造where
            #region where条件
            if (page.conditions != null && page.conditions.Count > 0)
            {
                if (page.conditions.ContainsKey("gp_code") && !page.conditions["gp_code"].IsEmpty())
                {
                    sqlEntity.where.AppendFormat(" and gp.gp_code='{0}' ", page.conditions["gp_code"]);
                }
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
                var groups = _groupRep.PageQueryBySp<GroupDto>(sqlEntity: sqlEntity, page: page);
                rst = OptResult.Build(ResultCode.Success, Msg_QueryByPage, new
                {
                    total = page.total,
                    pagecount = page.pageTotal,
                    rows = groups
                });
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_QueryByPage, ex);
                rst = OptResult.Build(ResultCode.DbError, Msg_QueryByPage);
            }
            return rst;
        }

        public OptResult GetAll()
        {
            OptResult rst = null;
            try
            {
                var groups = _groupRep.GetList(null);
                rst = OptResult.Build(ResultCode.Success, Msg_GetAll, groups);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_GetAll, ex);
                rst = OptResult.Build(ResultCode.DbError, Msg_GetAll);
            }

            return rst;
        }

        /// <summary>
        /// 验证上级是否存在
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="msg"></param>
        /// <returns>上级不为空且不存在时，返回false；其他返回true</returns>
        private bool ValidateParent(string parent, out string msg)
        {
            msg = "";
            if (!string.IsNullOrEmpty(parent))
            {
                var count = _groupRep.Count(Predicates.Field<Group>(gp => gp.gp_code, Operator.Eq, parent));
                if (count < 1)
                {
                    msg = "上级组织不存在！";
                    return false;
                }
            }

            return true;
        }

        private bool HasChild(IEnumerable<string> ids)
        {
            if (ids == null || ids.Count() < 1)
            {
                return false;
            }
            var sqlPlainText = _groupRep.GetSql(SqlName_HasChild);
            List<string> paraNames = new List<string>();
            var paraValues = new ExpandoObject() as IDictionary<string, Object>;
            int i = 0;
            foreach (var id in ids)
            {
                paraNames.Add("@gp_id_" + i);
                paraValues.Add("gp_id_" + i, id);

                i++;
            }

            string sqlText = string.Format(sqlPlainText, string.Join(",", paraNames));

            var val = _groupRep.ExecuteScalar<int>(sqlText, paraValues);

            return val > 0;
        }

        private bool HasUser(IEnumerable<string> ids)
        {
            if (ids == null || ids.Count() < 1)
            {
                return false;
            }
            var count = _usrRep.Count(Predicates.Field<User>(u => u.user_group, Operator.Eq, ids));
            return count > 0;
        }
    }
}
