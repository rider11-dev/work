using System.Windows;
using MyNet.Components.WPF.Windows;
using MyNet.Client.Models.CustomQuery;

namespace MyNet.Client.Pages.CustomQuery.Basic
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
