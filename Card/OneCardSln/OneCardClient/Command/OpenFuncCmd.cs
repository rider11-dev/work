using MyNet.Components.Logger;
using MyNet.Components.WPF.Windows;
using OneCardSln.OneCardClient.Pages;
using OneCardSln.OneCardClient.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace OneCardSln.OneCardClient.Command
{
    /// <summary>
    /// 打开功能菜单命令
    /// </summary>
    public class OpenFuncCmd : ICommand
    {
        private ILogHelper<OpenFuncCmd> _logHelper = LogHelperFactory.GetLogHelper<OpenFuncCmd>();
        public Frame Container { get; set; }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// 记录上次加载完成时绑定的事件委托，用来在下次绑定之前取消，避免多次绑定
        /// </summary>
        static LoadCompletedEventHandler LastEventHandler = null;//
        public void Execute(object parameter)
        {
            Container.Source = null;
            Container.LoadCompleted -= LastEventHandler;
            OpenFuncParam param = (OpenFuncParam)parameter;
            if (param == null)
            {
                _logHelper.LogError("parameter不是有效的OpenFuncParam类型");
                MessageWindow.ShowMsg(MessageType.Error, OperationDesc.OpenFunc, MsgConst.Msg_ViewAppLog);
                return;
            }
            if (string.IsNullOrEmpty(param.PageUri))
            {
                _logHelper.LogError("PageUri不能为空");
                MessageWindow.ShowMsg(MessageType.Error, OperationDesc.OpenFunc, MsgConst.Msg_ViewAppLog);
                return;
            }
            //需要在LoadCompleted事件中，才能获取到绑定到Frame的Page实例
            LastEventHandler = (o, e) =>
            {
                try
                {
                    var page = e.Content as BasePage;
                    page.FuncId = param.FuncId;
                }
                catch (Exception ex)
                {
                    _logHelper.LogError("LoadCompleted事件", ex);
                    throw new Exception("打开功能菜单错误", ex);
                }
            };
            Container.LoadCompleted += LastEventHandler;

            Container.Source = PageHelper.GetPageFullUri(param.PageUri);
        }


    }
}
