using Newtonsoft.Json;
using MyNet.Components.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Components.Misc
{
    public class BaseModel : IDataErrorInfo, INotifyPropertyChanged, IBaseModel
    {
        private readonly Dictionary<string, PropertyInfo> _propertyGetters = new Dictionary<string, PropertyInfo>();
        private readonly Dictionary<string, ValidationAttribute[]> _validators = new Dictionary<string, ValidationAttribute[]>();
        private readonly Type _type;

        /// <summary>
        /// 标识是否可以验证，主要用来避免界面初次展现后即验证的问题
        /// </summary>
        [JsonIgnore]
        public bool CanValidate { get; set; }

        [JsonIgnore]
        public bool IsValid
        {
            get
            {
                return string.IsNullOrEmpty(this.Error);
            }
        }

        [JsonIgnore]
        public string Error
        {
            get
            {
                IEnumerable<string> errors = from val in _validators
                                             from attr in val.Value
                                             where !attr.IsValid(_propertyGetters[val.Key].GetValue(this))
                                             select attr.FormatErrorMessage(attr.ErrorMessage);
                return string.Join(Environment.NewLine, errors.ToArray());
            }
        }

        public string this[string columnName]
        {
            get
            {
                if (!CanValidate)
                {
                    return string.Empty;
                }
                return Validate(columnName);
            }
        }

        private string Validate(string columnName)
        {
            if (_propertyGetters.ContainsKey(columnName) && _validators.ContainsKey(columnName) && _validators[columnName].Count() > 0)
            {
                object value = _propertyGetters[columnName].GetValue(this);
                var rst = _validators[columnName].Where(v => !v.IsValid(value)).FirstOrDefault();
                return rst == null ? string.Empty : rst.FormatErrorMessage(rst.ErrorMessage);
            }
            return string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected BaseModel()
        {
            _type = GetType();
            LoadProperties();
        }

        private void LoadProperties()
        {
            PropertyInfo[] properties = _type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var pInfo in properties)
            {
                var attrs = pInfo.GetCustomAttributes(typeof(ValidationAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    _validators.Add(pInfo.Name, attrs as ValidationAttribute[]);
                    _propertyGetters.Add(pInfo.Name, pInfo);
                }
            }
        }

        public virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
