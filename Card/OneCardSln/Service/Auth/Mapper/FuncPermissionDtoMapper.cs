using DapperExtensions.Mapper;
using MyNet.Dto.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Service.Auth.Mapper
{
    public class FuncPermissionDtoMapper : ClassMapper<FuncPermissionDto>
    {
        public FuncPermissionDtoMapper()
        {
            Table("auth_permission");

            AutoMap();
        }
    }
}
