using MyNet.Components.WPF.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MyNet.Components.WPF.Extension;

namespace MyNet.Components.WPF.Controls
{
    /// <summary>
    /// 帮助按钮扩展
    /// </summary>
    public class HelpButton : Button
    {
        /// <summary>
        /// 禁止输入，用来控制textbox输入框（参考inputBtnAddOnTemp）是否支持输入
        /// </summary>
        public bool ForbidInput
        {
            get { return (bool)GetValue(ForbidInputProperty); }
            set { SetValue(ForbidInputProperty, value); }
        }

        public static readonly DependencyProperty ForbidInputProperty =
           DependencyProperty.Register("ForbidInput", typeof(bool), typeof(HelpButton), new PropertyMetadata(false));

        ICommand _clearCmd;
        public ICommand ClearCmd
        {
            get
            {
                if (_clearCmd == null)
                {
                    _clearCmd = new DelegateCommand(ClearAction);
                }
                return _clearCmd;
            }
        }

        void ClearAction(object parameter)
        {
            var txtBox = this.FindVisualChild<TextBox>();
            if (txtBox != null)
            {
                txtBox.Text = "";
            }
        }
    }
}
