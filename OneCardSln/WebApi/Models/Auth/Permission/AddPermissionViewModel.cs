using OneCardSln.Components.Extensions;
using OneCardSln.WebApi.Extensions.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OneCardSln.WebApi.Models.Auth.Permission
{
    public class AddPermissionViewModel
    {
        [Required(ErrorMessageResourceName = "Code_Require", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        [MaxLength(20, ErrorMessageResourceName = "Per_Code_Length", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        public string per_code { get; set; }

        [Required(ErrorMessageResourceName = "Name_Require", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        [MaxLength(40, ErrorMessageResourceName = "Per_Name_Length", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        public string per_name { get; set; }

        [Required(ErrorMessageResourceName = "Per_Type_Require", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        public string per_type { get; set; }

        [MaxLength(255, ErrorMessageResourceName = "Per_Uri_Length", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        public string per_uri { get; set; }

        public string per_parent { get; set; }

        [Required(ErrorMessageResourceName = "Sort_Require", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        [RegularExpression(RegexExtension.Regex_Sort, ErrorMessageResourceName = "Sort_Regex", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        [MaxLength(10, ErrorMessageResourceName = "Sort_Length", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        public string per_sort { get; set; }

        [MaxLength(200, ErrorMessageResourceName = "Remark_Length", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        public string per_remark { get; set; }
    }
}