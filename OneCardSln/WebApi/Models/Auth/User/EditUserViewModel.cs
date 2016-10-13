using OneCardSln.Components.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OneCardSln.WebApi.Models.Auth.User
{
    public class EditUserViewModel
    {
        [Required(ErrorMessageResourceName = "Edit_By_Id", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        public string user_id { get; set; }

        [StringLength(10, MinimumLength = 3, ErrorMessageResourceName = "User_TrueName_Length", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        public string user_truename { get; set; }

        [StringLength(18, MinimumLength = 15, ErrorMessageResourceName = "Idcard_Length", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        [RegularExpression(RegexExtension.Regex_Idcard, ErrorMessageResourceName = "Idcard_Regx", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        public string user_idcard { get; set; }

        [MaxLength(10, ErrorMessageResourceName = "Regioncode_Max", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        public string user_regioncode { get; set; }

        [MaxLength(255, ErrorMessageResourceName = "Remark_Length", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        public string user_remark { get; set; }
    }
}