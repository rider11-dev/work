using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MyNet.Model.Card
{
    /// <summary>
    /// 金额种类
    /// </summary>
    public enum MoneyEnum
    {
        [Description("补贴")]
        gov,
        [Description("充值")]
        my
    }
}
