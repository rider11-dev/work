using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNet.WebApi.Models
{
    public class DelByPkViewModel
    {
        [Required(ErrorMessageResourceName = "DelByPk_Require", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        public string pk { get; set; }
    }
}