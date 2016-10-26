using MyNet.ClientFrame.Models.Auth;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MyNet.Components.WPF.Extension;
using MyNet.ClientFrame.Public;
using MyNet.Model.Base;
using MyNet.Components.WPF.Windows;
using MyNet.Components.WPF.Controls;

namespace MyNet.ClientFrame.Pages.Auth
{
    /// <summary>
    /// PermissionMngPage.xaml 的交互逻辑
    /// </summary>
    public partial class PermissionMngPage : BasePage
    {
        PermMngViewModel model;
        public PermissionMngPage()
        {
            InitializeComponent();

            model = (PermMngViewModel)dgPers.DataContext;
            base.Commands = model.Commands;
        }

        private void PermissionMngPage_Loaded(object sender, RoutedEventArgs e)
        {
            InitDataGrid();

            base.LoadButtons(panelBtns, Application.Current.FindResource("mngBtnStyle") as Style);

            if (panelBtns.Children == null || panelBtns.Children.Count < 1)
            {
                //“隐藏”工具栏——列表布局第一行，高度设为0
                gridLayout.RowDefinitions[0].Height = new GridLength(0);
            }
            //设置combobox数据源
            CacheHelper.SetCmbSource(cbPermType, DictType.Perm, "", false, true);
        }

        private void InitDataGrid()
        {
            dgPers.ShowRowNumber();
            model.CtlPage = ctlPagination;
        }
    }
}
