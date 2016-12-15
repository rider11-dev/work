using MyNet.Components.WPF.Models;
using MyNet.CustomQuery.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.CustomQuery.Client.Models.ExecQuery
{
    public class SortViewModel : BaseModel
    {
        public Sort Sort { get; private set; }

        public SortViewModel()
        {
            Sort = new Sort { SortType = SortType.Asc };
        }

        public string Field
        {
            get { return Sort.Field; }
            set
            {
                if (Sort.Field != value)
                {
                    Sort.Field = value;
                }
                //这里暂时没有绑定后修改，所以先不报告更改了
            }
        }
        public string DisplayName
        {
            get { return Sort.DisplayName; }
            set
            {
                if (Sort.DisplayName != value)
                {
                    Sort.DisplayName = value;
                }
                //这里暂时没有绑定后修改，所以先不报告更改了
            }
        }
        public SortType SortType
        {
            get { return Sort.SortType; }
            set
            {
                if (Sort.SortType != value)
                {
                    Sort.SortType = value;
                }
                base.RaisePropertyChanged("SortType");
            }
        }
        public string FieldFullName
        {
            get
            {
                return Sort.FieldFullName;
            }
        }

        private int _order;
        public int Order
        {
            get { return _order; }
            set
            {
                if (_order != value)
                {
                    _order = value;
                    base.RaisePropertyChanged("Order");
                }
            }
        }

        public void ResetOrder()
        {
            Order = 0;
        }

        public override string ToString()
        {
            return Sort.ToString();
        }
    }
}
