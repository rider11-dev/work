using MyNet.Client.Pages;
using MyNet.Components.Extensions;
using MyNet.Components.WPF.Command;
using MyNet.CustomQuery.Client.Models;
using MyNet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyNet.CustomQuery.Client.Pages
{
    /// <summary>
    /// ExecQueryPage.xaml 的交互逻辑
    /// </summary>
    public partial class ExecQueryPage : BasePage
    {
        ExecQueryModel model = null;
        public ExecQueryPage()
        {
            InitializeComponent();

            model = this.DataContext as ExecQueryModel;
            model.ViewSrcFields = this.FindResource("viewSrcFields") as CollectionViewSource;
            model.ViewSrcSelFields = this.FindResource("viewSrcSelFields") as CollectionViewSource;
        }

        private void ExecQueryPage_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }

        private void Init()
        {
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //datagrid的行改变时也会触发该事件，故这里过滤一下
            if (e.OriginalSource is TabControl)
            {
                //e.Handled = true;
                //return;
                if (!e.AddedItems.IsEmpty())
                {
                    var tab = e.AddedItems[0] as TabItem;
                    if (tab != tabTables)
                    {
                        model.FilterFields();
                        model.FilterSelFieldsSrc();
                    }
                }
            }

        }
    }
}
