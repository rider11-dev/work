using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNet.ViewModel.Auth.Group
{
    public class EditGroupViewModel : AddGroupViewModel
    {
        [Required(ErrorMessageResourceName = "Edit_By_Id", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        public string gp_id { get; set; }
    }
}