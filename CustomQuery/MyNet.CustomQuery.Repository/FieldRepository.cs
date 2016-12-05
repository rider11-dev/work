using MyNet.CustomQuery.Model;
using MyNet.Repository;
using MyNet.Repository.Db;

namespace MyNet.CustomQuery.Repository
{
    public class FieldRepository : BaseRepository<Field>, IBaseRepository<Field>
    {
        public FieldRepository(IDbSession dbsession) : base(dbsession)
        {
            SqlConf = new SqlConfEntity { area = "customquery", group = "fields" };
        }
    }
}
