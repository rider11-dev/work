using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using MyNet.Components.Extensions;

namespace MyNet.Components.WPF.Converter
{
    public class BooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool en = parameter != null && parameter.ToString() == "en";//是否返回英文
            if (value.IsEmpty())
            {
                return en ? "false" : "否";
            }
            bool val = false;
            Boolean.TryParse(value.ToString(), out val);
            if (en)
            {
                return val ? "true" : "false";
            }
            else
            {
                return val ? "是" : "否";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.IsEmpty())
            {
                return false;
            }
            return value.ToString() == "是" || value.ToString().ToLower() == "true";
        }
    }
}
