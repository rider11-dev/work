using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MyNet.Components.WPF.Extension
{
    public static class TreeViewExtension
    {
        public static void Select(this TreeView tv, int idx = 0)
        {
            if (tv.Items != null && tv.Items.Count > 0)
            {
                TreeViewItem item = ((TreeViewItem)tv.ItemContainerGenerator.ContainerFromIndex(idx));
                item.IsSelected = true;
                item.Focus();
            }
        }
    }
}
