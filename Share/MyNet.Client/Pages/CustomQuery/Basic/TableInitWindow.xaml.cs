using MyNet.Client.Models.CustomQuery;
using MyNet.Components.WPF.Extension;
using MyNet.Components.WPF.Windows;
using System.Windows;

namespace MyNet.Client.Pages.CustomQuery.Basic
{
    /// <summary>
    /// TableInitWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TableInitWindow : BaseWindow
    {
        TableInitViewModel _model;
        public TableInitWindow()
        {
            _model = new TableInitViewModel { Window = this };
            this.DataContext = _model;
            InitializeComponent();
        }

        private void TableInitWindow_Loaded(object sender, RoutedEventArgs e)
        {
            InitDataGrid();
            btnInit.Command.Execute(null);
        }

        private void InitDataGrid()
        {
            dgTables.ShowRowNumber();
        }
    }
}
