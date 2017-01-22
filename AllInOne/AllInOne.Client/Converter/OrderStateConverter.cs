using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AllInOne.Client.Converter
{
    public class OrderStateConverter : IValueConverter
    {
        static Dictionary<string, string> _dictStates = new Dictionary<string, string>
        {
            { "0","已取消"},{ "10","未付款"},{ "20","已付款"},
            { "30","已发货"},{ "40","已收货"}
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stateKey = (string)value;
            if (_dictStates.ContainsKey(stateKey))
            {
                return _dictStates[stateKey];
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}
