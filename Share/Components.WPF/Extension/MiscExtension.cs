using Microsoft.Win32;
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

        public static ImageSource GetImageSource(string filePath)
        {
            BitmapImage bitmap = new BitmapImage(new Uri(filePath, UriKind.Absolute));
            return bitmap;
        }

        public static void SetBackgroundImg(this Panel panel, string imgFilePath)
        {
            panel.Background = GetImageBrush(imgFilePath);
        }

        public static TElement FindVisualChild<TElement>(this DependencyObject obj)
            where TElement : DependencyObject
        {
            if (obj == null)
            {
                return null;
            }
            var cnt = VisualTreeHelper.GetChildrenCount(obj);
            if (cnt < 1)
            {
                return null;
            }
            for (int idx = 0; idx < cnt; idx++)
            {
                var child = VisualTreeHelper.GetChild(obj, idx);
                if (child != null && child is TElement)
                {
                    return (TElement)child;
                }
                else
                {
                    TElement childOfChild = FindVisualChild<TElement>(child);
                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }
            return null;
        }

        public static void SetSource(this Image img, string imgFilePath = "")
        {
            string imgFile = imgFilePath;
            if (string.IsNullOrEmpty(imgFile))
            {
                OpenFileDialog dia = new OpenFileDialog();
                dia.Filter = "图像文件|*.jpg;*.png;*.gif";
                var result = dia.ShowDialog();
                if (result != null && (bool)result == true)
                {
                    imgFile = dia.FileName;
                }
            }
            if (string.IsNullOrEmpty(imgFile))
            {
                return;
            }

            img.Source = MiscExtension.GetImageSource(imgFile);
        }
    }
}
