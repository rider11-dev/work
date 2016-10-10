using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OneCardSln.Components.WPF.Controls
{
    public class MyPasswordBox : TextBox
    {
        private char PWD_CHAR = '*';
        private string _password = string.Empty;
        private bool dirtyBaseText;
        /// <summary>
        /// 设置base.Text而不触发OnTextChanged事件
        /// </summary>
        private string BaseText
        {
            get { return base.Text; }
            set
            {
                dirtyBaseText = true;
                base.Text = value;
                dirtyBaseText = false;
            }
        }

        public new string Text
        {
            get { return _password; }
            set
            {
                _password = value;
                this.BaseText = new string(PWD_CHAR, value.Length);
            }
        }
        //数据绑定用的附加属性RealText  
        public static readonly DependencyProperty RealTextProperty =
            DependencyProperty.RegisterAttached("RealText", typeof(string), typeof(MyPasswordBox), new UIPropertyMetadata(""));

        public static string GetRealText(DependencyObject obj)
        {
            return (string)obj.GetValue(RealTextProperty);
        }

        public static void SetRealText(DependencyObject obj, string value)
        {
            obj.SetValue(RealTextProperty, value);
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            if (dirtyBaseText == true)
            {
                return;
            }
            string currentText = this.BaseText;
            int selStart = this.SelectionStart;
            //处理删除字符
            if (currentText.Length < _password.Length)
            {
                _password = _password.Remove(selStart, _password.Length - currentText.Length);
                SetRealText(this, _password);
            }
            //
            if (!string.IsNullOrEmpty(currentText))
            {
                for (int i = 0; i < currentText.Length; i++)
                {
                    if (currentText[i] != PWD_CHAR)
                    {
                        // Replace or insert char
                        if (this.BaseText.Length == _password.Length)
                        {
                            _password = _password.Remove(i, 1).Insert(i, currentText[i].ToString());
                        }
                        else
                        {
                            _password = _password.Insert(i, currentText[i].ToString());

                        }
                    }
                }
                SetRealText(this, _password);

                this.BaseText = new string(PWD_CHAR, _password.Length);
                this.SelectionStart = selStart;
            }

            base.OnTextChanged(e);
        }

    }
}
