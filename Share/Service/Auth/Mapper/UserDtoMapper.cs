using DapperExtensions.Mapper;
using MyNet.Dto.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Service.Auth.Mapper
{
    public class UserDtoMapper : ClassMapper<UserDto>
    {
        public UserDtoMapper()
        {
            Table("auth_user");

            Map(u => u.user_pwd).Ignore();

            AutoMap();
        }
    }
}
