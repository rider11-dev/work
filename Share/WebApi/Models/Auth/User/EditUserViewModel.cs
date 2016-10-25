using MyNet.Components.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNet.WebApi.Models.Auth.User
{
    public class EditUserViewModel : AddUserViewModel
    {
        [Required(ErrorMessageResourceName = "Edit_By_Id", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        public string user_id { get; set; }
    }
}