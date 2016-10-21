using MyNet.Model.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using MyNet.Components.Extensions;

namespace OneCardSln.OneCardClient.Converter
{
    public class PermTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.IsEmpty())
            {
                return "";
            }
            var str = value.ToString();
            return str.ToEnum<PermType>().GetDescription();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return "";
        }
    }
}
