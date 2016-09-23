using DapperExtensions;
using OneCardSln.Components.Extensions;
using OneCardSln.Model;
using OneCardSln.Model.Base;
using OneCardSln.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.Service.Base
{
    public class DictService
    {
        const string Msg_Add = "添加字典数据";
        const string Msg_Delete = "删除字典数据";
        const string Msg_QueryByPage = "分页查询字典数据";

        DictRepository _dictRep;
        DictTypeRepository _dictTypeRep;
        public DictService(DictRepository dictRep, DictTypeRepository dictTypeRep)
        {
            _dictRep = dictRep;
            _dictTypeRep = dictTypeRep;
        }

        public OptResult Add(Dict dict)
        {
            OptResult rst = null;
            //1、编号类型是否存在
            var count = _dictTypeRep.Count(Predicates.Field<DictType>(t => t.type_code, Operator.Eq, dict.dict_type));
            if (count < 1)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, string.Format("{0}，编号类型{1}不存在！", Msg_Add, dict.dict_type));
                return rst;
            }
            //2、相同类型下，编号是否已存在
            PredicateGroup pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<Dict>(d => d.dict_type, Operator.Eq, dict.dict_type));
            pg.Predicates.Add(Predicates.Field<Dict>(d => d.dict_code, Operator.Eq, dict.dict_code));
            count = _dictRep.Count(pg);
            if (count > 0)
            {
                rst = OptResult.Build(ResultCode.DataRepeat, string.Format("{0}，类型{1}下已存在编号{2}！", Msg_Add, dict.dict_type, dict.dict_code));
                return rst;
            }
            //3、新增
            dict.dict_id = GuidExtension.GetOne();
            var val = _dictRep.Insert(dict);

            rst = OptResult.Build(ResultCode.Success, Msg_Add);

            return rst;
        }

        public OptResult Delete(dynamic pkId)
        {
            OptResult rst = null;
            //1、字典数据是否存在
            var predicate = Predicates.Field<Dict>(t => t.dict_id, Operator.Eq, pkId as object);
            var dict = _dictRep.GetList(predicate).FirstOrDefault();
            if (dict == null)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, Msg_Delete);
                return rst;
            }
            //2、是否已被引用，暂不实现（可以在一张中间表保存依赖关系？）
            //TODO
            //3、是否系统预制
            if (dict.dict_system)
            {
                rst = OptResult.Build(ResultCode.Fail, Msg_Delete + "，不能删除系统预制字典数据！");
                return rst;
            }
            //4、删除
            bool val = _dictRep.Delete(predicate);
            rst = OptResult.Build(val ? ResultCode.Success : ResultCode.Fail, Msg_Delete);

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
            var pg = GetPredicates(page.conditions);
            //2、排序
            IList<ISort> sort = GetSort();
            long total = 0;
            var dicts = _dictRep.GetPageList(page.pageIndex, page.pageSize, out total, sort, pg);
            rst = OptResult.Build(ResultCode.Success, Msg_QueryByPage, new
            {
                total = total,
                rows = dicts
            });

            return rst;
        }

        public OptResult GetList(Dictionary<string, object> conditions)
        {
            OptResult rst = null;
            //1、过滤条件
            var pg = GetPredicates(conditions);
            //2、排序
            IList<ISort> sort = GetSort();

            var types = _dictRep.GetList(pg, sort);

            rst = OptResult.Build(ResultCode.Success, null, new
            {
                rows = types
            });

            return rst;
        }

        private IList<ISort> GetSort()
        {
            return new[]
            {
                new Sort{PropertyName="dict_type",Ascending=true},
                new Sort{PropertyName="dict_order",Ascending=true}
            }; ;
        }

        private PredicateGroup GetPredicates(Dictionary<string, object> conditions)
        {
            PredicateGroup pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            if (conditions != null && conditions.Count > 0)
            {
                if (conditions.ContainsKey("dict_type"))
                {
                    pg.Predicates.Add(Predicates.Field<Dict>(d => d.dict_type, Operator.Eq, conditions["dict_type"]));
                }
                if (conditions.ContainsKey("dict_code"))
                {
                    pg.Predicates.Add(Predicates.Field<Dict>(d => d.dict_type, Operator.Like, "%" + conditions["dict_code"] + "%"));
                }
                if (conditions.ContainsKey("dict_name"))
                {
                    pg.Predicates.Add(Predicates.Field<Dict>(d => d.dict_type, Operator.Like, "%" + conditions["dict_name"] + "%"));
                }
            }

            return pg;
        }
    }
}
