using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    [MetadataType(typeof(LoginViewModelMetadata))]
    public partial class LoginViewModel : BaseModel, ILoginViewModel
    {
        string _username;
        [MaxLength(length: 6, ErrorMessage = "最大长度为6个字符")]
        public string username
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    base.RaisePropertyChanged("username");
                }
            }
        }


    }

    public class LoginViewModelMetadata
    {
        [MaxLength(length: 6, ErrorMessage = "最大长度为6个字符")]
        public object username { get; set; }
    }
}
