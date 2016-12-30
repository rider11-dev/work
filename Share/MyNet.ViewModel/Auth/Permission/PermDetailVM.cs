using MyNet.Components.Extensions;
using MyNet.Components.Mapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNet.ViewModel.Auth.Permission
{
    public class PermDetailVM : IPermDetailVM
    {
        public string per_id { get; set; }
        [Required(ErrorMessageResourceName = "Code_Require", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        [MaxLength(40, ErrorMessageResourceName = "Per_Code_Length", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        public string per_code { get; set; }

        [Required(ErrorMessageResourceName = "Name_Require", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        [MaxLength(40, ErrorMessageResourceName = "Per_Name_Length", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        public string per_name { get; set; }

        [Required(ErrorMessageResourceName = "Per_Type_Require", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        public string per_type { get; set; }

        [MaxLength(255, ErrorMessageResourceName = "Per_Uri_Length", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        public string per_uri { get; set; }

        [MaxLength(255, ErrorMessageResourceName = "Per_Method_Length", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        public string per_method { get; set; }

        public string per_parent { get; set; }
        public bool per_system { get; set; }

        [Required(ErrorMessageResourceName = "Sort_Require", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        [RegularExpression(RegexExtension.Regex_Sort, ErrorMessageResourceName = "Sort_Regex", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        [MaxLength(10, ErrorMessageResourceName = "Sort_Length", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        public string per_sort { get; set; }

        [MaxLength(200, ErrorMessageResourceName = "Remark_Length", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        public string per_remark { get; set; }

        public Type ValidateMetadataType { get { return this.GetType(); } set { } }

        public void CopyTo(object target)
        {
            if (target == null)
            {
                return;
            }
            OOMapper.Map(this.GetType(), this.GetType(), this, target);
        }
    }
}