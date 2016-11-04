using Biz.PartyBuilding.YS.Client.PartyOrg.Models;
using MyNet.Client.Help;
using MyNet.Client.Pages;
using MyNet.Client.Public;
using MyNet.Components.WPF.Controls;
using MyNet.Components.WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// PartyMemberPage.xaml 的交互逻辑
    /// </summary>
    public partial class PartyMemberPage : BasePage
    {
        TreeViewData _gpTreeData = null;
        public PartyMemberPage()
        {
            InitializeComponent();
            _gpTreeData = (TreeViewData)gpTree.DataContext;

            CmbModel model = cmbSex.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.CmbItemsSex);

            model = cmbNation.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.CmbItemsNation);

            model = cmbXL.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.CmbItemsXL);

            model = cmbNowGzgw.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.CmbItemsNowGzgw);
        }

        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            //组织树
            var nodes = TreeHelper.ParseGroupsTreeData(DataCacheHelper.AllGroups.Where(kvp => kvp.Value.gp_system == false).Select(kvp => kvp.Value));

            _gpTreeData.Bind(nodes);
        }

        private void menuTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            dg.ItemsSource = null;
            var node = (TreeViewData.TreeNode)e.NewValue;
            if (node == null)
            {
                return;
            }
            var mems = PartyBuildingContext.partymembers.Where(p => p.party == node.Label);
            dg.ItemsSource = mems;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new DetailPartyMemWindow().ShowDialog();
        }

        private void btnZZ_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var srcOld = dg.ItemsSource as IEnumerable<PartyMemberViewModel>;
                dg.ItemsSource = null;
                srcOld.Where(m => m.type == "预备党员").First().type = "正式党员";
                dg.ItemsSource = srcOld;
            }
            catch { }
        }

        private void btnZybdw_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var srcOld = dg.ItemsSource as IEnumerable<PartyMemberViewModel>;
                dg.ItemsSource = null;
                srcOld.Where(m => m.type == "入党积极分子").First().type = "预备党员";
                dg.ItemsSource = srcOld;
            }
            catch { }
        }
    }
}
