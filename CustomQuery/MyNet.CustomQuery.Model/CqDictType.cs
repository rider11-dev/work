using MyNet.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.CustomQuery.Model
{
    public class CqDictType
    {
        /// <summary>
        /// 字段类型，对应数据表base_dict_type中的一条数据
        /// </summary>
        public static readonly DictType FieldType = new DictType { type_code = "query.fieldtype", type_name = "查询字段类型", type_system = true };
        /// <summary>
        /// 查询关系符，对应数据表base_dict_type中的一条数据
        /// </summary>
        public static readonly DictType QRelType = new DictType { type_code = "query.rel", type_name = "查询关系符", type_system = true };
    }
}
