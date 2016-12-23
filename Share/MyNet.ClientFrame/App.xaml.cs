using MyNet.Components;
using MyNet.Components.Extensions;
using MyNet.Components.Logger;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Threading;

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
            Upgrade();
            Start();
        }

        //程序出错时触发的事件
        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            _logHelper.LogError(e.Exception);
            var rst = MessageBox.Show(e.Exception.Message + Environment.NewLine + "是否退出？", "程序错误", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (rst == MessageBoxResult.Yes)
            {
                this.Shutdown(-1);
            }
        }

        private void Upgrade()
        {
            //先检查更新
            var app = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('/', '\\') + "/" + AppSettingUtils.Get("upgradeapp");
            var process = Process.Start(app);
            process.WaitForExit();
        }

        private void Start()
        {
            LoadResources();
            //打开第一个窗口
            if (string.IsNullOrEmpty(FrameContext.StartupWindow))
            {
                MessageBox.Show("未能找到启动窗口，请检查配置文件！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Shutdown(-1);
            }
            Application.Current.StartupUri = new Uri(FrameContext.StartupWindow, UriKind.Relative);
        }

        private void LoadResources()
        {
            //加载插件目录程序集
            AssemblyExtention.LoadAssemblies(FrameContext.PluginPath, "^*.dll$");

            //加载资源文件
            List<FileInfo> resFiles = new List<FileInfo>();
            //框架资源文件
            var frameResFile = FileExtension.GetFiles(FrameContext.BasePath, FrameContext.ResConfFileName, SearchOption.TopDirectoryOnly);
            if (frameResFile.IsNotEmpty())
            {
                resFiles.AddRange(frameResFile);
            }
            //插件资源文件
            var pluginResFiles = FileExtension.GetFiles(FrameContext.PluginPath, FrameContext.ResConfFileName);
            if (pluginResFiles.IsNotEmpty())
            {
                resFiles.AddRange(pluginResFiles);
            }

            if (resFiles.IsNotEmpty())
            {
                foreach (var file in resFiles)
                {
                    LoadResourseFromFile(file);
                }
            }
        }

        private void LoadResourseFromFile(FileInfo file)
        {
            if (file == null || !file.Exists)
            {
                return;
            }
            var confData = File.ReadAllText(file.FullName);
            var reses = JsonConvert.DeserializeObject<IList<string>>(confData);
            if (reses != null && reses.Count > 0)
            {
                Application.Current.Resources.MergedDictionaries.AddRange(reses.Select(s => new ResourceDictionary { Source = new Uri(s, UriKind.Relative) }));
            }
        }
    }
}
