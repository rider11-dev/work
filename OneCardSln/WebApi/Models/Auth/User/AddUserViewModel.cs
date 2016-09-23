using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OneCardSln.WebApi.Models.Auth.User
{
    public class AddUserViewModel
    {
        [Required(ErrorMessageResourceName = "User_Name_Require", ErrorMessageResourceType = typeof(Resources.Resource))]
        [StringLength(10, MinimumLength = 3, ErrorMessageResourceName = "User_Name_Length", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string user_name { get; set; }

        //public string user_pwd { get; set; }

        [Required(ErrorMessageResourceName = "Idcard_Require", ErrorMessageResourceType = typeof(Resources.Resource))]
        [StringLength(18, MinimumLength = 15, ErrorMessageResourceName = "Idcard_Length", ErrorMessageResourceType = typeof(Resources.Resource))]
        [RegularExpression(@"^(\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$", ErrorMessageResourceName = "Idcard_Regx", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string user_idcard { get; set; }

        [StringLength(10, MinimumLength = 3, ErrorMessageResourceName = "User_TrueName_Length", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string user_truename { get; set; }

        [Required(ErrorMessageResourceName = "Regioncode_Require", ErrorMessageResourceType = typeof(Resources.Resource))]
        [MaxLength(10, ErrorMessageResourceName = "Regioncode_Max", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string user_regioncode { get; set; }

        [MaxLength(255, ErrorMessageResourceName = "Remark_Length", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string user_remark { get; set; }
        //public string user_creator { get; set; }
    }
}