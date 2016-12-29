using MyNet.Model.Interface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Model.Base
{
    /// <summary>
    /// 字典类型
    /// </summary>
    public class DictType : IDictType
    {
        /// <summary>
        /// 权限类型，对应数据表base_dict_type中的一条数据
        /// </summary>
        public static readonly DictType Perm = new DictType { type_code = "auth.permtype", type_name = "权限类型", type_system = true };
        /// <summary>
        /// 一卡通状态类别，对应数据表base_dict_type中的一条数据
        /// </summary>
        public static readonly DictType CardState = new DictType { type_code = "card.cardstate", type_name = "一卡通状态", type_system = true };

        public string type_code { get; set; }
        public string type_name { get; set; }
        public bool type_system { get; set; }
    }
}
