using Biz.PartyBuilding.YS.Client.PartyOrg.Models;
using Biz.PartyBuilding.YS.Client.Sys;
using Biz.PartyBuilding.YS.Client.Sys.Models;
using MyNet.Client.Help;
using MyNet.Client.Pages;
using MyNet.Client.Public;
using MyNet.Components.Extensions;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Controls;
using MyNet.Components.WPF.Extension;
using MyNet.Components.WPF.Models;
using MyNet.Components.WPF.Windows;
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

namespace Biz.PartyBuilding.YS.Client.Evaluation
{
    /// <summary>
    /// EvaluatePage.xaml 的交互逻辑
    /// </summary>
    public partial class EvaluatePage : BasePage
    {
        TreeViewData _gpTreeData = null;
        EvaluateProject _model;

        public EvaluatePage()
        {
            InitializeComponent();

            _gpTreeData = (TreeViewData)gpTree.DataContext;
            _model = this.DataContext as EvaluateProject;
        }

        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            dgAttach.ItemsSource = EvaluationContext.upload_attaches;

        }

        private void RefreshProjects(string party)
        {
            IList<TreeViewData.NodeViewModel> nodes = new List<TreeViewData.NodeViewModel>();
            if (SysContext.party_projects.ContainsKey(party))
            {
                var projs = SysContext.party_projects[party];

                nodes = SysContext.ParseProjectsTreeData(SysContext.projects.Where(e => projs.Contains(e.id)));
            }

            _gpTreeData.Bind(nodes);
            gpTree.ExpandAll();
            gpTree.Select(0);

        }

        bool passornot = false;
        private void menuTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var node = (TreeViewData.TreeNode)e.NewValue;
            if (node != null && SysContext.projects.Exists(m => m.id == node.Id))
            {
                var vm = SysContext.projects.Where(m => m.id == node.Id).First();
                if (vm != null)
                {
                    vm.CopyTo(_model);
                    passornot = vm.check_type == "passornot";
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
            new EvaluateWindow(passornot).ShowDialog();
        }

        private ICommand _groupParentHelpCmd;
        public ICommand GroupParentHelpCmd
        {
            get
            {
                if (_groupParentHelpCmd == null)
                {
                    _groupParentHelpCmd = new DelegateCommand(OpenGroupParentHelp);
                }
                return _groupParentHelpCmd;
            }
        }

        private void OpenGroupParentHelp(object parameter)
        {
            TreeHelper.OpenAllGroupsHelp(false, node =>
            {
                var tNode = (TreeViewData.TreeNode)node;
                ctlHelpBtn.Content = tNode.Label;
                RefreshProjects(tNode.Label);
            });
        }

        private ICommand _viewAttachCmd;
        public ICommand ViewAttachCmd
        {
            get
            {
                if (_viewAttachCmd == null)
                {
                    _viewAttachCmd = new DelegateCommand(ViewAttachAction);
                }
                return _viewAttachCmd;
            }
        }

        private void ViewAttachAction(object parameter)
        {
            var attach = (Attach)parameter;
            if (attach == null || string.IsNullOrEmpty(attach.att_name))
            {
                return;
            }
            string fullPath = "";
            if (FileExtension.GetFileFullPath(AppDomain.CurrentDomain.BaseDirectory, attach.att_name, out fullPath))
            {
                Process.Start(fullPath);
            }
        }
    }
}
