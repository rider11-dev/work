using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNet.ViewModel.Card
{
    public class MakeupCardViewModel : EditByIdcardViewModel
    {
        [Required(ErrorMessageResourceName = "Card_Number_Require", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        [MaxLength(20, ErrorMessageResourceName = "Card_Number_Length", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        public String number { get; set; }
    }
}