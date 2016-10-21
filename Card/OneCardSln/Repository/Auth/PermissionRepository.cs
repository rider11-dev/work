using MyNet.Model.Auth;
using MyNet.Repository.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Repository.Auth
{
    public class PermissionRepository : BaseRepository<Permission>, IBaseRepository<Permission>
    {
        public PermissionRepository(IDbSession dbsession)
            : base(dbsession)
        {
            SqlConf = new SqlConfEntity { area = "auth", group = "auth_permission" };
        }
    }
}
