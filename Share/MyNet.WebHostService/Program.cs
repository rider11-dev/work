using MyNet.Components.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.WebHostService
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main(string[] args)
        {
            //作为服务运行
            if (args != null && args.Count() > 0 && !string.IsNullOrEmpty(args[0]))
            {
                var cmd = args[0];
                if (cmd.ToLower() == "s")
                {
                    RunService();
                }
                return;
            }

            ServiceManagement();
        }

        private static void RunService()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new HostService()
            };
            ServiceBase.Run(ServicesToRun);
        }

        private static void ServiceManagement()
        {
            Console.WriteLine("欢迎使用" + HostContext.SvrName);
            Console.WriteLine("请选择：[1]安装服务 [2]卸载服务 [3]启动服务 [4]停止服务 [5]重启服务");
            var input = 0;
            int.TryParse(Console.ReadLine(), out input);
            switch (input)
            {
                case 1:
                    InstallService();
                    break;
                case 2:
                    UninstallService();
                    break;
                case 3:
                    StartService();
                    break;
                case 4:
                    StopService();
                    break;
                case 5:
                    RestartService();
                    break;
                default:
                    break;
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        private static void UninstallService()
        {
            //停止服务
            StopService();
            Console.WriteLine(string.Format("将要卸载服务【{0}】...", HostContext.SvrName));
            string uninstallCmd = string.Format("sc delete {0}", HostContext.SvrName);
            var rst = ExecuteCmd(uninstallCmd);
            if (rst == false)
            {
                Console.WriteLine("服务【{0}】卸载失败", HostContext.SvrName);
                return;
            }
            Console.WriteLine(string.Format("服务【{0}】卸载成功", HostContext.SvrName));
        }

        private static void InstallService()
        {
            Console.WriteLine(string.Format("将要安装服务【{0}】...", HostContext.SvrName));
            var path = Process.GetCurrentProcess().MainModule.FileName + " s";
            string installCmd = string.Format("sc create {0} binpath= \"{1}\" displayName= {0} start= auto", HostContext.SvrName, path);//注意等号后面有空格
            bool rst = ExecuteCmd(installCmd);
            if (rst == false)
            {
                Console.WriteLine("服务【{0}】安装失败", HostContext.SvrName);
                return;
            }
            Console.WriteLine(string.Format("服务【{0}】已安装成功", HostContext.SvrName));
            StartService();
        }

        private static bool StartService()
        {
            Console.WriteLine(string.Format("将要启动服务【{0}】...", HostContext.SvrName));
            string startCmd = string.Format("net start {0}", HostContext.SvrName);
            var rst = ExecuteCmd(startCmd);
            if (rst == true)
            {
                Console.WriteLine(string.Format("启动服务【{0}】成功", HostContext.SvrName));
            }
            return rst;
        }

        private static bool StopService()
        {
            Console.WriteLine(string.Format("将要停止服务【{0}】...", HostContext.SvrName));
            string stopCmd = string.Format("net stop {0}", HostContext.SvrName);
            bool rst = ExecuteCmd(stopCmd);
            if (rst)
            {
                Console.WriteLine(string.Format("停止服务【{0}】成功", HostContext.SvrName));
            }
            return rst;
        }

        private static void RestartService()
        {
            StopService();
            StartService();
        }

        private static bool ExecuteCmd(string cmd)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardInput = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.Arguments = "/c " + cmd;
            proc.Start();
            proc.WaitForExit();
            if (proc.StandardError != null)
            {
                var error = proc.StandardError.ReadToEnd();
                if (!error.IsEmpty())
                {
                    Console.WriteLine(error);
                    return false;
                }
            }
            return true;
        }
    }
}
