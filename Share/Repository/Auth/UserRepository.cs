using MyNet.Model.Auth;
using MyNet.Repository.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Repository.Auth
{
    public class UserRepository : BaseRepository<User>, IBaseRepository<User>
    {
        public UserRepository(IDbSession dbsession)
            : base(dbsession)
        {
            SqlConf = new SqlConfEntity { area = "auth", group = "auth_user" };
        }
    }
}
