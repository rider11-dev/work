using MyNet.CustomQuery.Model;
using MyNet.Repository;
using MyNet.Repository.Db;

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
