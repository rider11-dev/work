using MyNet.Components.Extensions;
using MyNet.Components.Misc;
using MyNet.Components.WPF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Client.Models.Auth
{
    public class GroupViewModel : CheckableModel, Iindexer, ICopytToable
    {
        public string gp_id { get; set; }
        string _gp_code;
        [Required(ErrorMessageResourceName = "Code_Require", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        [MaxLength(40, ErrorMessageResourceName = "Group_Code_Length", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        public string gp_code
        {
            get { return _gp_code; }
            set
            {
                if (_gp_code != value)
                {
                    _gp_code = value;
                    base.RaisePropertyChanged("gp_code");
                }
            }
        }

        string _gp_name;
        [Required(ErrorMessageResourceName = "Name_Require", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        [MaxLength(40, ErrorMessageResourceName = "Group_Name_Length", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        public string gp_name
        {
            get { return _gp_name; }
            set
            {
                if (_gp_name != value)
                {
                    _gp_name = value;
                    base.RaisePropertyChanged("gp_name");
                }
            }
        }

        string _gp_system;
        public string gp_system
        {
            get { return _gp_system; }
            set
            {
                if (_gp_system != value)
                {
                    _gp_system = value;
                    base.RaisePropertyChanged("gp_system");
                }
            }
        }

        string _gp_parent;
        public string gp_parent
        {
            get { return _gp_parent; }
            set
            {
                if (_gp_parent != value)
                {
                    _gp_parent = value;
                    base.RaisePropertyChanged("gp_parent");
                }
            }
        }

        string _gp_parent_name;
        public string gp_parent_name
        {
            get { return _gp_parent_name; }
            set
            {
                if (_gp_parent_name != value)
                {
                    _gp_parent_name = value;
                    base.RaisePropertyChanged("gp_parent_name");
                    if (string.IsNullOrEmpty(_gp_parent_name))
                    {
                        gp_parent = "";
                    }
                }
            }
        }

        string _gp_sort;
        [Required(ErrorMessageResourceName = "Sort_Require", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        [RegularExpression(RegexExtension.Regex_Sort, ErrorMessageResourceName = "Sort_Regex", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        [MaxLength(10, ErrorMessageResourceName = "Sort_Length", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        public string gp_sort
        {
            get { return _gp_sort; }
            set
            {
                if (_gp_sort != value)
                {
                    _gp_sort = value;
                    base.RaisePropertyChanged("gp_sort");
                }
            }
        }

        public void CopyTo(IBaseModel targetModel)
        {
            if (targetModel == null)
            {
                return;
            }
            var vmGroup = (GroupViewModel)targetModel;
            vmGroup.gp_id = this.gp_id;
            vmGroup.gp_code = this.gp_code;
            vmGroup.gp_name = this.gp_name;
            vmGroup.gp_system = this.gp_system;
            vmGroup.gp_parent = this.gp_parent;
            vmGroup.gp_sort = this.gp_sort;
            vmGroup.gp_parent_name = this.gp_parent_name;
        }
    }
}
