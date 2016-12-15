using MyNet.Components.WPF.Models;
using MyNet.CustomQuery.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.CustomQuery.Client.Models.ExecQuery
{
    public class ConditionViewModel : BaseModel
    {
        public Condition Condition { get; private set; }

        public ConditionViewModel()
        {
            Condition = new Condition();
        }

        public string Field
        {
            get { return Condition.Field; }
            set
            {
                if (Condition.Field != value)
                {
                    Condition.Field = value;
                    base.RaisePropertyChanged("Field");
                }
            }
        }

        public CompositeType CmpType
        {
            get { return Condition.CmpType; }
            set
            {
                if (Condition.CmpType != value)
                {
                    Condition.CmpType = value;
                    base.RaisePropertyChanged("CmpType");
                }
            }
        }

        public ConditionType ConditionType
        {
            get { return Condition.ConditionType; }
            set
            {
                if (Condition.ConditionType != value)
                {
                    Condition.ConditionType = value;
                    base.RaisePropertyChanged("ConditionType");
                }
            }
        }

        public FieldType FieldType
        {
            get { return Condition.FieldType; }
            set
            {
                if (Condition.FieldType != value)
                {
                    Condition.FieldType = value;
                    base.RaisePropertyChanged("FieldType");
                }
            }
        }

        /// <summary>
        /// 是否取反
        /// </summary>
        public bool IsChecked
        {
            get { return Condition.Not; }
            set
            {
                if (Condition.Not != value)
                {
                    Condition.Not = value;
                    base.RaisePropertyChanged("IsChecked");
                }
            }
        }

        public dynamic Value
        {
            get { return Condition.Value; }
            set
            {
                if (Condition.Value != value)
                {
                    Condition.Value = value;
                    base.RaisePropertyChanged("Value");
                }
            }
        }

        private string _fieldFullName;
        public string FieldFullName
        {
            get { return _fieldFullName; }
            set
            {
                if (_fieldFullName != value)
                {
                    _fieldFullName = value;
                    base.RaisePropertyChanged("FieldFullName");
                }
            }
        }


    }
}
