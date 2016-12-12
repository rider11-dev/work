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
    /// FieldInitWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FieldInitWindow : BaseWindow
    {
        FieldInitViewModel _model;
        public FieldInitWindow(IEnumerable<TableViewModel> tables)
        {
            _model = new FieldInitViewModel(tables)
            {
                Window = this
            };
            this.DataContext = _model;
            InitializeComponent();
        }

        private void FieldInitWindow_Loaded(object sender, RoutedEventArgs e)
        {
            InitDataGrid();

            btnInit.Command.Execute(null);
        }

        private void InitDataGrid()
        {
            dgFields.ShowRowNumber();
        }
    }
}
