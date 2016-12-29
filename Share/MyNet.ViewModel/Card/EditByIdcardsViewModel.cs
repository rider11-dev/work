using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNet.ViewModel.Card
{
    public class EditByIdcardsViewModel
    {
        [Required(ErrorMessageResourceName = "Idcard_Require", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        public IEnumerable<string> idcards { get; set; }
    }
}