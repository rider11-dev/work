using System;
using System.Collections.Generic;
using MyNet.Components.WPF.Extension;
using MyNet.Components.WPF.Controls;

namespace MyNet.Components.WPF.Windows
{
    /// <summary>
    /// HelpWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TreeHelpWindow : BaseWindow
    {
        private TreeViewData _treeData;
        private TreeHelpViewModel _model;

        private TreeHelpWindow()
        {
            InitializeComponent();
            //允许拖拽
            this.DragWhenLeftMouseDown();

            _treeData = ctlTree.Tree.DataContext as TreeViewData;

            _model = new TreeHelpViewModel();
            this.DataContext = _model;
            _model.TreeCtl = ctlTree;
            _model.Window = this;
        }

        public TreeHelpWindow(string title,
            Func<IList<TreeViewData.NodeViewModel>> dataProvider,
            bool multiSel = false,
            Action<TreeViewData.TreeNode> singleSelAction = null,
            Action<IList<TreeViewData.TreeNode>> multiSelAction = null)
            : this()
        {
            base.Title = title;
            base.CustomTitle = title;
            if (dataProvider != null)
            {
                var src = dataProvider();
                _treeData.Bind(src);
            }
            _model.SingleSelectCallback = singleSelAction;
            _model.MultiSelectCallback = multiSelAction;
            //MultiSelect赋值需要在构造函数中调用，因为ctlTree的Loaded事件用到了
            ctlTree.MultiSelect = multiSel;
        }

        public static void ShowHelp(string title,
            Func<IList<TreeViewData.NodeViewModel>> dataProvider,
            bool multiSel = false,
            Action<TreeViewData.TreeNode> singleSelAction = null,
            Action<IList<TreeViewData.TreeNode>> multiSelAction = null)
        {
            var win = new TreeHelpWindow(title, dataProvider, multiSel, singleSelAction, multiSelAction);
            win.ShowDialog();
        }
    }
}
