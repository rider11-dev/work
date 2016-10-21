using MyNet.Model.Card;
using MyNet.Repository.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Repository.Card
{
    public class CardRecordRepository : BaseRepository<CardRecord>, IBaseRepository<CardRecord>
    {
        public CardRecordRepository(IDbSession session)
            : base(session)
        {
            SqlConf = new SqlConfEntity {area="card",group="cardrecord" };
        }
    }
}
