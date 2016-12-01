using Newtonsoft.Json;
using MyNet.Client.Models.Auth;
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
using MyNet.Components;
using MyNet.Client.Public;
using MyNet.Client.Models;
using MyNet.Components.Result;
using Newtonsoft.Json.Linq;
using MyNet.Components.WPF.Controls;
using MyNet.Components.WPF.Extension;
using MyNet.Model.Auth;
using MyNet.Components.Extensions;
using MyNet.Components.WPF.Models;

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

            model = (UserMngViewModel)dgUsers.DataContext;
            base.Commands = model.Commands;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitDataGrid();

            base.LoadButtons(panelBtns, StyleCacheHelper.MngBtnStyle);

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
