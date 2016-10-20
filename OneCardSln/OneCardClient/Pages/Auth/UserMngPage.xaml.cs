using Newtonsoft.Json;
using OneCardSln.OneCardClient.Models.Auth;
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
using OneCardSln.Components;
using OneCardSln.OneCardClient.Public;
using OneCardSln.OneCardClient.Models;
using OneCardSln.Components.Result;
using Newtonsoft.Json.Linq;
using OneCardSln.Components.WPF.Controls;
using OneCardSln.Components.WPF.Extension;
using OneCardSln.Model.Auth;
using OneCardSln.Components.Extensions;
using OneCardSln.Components.WPF.Models;

namespace OneCardSln.OneCardClient.Pages.Auth
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

            model = (UserMngViewModel)dgUsers.DataContext;
            base.Commands = model.Commands;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitDataGrid();

            base.LoadButtons(panelBtns, Application.Current.FindResource("mngBtnStyle") as Style);

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
