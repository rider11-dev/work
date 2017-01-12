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

        public new string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public new static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(string), typeof(BaseWindow), new PropertyMetadata(null));

        public bool CanResize
        {
            get { return (bool)GetValue(CanResizeProperty); }
            set { SetValue(CanResizeProperty, value); }
        }

        public static readonly DependencyProperty CanResizeProperty = DependencyProperty.Register("CanResize", typeof(bool), typeof(BaseWindow), new PropertyMetadata(false, null));


        public BaseWindow()
            : base()
        {
            Init();
        }


        protected void Init()
        {
            this.AllowDrop = true;
            this.DragWhenLeftMouseDown();

            this.Icon = AppDomain.CurrentDomain.BaseDirectory + AppSettingUtils.Get("icon");
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.MouseDown += BaseWindow_MouseDown;
        }

        private void BaseWindow_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (CanResize == false)
            {
                return;
            }
            if (e.ClickCount == 2)
            {
                Resize();
                e.Handled = true;
            }
        }

        private void CloseAction(object parameter)
        {
            BeforeClose();
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

        protected virtual void BeforeClose()
        {

        }
    }
}
