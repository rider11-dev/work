using DapperExtensions;
using MyNet.Components.Extensions;
using MyNet.Components.Result;
using MyNet.CustomQuery.Model;
using MyNet.CustomQuery.Model.Dto;
using MyNet.CustomQuery.Repository;
using MyNet.Model;
using MyNet.Repository.Db;
using MyNet.Service;
using System;
using System.Collections.Generic;

namespace MyNet.CustomQuery.Service
{
    public class FieldService : BaseService<Field>
    {
        const string Msg_QueryByPage = "分页获取查询字段信息";
        const string Msg_AddField = "新增查询字段信息";
        const string Msg_UpdateField = "修改查询字段信息";
        const string Msg_BatchDeleteField = "批量删除查询字段信息";

        const string SqlName_PageQuery = "pagequery";

        FieldRepository _fieldRep;
        TableRepository _tableRep;
        public FieldService(IDbSession session, FieldRepository fieldRep, TableRepository tableRep) : base(session, fieldRep)
        {
            _fieldRep = fieldRep;
            _tableRep = tableRep;
        }

        public OptResult QueryByPage(PageQuery page)
        {
            OptResult rst = null;
            if (page == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, Msg_QueryByPage + "，分页参数不能为空！");
                return rst;
            }
            PageQuerySqlEntity sqlEntity = _fieldRep.GetPageQuerySql(SqlName_PageQuery);
            if (sqlEntity == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, Msg_QueryByPage + "，未能获取sql配置！");
                return rst;
            }
            //构造where
            #region where条件
            if (page.conditions != null && page.conditions.Count > 0)
            {
                if (page.conditions.ContainsKey("tbname") && !page.conditions["tbname"].IsEmpty())
                {
                    sqlEntity.where.AppendFormat(" and qt.tbname like '%{0}%' ", page.conditions["tbname"]);
                }
                if (page.conditions.ContainsKey("tbalias") && !page.conditions["tbalias"].IsEmpty())
                {
                    sqlEntity.where.AppendFormat(" and qt.alias like '%{0}%' ", page.conditions["tbalias"]);
                }
                if (page.conditions.ContainsKey("tbid") && !page.conditions["tbid"].IsEmpty())
                {
                    sqlEntity.where.AppendFormat(" and qf.tbid = '{0}' ", page.conditions["tbid"]);
                }
                if (page.conditions.ContainsKey("fieldname") && !page.conditions["fieldname"].IsEmpty())
                {
                    sqlEntity.where.AppendFormat(" and qf.fieldname like '%{0}%' ", page.conditions["fieldname"]);
                }
                if (page.conditions.ContainsKey("displayname") && !page.conditions["displayname"].IsEmpty())
                {
                    sqlEntity.where.AppendFormat(" and qf.displayname like '%{0}%' ", page.conditions["displayname"]);
                }
            }
            #endregion
            try
            {
                var fields = _fieldRep.PageQueryBySp<FieldDto>(sqlEntity: sqlEntity, page: page);
                rst = OptResult.Build(ResultCode.Success, Msg_QueryByPage, new
                {
                    total = page.total,
                    pagecount = page.pageTotal,
                    rows = fields
                });
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_QueryByPage, ex);
                rst = OptResult.Build(ResultCode.DbError, Msg_QueryByPage);
            }
            return rst;
        }

        public OptResult Add(Field field)
        {
            OptResult rst = null;
            if (field == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, string.Format("{0}，参数不能为空！", Msg_AddField));
                return rst;
            }
            //1、字段名是否存在
            if (IsRepeat(field))
            {
                rst = OptResult.Build(ResultCode.DataRepeat, string.Format("{0},字段名称已存在！", Msg_AddField));
                return rst;
            }
            //2、查询表是否存在
            var count = _tableRep.Count(Predicates.Field<Table>(t => t.id, Operator.Eq, field.tbid));
            if (count < 1)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, string.Format("{0},对应查询表不存在！", Msg_AddField));
                return rst;
            }
            //3、插入数据库
            field.id = GuidExtension.GetOne();
            try
            {
                _fieldRep.Insert(field);

                rst = OptResult.Build(ResultCode.Success, Msg_AddField);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_AddField, ex);
                rst = OptResult.Build(ResultCode.DbError, Msg_AddField);
            }
            return rst;
        }

        public OptResult Update(Field field)
        {
            OptResult rst = null;
            if (field == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, string.Format("{0}，参数不能为空！", Msg_UpdateField));
                return rst;
            }
            //1、指定id是否存在
            var oldField = _fieldRep.GetById(field.id);
            if (oldField == null)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, string.Format("{0}，未找到数据！", Msg_UpdateField));
                return rst;
            }
            //2、是否重复
            if (IsRepeat(field))
            {
                rst = OptResult.Build(ResultCode.DataRepeat, string.Format("{0},字段名称已存在！", Msg_UpdateField));
                return rst;
            }
            //3、更新
            try
            {
                oldField.fieldname = field.fieldname;
                oldField.displayname = field.displayname;
                oldField.fieldtype = field.fieldtype;
                oldField.remark = field.remark;
                bool val = _fieldRep.Update(oldField);
                rst = OptResult.Build(val ? ResultCode.Success : ResultCode.Fail, Msg_UpdateField);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_UpdateField, ex);
                rst = OptResult.Build(ResultCode.DbError, Msg_UpdateField);
            }
            return rst;
        }

        public OptResult DeleteBatch(IList<string> ids)
        {
            OptResult rst = null;
            if (ids.IsEmpty())
            {
                rst = OptResult.Build(ResultCode.ParamError, string.Format("{0}，参数不能为空！", Msg_BatchDeleteField));
                return rst;
            }
            //1、是否有不存在的数据
            var where = Predicates.Field<Field>(f => f.id, Operator.Eq, ids);
            var count = _fieldRep.Count(where);
            if (count != ids.Count)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, Msg_BatchDeleteField);
                return rst;
            }
            //2、删
            try
            {
                bool val = _fieldRep.Delete(where);
                rst = OptResult.Build(val ? ResultCode.Success : ResultCode.Fail, Msg_BatchDeleteField);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_BatchDeleteField, ex);
                rst = OptResult.Build(ResultCode.DbError, Msg_BatchDeleteField);
            }
            return rst;
        }

        private bool IsRepeat(Field field)
        {
            //同表，查询字段名是否重复
            PredicateGroup gp = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            gp.Predicates.Add(Predicates.Field<Field>(f => f.tbid, Operator.Eq, field.tbid));
            gp.Predicates.Add(Predicates.Field<Field>(f => f.fieldname, Operator.Eq, field.fieldname));
            //排除主键
            if (field.id.IsNotEmpty())
            {
                gp.Predicates.Add(Predicates.Field<Field>(f => f.id, Operator.Eq, field.id, true));//主键不等
            }

            var count = _fieldRep.Count(gp);
            return count > 0;
        }

    }
}
