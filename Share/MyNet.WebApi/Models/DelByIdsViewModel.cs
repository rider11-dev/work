using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNet.WebApi.Models
{
    public class DelByIdsViewModel
    {
        [Required(ErrorMessageResourceName = "DelByPk_Require", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        public IList<string> pks { get; set; }
    }
}