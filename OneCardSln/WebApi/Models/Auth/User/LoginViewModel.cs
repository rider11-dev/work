using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OneCardSln.WebApi.Models.Auth.User
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceName = "User_Name_Require", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string username { get; set; }

        [Required(ErrorMessageResourceName = "Pwd_Require", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string pwd { get; set; }
    }
}