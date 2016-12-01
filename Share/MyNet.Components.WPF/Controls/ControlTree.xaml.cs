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

namespace MyNet.Components.WPF.Controls
{
    /// <summary>
    /// ControlTree.xaml 的交互逻辑
    /// </summary>
    public partial class ControlTree : UserControl
    {
        public bool MultiSelect
        {
            get { return (bool)GetValue(MultiSelectProperty); }
            set { SetValue(MultiSelectProperty, value); }
        }

        public static readonly DependencyProperty MultiSelectProperty = DependencyProperty.Register("MultiSelect", typeof(bool), typeof(ControlTree), new PropertyMetadata(false));

        public TreeView Tree
        {
            get { return tree; }
        }

        public ControlTree()
        {
            InitializeComponent();
            this.Loaded += (o, e) =>
            {
                Init();
            };
        }

        private void Init()
        {
            string style = MultiSelect ? "ckTreeViewStyle" : "menuTreeViewStyle";
            tree.Style = Application.Current.FindResource(style) as Style;
        }
    }
}
