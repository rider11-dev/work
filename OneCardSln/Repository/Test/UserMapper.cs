using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.Repository.Test
{
    public class UserMapper : ClassMapper<User>
    {
        public UserMapper()
        {
            Table("Users");

            Map(u => u.Name).Column("UserName");

            Map(u => u.Email).Ignore();

            AutoMap();
        }
    }
}
