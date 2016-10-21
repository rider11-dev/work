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

namespace MyNet.Components.WPF.Windows
{
    /// <summary>
    /// HelpWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HelpWindow : Window
    {
        const string msgWindowTemplateName = "msgWindowTemplate";
        public MessageType MsgType { get; set; }
        public string MsgTitle { get; private set; }
        public string Msg { get; private set; }

        public HelpWindow()
        {
            InitializeComponent();
            //允许拖拽
            this.DragWhenLeftMouseDown();
        }
    }
}
