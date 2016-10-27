using Biz.PartyBuilding.Model;
using MyNet.Repository;
using MyNet.Repository.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.PartyBuilding.Repository
{
    public class PartyOrgRepository : BaseRepository<PartyOrgDto>, IBaseRepository<PartyOrgDto>
    {
        public PartyOrgRepository(IDbSession dbsession)
            : base(dbsession)
        {
            SqlConf = new SqlConfEntity { area = "party", group = "party_org" };
        }
    }
}
