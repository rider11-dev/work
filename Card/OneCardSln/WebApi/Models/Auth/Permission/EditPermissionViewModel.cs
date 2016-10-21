using MyNet.Components.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNet.WebApi.Models.Auth.Permission
{
    public class EditPermissionViewModel
    {
        [Required(ErrorMessageResourceName = "Edit_By_Id", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        public string per_id { get; set; }

        [MaxLength(40, ErrorMessageResourceName = "Per_Name_Length", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        public string per_name { get; set; }

        [Required(ErrorMessageResourceName = "Per_Type_Require", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        public string per_type { get; set; }

        [MaxLength(255, ErrorMessageResourceName = "Per_Uri_Length", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        public string per_uri { get; set; }

        [MaxLength(255, ErrorMessageResourceName = "Per_Method_Length", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        public string per_method { get; set; }

        public string per_parent { get; set; }

        [Required(ErrorMessageResourceName = "Sort_Require", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        [RegularExpression(RegexExtension.Regex_Sort, ErrorMessageResourceName = "Sort_Regex", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        [MaxLength(10, ErrorMessageResourceName = "Sort_Length", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        public string per_sort { get; set; }

        [MaxLength(200, ErrorMessageResourceName = "Remark_Length", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        public string per_remark { get; set; }
    }
}