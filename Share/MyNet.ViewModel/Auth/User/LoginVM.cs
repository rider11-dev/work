using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNet.ViewModel.Auth.User
{
    public class LoginVM : ILoginVM
    {
        [Required(ErrorMessageResourceName = "User_Name_Require", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        [MaxLength(6, ErrorMessage = "最大长度为{1}个字符")]
        public string username { get; set; }

        [Required(ErrorMessageResourceName = "Pwd_Require", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        public string pwd { get; set; }

        public Type ValidateMetadataType
        {
            get
            {
                return this.GetType();
            }

            set
            {

            }
        }
    }
}