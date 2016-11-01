using Biz.PartyBuilding.YS.Client.PartyOrg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.PartyBuilding.YS.Client
{
    public class PartyBuildingContext
    {
        public static List<OrgStrucViewModel> orgs = new List<OrgStrucViewModel>
        {
            new OrgStrucViewModel{org_code="cxxwzzb",org_name="曹县县委组织部",org_contacts="张老师",org_phone="13525008945",org_addr="县委前街1号"},
            new OrgStrucViewModel{org_code="cxccbsc",org_name="曹城办事处党组织",org_parent_name="曹县县委组织部",org_contacts="王晓辉",org_phone="05301234567",org_addr="曹城办事处"}
        };
    }
}
