using MyNet.Components.Logger;
using MyNet.Components.WPF.Windows;
using MyNet.ClientFrame.Public;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MyNet.ClientFrame
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        static ILogHelper<App> _logHelper = LogHelperFactory.GetLogHelper<App>();
        //程序出错时触发的事件
        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            _logHelper.LogError(e.Exception);
            var rst = MessageWindow.ShowMsg(MessageType.Error, "程序错误", e.Exception.Message + Environment.NewLine + "是否退出？");
            if (rst != null && (bool)rst == true)
            {
                //这里需要加一些退出前处理
                //TODO
                this.Shutdown(-1);
            }
        }
    }
}
