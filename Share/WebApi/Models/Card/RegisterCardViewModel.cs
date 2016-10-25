using MyNet.Components.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNet.WebApi.Models.Card
{
    public class RegisterCardViewModel
    {
        [Required(ErrorMessageResourceName = "Card_Number_Require", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        [MaxLength(20, ErrorMessageResourceName = "Card_Number_Length", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        public String card_number { get; set; }


        [Required(ErrorMessageResourceName = "Idcard_Require", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        [StringLength(18, MinimumLength = 15, ErrorMessageResourceName = "Idcard_Length", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        [RegularExpression(RegexExtension.Regex_Idcard, ErrorMessageResourceName = "Idcard_Regx", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        public String card_idcard { get; set; }


        [Required(ErrorMessageResourceName = "Card_Account_Require", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        [MaxLength(20, ErrorMessageResourceName = "Card_Account_Length", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        public String card_username { get; set; }


        [Required(ErrorMessageResourceName = "Phone_Require", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        [RegularExpression(RegexExtension.Regex_CellPhone, ErrorMessageResourceName = "Regex_CellPhone", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        public String card_phone { get; set; }
        public Decimal card_govmoney { get; set; }
        public Decimal card_mymoney { get; set; }
        public String card_remark { get; set; }
    }
}