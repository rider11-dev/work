using OneCardSln.Model.Auth;
using OneCardSln.Repository.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.Repository.Auth
{
    public class UserPermissionRelRepository : BaseRepository<UerPermissionRel>, IBaseRepository<UerPermissionRel>
    {
        public UserPermissionRelRepository(IDbSession dbsession)
            : base(dbsession)
        {

        }
    }
}
