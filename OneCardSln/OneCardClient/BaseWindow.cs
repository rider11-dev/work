using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using OneCardSln.Components.WPF.Extension;
using System.Windows.Controls;
using OneCardSln.Components.WPF.Command;


namespace OneCardSln.OneCardClient
{
    public class BaseWindow : Window
    {
        const string BaseWindowTemplateName = "popWindowTemplate";

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

        private void CloseAction(object parameter)
        {
            this.Close();
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

        private void MinimizeAction(object parameter)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
