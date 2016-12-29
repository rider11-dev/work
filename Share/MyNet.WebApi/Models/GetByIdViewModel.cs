using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNet.WebApi.Models
{
    public class GetByIdViewModel
    {
        [Required(ErrorMessageResourceName = "GetByPk_Require", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        public string pk { get; set; }
    }
}