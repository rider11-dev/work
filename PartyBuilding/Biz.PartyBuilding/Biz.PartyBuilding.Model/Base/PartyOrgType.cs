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
        PartyOrgDW = 0,
        [Description("机关党委")]
        PartyOrgJGDW = 1,
        [Description("基层党委")]
        PartyOrgJCDW = 2,
        [Description("党总支部")]
        PartyOrgDZZB = 3,
        [Description("党支部")]
        PartyOrgDZB = 4
    }
}
