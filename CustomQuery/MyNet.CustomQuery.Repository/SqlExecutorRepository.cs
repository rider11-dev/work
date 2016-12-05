using MyNet.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNet.Repository.Db;
using MyNet.CustomQuery.Model;

namespace MyNet.CustomQuery.Repository
{
    public class SqlExecutorRepository : BaseRepository<QueryModel>, IBaseRepository<QueryModel>
    {
        public SqlExecutorRepository(IDbSession session) : base(session)
        {

        }
    }
}
