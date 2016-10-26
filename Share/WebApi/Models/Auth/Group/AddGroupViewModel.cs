using MyNet.Components.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNet.WebApi.Models.Auth.Group
{
    public class AddGroupViewModel
    {
        [Required(ErrorMessageResourceName = "Code_Require", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        [MaxLength(20, ErrorMessageResourceName = "Group_Code_Length", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        public string gp_code { get; set; }
        [Required(ErrorMessageResourceName = "Name_Require", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        [MaxLength(40, ErrorMessageResourceName = "Group_Name_Length", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        public string gp_name { get; set; }
        public bool gp_system { get; set; }
        /// <summary>
        /// 上级组织编号
        /// </summary>
        public string gp_parent { get; set; }

        [Required(ErrorMessageResourceName = "Sort_Require", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        [RegularExpression(RegexExtension.Regex_Sort, ErrorMessageResourceName = "Sort_Regex", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        [MaxLength(10, ErrorMessageResourceName = "Sort_Length", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        public string gp_sort { get; set; }
    }
}