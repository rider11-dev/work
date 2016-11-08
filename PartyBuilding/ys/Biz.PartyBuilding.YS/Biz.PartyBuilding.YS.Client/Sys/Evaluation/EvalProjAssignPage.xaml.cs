using Biz.PartyBuilding.YS.Client.PartyOrg.Models;
using Biz.PartyBuilding.YS.Client.Sys.Models;
using MyNet.Client.Help;
using MyNet.Client.Pages;
using MyNet.Client.Public;
using MyNet.Components.WPF.Controls;
using MyNet.Components.WPF.Models;
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

namespace Biz.PartyBuilding.YS.Client.Sys.Evaluation
{
    /// <summary>
    /// EvalProjAssignPage.xaml 的交互逻辑
    /// </summary>
    public partial class EvalProjAssignPage : BasePage
    {
        TreeViewData _gpTreeData = null;
        TreeViewData _projTreeData = null;

        public EvalProjAssignPage()
        {
            InitializeComponent();

            _gpTreeData = (TreeViewData)gpTree.DataContext;
            _projTreeData = (TreeViewData)projTree.DataContext;
        }

        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            //树
            var nodes = TreeHelper.ParseGroupsTreeData(DataCacheHelper.AllGroups.Where(kvp => kvp.Value.gp_system == false).Select(kvp => kvp.Value));
            _gpTreeData.Bind(nodes);

            nodes = SysContext.ParseProjectsTreeData(SysContext.projects);
            _projTreeData.Bind(nodes);
        }

        private void menuTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var node = (TreeViewData.TreeNode)e.NewValue;
            if (node != null)
            {
                curParty.Text = node.Label;
                SelectProjects(node.Label);

            }

        }

        void SelectProjects(string party)
        {
            _projTreeData.UncheckAll();
            if (!SysContext.party_projects.ContainsKey(party))
            {
                return;
            }
            _projTreeData.Check(SysContext.party_projects[party]);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            TreeViewData.TreeNode selNode = (gpTree.SelectedItem as TreeViewData.TreeNode);

            TreeViewData.TreeNode node = new TreeViewData.TreeNode { Label = "新增党组织" };

            if (selNode == null || _gpTreeData.RootNodes.Contains(selNode))
            {
                node.Level = 1;
                _gpTreeData.RootNodes.Add(node);
            }
            else
            {
                node.ParentNode = selNode.ParentNode;
                node.Parent = selNode.Parent;
                node.Level = selNode.Level;

                selNode.ParentNode.SubNodes.Add(node);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageWindow.ShowMsg(MessageType.Info, "权限分配", "操作成功！");
        }

    }
}
