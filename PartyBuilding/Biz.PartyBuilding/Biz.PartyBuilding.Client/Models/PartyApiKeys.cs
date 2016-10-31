using MyNet.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.PartyBuilding.Client.Models
{
    public struct PartyApiKeys
    {
        public const string Key_ApiProvider_Party = "party";

        /*————————————party-org——————————*/
        public const string PartyOrgGetById = "party_org_get";
        public const string SaveOrg = "party_org_save";
    }
}
