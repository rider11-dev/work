using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneCardSln.Model.Tools
{
    /// <summary>
    /// 工具归还流水信息
    /// </summary>
    public class ToolReturn
    {
        public string id { get; set; }
        /// <summary>
        /// 租借流水id
        /// </summary>
        public string rentid { get; set; }

        /// <summary>
        /// 工具id
        /// </summary>
        public string tool { get; set; }

        /// <summary>
        /// 工具名称
        /// </summary>
        public string tool_name { get; set; }

        /// <summary>
        /// 租借人id
        /// </summary>
        public string hirer { get; set; }
        /// <summary>
        /// 借出人姓名
        /// </summary>
        public string hirer_name { get; set; }
        /// <summary>
        /// 归还时间
        /// </summary>
        public DateTime time_return { get; set; }
        /// <summary>
        /// 归还数量
        /// </summary>
        public decimal amount_return { get; set; }
    }
}
