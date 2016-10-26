using DapperExtensions.Mapper;
using MyNet.Model.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Repository.Auth.Mapper
{
    public class GroupMapper : ClassMapper<Group>
    {
        public GroupMapper()
        {
            Table("auth_group");

            AutoMap();
        }
    }
}
