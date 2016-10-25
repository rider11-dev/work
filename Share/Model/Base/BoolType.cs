using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Model.Base
{
    public enum BoolType
    {
        [Description("是")]
        @true,
        [Description("否")]
        @false
    }
}
