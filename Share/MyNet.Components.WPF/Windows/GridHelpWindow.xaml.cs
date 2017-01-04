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
using MyNet.Components.WPF.Extension;
using MyNet.Components.WPF.Controls;
using MyNet.Components.WPF.Models;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Windows;

namespace MyNet.Components.WPF.Windows
{
    /// <summary>
    /// HelpWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GridHelpWindow : BaseWindow
    {
        private GridHelpViewModel _model;

        private GridHelpWindow()
        {
            _model = new GridHelpViewModel { Window = this };
            this.DataContext = _model;
            InitializeComponent();
            //允许拖拽
            this.DragWhenLeftMouseDown();
            _model.DataGrid = dg;
        }

        private GridHelpWindow(string title,
            Func<IEnumerable<CheckableModel>> dataProvider,
            bool multiSel = false,
            Action<CheckableModel> singleSelAction = null,
            Action<IEnumerable<CheckableModel>> multiSelAction = null,
            IList<DataGridColModel> cols = null)
            : this()
        {
            base.Title = title;
            base.CustomTitle = title;

            _model.DataProvider = dataProvider;
            _model.SingleSelectCallback = singleSelAction;
            _model.MultiSelectCallback = multiSelAction;
            _model.MultiSelect = multiSel;

            InitDataGrid(cols);
        }

        public static void ShowHelp(string title,
            Func<IEnumerable<CheckableModel>> dataProvider,
            bool multiSel = false,
            Action<CheckableModel> singleSelAction = null,
            Action<IEnumerable<CheckableModel>> multiSelAction = null,
            IList<DataGridColModel> cols = null)
        {
            var win = new GridHelpWindow(title, dataProvider, multiSel, singleSelAction, multiSelAction, cols);
            win.ShowDialog();
        }

        private void InitDataGrid(IEnumerable<DataGridColModel> cols = null)
        {
            if (_model.MultiSelect)
            {
                dg.Columns.Add(new DataGridTemplateColumn
                {
                    CellStyle = this.FindResource("ckCellStyle") as Style,
                    HeaderStyle = this.FindResource("ckCellHeaderStyle") as Style,
                    CanUserResize = false,
                });
            }
            dg.ShowRowNumber();
            //添加列
            dg.AddColumns(cols);
        }

        private void GridHelpWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _model.RefreshCmd.Execute(null);
        }
    }
}
