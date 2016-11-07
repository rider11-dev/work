using Microsoft.Win32;
using MyNet.Client.Help;
using MyNet.Client.Pages;
using MyNet.Client.Public;
using MyNet.Components.Npoi;
using MyNet.Components.WPF.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Biz.PartyBuilding.YS.Client.PartyOrg
{
    /// <summary>
    /// ActivityPlacePage.xaml 的交互逻辑
    /// </summary>
    public partial class ActivityPlacePage : BasePage
    {
        public ActivityPlacePage()
        {
            InitializeComponent();

            _gpTreeData = (TreeViewData)gpTree.DataContext;
        }

        private void gpTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //dg.ItemsSource=PartyBuildingContext.DyPhones.Where()

            dg.ItemsSource = null;
            var node = (TreeViewData.TreeNode)e.NewValue;
            if (node == null)
            {
                return;
            }
            var dfs = PartyBuildingContext.act_place.Where(p => p.party == node.Label);
            dg.ItemsSource = dfs;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new DetailActivityPlaceWindow().ShowDialog();
        }

        TreeViewData _gpTreeData = null;
        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            //组织树
            var nodes = TreeHelper.ParseGroupsTreeData(DataCacheHelper.AllGroups.Where(kvp => kvp.Value.gp_system == false).Select(kvp => kvp.Value));

            _gpTreeData.Bind(nodes);
        }
    }
}
