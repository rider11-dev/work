using MyNet.Components.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNet.WebApi.Models.Auth.User
{
    public class AddUserViewModel
    {
        [Required(ErrorMessageResourceName = "User_Name_Require", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        [StringLength(10, MinimumLength = 3, ErrorMessageResourceName = "User_Name_Length", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        public string user_name { get; set; }

        [Required(ErrorMessageResourceName = "Idcard_Require", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        [StringLength(18, MinimumLength = 15, ErrorMessageResourceName = "Idcard_Length", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        [RegularExpression(RegexExtension.Regex_Idcard, ErrorMessageResourceName = "Idcard_Regx", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        public string user_idcard { get; set; }

        [StringLength(10, MinimumLength = 2, ErrorMessageResourceName = "User_TrueName_Length", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        public string user_truename { get; set; }

        [Required(ErrorMessageResourceName = "Regioncode_Require", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        [MaxLength(10, ErrorMessageResourceName = "Regioncode_Max", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        public string user_regioncode { get; set; }

        [MaxLength(255, ErrorMessageResourceName = "Remark_Length", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        public string user_remark { get; set; }
        //public string user_creator { get; set; }
    }
}