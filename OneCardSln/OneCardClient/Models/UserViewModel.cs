using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.OneCardClient.Models
{
    public class UserViewModel : BaseViewModel
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
        public string user_idcard { get; set; }
        public string user_truename { get; set; }
        public string user_regioncode { get; set; }
        public string user_remark { get; set; }

    }
}
