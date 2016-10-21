using MyNet.Components.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MyNet.Components.WPF.Extension
{
    public static class DataGridExtension
    {
        /// <summary>
        /// 暂不使用
        /// </summary>
        /// <param name="dg"></param>
        public static void AddCheckBoxCol(this DataGrid dg)
        {
            DataGridTemplateColumn ckCol = new DataGridTemplateColumn();
            ckCol.CellTemplate = new DataTemplate();
            FrameworkElementFactory fef = new FrameworkElementFactory(typeof(CheckBox));
            Binding binding = new Binding();
            binding.Path = new PropertyPath("IsChecked");
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
            dg.SelectionMode = DataGridSelectionMode.Single;
        }

        /// <summary>
        /// 显示行号
        /// 如果行绑定模型实现Iindexer接口，则取其RowNumber；否则，设置为datagrid行号
        /// </summary>
        /// <param name="dg"></param>
        public static void ShowRowNumber(this DataGrid dg)
        {
            //TODO行号需要根据分页设置
            dg.LoadingRow += (o, e) =>
            {
                var item = e.Row.Item;
                if (item != null && item is Iindexer)
                {
                    e.Row.Header = ((Iindexer)item).RowNumber;
                }
                else
                {
                    e.Row.Header = e.Row.GetIndex() + 1;
                }
            };
        }

        /// <summary>
        /// 获取所有选中的行数据（通过CheckBox过滤）
        /// 暂不使用，通过模型绑定解决了
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static IList<TModel> GetCheckedItems<TModel>(this DataGrid grid) where TModel : class,ICheckable
        {
            IList<TModel> models = new List<TModel>();
            var rows = grid.Items.Count;
            if (rows > 0)
            {
                for (int idx = 0; idx < rows; idx++)
                {
                    DataGridRow row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(idx);
                    var model = (ICheckable)row.DataContext;
                    //因为CheckBox与ICheckable接口的IsChecked绑定了
                    /*如：
                     * <ControlTemplate>
                           <Border BorderThickness="0">
                     *          <CheckBox  HorizontalAlignment="Center"  VerticalAlignment="Center" IsChecked="{Binding Path=IsChecked, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}" />
                     *     </Border>
                       </ControlTemplate>
                     */
                    if (model.IsChecked)
                    {
                        models.Add((TModel)row.DataContext);
                    }
                }
            }

            return models;
        }

    }
}
