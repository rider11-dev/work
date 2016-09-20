using OneCardSln.Model;
using OneCardSln.Repository.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.Repository.Test
{
    public class CardInfoRepository : BaseRepository<CardInfo>, IBaseRepository<CardInfo>
    {
        public CardInfoRepository(IDbSession session)
            : base(session)
        {

        }
    }
}
