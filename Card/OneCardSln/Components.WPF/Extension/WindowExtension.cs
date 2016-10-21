using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyNet.Components.WPF.Extension
{
    public static class WindowExtension
    {
        public static void DragWhenLeftMouseDown(this Window win)
        {
            win.MouseLeftButtonDown += (o, e) =>
            {
                e.Handled = true;
                win.DragMove();
            };
        }
    }
}
