using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MyNet.Components.WPF.Validation
{
    public class RequiredRule : ValidationRule
    {
        /// <summary>
        /// 输入字段描述
        /// </summary>
        public string Description { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult(false, Description + "是必填项！");
            }
            return ValidationResult.ValidResult;
        }
    }
}
