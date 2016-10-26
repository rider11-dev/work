using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.PartyBuilding.Model.Base
{
    public enum PartyOrgType
    {
        [Description("党委")]
        DW = 0,
        [Description("机关党委")]
        JGDW = 1,
        [Description("基层党委")]
        JCDW = 2,
        [Description("党总支部")]
        DZZB = 3,
        [Description("党支部")]
        DZB = 4
    }
}
