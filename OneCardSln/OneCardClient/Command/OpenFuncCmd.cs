using OneCardSln.Components.Logger;
using OneCardSln.OneCardClient.Pages;
using OneCardSln.OneCardClient.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

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

        public void Execute(object parameter)
        {
            Container.Source = null;
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
            //MessageWindow.ShowMsg(MessageType.Info, "提示", "我被单击了！" + param.PageUri);
            Container.Source = PageHelper.GetPageFullUri(param.PageUri);
        }   
    }
}
