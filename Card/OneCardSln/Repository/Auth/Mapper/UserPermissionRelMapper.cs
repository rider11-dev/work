using DapperExtensions.Mapper;
using MyNet.Model.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Repository.Auth.Mapper
{
    public class UserPermissionRelMapper : ClassMapper<UserPermissionRel>
    {
        public UserPermissionRelMapper()
        {
            Table("auth_user_permission");

            AutoMap();
        }
    }
}
