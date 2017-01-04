using MyNet.Components.Extensions;
using MyNet.Components.Mapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNet.ViewModel.Auth.User
{
    public class UserVM
    {
        public string user_id { get; set; }
        [Required(ErrorMessageResourceName = "User_Name_Require", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        [StringLength(10, MinimumLength = 3, ErrorMessageResourceName = "User_Name_Length", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        public string user_name { get; set; }

        [Required(ErrorMessageResourceName = "Idcard_Require", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        [StringLength(18, MinimumLength = 15, ErrorMessageResourceName = "Idcard_Length", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        [RegularExpression(RegexExtension.Regex_Idcard, ErrorMessageResourceName = "Idcard_Regx", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        public string user_idcard { get; set; }

        [StringLength(10, MinimumLength = 2, ErrorMessageResourceName = "User_TrueName_Length", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        public string user_truename { get; set; }

        [Required(ErrorMessageResourceName = "Regioncode_Require", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        [MaxLength(10, ErrorMessageResourceName = "Regioncode_Max", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        public string user_regioncode { get; set; }

        [MaxLength(255, ErrorMessageResourceName = "Remark_Length", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        public string user_remark { get; set; }

        [Required(ErrorMessageResourceName = "User_Group_Require", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        public string user_group { get; set; }
    }
}