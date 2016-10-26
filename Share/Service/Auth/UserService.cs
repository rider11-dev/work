using MyNet.Model;
using MyNet.Repository.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions;
using MyNet.Model.Auth;
using MyNet.Components;
using System.Dynamic;
using MyNet.Components.Extensions;
using MyNet.Components.Mapper;
using MyNet.Repository.Db;
using MyNet.Components.Result;
using MyNet.Dto.Auth;

namespace MyNet.Service.Auth
{
    public class UserService : BaseService<User>
    {
        //常量
        const string Msg_Login = "用户登录";
        const string Msg_AddUser = "新增用户";
        const string Msg_UpdateUser = "修改用户";
        const string Msg_DeleteUser = "删除用户";
        const string Msg_BatchDeleteUser = "批量删除用户";
        const string Msg_ChangePwd = "修改密码";
        const string Msg_QueryByPage = "分页查询用户信息";

        const string SqlName_Update = "update";

        //私有变量
        private UserRepository _usrRep;

        public UserService(IDbSession session, UserRepository usrRep)
            : base(session, usrRep)
        {
            _usrRep = usrRep;
        }

        public OptResult Login(string username, string pwd)
        {
            OptResult rst = null;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(pwd))
            {
                rst = OptResult.Build(ResultCode.ParamError, Msg_Login + "失败,用户名或密码不能为空");
                return rst;
            }
            //用户名
            var where = Predicates.Field<User>(u => u.user_name, Operator.Eq, username);
            var usr = _usrRep.GetList(where).FirstOrDefault();
            if (usr == null)
            {
                rst = OptResult.Build(ResultCode.Fail, Msg_Login + "失败,用户不存在");
                return rst;
            }
            //密码
            var pwdHash = EncryptionExtension.GetMd5Hash(pwd);
            if (string.Equals(pwdHash, usr.user_pwd))
            {
                rst = OptResult.Build(ResultCode.Success, null, usr);
            }
            else
            {
                rst = OptResult.Build(ResultCode.Fail, Msg_Login + "失败,密码不正确");
            }

            return rst;
        }

        public OptResult Add(User usr)
        {
            OptResult rst = null;
            //1、用户名是否已存在
            PredicateGroup pg = new PredicateGroup { Operator = GroupOperator.Or, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<User>(u => u.user_name, Operator.Eq, usr.user_name));
            pg.Predicates.Add(Predicates.Field<User>(u => u.user_idcard, Operator.Eq, usr.user_idcard));
            var count = _usrRep.Count(pg);
            if (count > 0)
            {
                rst = OptResult.Build(ResultCode.DataRepeat,
                    string.Format("{0}，用户{1}或身份证号{2}已存在", Msg_AddUser, usr.user_name, usr.user_idcard));
                return rst;
            }
            //2、处理
            usr.user_id = GuidExtension.GetOne();
            usr.user_pwd = EncryptionExtension.GetMd5Hash(usr.user_idcard.Substring(usr.user_idcard.Length - 6, 6));//初始密码身份证后六位
            try
            {
                var val = _usrRep.Insert(usr);

                //3、新增用户默认权限
                //TODO

                rst = OptResult.Build(ResultCode.Success, Msg_AddUser);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_AddUser, ex);
                rst = OptResult.Build(ResultCode.DbError, Msg_AddUser);
            }
            return rst;
        }

        public OptResult Update(User usr)
        {
            OptResult rst = null;
            //1、用户是否存在
            var count = _usrRep.Count(Predicates.Field<User>(u => u.user_id, Operator.Eq, usr.user_id));
            if (count < 1)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, Msg_UpdateUser);
                return rst;
            }
            //2、用户名不能修改，这里只检查身份证号即可（身份证号）
            PredicateGroup pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<User>(u => u.user_idcard, Operator.Eq, usr.user_idcard));
            pg.Predicates.Add(Predicates.Field<User>(u => u.user_id, Operator.Eq, usr.user_id, true));//user_id <> usr.user_id
            count = _usrRep.Count(pg);
            if (count > 0)
            {
                rst = OptResult.Build(ResultCode.Fail,
                    string.Format("{0}，身份证号{1}已存在", Msg_UpdateUser, usr.user_idcard));
                return rst;
            }
            //3、更新：获取旧对象，赋值，更新
            //TODO
            //这里修改时，需要传递完整实体信息，加大网络传输量，后续改造成只修改部分字段：vm字段设置dynamic？
            try
            {
                var oldUsr = _usrRep.GetById(usr.user_id);
                oldUsr.user_idcard = usr.user_idcard;
                oldUsr.user_regioncode = usr.user_regioncode;
                oldUsr.user_truename = usr.user_truename;
                oldUsr.user_remark = usr.user_remark;
                oldUsr.user_group = usr.user_group;

                bool val = _usrRep.Update(oldUsr);
                rst = OptResult.Build(val ? ResultCode.Success : ResultCode.Fail, Msg_UpdateUser);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_UpdateUser, ex);
                rst = OptResult.Build(ResultCode.DbError, Msg_UpdateUser);
            }
            return rst;
        }

        public OptResult Delete(dynamic pkId)
        {
            OptResult rst = null;
            //1、用户是否存在
            var predicate = Predicates.Field<User>(u => u.user_id, Operator.Eq, pkId as object);
            var count = _usrRep.Count(predicate);
            if (count < 1)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, Msg_DeleteUser);
                return rst;
            }
            //2、超级管理员不能被删除（这里只根据用户名判断，后续可以扩展根据角色或其他规则）
            var usr = _usrRep.GetById(pkId);
            if (usr.user_name.Equals("admin"))
            {
                rst = OptResult.Build(ResultCode.Fail, string.Format(Msg_DeleteUser + "，用户{0}不允许删除", usr.user_name));
                return rst;
            }
            try
            {
                //TODO，应该起事务，删除用户权限
                bool val = _usrRep.Delete(predicate);
                rst = OptResult.Build(val ? ResultCode.Success : ResultCode.Fail, Msg_DeleteUser);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_DeleteUser, ex);
                rst = OptResult.Build(ResultCode.DbError, Msg_DeleteUser);
            }
            return rst;
        }

        public OptResult DeleteBatch(IEnumerable<string> ids)
        {
            OptResult rst = null;
            //1、用户是否存在
            var predicate = Predicates.Field<User>(u => u.user_id, Operator.Eq, ids);
            var count = _usrRep.Count(predicate);
            if (count < ids.Count())
            {
                rst = OptResult.Build(ResultCode.DataNotFound, Msg_BatchDeleteUser);
                return rst;
            }
            //2、超级管理员不能被删除（这里只根据用户名判断，后续可以扩展根据角色或其他规则）
            PredicateGroup pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate> { predicate } };
            pg.Predicates.Add(Predicates.Field<User>(u => u.user_name, Operator.Eq, "admin"));
            count = _usrRep.Count(pg);
            if (count > 0)
            {
                rst = OptResult.Build(ResultCode.IllegalOpt, Msg_BatchDeleteUser + "，管理员不允许删除");
                return rst;
            }

            try
            {
                //TODO，应该起事务，删除用户权限
                bool val = _usrRep.Delete(predicate);
                rst = OptResult.Build(val ? ResultCode.Success : ResultCode.Fail, Msg_BatchDeleteUser);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_BatchDeleteUser, ex);
                rst = OptResult.Build(ResultCode.DbError, Msg_BatchDeleteUser);
            }
            return rst;
        }

        public OptResult ChangePwd(string userid, string oldpwd, string newpwd)
        {
            OptResult rst = null;
            //1、新旧密码是否相同
            if (string.Equals(oldpwd, newpwd))
            {
                rst = OptResult.Build(ResultCode.ParamError, Msg_ChangePwd + "，新旧密码不能相同");
                return rst;
            }

            //2、用户是否存在
            var usr = _usrRep.GetById(userid);
            if (usr == null)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, Msg_ChangePwd + "，指定用户不存在");
                return rst;
            }

            //3、旧密码是否正确
            if (!EncryptionExtension.GetMd5Hash(oldpwd).Equals(usr.user_pwd))
            {
                rst = OptResult.Build(ResultCode.DataNotFound, Msg_ChangePwd + "，旧密码不正确");
                return rst;
            }

            //4、执行sql
            try
            {
                var val = _usrRep.UpdateBySqlName(SqlName_Update, new { user_pwd = EncryptionExtension.GetMd5Hash(newpwd), user_id = userid }, new string[] { "user_pwd" });
                rst = OptResult.Build(val > 0 ? ResultCode.Success : ResultCode.Fail, Msg_ChangePwd);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_ChangePwd, ex);
                rst = OptResult.Build(ResultCode.DbError, Msg_ChangePwd);
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
            page.Verify();
            //过滤条件
            PredicateGroup pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            if (page.conditions != null && page.conditions.Count > 0)
            {
                if (page.conditions.ContainsKey("regioncode") && !page.conditions["regioncode"].IsEmpty())
                {
                    pg.Predicates.Add(Predicates.Field<User>(u => u.user_regioncode, Operator.Eq, page.conditions["regioncode"]));
                }
                if (page.conditions.ContainsKey("username") && !page.conditions["username"].IsEmpty())
                {
                    pg.Predicates.Add(Predicates.Field<User>(u => u.user_name, Operator.Like, "%" + page.conditions["username"] + "%"));
                }
                if (page.conditions.ContainsKey("truename") && !page.conditions["truename"].IsEmpty())
                {
                    pg.Predicates.Add(Predicates.Field<User>(u => u.user_truename, Operator.Like, "%" + page.conditions["truename"] + "%"));
                }
                if (page.conditions.ContainsKey("group") && !page.conditions["group"].IsEmpty())
                {
                    pg.Predicates.Add(Predicates.Field<User>(u => u.user_truename, Operator.Eq, page.conditions["group"]));
                }
                else if (page.conditions.ContainsKey("group_name") && !page.conditions["group_name"].IsEmpty())
                {
                    //TODO
                    //用户所属组织模糊查询（方法很笨，考虑后续优化）
                    var groups = _usrRep.GetList<Group>(Predicates.Field<Group>(gp => gp.gp_name, Operator.Like, "%" + page.conditions["group_name"] + "%"));
                    if (groups == null || groups.Count() < 1)
                    {
                        rst = OptResult.Build(ResultCode.Success, Msg_QueryByPage, new
                        {
                            total = 0
                        });
                        return rst;
                    }
                    pg.Predicates.Add(Predicates.Field<User>(u => u.user_group, Operator.Eq, groups.Select(gp => gp.gp_id)));
                }
            }

            //排序
            long total = 0;
            IList<ISort> sort = new[]
            {
                new Sort{PropertyName="user_regioncode",Ascending=true}
            };
            //这里返回UserDto，忽略密码字段
            try
            {
                var usrs = _usrRep.GetPageList<UserDto>(page.pageIndex, page.pageSize, out total, sort, pg);
                rst = OptResult.Build(ResultCode.Success, Msg_QueryByPage, new
                {
                    total = total,
                    rows = usrs
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
