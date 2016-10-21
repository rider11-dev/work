using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Model.Card
{
    /// <summary>
    /// 一卡通操作类型枚举
    /// </summary>
    public enum CardOperation
    {
        [Description("发卡")]
        Reg = 0,
        [Description("补卡")]
        Makeup = 1,
        [Description("变更手机号")]
        ChgPhone = 2,
        [Description("注销")]
        CloseDwn = 3,
        [Description("挂失")]
        RepLoss = 4,
        [Description("账号恢复")]
        Recover = 5,
        [Description("补贴")]
        SetGov = 6,
        [Description("充值")]
        SetMy = 7,
        [Description("付款")]
        Pay = 8,
        [Description("退款")]
        Refund = 9
    }
}
