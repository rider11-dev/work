using MyNet.Model.CustomQuery;
using MyNet.Repository;
using MyNet.Repository.Db;
using System.Collections;
using System.Collections.Generic;

namespace MyNet.Repository.CustomQuery
{
    public class TableRepository : BaseRepository<Table>, IBaseRepository<Table>
    {
        public TableRepository(IDbSession dbsession) : base(dbsession)
        {
            SqlConf = new SqlConfEntity { area = "customquery", group = "tables" };
        }
    }
}
