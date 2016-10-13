using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OneCardSln.WebApi.Models.Card
{
    public class MakeupCardViewModel : EditByIdcardViewModel
    {
        [Required(ErrorMessageResourceName = "Card_Number_Require", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        [MaxLength(20, ErrorMessageResourceName = "Card_Number_Length", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        public String number { get; set; }
    }
}