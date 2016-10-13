using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OneCardSln.WebApi.Models.Card
{
    public class SetMoneyViewModel
    {
        [Required(ErrorMessageResourceName = "Idcards_Require", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        public IEnumerable<string> idcards { get; set; }
        public decimal money { get; set; }
    }
}