using Biz.PartyBuilding.YS.Client.PartyOrg.Models;
using Biz.PartyBuilding.YS.Client.Sys;
using Biz.PartyBuilding.YS.Client.Sys.Models;
using MyNet.Client.Help;
using MyNet.Client.Pages;
using MyNet.Client.Public;
using MyNet.Components.WPF.Controls;
using MyNet.Components.WPF.Extension;
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

namespace Biz.PartyBuilding.YS.Client.Evaluation
{
    /// <summary>
    /// FileuploadPage.xaml 的交互逻辑
    /// </summary>
    public partial class FileuploadPage : BasePage
    {
        TreeViewData _gpTreeData = null;
        EvaluateProject _model;

        public FileuploadPage()
        {
            InitializeComponent();

            _gpTreeData = (TreeViewData)gpTree.DataContext;
            _model = this.DataContext as EvaluateProject;
        }

        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            //考核项目树
            var nodes = SysContext.ParseProjectsTreeData(SysContext.projects);
            _gpTreeData.Bind(nodes);
            gpTree.ExpandAll();
            gpTree.Select(0);

            dgAttach.ItemsSource = EvaluationContext.upload_attaches;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageWindow.ShowMsg(MessageType.Info, "资料上传", "操作成功");
        }

    }
}
