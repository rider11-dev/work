using MyNet.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.PartyBuilding.Model.Base
{
    public class PartyDictType : DictType
    {
        /// <summary>
        /// 党组织类型，对应数据表base_dict_type中的一条数据
        /// </summary>
        public static readonly DictType PartyOrg = new DictType { type_code = "partyorg", type_name = "党组织类型", type_system = true };
    }
}
