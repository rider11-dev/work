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
    /// MessageWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MessageWindow : Window
    {
        const string msgWinTemplateName = "FlatMsgWinTemplate";
        public MessageType MsgType { get; set; }
        public string MsgTitle { get; private set; }
        public string Msg { get; private set; }

        private MessageWindow()
        {
            InitializeComponent();
            //允许拖拽
            this.DragWhenLeftMouseDown();
        }

        private MessageWindow(MessageType mstType, string title = "", string msg = "")
            : this()
        {
            MsgType = mstType;
            MsgTitle = title;
            Msg = msg;

            this.Loaded += delegate
            {
                Bind();
            };
        }

        void Bind()
        {
            ControlTemplate msgWindowTemplate = this.Template;
            //
            Image img = (Image)msgWindowTemplate.FindName("imgTitle", this);
            BitmapImage bitmap = new BitmapImage(new Uri(GetIconFile(), UriKind.RelativeOrAbsolute));
            img.Source = bitmap;
            //
            TextBlock txt = (TextBlock)msgWindowTemplate.FindName("txtTitle", this);
            txt.Text = MsgTitle;
            //
            txt = (TextBlock)msgWindowTemplate.FindName("txtMsg", this);
            txt.Text = Msg;
            //
            Button btn = (Button)msgWindowTemplate.FindName("btnOK", this);
            btn.Click += (o, e) =>
            {
                this.DialogResult = true;
            };
            btn = (Button)msgWindowTemplate.FindName("btnCancel", this);
            btn.Click += (o, e) =>
            {
                this.DialogResult = false;
            };
        }

        string GetIconFile()
        {
            switch (MsgType)
            {
                case MessageType.Ask:
                    return "/MyNet.Components.WPF;component/Resources/img/ask.png";
                case MessageType.Error:
                    return "/MyNet.Components.WPF;component/Resources/img/error.png";
                case MessageType.Info:
                    return "/MyNet.Components.WPF;component/Resources/img/info.png";
                case MessageType.Warning:
                    return "/MyNet.Components.WPF;component/Resources/img/warn.png";
            }
            return string.Empty;
        }

        public static bool? ShowMsg(MessageType msgType, string title = "", string msg = "")
        {
            MessageWindow msgWin = new MessageWindow(msgType, title, msg);
            return msgWin.ShowDialog();
        }

    }

    public enum MessageType
    {
        Error = 0,
        Warning = 1,
        Info = 2,
        Ask = 3
    }
}
