using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using MyNet.Components.Extensions;
using MyNet.ClientFrame.Public;

namespace MyNet.ClientFrame.Converter
{
    public class GroupCodeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.IsEmpty())
            {
                return "";
            }
            var code = value.ToString();
            if (!DataCacheHelper.AllGroups.ContainsKey(value.ToString()))
            {
                return "";
            }
            return DataCacheHelper.AllGroups[code].gp_name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return "";
        }
    }
}
