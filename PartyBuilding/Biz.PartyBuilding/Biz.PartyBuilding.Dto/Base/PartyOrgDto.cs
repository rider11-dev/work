using MyNet.Dto.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.PartyBuilding.Model
{
    /// <summary>
    /// 党组织业务信息——扩展auth_group表
    /// </summary>
    public class PartyOrgDto : PartyOrg
    {
        /// <summary>
        /// 组织id
        /// </summary>
        public string po_gp_id { get; set; }
        /// <summary>
        /// 组织基本信息
        /// </summary>
        public GroupDto po_group { get; set; }
    }
}
