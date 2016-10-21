using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyNet.Components.WPF.Command
{
    public class DelegateCommand : ICommand
    {
        Func<object, bool> _canExecute;
        Action<object> _executeAction;
        bool _canExecuteCache = true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="executeAction"></param>
        /// <param name="canExecute">获取是否可执行（为空则默认可执行）</param>
        public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecute = null)
        {
            _canExecute = canExecute;
            _executeAction = executeAction;
        }

        #region ICommand Members
        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
            {
                bool temp = _canExecute((object)parameter);
                if (_canExecuteCache != temp)
                {
                    _canExecuteCache = temp;
                    if (CanExecuteChanged != null)
                    {
                        CanExecuteChanged(this, new EventArgs());
                    }
                }
            }
            return _canExecuteCache;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _executeAction((object)parameter);
        }
        #endregion
    }
}
