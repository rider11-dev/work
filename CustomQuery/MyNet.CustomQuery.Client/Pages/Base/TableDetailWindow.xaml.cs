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
using System.Windows.Shapes;
using MyNet.CustomQuery.Client.Models;
using MyNet.Components.WPF.Windows;

namespace MyNet.CustomQuery.Client.Pages.Base
{
    /// <summary>
    /// TableDetailWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TableDetailWindow : BaseWindow
    {
        private TableDetailViewModel _vmTable;

        public TableDetailWindow()
        {
            InitializeComponent();

            _vmTable = this.DataContext as TableDetailViewModel;
            _vmTable.Window = this;
        }

        public TableDetailWindow(TableViewModel vmTable = null) : this()
        {
            if (vmTable != null)
            {
                vmTable.CopyTo(_vmTable);
            }
            base.Title = _vmTable.IsNew ? "新增查询表" : "修改查询表";
        }

        private void TableDetailWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _vmTable.CanValidate = true;
        }
    }
}
