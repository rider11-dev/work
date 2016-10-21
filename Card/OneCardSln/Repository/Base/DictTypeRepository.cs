using MyNet.Model.Base;
using MyNet.Repository.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Repository.Base
{
    public class DictTypeRepository : BaseRepository<DictType>, IBaseRepository<DictType>
    {
        public DictTypeRepository(IDbSession session)
            : base(session)
        {
            SqlConf = new SqlConfEntity { area = "base", group = "base_dict_type" };
        }
    }
}
