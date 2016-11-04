using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.PartyBuilding.YS.Client.PartyOrg.Models
{
    public class PartyMemberViewModel
    {
        public string type { get; set; }
        public string name { get; set; }
        public string sex { get; set; }
        public string age { get; set; }
        public string nation { get; set; }
        public string party { get; set; }
        public string dnzw { get; set; }
        public string join_in_time { get; set; }
        public string zz_time { get; set; }
        public string idcard { get; set; }
        public string xl { get; set; }
        public string phone { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>
        public string jg { get; set; }
        public string now_gzgw { get; set; }
        public string month_salary { get; set; }
        public string month_party_money { get; set; }
        public string remark { get; set; }

        public class FamilyViewModel
        {
            /// <summary>
            /// 称谓
            /// </summary>
            public string cw { get; set; }
            public string xm { get; set; }
            public string nl { get; set; }
            public string zzmm { get; set; }
            public string work_company { get; set; }
            public string work_zw { get; set; }
        }
    }
}
