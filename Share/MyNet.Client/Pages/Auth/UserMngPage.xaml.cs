using MyNet.Client.Models.Auth;
using System.Windows;
using MyNet.Client.Public;
using MyNet.Components.WPF.Extension;

namespace MyNet.Client.Pages.Auth
{
    /// <summary>
    /// UserMngPage.xaml 的交互逻辑
    /// </summary>
    public partial class UserMngPage : BasePage
    {
        UserMngViewModel model;
        public UserMngPage()
        {
            InitializeComponent();

            model = new UserMngViewModel();
            this.DataContext = model;
            base.Commands = model.Commands;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
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
            //TODO是否有必要继承DataGrid，默认包含下面设置
            dgUsers.ShowRowNumber();

            model.CtlPage = ctlPagination;
        }
    }
}
