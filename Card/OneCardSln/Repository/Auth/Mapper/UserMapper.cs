using DapperExtensions.Mapper;
using MyNet.Model.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Repository.Auth.Mapper
{
    public class UserMapper : ClassMapper<User>
    {
        public UserMapper()
        {
            Table("auth_user");

            AutoMap();
        }
    }
}
