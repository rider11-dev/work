using MyNet.Components.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    public class FileContent : BaseModel
    {
        public string filename { get; set; }
        public string filecode { get; set; }
        public string versioncode { get; set; }
        public string efdate { get; set; }
        public string writer { get; set; }
        public string remark { get; set; }
        public string draftdept { get; set; }
        public string checkdept1 { get; set; }
        public string checkdept2 { get; set; }
        public string checkdept3 { get; set; }
        public string approver { get; set; }
        private string _logo;
        public string logo
        {
            get { return _logo; }
            set
            {
                if (_logo != value)
                {
                    _logo = value;
                    base.RaisePropertyChanged("logo");
                }
            }
        }

    }
}
