using MyNet.CustomQuery.Model;
using MyNet.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNet.Repository;
using MyNet.Repository.Db;
using MyNet.CustomQuery.Repository;
using MyNet.Components.Result;
using MyNet.Components.Extensions;

namespace MyNet.CustomQuery.Service
{
    public class CustomQueryService : BaseService<EmptyModel>
    {
        const string Msg_ExeQuery = "执行通用查询";
        CustomQueryRepository _cqRep;
        const string SqlName_PageQuery = "pagequery";
        public CustomQueryService(IDbSession session, CustomQueryRepository cqRep) : base(session, cqRep)
        {
            _cqRep = cqRep;
        }

        public OptResult Query(QueryModel queryModel)
        {
            OptResult rst = null;
            if (queryModel == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, Msg_ExeQuery + "，查询模型参数不能为空！");
                return rst;
            }
            try
            {
                PageQuerySqlEntity sqlEntity = _cqRep.GetPageQuerySql(SqlName_PageQuery);
                if (sqlEntity == null)
                {
                    rst = OptResult.Build(ResultCode.ParamError, Msg_ExeQuery + "，未能获取sql配置！");
                    return rst;
                }
                //1、查询模型转换
                ConvertQueryModel(queryModel, sqlEntity);
                //2、执行查询
                IEnumerable<dynamic> data = _cqRep.PageQueryBySp<dynamic>(sqlEntity: sqlEntity, page: queryModel.Page);
                rst = OptResult.Build(ResultCode.Success, Msg_ExeQuery, new
                {
                    total = queryModel.Page.total,
                    pagecount = queryModel.Page.pageTotal,
                    rows = data
                });
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_ExeQuery, ex);
                rst = OptResult.Build(ResultCode.DbError, Msg_ExeQuery);
            }
            return rst;
        }

        private void ConvertQueryModel(QueryModel queryModel, PageQuerySqlEntity sqlEntity)
        {
            //1、Fields——fields
            if (queryModel.Fields.IsEmpty())
            {
                throw new Exception("查询字段不能为空！");
            }
            sqlEntity.fields = string.Join(",", queryModel.Fields);
            //2、TableRelations——tables
            if (queryModel.TableRelation == null || queryModel.TableRelation.PrimeTable.IsEmpty())
            {
                throw new Exception("查询表不能为空！");
            }
            StringBuilder sb = new StringBuilder();
            ConvertTableRelation(queryModel, sb);
            sqlEntity.tables = sb.ToString();
            //3、Conditions——where
            ConvertConditions(queryModel.Conditions, sqlEntity.where);
            //4、Sorts——order
            ConvertSorts(queryModel.Sorts, sqlEntity.order);
        }

        private void ConvertTableRelation(QueryModel queryModel, StringBuilder sb)
        {
            sb.AppendFormat("{0} ", queryModel.TableRelation.PrimeTable);
            if (queryModel.TableRelation.JoinTables.IsNotEmpty())
            {
                JoinTable joinTable = null;
                for (int idx = 0; idx < queryModel.TableRelation.JoinTables.Count; idx++)
                {
                    joinTable = queryModel.TableRelation.JoinTables[idx];
                    ConvertJoinTable(joinTable, sb);
                }
            }
        }

        private void ConvertJoinTable(JoinTable joinTable, StringBuilder sbTarget)
        {
            if (joinTable == null || joinTable.RelFields.IsEmpty())
            {
                throw new Exception("关联表信息不完整！");
            }

            //关联表sql模板： join base_dict_type bdt on bd.dict_type=bdt.type_code
            StringBuilder sbInner = new StringBuilder();
            sbInner.AppendFormat("{0} join {1} on ", joinTable.JoinType, joinTable.Table);
            RelationField relField = null;
            for (int idx = 0; idx < joinTable.RelFields.Count; idx++)
            {
                relField = joinTable.RelFields[idx];
                sbInner.AppendFormat("{0} = {1} {2} ", relField.Field1, relField.Field2, idx == joinTable.RelFields.Count - 1 ? "" : "and");
            }
            sbTarget.Append(sbInner.ToString());
        }

        private void ConvertConditions(IList<Condition> conditions, StringBuilder sbTarget)
        {
            //sbTarget现在已经是1=1
            if (conditions.IsEmpty())
            {
                return;
            }
            StringBuilder sbInner = new StringBuilder();
            Condition condition = null;
            for (var idx = 0; idx < conditions.Count; idx++)
            {
                condition = conditions[idx];
                sbInner.AppendFormat("{0}", condition.Parse(idx == 0));
            }
            if (sbInner.Length > 0)
            {
                sbTarget.AppendFormat(" and ({0})", sbInner.ToString());
            }
        }

        private void ConvertSorts(IList<Sort> sorts, StringBuilder sbTarget)
        {
            if (sorts.IsEmpty())
            {
                return;
            }
            Sort sort = null;
            for (var idx = 0; idx < sorts.Count; idx++)
            {
                sort = sorts[idx];
                sbTarget.AppendFormat("{0} {1}{2}", sort.Field, sort.SortType.ToString(), idx == sorts.Count - 1 ? "" : ",");
            }
        }
    }
}
