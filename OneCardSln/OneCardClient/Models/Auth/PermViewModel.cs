using Newtonsoft.Json;
using OneCardSln.Components.Extensions;
using OneCardSln.Components.Misc;
using OneCardSln.Components.WPF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OneCardSln.OneCardClient.Models.Auth
{
    public class PermViewModel : CheckableModel, Iindexer
    {
        public string per_id { get; set; }

        string _per_code;
        [Required(ErrorMessageResourceName = "Code_Require", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        [MaxLength(20, ErrorMessageResourceName = "Per_Code_Length", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        public string per_code
        {
            get { return _per_code; }
            set
            {
                if(_per_code!=value)
                {
                    _per_code = value;
                    base.RaisePropertyChanged("per_code");
                }
            }
        }

        string _per_name;
        [Required(ErrorMessageResourceName = "Name_Require", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        [MaxLength(40, ErrorMessageResourceName = "Per_Name_Length", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        public string per_name
        {
            get { return _per_name; }
            set
            {
                if (_per_name != value)
                {
                    _per_name = value;
                    base.RaisePropertyChanged("per_name");
                }
            }
        }

        string _per_type;
        [Required(ErrorMessageResourceName = "Per_Type_Require", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        public string per_type
        {
            get { return _per_type; }
            set
            {
                if (_per_type != value)
                {
                    _per_type = value;
                    base.RaisePropertyChanged("per_type");
                }
            }
        }

        string _per_uri;
        [MaxLength(255, ErrorMessageResourceName = "Per_Uri_Length", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        public string per_uri
        {
            get { return _per_uri; }
            set
            {
                if (_per_uri != value)
                {
                    _per_uri = value;
                    base.RaisePropertyChanged("per_uri");
                }
            }
        }

        string _per_method;
        [MaxLength(255, ErrorMessageResourceName = "Per_Method_Length", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        public string per_method
        {
            get { return _per_method; }
            set
            {
                if (_per_method != value)
                {
                    _per_method = value;
                    base.RaisePropertyChanged("per_method");
                }
            }
        }

        string _per_parent;
        public string per_parent
        {
            get { return _per_parent; }
            set
            {
                if (_per_parent != value)
                {
                    _per_parent = value;
                    base.RaisePropertyChanged("per_parent");
                }
            }
        }

        string _per_sort;
        [Required(ErrorMessageResourceName = "Sort_Require", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        [RegularExpression(RegexExtension.Regex_Sort, ErrorMessageResourceName = "Sort_Regex", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        [MaxLength(10, ErrorMessageResourceName = "Sort_Length", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        public string per_sort
        {
            get { return _per_sort; }
            set
            {
                if (_per_sort != value)
                {
                    _per_sort = value;
                    base.RaisePropertyChanged("per_sort");
                }
            }
        }

        string _per_remark;
        [MaxLength(200, ErrorMessageResourceName = "Remark_Length", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        public string per_remark
        {
            get { return _per_remark; }
            set
            {
                if (_per_remark != value)
                {
                    _per_remark = value;
                    base.RaisePropertyChanged("per_remark");
                }
            }
        }

    }
}