using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Client
{
    /// <summary>
    /// TreeViewTest.xaml 的交互逻辑
    /// </summary>
    public partial class TreeViewTest : Window
    {

        public TreeViewTest()
        {
            InitializeComponent();
            var model = new TestModel();
            tree.DataContext = model;
        }
    }

    public class TestModel
    {
        public ObservableCollection<TreeNode> TreeNodes { get; set; }
        public TestModel()
        {
            TreeNodes = new ObservableCollection<TreeNode>
                {
                    new TreeNode {
                        Label ="权限管理",
                        ChildNodes =new ObservableCollection<TreeNode>
                        {
                            new TreeNode
                            {
                                Label="用户管理"
                            },
                            new TreeNode
                            {
                                Label="权限管理"
                            },
                            new TreeNode
                            {
                                Label="权限分配"
                            }
                        }
                    },
                };
        }
    }
}
