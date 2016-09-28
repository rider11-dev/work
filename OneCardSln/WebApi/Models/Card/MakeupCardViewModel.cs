using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OneCardSln.WebApi.Models.Card
{
    public class MakeupCardViewModel : EditByIdcardViewModel
    {
        [Required(ErrorMessageResourceName = "Card_Number_Require", ErrorMessageResourceType = typeof(Resources.Resource))]
        [MaxLength(20, ErrorMessageResourceName = "Card_Number_Length", ErrorMessageResourceType = typeof(Resources.Resource))]
        public String number { get; set; }
    }
}