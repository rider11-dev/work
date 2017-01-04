using MyNet.Components.WPF.Extension;
using MyNet.Components.WPF.Windows;
using MyNet.CustomQuery.Client.Models;
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

namespace MyNet.CustomQuery.Client.Pages.Base
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
