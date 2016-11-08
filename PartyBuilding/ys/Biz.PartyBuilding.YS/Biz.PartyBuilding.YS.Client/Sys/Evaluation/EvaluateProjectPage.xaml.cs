using Biz.PartyBuilding.YS.Client.PartyOrg.Models;
using Biz.PartyBuilding.YS.Client.Sys.Models;
using MyNet.Client.Help;
using MyNet.Client.Pages;
using MyNet.Client.Public;
using MyNet.Components.WPF.Controls;
using MyNet.Components.WPF.Models;
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
    /// EvaluateProjectPage.xaml 的交互逻辑
    /// </summary>
    public partial class EvaluateProjectPage : BasePage
    {
        TreeViewData _gpTreeData = null;
        EvaluateProject _model;

        public EvaluateProjectPage()
        {
            InitializeComponent();

            _gpTreeData = (TreeViewData)gpTree.DataContext;
            _model = this.DataContext as EvaluateProject;

            CmbModel m = cmbPartyType.DataContext as CmbModel;
            m.Bind(SysContext.CmbItemsPartyType);
            m = cmbTimeType.DataContext as CmbModel;
            m.Bind(SysContext.CmbItemsTimeType);
        }

        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            //考核项目树
            var nodes = SysContext.ParseProjectsTreeData(SysContext.projects);

            _gpTreeData.Bind(nodes);
        }

        private void menuTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var node = (TreeViewData.TreeNode)e.NewValue;
            if (node != null && SysContext.projects.Exists(m => m.id == node.Id))
            {
                var vm = SysContext.projects.Where(m => m.id == node.Id).First();
                if (vm != null)
                {
                    vm.CopyTo(_model);
                }
            }

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

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (tab2 != null)
            {
                tab2.Visibility = Visibility.Collapsed;
            }
            if (tab1 != null)
            {
                tab1.Visibility = Visibility.Visible;
                tab1.IsSelected = true;
            }
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            if (tab1 != null)
            {
                tab1.Visibility = Visibility.Collapsed;
            }
            if (tab2 != null)
            {
                tab2.Visibility = Visibility.Visible;
                tab2.IsSelected = true;
            }
        }
    }
}
