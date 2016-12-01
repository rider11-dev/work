using MyNet.Components.Extensions;
using MyNet.Components.WPF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNet.Client.Models.Account
{
    public class ChangePwdViewModel : BaseModel
    {
        string _oldpwd;
        [Required(ErrorMessageResourceName = "OldPwd_Require", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        public string oldpwd
        {
            get { return _oldpwd; }
            set
            {
                if (_oldpwd != value)
                {
                    _oldpwd = value;
                    base.RaisePropertyChanged("oldpwd");
                }
            }
        }

        string _newpwd;
        [Required(ErrorMessageResourceName = "NewPwd_Require", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        [RegularExpression(RegexExtension.Regex_Pwd, ErrorMessageResourceName = "Pwd_Regex", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        public string newpwd
        {
            get { return _newpwd; }
            set
            {
                if (_newpwd != value)
                {
                    _newpwd = value;
                    base.RaisePropertyChanged("newpwd");
                }
            }
        }

        string _newpwd2;
        [Required(ErrorMessageResourceName = "NewPwd2_Require", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        //TODO
        //[Compare("newpwd", ErrorMessage = "两次输入新密码不相同")]//报错，暂不启用，后续处理
        public string newpwd2
        {
            get { return _newpwd2; }
            set
            {
                if (_newpwd2 != value)
                {
                    _newpwd2 = value;
                    base.RaisePropertyChanged("newpwd2");
                }
            }
        }

        public bool HandlyCheck(out string error)
        {
            error = string.Empty;
            if (!newpwd.Equals(newpwd2))
            {
                error = "两次输入新密码不一致";
                return false;
            }
            if (oldpwd.Equals(newpwd))
            {
                error = "新旧密码不能相同";
                return false;
            }

            return true;
        }

        public void Reset()
        {
            oldpwd = newpwd = newpwd2 = "";
        }
    }
}