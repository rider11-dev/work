using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

namespace OneCardSln.Components.WPF.Extension
{
    public static class InputExtension
    {
        /// <summary>
        /// 给指定控件添加附加图片
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="imgUrl">图片文件绝对路径，如果为空，则取Control.Tag属性</param>
        /// <param name="location">图片位置：Left、Right</param>
        /// <param name="imgWidth">图片宽度，默认32，最大宽度为控件宽度</param>
        /// <param name="imgHeight">图片高度，默认32，最大高度为控件高度；小于控件高度时，居中显示</param>
        public static void BindImageAddOn(this Control ctl, string imgUrl = "",
            InputImgAddOnAdorner.AddOnLocation location = InputImgAddOnAdorner.AddOnLocation.Right,
            double imgWidth = 32,
            double imgHeight = 32
            )
        {
            var layer = AdornerLayer.GetAdornerLayer(ctl);
            layer.Add(new InputImgAddOnAdorner(ctl, location, imgUrl, imgWidth, imgHeight));
        }
    }
}
