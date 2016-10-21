using MyNet.Components.WPF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.OneCardClient.Models
{
    [Serializable]
    public class LoginViewModel : BaseModel
    {
        string _userName;
        [Required(ErrorMessage = "用户名不能为空")]
        [MaxLength(10, ErrorMessage = "用户名最大长度为{1}个字符")]
        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    base.RaisePropertyChanged("UserName");
                }
            }
        }
        string _pwd;
        [Required(ErrorMessage = "密码不能为空")]
        [MaxLength(10, ErrorMessage = "密码最大长度为{1}个字符")]
        public string Pwd
        {
            get { return _pwd; }
            set
            {
                if (_pwd != value)
                {
                    _pwd = value;
                    base.RaisePropertyChanged("Pwd");
                }
            }
        }
        string _verifyCode;
        [Required(ErrorMessage = "验证码不能为空")]
        //[Compare("VerifyCodeTarget", ErrorMessage = "验证码错误")]
        public string VerifyCode
        {
            get
            {
                return _verifyCode;
            }
            set
            {
                if (_verifyCode != value)
                {
                    _verifyCode = value;
                    base.RaisePropertyChanged("VerifyCode");
                }
            }
        }
        /// <summary>
        /// 目标验证码
        /// </summary>
        public string VerifyCodeTarget { get; set; }
        public bool RememberMe { get; set; }

        public LoginViewModel()
        {
            UserName = "";
            Pwd = "";
            VerifyCode = "";
        }

        public override string ToString()
        {
            return string.Format("UserName:{0},Pwd:{1},VerifyCode:{2},RememberMe:{3}", UserName, Pwd, VerifyCode, RememberMe);
        }

        public bool CheckVerifyCode()
        {
            return VerifyCode == VerifyCodeTarget;
        }
    }
}
