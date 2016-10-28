using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MyNet.Components.WPF.Extension
{
    /// <summary>
    /// 带附加图片的输入框装饰器
    /// </summary>
    public class InputImgAddOnAdorner : Adorner
    {
        public string ImageFile { get; private set; }
        public double ImageWidth { get; private set; }
        public double ImageHeight { get; private set; }
        public InputExtension.AddOnLocation Location { get; private set; }
        const int ImagePadding = 2;//为了不让图片覆盖输入框边框
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="adornedElement"></param>
        /// <param name="location">图片位置：Left、Right</param>
        /// <param name="imageFile">图片文件绝对路径，如果为空，则取Control.Tag属性</param>
        /// <param name="imageWidth">图片宽度，默认32，最大宽度为控件宽度</param>
        /// <param name="imageHeight">图片高度，默认32，最大高度为控件高度；小于控件高度时，居中显示</param>
        public InputImgAddOnAdorner(UIElement adornedElement, InputExtension.AddOnLocation location, string imageFile = "", double imageWidth = 32, double imageHeight = 32)
            : base(adornedElement)
        {
            var ctl = AdornedElement as Control;

            Location = location;
            ImageFile = string.IsNullOrEmpty(imageFile) ? ctl.Tag.ToString() : imageFile;
            if (ctl.Width < imageWidth || imageWidth <= 0)
            {
                ImageWidth = ctl.Width - ImagePadding;
            }
            else
            {
                ImageWidth = imageWidth;
            }

            if (ctl.Height - ImagePadding * 2 < imageHeight || imageHeight <= 0)
            {
                ImageHeight = ctl.Height - ImagePadding * 2;
            }
            else
            {
                ImageHeight = imageHeight;
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            Rect adornedElementRect = new Rect(this.AdornedElement.DesiredSize);
            var ctl = AdornedElement as Control;

            double x = Location == InputExtension.AddOnLocation.Left ? (adornedElementRect.TopLeft.X + ImagePadding) : (adornedElementRect.TopRight.X - ImageWidth - ImagePadding);
            double y = (adornedElementRect.Height - ImageHeight) / 2;
            var padding = Location == InputExtension.AddOnLocation.Left ? new Thickness(ImageWidth + 2, 0, 0, 0) : new Thickness(0, 0, ImageWidth + 2, 0);

            var imgFilePath = AppDomain.CurrentDomain.BaseDirectory + ImageFile;
            var uri = new Uri(File.Exists(imgFilePath) ? imgFilePath : ("pack://application:,,," + ImageFile), UriKind.Absolute);

            ImageSource img = new BitmapImage(uri);
            drawingContext.DrawImage(img, new Rect(new Point(x, y), new Size(ImageWidth, ImageHeight)));

            ctl.Padding = padding;
        }


    }
}
