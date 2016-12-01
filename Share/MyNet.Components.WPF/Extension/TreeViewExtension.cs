using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        public static void ExpandAll(this TreeView tree)
        {
            if (tree == null)
            {
                return;
            }
            foreach (var item in tree.Items)
            {
                DependencyObject dObj = tree.ItemContainerGenerator.ContainerFromItem(item);
                ((TreeViewItem)dObj).ExpandSubtree();
            }
        }

        public static void CollapseAll(this TreeView tree)
        {
            if (tree == null)
            {
                return;
            }
            foreach (var item in tree.Items)
            {
                DependencyObject dObject = tree.ItemContainerGenerator.ContainerFromItem(item);
                CollapseTreeviewItems(tree, ((TreeViewItem)dObject));
            }
        }

        private static void CollapseTreeviewItems(TreeView tree, TreeViewItem item)
        {
            item.IsExpanded = false;

            foreach (var subItem in item.Items)
            {
                DependencyObject dObject = tree.ItemContainerGenerator.ContainerFromItem(subItem);

                if (dObject != null)
                {
                    ((TreeViewItem)dObject).IsExpanded = false;

                    if (((TreeViewItem)dObject).HasItems)
                    {
                        CollapseTreeviewItems(tree, ((TreeViewItem)dObject));
                    }
                }
            }
        }


    }
}
