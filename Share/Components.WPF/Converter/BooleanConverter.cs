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
            if (value.IsEmpty())
            {
                return "否";
            }
            bool val = false;
            Boolean.TryParse(value.ToString(), out val);
            return val ? "是" : "否";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.IsEmpty())
            {
                return false;
            }
            return value.ToString() == "是";
        }
    }
}
