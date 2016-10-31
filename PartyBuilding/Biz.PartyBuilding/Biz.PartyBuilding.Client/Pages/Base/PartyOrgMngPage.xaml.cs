using Biz.PartyBuilding.Client.Models;
using Biz.PartyBuilding.Model.Base;
using MyNet.Client.Help;
using MyNet.Client.Pages;
using MyNet.Client.Public;
using MyNet.Components;
using MyNet.Components.Result;
using MyNet.Components.WPF.Controls;
using MyNet.Components.WPF.Windows;
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
using Biz.PartyBuilding.Client.Models.Base;
using Newtonsoft.Json;
using MyNet.Components.Serialize;
using MyNet.Components.WPF.Models;

namespace Biz.PartyBuilding.Client.Pages.Base
{
    /// <summary>
    /// PartyOrgMngPage.xaml 的交互逻辑
    /// </summary>
    public partial class PartyOrgMngPage : BasePage
    {
        PartyOrgDetailViewModel model;
        private TreeViewData _treeGroupData;
        public PartyOrgMngPage()
        {
            InitializeComponent();

            model = this.DataContext as PartyOrgDetailViewModel;
            model.CmbModelOrgType = cbOrgType.DataContext as CmbModel;
            model.CmbModelChgRemind = cbChgRemind.DataContext as CmbModel;
            _treeGroupData = ctlTreeGroup.Tree.DataContext as TreeViewData;


            ctlTreeGroup.Tree.SelectedItemChanged += (o, e) =>
            {
                model.GetCmd.Execute(((TreeViewData.TreeNode)e.NewValue).DataId);
            };
        }
        private void PartyOrgMngPage_Loaded(object sender, RoutedEventArgs e)
        {
            //组织树
            var nodes = TreeHelper.ParseGroupsTreeData(DataCacheHelper.AllGroups.Where(kvp => kvp.Value.gp_system == false).Select(kvp => kvp.Value));
            _treeGroupData.Bind(nodes);

            //设置combobox数据源
            DataCacheHelper.SetCmbSource(cbOrgType, PartyDictType.PartyOrg, "", true, false);
            //换届是否提醒下拉框
            DataCacheHelper.SetCmbSource(cbChgRemind, PartyDictType.Bool);

            //选中第一个组织
            ctlTreeGroup.Tree.Select();

        }
    }
}
