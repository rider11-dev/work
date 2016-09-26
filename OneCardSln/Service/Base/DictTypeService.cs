using DapperExtensions;
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
    public class DictTypeService
    {
        const string Msg_QueryByPage = "分页查询字典类型信息";
        const string Msg_Add = "添加字典类型信息";
        const string Msg_Delete = "删除字典类型信息";

        DictTypeRepository _dictTypeRep;
        DictRepository _dictRep;

        public DictTypeService(DictTypeRepository dictTypeRep, DictRepository dictRep)
        {
            _dictTypeRep = dictTypeRep;
            _dictRep = dictRep;
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

            //排序
            long total = 0;
            IList<ISort> sort = new[]
            {
                new Sort{PropertyName="type_code",Ascending=true}
            };

            var types = _dictTypeRep.GetPageList(page.pageIndex, page.pageSize, out total, sort);

            rst = OptResult.Build(ResultCode.Success, null, new
            {
                total = total,
                rows = types
            });

            return rst;
        }

        public OptResult GetList()
        {
            OptResult rst = null;
            //排序
            IList<ISort> sort = new[]
            {
                new Sort{PropertyName="type_code",Ascending=true}
            };

            var types = _dictTypeRep.GetList(null, sort);

            rst = OptResult.Build(ResultCode.Success, null, new
            {
                rows = types
            });

            return rst;
        }

        public OptResult Add(DictType dictType)
        {
            OptResult rst = null;
            //1、code是否已存在
            var count = _dictTypeRep.Count(Predicates.Field<DictType>(t => t.type_code, Operator.Eq, dictType.type_code));
            if (count > 0)
            {
                rst = OptResult.Build(ResultCode.DataRepeat, string.Format("{0}，类型编号{1}已存在！", Msg_Add, dictType.type_code));
                return rst;
            }
            //2、添加
            var val = _dictTypeRep.Insert(dictType);
            rst = OptResult.Build(ResultCode.Success, Msg_Add);

            return rst;
        }

        public OptResult Delete(string typeCode)
        {
            OptResult rst = null;
            //1、是否存在
            var predicate = Predicates.Field<DictType>(t => t.type_code, Operator.Eq, typeCode);
            var type = _dictTypeRep.GetList(predicate).FirstOrDefault();
            if (type == null)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, string.Format("{0}，编号为{1}的字典类型不存在！", Msg_Delete, typeCode));
                return rst;
            }
            //2、是否系统预制
            if (type.type_system)
            {
                rst = OptResult.Build(ResultCode.DataSystem, string.Format("{0}，编号{1}为系统预制类型！", Msg_Delete, typeCode));
                return rst;
            }
            //3、是否已被引用
            var count = _dictRep.Count(Predicates.Field<Dict>(t => t.dict_type, Operator.Eq, typeCode));
            if (count > 0)
            {
                rst = OptResult.Build(ResultCode.DataInUse, string.Format("{0}，编号为{1}的字典类型已被引用！", Msg_Delete, typeCode));
                return rst;
            }
            //3、删除
            bool val = _dictTypeRep.Delete(predicate);

            rst = OptResult.Build(val ? ResultCode.Success : ResultCode.Fail, Msg_Delete);

            return rst;
        }
    }
}
