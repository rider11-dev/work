using MyNet.Client.Models.Auth;
using System.Windows;
using MyNet.Components.WPF.Extension;
using MyNet.Client.Public;
using MyNet.Model.Base;

namespace MyNet.Client.Pages.Auth
{
    /// <summary>
    /// PermissionMngPage.xaml 的交互逻辑
    /// </summary>
    public partial class PermissionMngPage : BasePage
    {
        PermMngViewModel model;
        public PermissionMngPage()
        {
            model = new PermMngViewModel();
            this.DataContext = model;
            this.AddModel(model);
            base.Commands = model.Commands;

            InitializeComponent();
        }

        private void PermissionMngPage_Loaded(object sender, RoutedEventArgs e)
        {
            InitDataGrid();

            base.LoadButtons(panelBtns, StyleCacheUtils.MngBtnStyle);

            if (panelBtns.Children == null || panelBtns.Children.Count < 1)
            {
                //“隐藏”工具栏——列表布局第一行，高度设为0
                gridLayout.RowDefinitions[0].Height = new GridLength(0);
            }
            //设置combobox数据源
            DataCacheUtils.SetCmbSource(cbPermType, DictType.Perm, "", false, true);
        }

        private void InitDataGrid()
        {
            dgPers.ShowRowNumber();
            model.CtlPage = ctlPagination;
        }
    }
}
