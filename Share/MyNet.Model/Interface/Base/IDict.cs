using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Model.Interface.Base
{
    /// <summary>
    /// 基础数据——字典
    /// </summary>
    public interface IDict
    {
        string dict_id { get; set; }
        /// <summary>
        /// 字典编号
        /// </summary>
        string dict_code { get; set; }
        /// <summary>
        /// 字典名称
        /// </summary>
        string dict_name { get; set; }
        /// <summary>
        /// 字典类型
        /// </summary>
        string dict_type { get; set; }

        /// <summary>
        /// 是否系统预制
        /// </summary>
        bool dict_system { get; set; }

        /// <summary>
        /// 是否默认值
        /// </summary>
        bool dict_default { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        int dict_order { get; set; }
    }
}
