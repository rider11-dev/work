using OneCardSln.Components.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OneCardSln.WebApi.Models.Card
{
    public class ChangePhoneViewModel : EditByIdcardViewModel
    {
        [Required(ErrorMessageResourceName = "Phone_Require", ErrorMessageResourceType = typeof(Resources.Resource))]
        [RegularExpression(RegexExtension.Regex_CellPhone, ErrorMessageResourceName = "Regex_CellPhone", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string phone { get; set; }
    }
}