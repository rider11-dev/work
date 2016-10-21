using Newtonsoft.Json;
using MyNet.Components;
using MyNet.Components.Extensions;
using MyNet.Components.Misc;
using MyNet.Components.Result;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Models;
using OneCardSln.OneCardClient.Public;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.OneCardClient.Models.Auth
{
    public class UserViewModel : CheckableModel, ICopytToable
    {
        public string user_id { get; set; }

        private string _usr_name;
        [Required(ErrorMessageResourceName = "User_Name_Require", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        [StringLength(10, MinimumLength = 3, ErrorMessageResourceName = "User_Name_Length", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
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
        [Required(ErrorMessageResourceName = "Idcard_Require", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        [StringLength(18, MinimumLength = 15, ErrorMessageResourceName = "Idcard_Length", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        [RegularExpression(RegexExtension.Regex_Idcard, ErrorMessageResourceName = "Idcard_Regx", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
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
        [Required(ErrorMessageResourceName = "User_TrueName_Require", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        [StringLength(10, MinimumLength = 2, ErrorMessageResourceName = "User_TrueName_Length", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
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
        [Required(ErrorMessageResourceName = "Regioncode_Require", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        [MaxLength(10, ErrorMessageResourceName = "Regioncode_Max", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
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
        [MaxLength(255, ErrorMessageResourceName = "Remark_Length", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
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

        public void CopyTo(IBaseModel targetModel)
        {
            if (targetModel == null)
            {
                return;
            }
            var vmUsr = (UserViewModel)targetModel;
            vmUsr.user_id = this.user_id;
            vmUsr.user_name = this.user_name;
            vmUsr.user_idcard = this.user_idcard;
            vmUsr.user_truename = this.user_truename;
            vmUsr.user_regioncode = this.user_regioncode;
            vmUsr.user_remark = this.user_remark;
        }
    }
}
