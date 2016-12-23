using MyNet.Model.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Model.Dto.Auth
{
    public class PermissionDto : Permission
    {
        public string per_type_name { get; set; }
        public string per_parent_name { get; set; }
    }
}
