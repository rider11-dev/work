using MyNet.Model.Card;
using MyNet.Repository.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Repository.Card
{
    public class CardBillRepository : BaseRepository<CardBill>, IBaseRepository<CardBill>
    {
        public CardBillRepository(IDbSession session)
            : base(session)
        {
            SqlConf = new SqlConfEntity { area = "card", group = "cardbill" };
        }
    }
}
