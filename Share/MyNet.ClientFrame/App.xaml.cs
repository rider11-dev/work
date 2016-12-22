using MyNet.Components;
using MyNet.Components.Extensions;
using MyNet.Components.Logger;
using System;
using System.Diagnostics;
using System.Windows;

namespace ClientFrame
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        static ILogHelper<App> _logHelper = LogHelperFactory.GetLogHelper<App>();
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //先检查更新
            var app = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('/', '\\') + "/" + AppSettingHelper.Get("upgradeapp");
            var process = Process.Start(app);
            process.WaitForExit();

            //加载插件目录程序集
            AssemblyExtention.LoadAssemblies(AppDomain.CurrentDomain.BaseDirectory + "plugin", "^*.dll$");
        }

        //程序出错时触发的事件
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            _logHelper.LogError(e.Exception);
            var rst = MessageBox.Show(e.Exception.Message + Environment.NewLine + "是否退出？", "程序错误", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (rst == MessageBoxResult.Yes)
            {
                this.Shutdown(-1);
            }
        }
    }
}
