using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OneCardSln.WebApi.Models.Auth.User
{
    public class EditUserViewModel
    {
        [Required(ErrorMessageResourceName = "Edit_By_Id", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string user_id { get; set; }

        [StringLength(10, MinimumLength = 3, ErrorMessageResourceName = "User_TrueName_Length", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string user_truename { get; set; }

        [StringLength(18, MinimumLength = 15, ErrorMessageResourceName = "Idcard_Length", ErrorMessageResourceType = typeof(Resources.Resource))]
        [RegularExpression(@"^(\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$", ErrorMessageResourceName = "Idcard_Regx", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string user_idcard { get; set; }

        [MaxLength(10, ErrorMessageResourceName = "Regioncode_Max", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string user_regioncode { get; set; }

        [MaxLength(255, ErrorMessageResourceName = "Remark_Length", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string user_remark { get; set; }
    }
}