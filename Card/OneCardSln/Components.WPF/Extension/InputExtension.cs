using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;

namespace MyNet.Components.WPF.Extension
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
            InputExtension.AddOnLocation location = InputExtension.AddOnLocation.Right,
            double imgWidth = 32,
            double imgHeight = 32
            )
        {
            var layer = AdornerLayer.GetAdornerLayer(ctl);
            layer.Add(new InputImgAddOnAdorner(ctl, location, imgUrl, imgWidth, imgHeight));
        }

        /// <summary>
        /// 限制输入，使用正则表达式
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="pattern"></param>
        /// <param name="illegalKeys">非法字符</param>
        public static void LimitInput(this TextBox txt, string pattern, IEnumerable<Key> illegalKeys = null)
        {
            //屏蔽非法按键
            if (illegalKeys != null && illegalKeys.Count() > 0)
            {
                txt.KeyDown += (o, e) =>
                    {
                        if (illegalKeys.Contains(e.Key))
                        {
                            e.Handled = true;
                            return;
                        }
                    };
            }
            //限制输入
            if (!string.IsNullOrEmpty(pattern))
            {
                txt.TextChanged += (o, e) =>
                {
                    TextBox txtBox = o as TextBox;
                    TextChange[] chg = new TextChange[e.Changes.Count];
                    e.Changes.CopyTo(chg, 0);
                    int offset = chg[0].Offset;
                    if (chg[0].AddedLength > 0)
                    {
                        if (!Regex.IsMatch(txtBox.Text, pattern))
                        {
                            txtBox.Text = txtBox.Text.Remove(offset, chg[0].AddedLength);
                            txtBox.Select(offset, 0);
                        }
                    }
                };
            }
        }

        public enum AddOnLocation
        {
            Left,
            Right
        }
    }
}
