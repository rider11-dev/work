using MyNet.Components.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNet.WebApi.Models.Auth.User
{
    public class ChangePwdViewModel
    {
        [Required(ErrorMessageResourceName = "UserId_Require", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        public string userid { get; set; }

        [Required(ErrorMessageResourceName = "OldPwd_Require", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        public string oldpwd { get; set; }

        [Required(ErrorMessageResourceName = "NewPwd_Require", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        [RegularExpression(RegexExtension.Regex_Pwd, ErrorMessageResourceName = "Pwd_Regex", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        public string newpwd { get; set; }
    }
}