using MyNet.Components.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MyNet.Components.WPF.Converter
{
    public class UnixTimeStampConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.IsNotEmpty())
            {
                double timeStamp = System.Convert.ToDouble(value);
                if (timeStamp == 0)
                {
                    return null;
                }
                return DateTimeExtension.ParseUnixTimeStamp(timeStamp);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}
