using MyNet.Components.Extensions;
using MyNet.Components.Mapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNet.ViewModel.Auth.Group
{
    public class GroupVM 
    {
        public string gp_id { get; set; }
        [Required(ErrorMessageResourceName = "Code_Require", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        [MaxLength(40, ErrorMessageResourceName = "Group_Code_Length", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        public string gp_code { get; set; }
        [Required(ErrorMessageResourceName = "Name_Require", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        [MaxLength(40, ErrorMessageResourceName = "Group_Name_Length", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        public string gp_name { get; set; }
        public bool gp_system { get; set; }
        /// <summary>
        /// 上级组织编号
        /// </summary>
        public string gp_parent { get; set; }

        [Required(ErrorMessageResourceName = "Sort_Require", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        [RegularExpression(RegexExtension.Regex_Sort, ErrorMessageResourceName = "Sort_Regex", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        [MaxLength(10, ErrorMessageResourceName = "Sort_Length", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        public string gp_sort { get; set; }
    }
}