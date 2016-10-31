using Biz.PartyBuilding.Model;
using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.PartyBuilding.Repository.Base.Mapper
{
    public class PartyOrgMapper : ClassMapper<PartyOrg>
    {
        public PartyOrgMapper()
        {
            Table("party_org");

            AutoMap();
        }
    }
}
