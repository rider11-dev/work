using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OneCardSln.WebApi.Models.Card
{
    public class RefundViewModel
    {
        [Required(ErrorMessageResourceName = "Idcard_Require", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string idcard { get; set; }

        [Required(ErrorMessageResourceName = "Order_Require", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string order { get; set; }
        [MaxLength(255, ErrorMessageResourceName = "Refund_Src_Length", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string src { get; set; }
        [MaxLength(255, ErrorMessageResourceName = "Remark_Length", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string remark { get; set; }
        [MaxLength(32, ErrorMessageResourceName = "Opt_Length", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string opt { get; set; }

    }
}