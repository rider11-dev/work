using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNet.WebApi.Models.Card
{
    public class SetMoneyViewModel
    {
        [Required(ErrorMessageResourceName = "Idcards_Require", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        public IEnumerable<string> idcards { get; set; }
        public decimal money { get; set; }
    }
}