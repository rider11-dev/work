using MyNet.Model.CustomQuery;
using MyNet.Repository;
using MyNet.Repository.Db;

namespace MyNet.Repository.CustomQuery
{
    public class FieldRepository : BaseRepository<Field>, IBaseRepository<Field>
    {
        public FieldRepository(IDbSession dbsession) : base(dbsession)
        {
            SqlConf = new SqlConfEntity { area = "customquery", group = "fields" };
        }
    }
}
