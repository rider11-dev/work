using MyNet.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNet.Repository.Db;
using MyNet.CustomQuery.Model;
using MyNet.Components.Extensions;

namespace MyNet.CustomQuery.Repository
{
    public class CustomQueryRepository : BaseRepository<EmptyModel>
    {
        public CustomQueryRepository(IDbSession session) : base(session)
        {
            SqlConf = new SqlConfEntity { area = "customquery", group = "base" };
        }
    }
}
