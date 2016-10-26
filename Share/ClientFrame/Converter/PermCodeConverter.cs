using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using MyNet.Components.Extensions;
using MyNet.ClientFrame.Public;

namespace MyNet.ClientFrame.Converter
{
    public class PermCodeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.IsEmpty())
            {
                return "";
            }
            var code = value.ToString();
            if (!DataCacheHelper.AllFuncs.ContainsKey(value.ToString()))
            {
                return "";
            }
            return DataCacheHelper.AllFuncs[code].per_name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}
