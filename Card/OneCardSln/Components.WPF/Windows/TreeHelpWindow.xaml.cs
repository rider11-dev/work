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
using System.Windows.Shapes;
using MyNet.Components.WPF.Extension;
using MyNet.Components.WPF.Controls;
using MyNet.Components.WPF.Models;
using MyNet.Components.WPF.Command;

namespace MyNet.Components.WPF.Windows
{
    /// <summary>
    /// HelpWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TreeHelpWindow : BaseWindow
    {
        private TreeViewData _treeData;
        private HelpWindowViewModel _model;
        private TreeHelpWindow()
        {
            InitializeComponent();
            //允许拖拽
            this.DragWhenLeftMouseDown();

            _treeData = helpTree.FindResource("treeData") as TreeViewData;
            _model = this.FindResource("model") as HelpWindowViewModel;
            _model.TreeHelpControl = helpTree;
            _model.Window = this;
        }

        public TreeHelpWindow(string title, Func<IList<TreeViewData.NodeViewModel>> dataProvider, Action<dynamic> nodeSelAction = null)
            : this()
        {
            base.Title = title;
            if (dataProvider != null)
            {
                var src = dataProvider();
                _treeData.Bind(src);
                _model.NodeSelCallback = nodeSelAction;
            }
        }
    }
}
