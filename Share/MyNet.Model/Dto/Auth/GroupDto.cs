using MyNet.Model.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Model.Dto.Auth
{
    public class GroupDto : Group
    {
        public string gp_parent_name { get; set; }
    }
}
