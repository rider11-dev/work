using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneCardSln.Model
{
    /// <summary>
    /// 扣费优先级
    /// </summary>
    public enum EnumPayPriority
    {
        /// <summary>
        /// 优先扣除政府补贴金额
        /// </summary>
        gov,
        /// <summary>
        /// 优先扣除充值金额
        /// </summary>
        my
    }
}
