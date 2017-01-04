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
using System.Windows.Shapes;
using MyNet.Components.WPF.Extension;
using MyNet.Client.Public;

namespace MyNet.Client.Pages.Auth
{
    /// <summary>
    /// GoupMngPage.xaml 的交互逻辑
    /// </summary>
    public partial class GroupMngPage : BasePage
    {
        GroupMngViewModel model;
        public GroupMngPage()
        {
            InitializeComponent();

            model = new GroupMngViewModel();
            this.DataContext = model;

            base.Commands = model.Commands;
        }

        private void GroupMngPage_Loaded(object sender, RoutedEventArgs e)
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
            dgGroups.ShowRowNumber();
            model.CtlPage = ctlPagination;
        }
    }
}
