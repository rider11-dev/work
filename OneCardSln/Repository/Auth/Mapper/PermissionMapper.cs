using DapperExtensions.Mapper;
using OneCardSln.Model.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.Repository.Auth.Mapper
{
    public class PermissionMapper : ClassMapper<Permission>
    {
        public PermissionMapper()
        {
            Table("auth_permission");

            AutoMap();
        }
    }
}
