using DapperExtensions;
using MyNet.Components.Extensions;
using MyNet.Components.Result;
using MyNet.CustomQuery.Model;
using MyNet.CustomQuery.Repository;
using MyNet.Model;
using MyNet.Repository.Db;
using MyNet.Service;
using System;
using System.Collections.Generic;

namespace MyNet.CustomQuery.Service
{
    public class TableService : BaseService<Table>
    {
        const string Msg_QueryByPage = "分页获取查询表信息";
        const string Msg_AddTable = "新增查询表信息";
        const string Msg_UpdateTable = "修改查询表信息";
        const string Msg_BatchDeleteTable = "批量删除查询表信息";
        const string Msg_QueryWithFields = "获取查询表信息（包含查询字段信息）";
        const string Msg_GetDbTables = "获取数据库所有表信息";
        const string Msg_Init = "初始化自定义查询信息";

        const string SqlName_PageQuery = "pagequery";
        const string SqlName_QueryWithFields = "querywithfields";
        const string SqlName_GetDbTables = "getdbtables";

        TableRepository _tableRep;
        FieldRepository _fieldRep;
        public TableService(IDbSession session, TableRepository tableRep, FieldRepository fieldRep) : base(session, tableRep)
        {
            _tableRep = tableRep;
            _fieldRep = fieldRep;
        }

        public OptResult GetTableWithFields(Dictionary<string, string> conditions)
        {
            OptResult rst = null;
            string where = string.Empty;
            if (!conditions.IsEmpty())
            {
                if (conditions.ContainsKey("id") && !conditions["id"].IsEmpty())
                {
                    where += string.Format(" and qt.id = '{0}' ", conditions["id"]);
                }
                if (conditions.ContainsKey("tbname") && !conditions["tbname"].IsEmpty())
                {
                    where += string.Format(" and qt.tbname = '{0}' ", conditions["tbname"]);
                }
                if (conditions.ContainsKey("alias") && !conditions["alias"].IsEmpty())
                {
                    where += string.Format(" and qt.alias = '{0}' ", conditions["alias"]);
                }
                if (conditions.ContainsKey("comment") && !conditions["comment"].IsEmpty())
                {
                    where += string.Format(" and qt.comment = '{0}' ", conditions["comment"]);
                }
            }
            try
            {
                var lookUp = new Dictionary<string, Table>();
                var sql = _tableRep.GetSql(SqlName_QueryWithFields);
                var tables = _tableRep.Query<Table, Field, Table>(sql,
                    (t, f) =>
                        {
                            Table tt = null;
                            if (!lookUp.TryGetValue(t.id, out tt))
                            {
                                lookUp.Add(t.id, tt = t);
                            }
                            tt.fields.Add(f);
                            return t;
                        }, splitOn: "id,id");
                rst = OptResult.Build(ResultCode.Success, Msg_QueryByPage, new
                {
                    rows = lookUp.Values
                });
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_QueryWithFields, ex);
                rst = OptResult.Build(ResultCode.DbError, Msg_QueryWithFields);
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
            PageQuerySqlEntity sqlEntity = _tableRep.GetPageQuerySql(SqlName_PageQuery);
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
                if (page.conditions.ContainsKey("alias") && !page.conditions["alias"].IsEmpty())
                {
                    sqlEntity.where.AppendFormat(" and qt.alias like '%{0}%' ", page.conditions["alias"]);
                }
                if (page.conditions.ContainsKey("comment") && !page.conditions["comment"].IsEmpty())
                {
                    sqlEntity.where.AppendFormat(" and qt.comment like '%{0}%' ", page.conditions["comment"]);
                }
            }
            #endregion
            try
            {
                var fields = _tableRep.PageQueryBySp<Table>(sqlEntity: sqlEntity, page: page);
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

        public OptResult Add(Table table)
        {
            OptResult rst = null;
            if (table == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, string.Format("{0}，参数不能为空！", Msg_AddTable));
                return rst;
            }
            //1、表名或别名是否存在
            if (IsRepeat(table))
            {
                rst = OptResult.Build(ResultCode.DataRepeat, string.Format("{0},查询表名称或别名已存在！", Msg_AddTable));
                return rst;
            }
            //2、插入数据库
            table.id = GuidExtension.GetOne();
            try
            {
                _tableRep.Insert(table);

                rst = OptResult.Build(ResultCode.Success, Msg_AddTable);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_AddTable, ex);
                rst = OptResult.Build(ResultCode.DbError, Msg_AddTable);
            }
            return rst;
        }

        public OptResult Update(Table table)
        {
            OptResult rst = null;
            if (table == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, string.Format("{0}，参数不能为空！", Msg_UpdateTable));
                return rst;
            }
            //1、指定id是否存在
            var oldTable = _tableRep.GetById(table.id);
            if (oldTable == null)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, string.Format("{0}，未找到数据！", Msg_UpdateTable));
                return rst;
            }
            //2、是否重复
            if (IsRepeat(table))
            {
                rst = OptResult.Build(ResultCode.DataRepeat, string.Format("{0},查询表名称或别名已存在！", Msg_UpdateTable));
                return rst;
            }
            //3、更新
            try
            {
                oldTable.tbname = table.tbname;
                oldTable.alias = table.alias;
                oldTable.comment = table.comment;
                bool val = _tableRep.Update(oldTable);
                rst = OptResult.Build(val ? ResultCode.Success : ResultCode.Fail, Msg_UpdateTable);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_UpdateTable, ex);
                rst = OptResult.Build(ResultCode.DbError, Msg_UpdateTable);
            }
            return rst;
        }

        public OptResult DeleteBatch(IList<string> ids)
        {
            OptResult rst = null;
            if (ids.IsEmpty())
            {
                rst = OptResult.Build(ResultCode.ParamError, string.Format("{0}，参数不能为空！", Msg_BatchDeleteTable));
                return rst;
            }
            //1、是否存在
            var where = Predicates.Field<Table>(t => t.id, Operator.Eq, ids);
            var count = _tableRep.Count(where);
            if (count != ids.Count)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, Msg_BatchDeleteTable);
                return rst;
            }
            //2、是否存在查询字段信息
            if (HasFields(ids))
            {
                rst = OptResult.Build(ResultCode.DataInUse, string.Format("{0}，存在对应的查询字段信息！", Msg_BatchDeleteTable));
                return rst;
            }
            //3、删
            try
            {
                bool val = _tableRep.Delete(where);
                rst = OptResult.Build(val ? ResultCode.Success : ResultCode.Fail, Msg_BatchDeleteTable);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_BatchDeleteTable, ex);
                rst = OptResult.Build(ResultCode.DbError, Msg_BatchDeleteTable);
            }
            return rst;
        }

        private bool IsRepeat(Table table)
        {
            PredicateGroup gp = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            //表名、别名
            PredicateGroup gp1 = new PredicateGroup { Operator = GroupOperator.Or, Predicates = new List<IPredicate>() };
            gp1.Predicates.Add(Predicates.Field<Table>(t => t.tbname, Operator.Eq, table.tbname));
            gp1.Predicates.Add(Predicates.Field<Table>(t => t.alias, Operator.Eq, table.alias));
            gp.Predicates.Add(gp1);
            //排除主键
            if (table.id.IsNotEmpty())
            {
                gp.Predicates.Add(Predicates.Field<Table>(t => t.id, Operator.Eq, table.id, true));//主键不等
            }

            var count = _tableRep.Count(gp);
            return count > 0;
        }

        private bool HasFields(IEnumerable<string> tbIds)
        {
            if (tbIds.IsEmpty())
            {
                return false;
            }

            var count = _fieldRep.Count(Predicates.Field<Field>(f => f.tbid, Operator.Eq, tbIds));

            return count > 0;
        }

        public OptResult GetDbTables()
        {
            OptResult rst = null;
            try
            {
                var dbTables = _tableRep.QueryBySqlName<DbTable>(SqlName_GetDbTables);
                rst = OptResult.Build(ResultCode.Success, Msg_GetDbTables, new { rows = dbTables });
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_GetDbTables, ex);
                rst = OptResult.Build(ResultCode.DbError, Msg_GetDbTables);
            }

            return rst;
        }


        public OptResult Init(IEnumerable<Table> tables)
        {
            OptResult rst = null;
            if (tables.IsEmpty())
            {
                rst = OptResult.Build(ResultCode.ParamError, string.Format("{0}，参数不能为空！", Msg_Init));
                return rst;
            }
            //预处理
            List<Field> fields = new List<Field>();
            foreach (var t in tables)
            {
                //主键、别名
                t.id = GuidExtension.GetOne();
                if (t.alias.IsEmpty())
                {
                    t.alias = t.tbname;
                }
                fields.AddRange(t.fields);
            }
            var tran = _tableRep.Begin();
            try
            {
                //1、清除查询表所有信息、查询字段所有信息
                //TODO，如果后续还有查询模板啥的，是不是都得清除？
                _fieldRep.Delete(Predicates.Field<Field>(f => f.id, Operator.Eq, null, true), tran);
                _tableRep.Delete(Predicates.Field<Table>(t => t.id, Operator.Eq, null, true), tran);
                //2、新增
                _tableRep.InsertBatch(tables, tran);
                if (fields.IsNotEmpty())
                {
                    _fieldRep.InsertBatch(fields, tran);
                }
                _tableRep.Commit();
                rst = OptResult.Build(ResultCode.Success, Msg_Init);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_Init, ex);
                rst = OptResult.Build(ResultCode.DbError, Msg_Init);
            }
            return rst;
        }

    }
}
