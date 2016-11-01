using Biz.PartyBuilding.YS.Client.PartyOrg.Models;
using MyNet.Client.Help;
using MyNet.Client.Pages;
using MyNet.Client.Public;
using MyNet.Components.WPF.Controls;
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

namespace Biz.PartyBuilding.YS.Client.PartyOrg
{
    /// <summary>
    /// OrgStructPage.xaml 的交互逻辑
    /// </summary>
    public partial class OrgStructPage : BasePage
    {
        TreeViewData _menuTreeData = null;
        OrgStrucViewModel _model;

        public OrgStructPage()
        {
            InitializeComponent();

            _menuTreeData = (TreeViewData)menuTree.DataContext;
            _model = this.DataContext as OrgStrucViewModel;
        }

        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            //组织树
            var nodes = TreeHelper.ParseGroupsTreeData(DataCacheHelper.AllGroups.Where(kvp => kvp.Value.gp_system == false).Select(kvp => kvp.Value));

            _menuTreeData.Bind(nodes);
        }

        private void menuTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var node = (TreeViewData.TreeNode)e.NewValue;
            if (node != null && PartyBuildingContext.orgs.Exists(m => m.org_code == node.Id))
            {
                var vm = PartyBuildingContext.orgs.Where(m => m.org_code == node.Id).First();
                if (vm != null)
                {
                    vm.CopyTo(_model);
                }
            }

        }
    }
}
