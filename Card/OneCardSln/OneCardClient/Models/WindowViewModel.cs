using MyNet.Components.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.OneCardClient.Models
{
    public class WindowViewModel : BaseModel
    {
        string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    base.RaisePropertyChanged("Title");
                }
            }
        }
    }
}
