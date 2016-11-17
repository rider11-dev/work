using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MyNet.Components.WPF.Extension;
using System.IO;

namespace MyNet.Components.WPF.Controls
{
    /// <summary>
    /// ControlImgSelect.xaml 的交互逻辑
    /// </summary>
    public partial class ControlImgSelect : UserControl
    {
        public string ImgFile
        {
            get { return (string)GetValue(ImgFileProperty); }
            set
            {
                SetValue(ImgFileProperty, value);
                if (!string.IsNullOrEmpty(value))
                {
                    img.Source = MiscExtension.GetImageSource(value);
                }
            }
        }

        public static readonly DependencyProperty ImgFileProperty = DependencyProperty.Register("ImgFile", typeof(string), typeof(ControlImgSelect), new PropertyMetadata(""));

        public ControlImgSelect()
        {
            InitializeComponent();
        }

        private void btnSel_Click(object sender, RoutedEventArgs e)
        {
            img.SetSource();
        }

        public string Base64Data
        {
            get
            {
                if (img.Source == null)
                {
                    return "";
                }
                //WriteableBitmap wb = new WriteableBitmap(img.Source as BitmapSource);//将Image对象转换为WriteableBitmap 
                //wb.
                //byte[] b = Convert.FromBase64String(GetBase64Image(wb));//得到byte数组
                string data = "";
                try
                {
                    var dd = img.Source as BitmapImage;
                    var file = dd.UriSource.LocalPath;
                    if (File.Exists(file))
                    {
                        data = ImageHelper.Base64Encode(file);
                    }
                }
                catch { }
                return data;
            }
        }
    }
}
