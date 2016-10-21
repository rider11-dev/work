using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
    }
}
