using Microsoft.Win32;
using MyNet.Client.Help;
using MyNet.Client.Pages;
using MyNet.Client.Public;
using MyNet.Components.Extensions;

using MyNet.Components.WPF.Controls;
using MyNet.Components.WPF.Models;
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
    /// PartyMemDuesPage.xaml 的交互逻辑
    /// </summary>
    public partial class PartyMemDuesPage : BasePage
    {
        public PartyMemDuesPage()
        {
            InitializeComponent();

            _gpTreeData = (TreeViewData)gpTree.DataContext;

            CmbModel model = cmbDfScale.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.CmbItemsDfZxbz);


            btnAll.Click += (o, e) =>
            {
                Search(true);
            };

            btnSearch.Click += (o, e) =>
            {
                Search();
            };
        }

        private void gpTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Search(true);
        }

        private void Search(bool all = false)
        {
            var items = PartyBuildingContext.Df;
            if (items.IsEmpty())
            {
                return;
            }
            var node = (TreeViewData.TreeNode)gpTree.SelectedValue;
            if (node == null)
            {
                return;
            }
            dg.ItemsSource = null;

            items = items.Where(m => m.dy_party == node.Label);
            if (all)
            {
                dg.ItemsSource = items;
                return;
            }

            if (!txtName.Text.IsEmpty())
            {
                items = items.Where(m => ((string)m.dy_name).Contains(txtName.Text));
            }
            if (cmbDfScale.SelectedItem != null)
            {
                items = items.Where(m => m.df_zxbz == ((CmbItem)cmbDfScale.SelectedValue).Text);
            }

            dg.ItemsSource = items;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new DetailPartyMemPhoneWindow().ShowDialog();
        }

        TreeViewData _gpTreeData = null;
        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            //组织树
            var nodes = TreeHelper.ParseGroupsTreeData(DataCacheUtils.AllGroups.Where(kvp => kvp.Value.gp_system == false).Select(kvp => kvp.Value));

            _gpTreeData.Bind(nodes);
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            var node = gpTree.SelectedValue as TreeViewData.TreeNode;
            MyNet.Components.WPF.Misc.ExcelHelper.Export(dg, "党费记录——" + (node == null ? "全部" : node.Label));
        }
    }
}
