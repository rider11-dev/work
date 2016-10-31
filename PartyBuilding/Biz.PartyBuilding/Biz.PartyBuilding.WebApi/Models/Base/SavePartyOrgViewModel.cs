using Biz.PartyBuilding.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.PartyBuilding.WebApi.Models.Base
{
    public class SavePartyOrgViewModel
    {
        [Required(ErrorMessageResourceName = "Org_Id_Require", ErrorMessageResourceType = typeof(Biz.PartyBuilding.Resource.ViewModelResource))]
        public string po_gp_id { get; set; }

        public string po_id { get; set; }

        [Required(ErrorMessageResourceName = "OrgType_Require", ErrorMessageResourceType = typeof(Biz.PartyBuilding.Resource.ViewModelResource))]
        public string po_type { get; set; }

        [MaxLength(100, ErrorMessageResourceName = "Org_ChgNum_Length", ErrorMessageResourceType = typeof(Biz.PartyBuilding.Resource.ViewModelResource))]
        public string po_chg_num { get; set; }

        public DateTime po_chg_date { get; set; }

        public DateTime po_expire_date { get; set; }

        public bool po_chg_remind { get; set; }

        [MaxLength(255, ErrorMessageResourceName = "Remark_Length", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        public string po_remark { get; set; }
    }
}
