using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MyNet.Components.WPF.Converter
{
    public class LevelIndentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var level = (int)value;
            //return new Thickness(8 * level + 10 * (level - 1), 0, 0, 0);
            //int delta = level < 3 ? 40 : 8;
            int indent = 30 * (level - 1) + 20;

            return new Thickness(indent, 0, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
