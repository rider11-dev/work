using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OneCardSln.WebApi.Models
{
    public class DelByPkViewModel
    {
        [Required(ErrorMessageResourceName = "DelByPk_Require", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        public string pk { get; set; }
    }
}