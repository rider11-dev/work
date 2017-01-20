using MyNet.Components.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace MyNet.Components.WPF.Converter
{
    public class ImgSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return null;
            }
            Uri uri = new Uri(value.ToString(), UriKind.RelativeOrAbsolute);
            if (uri.IsAbsoluteUri && !uri.Host.ToLower().Contains("siteoforigin") &&
                (uri.AbsolutePath.IsEmpty() || !File.Exists(uri.AbsolutePath)))
            {
                return null;
            }
            BitmapImage bitmapImg = new BitmapImage();
            bitmapImg.BeginInit();
            bitmapImg.UriSource = uri;
            bitmapImg.EndInit();

            return bitmapImg;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
