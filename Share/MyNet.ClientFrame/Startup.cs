using MyNet.Components;
using MyNet.Components.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace ClientFrame
{
    public class Startup
    {
        private static Mutex myMutex;
        private static bool requestInitialOwnership = true;
        private static bool mutexWasCreated;

        [STAThread]
        static void Main()
        {
            myMutex = new Mutex(requestInitialOwnership, "singleton", out mutexWasCreated);
            if (mutexWasCreated == false)
            {
                return;
            }
            if (FrameContext.CheckUpdate)
            {
                Upgrade();
            }

            Start(new Application());
        }

        private static void Upgrade()
        {
            //先检查更新
            var upgradeApp = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('/', '\\') + "/" + AppSettingUtils.Get("upgradeapp");
            var process = Process.Start(upgradeApp);
            process.WaitForExit();
            process.Dispose();
        }

        private static void Start(Application app)
        {
            LoadResources(app);
            //打开第一个窗口
            if (string.IsNullOrEmpty(FrameContext.StartupWindow))
            {
                MessageBox.Show("未能找到启动窗口，请检查配置文件！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                app.Shutdown(-1);

            }
            app.StartupUri = new Uri(FrameContext.StartupWindow, UriKind.Relative);
            app.Activated += App_Activated;
            app.DispatcherUnhandledException += App_DispatcherUnhandledException;
            app.Run();
        }

        private static void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var rst = MessageBox.Show(e.Exception.Message + Environment.NewLine + "是否退出？", "程序异常", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (rst == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown(-1);
            }
            e.Handled = true;
        }

        private static void App_Activated(object sender, EventArgs e)
        {
            var app = sender as Application;
            if (app.MainWindow != null)
            {
                app.MainWindow.Activate();
            }
        }

        private static void LoadResources(Application app)
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
                    LoadResourseFromFile(file, app);
                }
            }
        }

        private static void LoadResourseFromFile(FileInfo file, Application app = null)
        {
            if (file == null || !file.Exists)
            {
                return;
            }
            var confData = File.ReadAllText(file.FullName);
            var reses = JsonConvert.DeserializeObject<IList<string>>(confData);
            if (reses != null && reses.Count > 0)
            {
                app.Resources.MergedDictionaries.AddRange(reses.Select(s => new ResourceDictionary { Source = new Uri(s, UriKind.Relative) }));
            }
        }

    }
}
