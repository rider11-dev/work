using MyNet.Client.Models.CustomQuery;
using MyNet.Client.Pages;
using MyNet.Client.Public;
using MyNet.Components.WPF.Extension;
using System.Linq;
using System.Windows;

namespace MyNet.Client.Pages.CustomQuery.Basic
{
    /// <summary>
    /// TablesMngPage.xaml 的交互逻辑
    /// </summary>
    public partial class TablesMngPage : BasePage
    {
        TableMngViewModel model;
        public TablesMngPage()
        {
            InitializeComponent();
            model = this.DataContext as TableMngViewModel;
            base.Commands = model.Commands;
        }

        private void TablesMngPage_Loaded(object sender, RoutedEventArgs e)
        {
            InitDataGrid();

            base.LoadButtons(panelBtns, StyleCacheUtils.MngBtnStyle);

            if (panelBtns.Children == null || panelBtns.Children.Count < 1)
            {
                //“隐藏”工具栏——列表布局第一行，高度设为0
                gridLayout.RowDefinitions[0].Height = new GridLength(0);
            }
        }

        private void InitDataGrid()
        {
            dgTables.ShowRowNumber();
            model.CtlPage = ctlPagination;
        }
    }
}
