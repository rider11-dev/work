using DapperExtensions.Mapper;
using MyNet.Dto.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Service.Auth.Mapper
{
    public class PermissionCacheDtoMapper : ClassMapper<PermissionCacheDto>
    {
        public PermissionCacheDtoMapper()
        {
            Table("auth_permission");

            AutoMap();
        }
    }
}
