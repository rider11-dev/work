using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MyNet.Components.WPF.Extension
{
    public static class MiscExtension
    {
        /// <summary>
        /// 处理鼠标按键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void HandleMouseButtonEvent(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        public static ImageBrush GetImageBrush(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }
            ImageBrush imgBrush = new ImageBrush();
            imgBrush.ImageSource = new BitmapImage(new Uri(filePath, UriKind.Absolute));
            return imgBrush;
        }

        public static void SetBackgroundImg(this Panel panel, string imgFilePath)
        {
            panel.Background = GetImageBrush(imgFilePath);
        }
    }
}
