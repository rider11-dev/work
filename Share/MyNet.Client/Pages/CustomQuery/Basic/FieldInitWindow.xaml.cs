using MyNet.Client.Models.CustomQuery;
using MyNet.Components.WPF.Extension;
using MyNet.Components.WPF.Windows;
using System.Collections.Generic;
using System.Windows;

namespace MyNet.Client.Pages.CustomQuery.Basic
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
