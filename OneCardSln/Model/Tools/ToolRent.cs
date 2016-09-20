using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneCardSln.Model.Tools
{
    /// <summary>
    /// 工具租借流水信息
    /// </summary>
    public class ToolRent
    {
        public string id { get; set; }
        /// <summary>
        /// 借出人id
        /// </summary>
        public string hirer { get; set; }
        /// <summary>
        /// 借出人姓名
        /// </summary>
        public string hirer_name { get; set; }
        /// <summary>
        /// 工具id
        /// </summary>
        public string tool { get; set; }
        /// <summary>
        /// 工具名称
        /// </summary>
        public string tool_name { get; set; }
        /// <summary>
        /// 借出数量
        /// </summary>
        public decimal amount_borrow { get; set; }
        /// <summary>
        /// 借出时间
        /// </summary>
        public DateTime time_borrow { get; set; }
        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime time_expire { get; set; }
        /// <summary>
        /// 状态:未归还、部分归还、全部归还
        /// </summary>
        public string state { get; set; }
    }
}
