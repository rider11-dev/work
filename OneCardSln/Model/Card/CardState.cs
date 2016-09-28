using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.Model
{
    public enum CardState
    {
        [Description("正常")]
        Normal = 0,
        [Description("挂失")]
        Loss = 1,
        [Description("注销")]
        Off = 2
    }
}
