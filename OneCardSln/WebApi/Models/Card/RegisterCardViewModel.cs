using OneCardSln.Components.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OneCardSln.WebApi.Models.Card
{
    public class RegisterCardViewModel
    {
        [Required(ErrorMessageResourceName = "RegCard_Number_Require", ErrorMessageResourceType = typeof(Resources.Resource))]
        [MaxLength(20, ErrorMessageResourceName = "Card_Number_Length", ErrorMessageResourceType = typeof(Resources.Resource))]
        public String card_number { get; set; }


        [Required(ErrorMessageResourceName = "Idcard_Require", ErrorMessageResourceType = typeof(Resources.Resource))]
        [StringLength(18, MinimumLength = 15, ErrorMessageResourceName = "Idcard_Length", ErrorMessageResourceType = typeof(Resources.Resource))]
        [RegularExpression(RegexExtension.Regex_Idcard, ErrorMessageResourceName = "Idcard_Regx", ErrorMessageResourceType = typeof(Resources.Resource))]
        public String card_idcard { get; set; }


        [Required(ErrorMessageResourceName = "RegCard_Account_Require", ErrorMessageResourceType = typeof(Resources.Resource))]
        [MaxLength(20, ErrorMessageResourceName = "RegCard_Account_Length", ErrorMessageResourceType = typeof(Resources.Resource))]
        public String card_username { get; set; }


        [Required(ErrorMessageResourceName = "Phone_Require", ErrorMessageResourceType = typeof(Resources.Resource))]
        [RegularExpression(RegexExtension.Regex_CellPhone, ErrorMessageResourceName = "Regex_CellPhone", ErrorMessageResourceType = typeof(Resources.Resource))]
        public String card_phone { get; set; }
        public Decimal card_govmoney { get; set; }
        public Decimal card_mymoney { get; set; }
        public String card_remark { get; set; }
    }
}