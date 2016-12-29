using MyNet.Components.Misc;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Controls;
using MyNet.Components.WPF.Models;
using MyNet.Components.WPF.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyNet.Components.WPF.Windows
{
    public class TreeHelpViewModel : BaseModel
    {
        public BaseWindow Window { get; set; }
        public ControlTree TreeCtl { get; set; }
        public Action<TreeViewData.TreeNode> SingleSelectCallback { get; set; }
        public Action<IList<TreeViewData.TreeNode>> MultiSelectCallback { get; set; }

        private ICommand _selectCmd;
        public ICommand SelectCmd
        {
            get
            {
                if (_selectCmd == null)
                {
                    _selectCmd = new DelegateCommand(SelectAction);
                }
                return _selectCmd;
            }
        }

        private void SelectAction(object parameter)
        {
            bool rst = TreeCtl.MultiSelect ? SelectMulti() : SelectOne();
            if (rst != true)
            {
                return;
            }
            this.Window.DialogResult = true;
            this.Window.Close();
        }

        private bool SelectOne()
        {
            var selNode = TreeCtl.tree.SelectedValue;
            if (selNode == null)
            {
                MessageWindow.ShowMsg(MessageType.Warning, "帮助选择", "请选择节点数据");
                return false;
            }

            SingleSelectCallback?.Invoke(selNode as TreeViewData.TreeNode);
            return true;
        }

        private bool SelectMulti()
        {
            TreeViewData treeData = TreeCtl.Tree.DataContext as TreeViewData;
            if (treeData == null || treeData.RootNodes == null || treeData.RootNodes.Count < 1)
            {
                return false;
            }
            //归集所有选择的节点
            List<TreeViewData.TreeNode> selNodes = new List<TreeViewData.TreeNode>();
            foreach (var node in treeData.RootNodes.Where(n => n.IsChecked == true))
            {
                selNodes.Add(node);
                CollectSelNodes(node, selNodes);
            }

            if (selNodes.Count < 1)
            {
                MessageWindow.ShowMsg(MessageType.Warning, "帮助选择", "请选择节点数据");
                return false;
            }

            MultiSelectCallback?.Invoke(selNodes);

            return true;
        }

        private void CollectSelNodes(TreeViewData.TreeNode parent, List<TreeViewData.TreeNode> target)
        {
            if (parent.SubNodes == null || parent.SubNodes.Count < 1)
            {
                return;
            }
            var selNodes = parent.SubNodes.Where(n => n.IsChecked == true);
            if (selNodes == null || selNodes.Count() < 1)
            {
                return;
            }
            foreach (var node in selNodes)
            {
                target.Add(node);
                CollectSelNodes(node, target);
            }
        }
    }
}
