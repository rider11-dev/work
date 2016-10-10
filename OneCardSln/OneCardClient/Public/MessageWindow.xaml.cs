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

namespace OneCardSln.OneCardClient.Public
{
    /// <summary>
    /// MessageWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MessageWindow : Window
    {
        public MessageType MsgType { get; set; }
        public string Title { get; private set; }
        public string Msg { get; private set; }

        private MessageWindow()
        {
            InitializeComponent();
        }

        public MessageWindow(MessageType mstType, string title = "", string msg = "")
            : this()
        {
            MsgType = mstType;
            Title = title;
            Msg = msg;

            this.Loaded += delegate
            {
                Bind();
            };
        }

        void Bind()
        {
            ControlTemplate msgWindowTemplate = (ControlTemplate)App.Current.Resources["MsgWindowTemplate"];
            //
            Image img = (Image)msgWindowTemplate.FindName("imgTitle", this);
            BitmapImage bitmap = new BitmapImage(new Uri(GetIconFile(), UriKind.Absolute));
            img.Source = bitmap;
            //
            TextBlock txt = (TextBlock)msgWindowTemplate.FindName("txtTitle", this);
            txt.Text = Title;
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
                    return "pack://application:,,,/Resources/img/ask.png";
                case MessageType.Error:
                    return "pack://application:,,,/Resources/img/error.png";
                case MessageType.Info:
                    return "pack://application:,,,/Resources/img/info.png";
                case MessageType.Warning:
                    return "pack://application:,,,/Resources/img/warn.png";
            }
            return string.Empty;
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
