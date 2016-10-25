using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MyNet.Components.WPF.Extension;
using System.Windows.Controls;
using MyNet.Components.WPF.Command;

namespace MyNet.Components.WPF.Windows
{
    public class BaseWindow : Window
    {
        const string BaseWindowTemplateName = "popWindowTemplate";

        private DelegateCommand _closeCmd;
        public DelegateCommand CloseCmd
        {
            get
            {
                if (_closeCmd == null)
                {
                    _closeCmd = new DelegateCommand(CloseAction);
                }
                return _closeCmd;
            }
        }

        private DelegateCommand _minCmd;
        public DelegateCommand MinCmd
        {
            get
            {
                if (_minCmd == null)
                {
                    _minCmd = new DelegateCommand(MinimizeAction);
                }
                return _minCmd;
            }
        }

        //依赖属性——自定义窗口title
        public string CustomTitle
        {
            get { return (string)GetValue(CustomTitleProperty); }
            set { SetValue(CustomTitleProperty, value); }
        }

        public static readonly DependencyProperty CustomTitleProperty = DependencyProperty.Register("CustomTitle", typeof(string), typeof(BaseWindow), new PropertyMetadata(null));
        public BaseWindow()
            : base()
        {
            Init();
        }


        protected void Init()
        {
            this.AllowDrop = true;
            this.DragWhenLeftMouseDown();
        }
        private void CloseAction(object parameter)
        {
            this.Close();
        }
        private void MinimizeAction(object parameter)
        {
            this.WindowState = WindowState.Minimized;
        }

        protected void Resize()
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }
    }
}
