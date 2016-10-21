using MyNet.Components.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNet.WebApi.Models.Card
{
    public class ChangePhoneViewModel : EditByIdcardViewModel
    {
        [Required(ErrorMessageResourceName = "Phone_Require", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        [RegularExpression(RegexExtension.Regex_CellPhone, ErrorMessageResourceName = "Regex_CellPhone", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        public string phone { get; set; }
    }
}