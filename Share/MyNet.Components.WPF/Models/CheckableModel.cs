using Newtonsoft.Json;
using MyNet.Components.Misc;
using MyNet.Components.WPF.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNet.Components.Validation;

namespace MyNet.Components.WPF.Models
{
    public class CheckableModel : BaseModel, ICheckable, IRowNumber
    {
        public CheckableModel() : this(true)
        {

        }
        public CheckableModel(bool needValidate) : base(needValidate)
        {

        }
        [JsonIgnore]
        [ValidateIgnore]
        public CheckableModelCollection BelongTo { get; set; }
        [JsonIgnore]
        [ValidateIgnore]
        public bool IsSingleSelect { get; set; }
        bool _checked;
        [JsonIgnore]
        [ValidateIgnore]
        public bool IsChecked
        {
            get
            {
                return _checked;
            }
            set
            {
                if (_checked != value)
                {
                    _checked = value;
                    base.RaisePropertyChanged("IsChecked");
                    if (BelongTo != null)
                    {
                        if (IsSingleSelect)
                        {
                            //单选
                            if (_checked == true)
                            {
                                var others = BelongTo.Models.Where(m => m != this).ToList();
                                others.ForEach(m => m.IsChecked = false);
                            }
                        }
                        else
                        {
                            //设置全选checkbox
                            if (_checked == false)
                            {
                                BelongTo.IsChecked = false;
                            }
                            else if (BelongTo.Models != null && BelongTo.Models.Count(m => m.IsChecked == false) == 0)
                            {
                                BelongTo.IsChecked = true;
                            }
                        }
                    }
                }
            }
        }

        [JsonIgnore]
        [ValidateIgnore]
        public int RowNumber { get; set; }
    }
}
