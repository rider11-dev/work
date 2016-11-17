using Biz.PartyBuilding.YS.Models;
using MyNet.Repository;
using MyNet.Repository.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.PartyBuilding.YS.Repository
{
    public class InfoRepository : BaseRepository<InfoModel>, IBaseRepository<InfoModel>
    {
        public InfoRepository(IDbSession dbsession)
            : base(dbsession)
        {
            SqlConf = new SqlConfEntity { area = "party", group = "party_info" };
        }
    }
}
