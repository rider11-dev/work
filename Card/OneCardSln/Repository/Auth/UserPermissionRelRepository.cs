using MyNet.Model.Auth;
using MyNet.Repository.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Repository.Auth
{
    public class UserPermissionRelRepository : BaseRepository<UserPermissionRel>, IBaseRepository<UserPermissionRel>
    {
        public UserPermissionRelRepository(IDbSession dbsession)
            : base(dbsession)
        {
            SqlConf = new SqlConfEntity { area = "auth", group = "auth_user_per_rel" };
        }
    }
}
