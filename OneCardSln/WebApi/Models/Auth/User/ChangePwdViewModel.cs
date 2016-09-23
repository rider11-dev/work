using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OneCardSln.WebApi.Models.Auth.User
{
    public class ChangePwdViewModel
    {
        [Required(ErrorMessageResourceName = "UserId_Require", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string userid { get; set; }

        [Required(ErrorMessageResourceName = "OldPwd_Require", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string oldpwd { get; set; }

        [Required(ErrorMessageResourceName = "NewPwd_Require", ErrorMessageResourceType = typeof(Resources.Resource))]
        [RegularExpression(@"^(\d|[a-zA-Z]|_){6,10}$", ErrorMessageResourceName = "Pwd_Regex", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string newpwd { get; set; }
    }
}