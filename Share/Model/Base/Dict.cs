using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Model.Base
{
    /// <summary>
    /// 基础数据——字典
    /// </summary>
    public class Dict
    {
        public string dict_id { get; set; }
        /// <summary>
        /// 字典编号
        /// </summary>
        public string dict_code { get; set; }
        /// <summary>
        /// 字典名称
        /// </summary>
        public string dict_name { get; set; }
        /// <summary>
        /// 字典类型
        /// </summary>
        public string dict_type { get; set; }

        /// <summary>
        /// 是否系统预制
        /// </summary>
        public bool dict_system { get; set; }

        /// <summary>
        /// 是否默认值
        /// </summary>
        public bool dict_default { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int dict_order { get; set; }
    }
}
