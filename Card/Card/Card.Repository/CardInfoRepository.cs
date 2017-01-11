using Card.Model;
using MyNet.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNet.Repository.Db;

namespace Card.Repository
{
    public class CardInfoRepository : BaseRepository<CardInfo>, IBaseRepository<CardInfo>
    {
        public CardInfoRepository(IDbSession session) : base(session)
        {
            SqlConf = new SqlConfEntity { area = "card", group = "info" };
        }
    }
}
