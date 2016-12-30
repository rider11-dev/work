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
using MyNet.Components.Validation;
using MyNet.Components.Extensions;

namespace MyNet.Components.Misc
{
    public partial class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public partial class BaseModel : IDataErrorInfo, IBaseModel, IValidateMeta
    {
        public BaseModel() : this(true)
        {

        }

        public BaseModel(bool needValidate)
        {
            if (needValidate)
            {
                LoadProperties();
            }
        }

        private readonly Dictionary<string, PropertyInfo> _properties = new Dictionary<string, PropertyInfo>();
        private readonly Dictionary<string, ValidationAttribute[]> _validators = new Dictionary<string, ValidationAttribute[]>();
        bool _canValidate;
        /// <summary>
        /// 标识是否可以验证，主要用来避免界面初次展现后即验证的问题（如需验证，需要在Load事件后设置为True）
        /// </summary>
        [JsonIgnore]
        [ValidateIgnore]
        public bool CanValidate
        {
            get { return _canValidate; }
            set
            {
                _canValidate = value;
                //遍历下级属性，如果有继承IBaseModel的，也把对应的CanValidate设置下
                _properties.ToList().ForEach(kvp =>
                {
                    if (PropertyIsIBaseModel(kvp.Value))
                    {
                        (kvp.Value.GetValue(this) as IBaseModel).CanValidate = _canValidate;
                    }
                });
            }
        }

        [JsonIgnore]
        [ValidateIgnore]
        public bool IsValid
        {
            get
            {
                if (_properties.IsEmpty())
                {
                    return true;
                }
                foreach (var kvp in _properties)
                {
                    //1、实现了IBaseModel接口的属性校验
                    if (PropertyIsIBaseModel(kvp.Value))
                    {
                        if ((kvp.Value.GetValue(this) as IBaseModel).IsValid == false)
                        {
                            return false;
                        }
                        continue;
                    }
                    //2、常规属性校验
                    var error = Validate(kvp.Key);
                    if (error.IsNotEmpty())
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        [JsonIgnore]
        [ValidateIgnore]
        public string Error
        {
            get
            {
                if (_properties.IsEmpty())
                {
                    return string.Empty;
                }
                var errors = _properties.Select(kvp =>
                {
                    return Validate(kvp.Value.Name);
                }).Where(s => s.IsNotEmpty());
                return string.Join(Environment.NewLine, errors.ToArray());
            }
        }

        [JsonIgnore]
        [ValidateIgnore]
        public Type ValidateMetadataType { get; set; }

        [ValidateIgnore]
        public string this[string columnName]
        {
            get
            {
                return Validate(columnName);
            }
        }

        private string Validate(string propName)
        {
            if (!CanValidate)
            {
                return string.Empty;
            }

            //1、继承IBaseModel的属性
            if (PropertyIsIBaseModel(_properties[propName]))
            {
                return (_properties[propName].GetValue(this) as IBaseModel).Error;
            }

            //2、一般属性
            //2.1、类型本身验证
            var error = LocalValidate(propName);
            if (error.IsNotEmpty())
            {
                return error;
            }
            //2.2、MetadataType验证
            if (ValidateMetadataType != null)
            {
                return this.ValidateProperty(propName, ValidateMetadataType);
            }
            return string.Empty;
        }

        private string LocalValidate(string columnName)
        {
            if (_properties.ContainsKey(columnName) && _validators.ContainsKey(columnName) && _validators[columnName].Count() > 0)
            {
                object value = _properties[columnName].GetValue(this);
                var rst = _validators[columnName].Where(v => !v.IsValid(value)).FirstOrDefault();
                return rst == null ? string.Empty : rst.FormatErrorMessage(rst.ErrorMessage);

            }
            return string.Empty;
        }

        private void LoadProperties()
        {
            //加载类属性，忽略BaseModel本身的属性
            PropertyInfo[] properties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var pInfo in properties)
            {
                //是否忽略验证
                var valIgnore = pInfo.GetCustomAttribute(typeof(ValidateIgnoreAttribute)) != null;
                if (valIgnore == true)
                {
                    continue;
                }
                _properties.Add(pInfo.Name, pInfo);
                var attrs = pInfo.GetCustomAttributes(typeof(ValidationAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    _validators.Add(pInfo.Name, attrs as ValidationAttribute[]);
                }
            }
        }

        private bool PropertyIsIBaseModel(PropertyInfo prop)
        {
            if (prop == null)
            {
                return false;
            }
            if (prop.PropertyType.IsA<IBaseModel>())
            {
                return true;
            }
            var propVal = prop.GetValue(this);
            if (propVal == null)
            {
                return false;
            }
            return propVal.GetType().IsA<IBaseModel>();
        }

    }
}
