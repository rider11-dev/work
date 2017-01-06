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
            //ContentPresenter cntPresenter = ctl.FindVisualChild<ContentPresenter>();
            //if (cntPresenter == null || cntPresenter.ContentTemplate == null)
            //{
            //    return;
            //}
            //var addonCtl = cntPresenter.ContentTemplate.FindName("addon", cntPresenter) as Control;
            //if (addonCtl == null)
            //{
            //    return;
            //}
            //addonCtl.Width = (int)e.NewValue;
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
            if (addonCtl == null)
            {
                return;
            }
            addonCtl.Width = GetAddonWidth(ctl);
        }
    }
}
