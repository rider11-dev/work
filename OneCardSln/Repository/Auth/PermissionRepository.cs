using OneCardSln.Model.Auth;
using OneCardSln.Repository.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.Repository.Auth
{
    public class PermissionRepository : BaseRepository<Permission>, IBaseRepository<Permission>
    {
        public PermissionRepository(IDbSession dbsession)
            : base(dbsession)
        {

        }
    }
}
