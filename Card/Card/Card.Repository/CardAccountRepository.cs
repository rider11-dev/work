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
    public class CardAccountRepository : BaseRepository<CardAccount>, IBaseRepository<CardAccount>
    {
        public CardAccountRepository(IDbSession session) : base(session)
        {
            SqlConf = new SqlConfEntity { area = "card", group = "account" };
        }
    }
}
