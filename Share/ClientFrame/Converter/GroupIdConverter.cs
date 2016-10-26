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
    public class GroupIdConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.IsEmpty())
            {
                return "";
            }
            var id = value.ToString();
            var val = DataCacheHelper.AllGroups.Where(kvp => kvp.Value.gp_id == id).FirstOrDefault();
            if (val.Value == null)
            {
                return "";
            }
            return val.Value.gp_name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return "";
        }
    }
}
