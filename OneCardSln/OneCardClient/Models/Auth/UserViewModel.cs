using OneCardSln.Components.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.OneCardClient.Models.Auth
{
    public class UserViewModel : BaseViewModel, ICloneable
    {
        public string user_id { get; set; }

        private string _usr_name;
        public string user_name
        {
            get { return _usr_name; }
            set
            {
                if (_usr_name == value)
                {
                    return;
                }
                _usr_name = value;
                base.RaisePropertyChanged("user_name");
            }
        }

        private string _user_idcard;
        [Required(ErrorMessageResourceName = "Idcard_Require", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        [StringLength(18, MinimumLength = 15, ErrorMessageResourceName = "Idcard_Length", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        [RegularExpression(RegexExtension.Regex_Idcard, ErrorMessageResourceName = "Idcard_Regx", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        public string user_idcard
        {
            get { return _user_idcard; }
            set
            {
                if (_user_idcard == value)
                {
                    return;
                }
                _user_idcard = value;
                base.RaisePropertyChanged("user_idcard");
            }
        }

        private string _user_truename;
        [Required(ErrorMessageResourceName = "User_TrueName_Require", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        [StringLength(10, MinimumLength = 2, ErrorMessageResourceName = "User_TrueName_Length", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        public string user_truename
        {
            get { return _user_truename; }
            set
            {
                if (_user_truename == value)
                {
                    return;
                }
                _user_truename = value;
                base.RaisePropertyChanged("user_truename");
            }
        }

        private string _user_regioncode;
        [Required(ErrorMessageResourceName = "Regioncode_Require", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        [MaxLength(10, ErrorMessageResourceName = "Regioncode_Max", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        public string user_regioncode
        {
            get { return _user_regioncode; }
            set
            {
                if (_user_regioncode == value)
                {
                    return;
                }
                _user_regioncode = value;
                base.RaisePropertyChanged("user_regioncode");
            }
        }

        private string _user_remark;
        [MaxLength(255, ErrorMessageResourceName = "Remark_Length", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        public string user_remark
        {
            get { return _user_remark; }
            set
            {
                if (_user_remark == value)
                {
                    return;
                }
                _user_remark = value;
                base.RaisePropertyChanged("user_remark");
            }
        }


        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
