using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MyNet.Components.WPF.Extension
{
    public class InputAddonExtension
    {
        public static readonly DependencyProperty AddonWidthProperty = DependencyProperty.RegisterAttached("AddonWidth",
            typeof(int),
            typeof(InputAddonExtension), new PropertyMetadata(0, OnAddonWidthChanged));

        private static void OnAddonWidthChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var ele = obj as FrameworkElement;
            if (ele == null)
            {
                return;
            }
            ele.Loaded -= ItemContainerLoaded;
            ele.Loaded += ItemContainerLoaded;
        }

        public static int GetAddonWidth(DependencyObject obj)
        {
            return (int)obj.GetValue(AddonWidthProperty);
        }

        public static void SetAddonWidth(DependencyObject obj, int value)
        {
            obj.SetValue(AddonWidthProperty, value);
        }

        static void ItemContainerLoaded(object sender, RoutedEventArgs e)
        {
            var ctl = sender as Control;
            ctl.Loaded -= ItemContainerLoaded;
            if (ctl.Template == null)
            {
                return;
            }
            var addonCtl = ctl.Template.FindName("addon", ctl) as Control;
            if (addonCtl != null)
            {
                addonCtl.Width = GetAddonWidth(ctl);
            }

            var cntCtl = ctl.Template.FindName("PART_ContentHost", ctl) as Control;
            if (cntCtl != null)
            {
                var cntWidth = GetContentWidth(ctl);
                if (cntWidth != 0)
                {
                    cntCtl.Width = cntWidth;
                }
            }

        }

        public static readonly DependencyProperty ContentWidthProperty = DependencyProperty.RegisterAttached("ContentWidth",
           typeof(int),
           typeof(InputAddonExtension), new PropertyMetadata(0, OnContentWidthChanged));

        public static int GetContentWidth(DependencyObject obj)
        {
            return (int)obj.GetValue(ContentWidthProperty);
        }

        public static void SetContentWidth(DependencyObject obj, int value)
        {
            obj.SetValue(ContentWidthProperty, value);
        }

        private static void OnContentWidthChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var ele = obj as FrameworkElement;
            if (ele == null)
            {
                return;
            }
            ele.Loaded -= ItemContainerLoaded;
            ele.Loaded += ItemContainerLoaded;
        }

    }
}
