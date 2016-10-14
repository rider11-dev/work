using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace OneCardSln.Components.WPF.Extension
{
    public static class DataGridExtension
    {
        public static void AddCheckBoxCol(this DataGrid dg)
        {
            DataGridTemplateColumn ckCol = new DataGridTemplateColumn();
            ckCol.CellTemplate = new DataTemplate();
            FrameworkElementFactory fef = new FrameworkElementFactory(typeof(CheckBox));
            Binding binding = new Binding();
            binding.Path = new PropertyPath("Selected");
            fef.SetBinding(CheckBox.ContentProperty, binding);
            ckCol.CellTemplate.VisualTree = fef;

            //下面代码没有使用，但展示了通过后台代码应用Style的途径，值得参考
            //Style style = new Style();
            ////headerStyle
            //style.Setters.Add(new Setter(FrameworkElement.WidthProperty, 40.0));//Width类型为double，这里不能输入整数
            //style.Setters.Add(new Setter(FrameworkElement.MaxWidthProperty, 40.0));
            //style.Setters.Add(new Setter(FrameworkElement.MinWidthProperty, 40.0));
            //ckCol.HeaderStyle = style;
            ////cellStyle
            //style = new Style();
            //style.Setters.Add(new Setter(FrameworkElement.MaxWidthProperty, 40.0));
            //style.Setters.Add(new Setter(FrameworkElement.MinWidthProperty, 40.0));
            //ckCol.CellStyle = style;

            ckCol.Width = 32;
            ckCol.CanUserResize = false;

            ckCol.DisplayIndex = 0;
            dg.Columns.Add(ckCol);
        }

        public static void ShowRowNumber(this DataGrid dg)
        {
            //TODO行号需要根据分页设置
            dg.LoadingRow += (o, e) =>
            {
                e.Row.Header = e.Row.GetIndex() + 1;
            };
        }
    }
}
