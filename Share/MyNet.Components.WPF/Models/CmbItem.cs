using MyNet.Components.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Components.WPF.Models
{
    /// <summary>
    /// 下拉框子项
    /// </summary>
    public class CmbItem : BaseModel
    {
        string _id;
        public string Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    base.RaisePropertyChanged("Id");
                }
            }
        }
        string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    base.RaisePropertyChanged("Text");
                }
            }
        }

        public bool IsDefault { get; set; }

        public override string ToString()
        {
            return string.Format("id:{0},text:{1}", Id, Text);
        }
    }
}
