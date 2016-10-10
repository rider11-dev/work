using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using OneCardSln.Components.WPF.Extension;
using System.Windows.Controls;


namespace OneCardSln.OneCardClient
{
    public class BaseWindow : Window
    {
        public BaseWindow()
            : base()
        {
            Init();
        }

        protected void Init()
        {
            this.AllowDrop = true;
            this.DragWhenLeftMouseDown();

            this.Loaded += delegate { BindWindowButtonEvent(); };
        }

        private void BindWindowButtonEvent()
        {
            //右上角按钮绑定事件，在Loaded事件中处理，不能在构造函数中
            ControlTemplate baseWindowTemplate = (ControlTemplate)App.Current.Resources["BaseWindowTemplate"];
            Button btn = (Button)baseWindowTemplate.FindName("btnMinimize", this);
            if (btn != null)
            {
                btn.Click += delegate
                {
                    this.WindowState = WindowState.Minimized;
                };
            }
            btn = (Button)baseWindowTemplate.FindName("btnClose", this);
            if (btn != null)
            {
                btn.Click += delegate
                {
                    this.Close();
                };
            }
        }
    }
}
