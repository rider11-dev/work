using MyNet.Components.Emit;
using MyNet.Components.Misc;
using MyNet.Components.WPF.Models;
using MyNet.ViewModel.Auth.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Client.Models
{
    public class LoginViewModel : BaseModel
    {
        public ILoginVM logindata { get; private set; }

        public LoginViewModel()
        {
            logindata = DynamicModelBuilder.GetInstance<ILoginVM>(typeof(BaseModel));
            logindata.ValidateMetadataType = typeof(LoginVM);
        }

        string _verifyCode;
        [Required(ErrorMessage = "验证码不能为空")]
        //TODO：加Compare的话，会报错，原因不明， 暂不加
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

        public bool CheckVerifyCode()
        {
            return VerifyCode == VerifyCodeTarget;
        }
    }
}
