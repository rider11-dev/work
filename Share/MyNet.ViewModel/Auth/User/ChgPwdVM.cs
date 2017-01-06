using MyNet.Components.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNet.ViewModel.Auth.User
{
    public class ChgPwdVM
    {
        [Required(ErrorMessageResourceName = "UserId_Require", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        public string userid { get; set; }

        [Required(ErrorMessageResourceName = "OldPwd_Require", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        public string oldpwd { get; set; }

        [Required(ErrorMessageResourceName = "NewPwd_Require", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        [RegularExpression(RegexExtension.Regex_Pwd, ErrorMessageResourceName = "Pwd_Regex", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        public string newpwd { get; set; }

        [Required(ErrorMessageResourceName = "NewPwd2_Require", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        [Compare("newpwd", ErrorMessage = "两次输入新密码不相同")]
        public string newpwd2 { get; set; }
    }
}