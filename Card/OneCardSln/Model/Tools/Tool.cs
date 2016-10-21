using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyNet.Model.Tools
{
    /// <summary>
    /// 工具信息
    /// </summary>
    public class Tool
    {
        public string id { get; set; }
        /// <summary>
        /// 工具种类
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 工具名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 工具型号
        /// </summary>
        public string modal { get; set; }
        /// <summary>
        /// 总数量
        /// </summary>
        public decimal amount_all { get; set; }
        /// <summary>
        /// 借出数量
        /// </summary>
        public decimal amount_onloan { get; set; }
        /// <summary>
        /// 现存数量
        /// </summary>
        public decimal amount_now { get; set; }
        /// <summary>
        /// 状态：未借出、部分借出、全部借出
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// 变动数量：发生租借和归还业务时可以使用
        /// </summary>
        public decimal amount_changed { get; set; }
    }
}
