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
    public class PartyOrgDto
    {
        /// <summary>
        /// 组织id，对应auth_group的gp_id
        /// </summary>
        public string po_id { get; set; }
        /// <summary>
        /// 党组织类型，参见PartyOrgType
        /// </summary>
        public string po_type { get; set; }
        /// <summary>
        /// 换届文号
        /// </summary>
        public string po_chg_num { get; set; }
        /// <summary>
        /// 换届日期
        /// </summary>
        public DateTime po_chg_date { get; set; }
        /// <summary>
        /// 任届期满日期
        /// </summary>
        public DateTime po_expire_date { get; set; }
        /// <summary>
        /// 换届是否提醒
        /// </summary>
        public bool po_chg_remind { get; set; }
        /// <summary>
        /// 正式党员人数
        /// </summary>
        public int po_mem_normal { get; set; }
        /// <summary>
        /// 发展党员人数
        /// </summary>
        public int po_mem_potential { get; set; }
        /// <summary>
        /// 入党积极分子人数
        /// </summary>
        public int po_mem_activists { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string po_remark { get; set; }

        /// <summary>
        /// 组织基本信息
        /// </summary>
        public GroupDto po_group { get; set; }
    }
}
