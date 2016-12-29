using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public partial class LoginModel : ILoginViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        string _username;
        public string username
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    RaisePropertyChanged("username");
                }
            }
        }

    }

    public partial class LoginModel : IDataErrorInfo
    {
        public string this[string columnName]
        {
            get
            {
                return this.ValidateProperty(columnName, typeof(LoginModelMetadata));
            }
        }

        public bool CanValidate { get; set; }

        public string Error
        {
            get
            {
                return string.Empty;
            }
        }

    }

    public class LoginModelMetadata
    {
        [DisplayName("用户名")]
        [MaxLength(6, ErrorMessage = "{0}最大长度为{1}个字符")]
        public string username { get; set; }
    }
}
