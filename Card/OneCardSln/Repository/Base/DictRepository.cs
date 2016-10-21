using MyNet.Model.Base;
using MyNet.Repository.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Repository.Base
{
    public class DictRepository : BaseRepository<Dict>, IBaseRepository<Dict>
    {
        public DictRepository(IDbSession dbsession)
            : base(dbsession)
        {
            SqlConf = new SqlConfEntity { area = "base", group = "base_dict" };
        }
    }
}
