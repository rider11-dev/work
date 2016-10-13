using OneCardSln.Components.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OneCardSln.WebApi.Models.Auth.User
{
    public class ChangePwdViewModel
    {
        [Required(ErrorMessageResourceName = "UserId_Require", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        public string userid { get; set; }

        [Required(ErrorMessageResourceName = "OldPwd_Require", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        public string oldpwd { get; set; }

        [Required(ErrorMessageResourceName = "NewPwd_Require", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        [RegularExpression(RegexExtension.Regex_Pwd, ErrorMessageResourceName = "Pwd_Regex", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        public string newpwd { get; set; }
    }
}