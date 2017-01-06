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

    }
}
