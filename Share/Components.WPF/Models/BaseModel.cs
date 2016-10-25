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
using System.Windows.Input;

namespace MyNet.Components.WPF.Models
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
                //return Validate(columnName);
                return Validate1(columnName);
            }
        }

        private string Validate(string columnName)
        {
            var context = new ValidationContext(this);
            context.MemberName = columnName;
            var valResult = new List<ValidationResult>();
            Validator.TryValidateProperty(this.GetType().GetProperty(columnName).GetValue(this), context, valResult);
            if (valResult.Count > 0)
            {
                //获取第一个错误信息
                var valRresult1 = valResult
                    .Where(r => !string.IsNullOrEmpty(r.ErrorMessage))
                    .FirstOrDefault();
                if (valRresult1 != null)
                {
                    return valRresult1.ErrorMessage;
                }
            }
            return string.Empty;
        }

        private string Validate1(string columnName)
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
            LoadData();
        }

        private void LoadData()
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

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        [JsonIgnore]
        public virtual Dictionary<string, ICommand> Commands
        {
            get
            {
                return null;
            }
        }
    }
}
