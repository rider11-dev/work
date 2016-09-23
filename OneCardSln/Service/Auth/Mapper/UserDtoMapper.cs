using DapperExtensions.Mapper;
using OneCardSln.Service.Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.Service.Auth.Mapper
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
