using MyNet.CustomQuery.Model;
using MyNet.Repository;
using MyNet.Repository.Db;
using System.Collections;
using System.Collections.Generic;

namespace MyNet.CustomQuery.Repository
{
    public class TableRepository : BaseRepository<Table>, IBaseRepository<Table>
    {
        public TableRepository(IDbSession dbsession) : base(dbsession)
        {
            SqlConf = new SqlConfEntity { area = "customquery", group = "tables" };
        }
    }
}
