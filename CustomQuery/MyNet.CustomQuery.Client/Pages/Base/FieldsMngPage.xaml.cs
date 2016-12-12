using MyNet.Client.Pages;
using MyNet.Client.Public;
using MyNet.Components.WPF.Extension;
using MyNet.Components.WPF.Models;
using MyNet.CustomQuery.Client.Models;
using MyNet.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MyNet.CustomQuery.Client.Pages.Base
{
    /// <summary>
    /// FieldsMngPage.xaml 的交互逻辑
    /// </summary>
    public partial class FieldsMngPage : BasePage
    {
        FieldMngViewModel model;
        public FieldsMngPage()
        {
            InitializeComponent();

            model = this.DataContext as FieldMngViewModel;
            base.Commands = model.Commands;
            model.DgTables = dgTables;
        }

        private void FieldsMngPage_Loaded(object sender, RoutedEventArgs e)
        {
            InitDataGrid();
            LoadToolBar();
        }

        private void InitDataGrid()
        {
            dgTables.ShowRowNumber();
            dgFields.ShowRowNumber();
            model.CtlPage = ctlPagination;

            //加载表信息列表
            var tables = TableMngViewModel.GetTables(new PageQuery { pageIndex = 1, pageSize = 1000 });//pageSize = 1000，加载所有表信息
            model.TbMngModel.Models = ((IEnumerable<CheckableModel>)tables).ToList();
            foreach (var tb in model.TbMngModel.Models)
            {
                tb.IsSingleSelect = true;
            }

            dgTables.SelectionChanged += dgTables_SelectionChanged;
        }

        private void LoadToolBar()
        {
            base.LoadButtons(panelBtns, StyleCacheHelper.MngBtnStyle);
            if (panelBtns.Children == null || panelBtns.Children.Count < 1)
            {
                //“隐藏”工具栏——列表布局第一行，高度设为0
                gridLayout.RowDefinitions[0].Height = new GridLength(0);
            }
        }

        private void dgTables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnSearch.Command.Execute(this.FindResource("page"));
        }
    }
}
