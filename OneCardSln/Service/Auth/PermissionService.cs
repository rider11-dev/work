using DapperExtensions;
using OneCardSln.Components.Extensions;
using OneCardSln.Model;
using OneCardSln.Model.Auth;
using OneCardSln.Model.Base;
using OneCardSln.Repository.Auth;
using OneCardSln.Repository.Base;
using OneCardSln.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.Service.Auth
{
    public class PermissionService
    {
        //常量
        const string Msg_AddPer = "新增权限";
        const string Msg_UpdatePer = "修改权限";
        const string Msg_DeletePer = "删除权限";
        const string Msg_QueryByPage = "分页查询权限信息";

        //私有变量
        private PermissionRepository _perRep;
        private UserPermissionRelRepository _usrPerRelRep;
        private DictRepository _dictRep;

        public PermissionService(PermissionRepository perRep, UserPermissionRelRepository usrPerRelRep, DictRepository dictRep)
        {
            _perRep = perRep;
            _usrPerRelRep = usrPerRelRep;
            _dictRep = dictRep;
        }

        public OptResult Add(Permission per)
        {
            OptResult rst = null;

            //1、校验权限编号是否已存在
            var count = _perRep.Count(Predicates.Field<Permission>(p => p.per_code, Operator.Eq, per.per_code));
            if (count > 0)
            {
                rst = OptResult.Build(ResultCode.DataRepeat, string.Format("{0}，编号为{1}的权限已存在！", Msg_AddPer, per.per_code));
                return rst;
            }

            //2、权限类型是否合法
            string msg = "";
            if (!ValidatePermissionType(per.per_type, out msg))
            {
                rst = OptResult.Build(ResultCode.ParamError, Msg_AddPer + "，" + msg);
                return rst;
            }

            //3、上级权限是否存在
            if (!ValidateParent(per.per_parent, out msg))
            {
                rst = OptResult.Build(ResultCode.ParamError, Msg_AddPer + "，" + msg);
                return rst;
            }

            per.per_id = GuidExtension.GetOne();
            _perRep.Insert(per);

            rst = OptResult.Build(ResultCode.Success, Msg_AddPer);

            return rst;
        }

        public OptResult Find(dynamic pkId)
        {
            var per = _perRep.GetById(pkId as object);
            return OptResult.Build(ResultCode.Success, null, per);
        }

        public OptResult Update(Permission per)
        {
            OptResult rst = null;
            //1、权限是否存在
            var count = _perRep.Count(Predicates.Field<Permission>(p => p.per_id, Operator.Eq, per.per_id));
            if (count < 1)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, Msg_UpdatePer);
                return rst;
            }
            //2、权限类型
            string msg = "";
            if (!ValidatePermissionType(per.per_type, out msg))
            {
                rst = OptResult.Build(ResultCode.ParamError, Msg_UpdatePer + "，" + msg);
                return rst;
            }
            //3、父级权限
            if (!ValidateParent(per.per_parent, out msg))
            {
                rst = OptResult.Build(ResultCode.ParamError, Msg_UpdatePer + "，" + msg);
                return rst;
            }

            //2、更新
            var oldPer = _perRep.GetById(per.per_id);
            oldPer.per_name = per.per_name;
            oldPer.per_type = per.per_type;
            oldPer.per_remark = per.per_remark;
            oldPer.per_parent = per.per_parent;

            bool val = _perRep.Update(oldPer);
            rst = OptResult.Build(val ? ResultCode.Success : ResultCode.Fail, Msg_UpdatePer);

            return rst;
        }

        public OptResult Delete(dynamic pkId)
        {
            OptResult rst = null;

            //1、权限是否存在
            var per = _perRep.GetById(pkId);
            if (per == null)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, Msg_DeletePer);
                return rst;
            }
            //2、是否包含下级权限
            var count = _perRep.Count(Predicates.Field<Permission>(p => p.per_parent, Operator.Eq, pkId as object));
            if (count > 0)
            {
                rst = OptResult.Build(ResultCode.DataInUse, string.Format("{0}，存在下级权限", Msg_DeletePer));
                return rst;
            }
            //3、是否已被分配
            count = _usrPerRelRep.Count(Predicates.Field<UserPermissionRel>(r => r.rel_permissionid, Operator.Eq, pkId as object));
            if (count > 0)
            {
                rst = OptResult.Build(ResultCode.DataInUse, string.Format("{0}，已分配到用户", Msg_DeletePer));
                return rst;
            }
            //4、删除
            var val = _perRep.Delete(Predicates.Field<Permission>(p => p.per_id, Operator.Eq, pkId as object));
            rst = OptResult.Build(val ? ResultCode.Success : ResultCode.Fail, Msg_DeletePer);

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
            page.Verify();
            //1、过滤条件
            PredicateGroup pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            if (page.conditions != null && page.conditions.Count > 0)
            {
                if (page.conditions.ContainsKey("per_type"))
                {
                    pg.Predicates.Add(Predicates.Field<Permission>(p => p.per_type, Operator.Eq, page.conditions["per_type"]));
                }
                if (page.conditions.ContainsKey("per_code"))
                {
                    pg.Predicates.Add(Predicates.Field<Permission>(p => p.per_code, Operator.Like, "%" + page.conditions["per_code"] + "%"));
                }
                if (page.conditions.ContainsKey("per_name"))
                {
                    pg.Predicates.Add(Predicates.Field<Permission>(p => p.per_name, Operator.Like, "%" + page.conditions["per_name"] + "%"));
                }
                if (page.conditions.ContainsKey("per_parent"))
                {
                    pg.Predicates.Add(Predicates.Field<Permission>(p => p.per_parent, Operator.Eq, page.conditions["per_parent"]));
                }
            }
            //2、排序
            long total = 0;
            IList<ISort> sort = new[]
            {
                new Sort{PropertyName="per_parent",Ascending=true},
                new Sort{PropertyName="per_code",Ascending=true},
            };
            var pers = _perRep.GetPageList(page.pageIndex, page.pageSize, out total, sort, pg);
            rst = OptResult.Build(ResultCode.Success, Msg_QueryByPage, new
            {
                total = total,
                rows = pers
            });

            return rst;
        }

        //私有方法
        //TODO
        /// <summary>
        /// 权限类型是否合法，暂时写死，应该从数据库基础字典查询
        /// </summary>
        /// <param name="perType"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool ValidatePermissionType(string perType, out string msg)
        {
            msg = "";
            var val = _dictRep.Count(Predicates.Field<Dict>(d => d.dict_id, Operator.Eq, perType)) > 0;

            msg = val ? "" : "权限类型不存在！";

            return val;
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
                var count = _perRep.Count(Predicates.Field<Permission>(p => p.per_id, Operator.Eq, parent));
                if (count < 1)
                {
                    msg = "父级权限不存在！";
                    return false;
                }
            }

            return true;
        }

    }
}
