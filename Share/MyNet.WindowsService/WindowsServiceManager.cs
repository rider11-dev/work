using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.WindowsService
{
    public class WindowsServiceManager
    {

        public string ServiceName { get; private set; }

        public WindowsServiceManager(string svrname)
        {
            ServiceName = svrname;
        }

        public void Manage()
        {
            Console.WriteLine("欢迎使用" + ServiceName);
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

        private void UninstallService()
        {
            //停止服务
            StopService();
            Console.WriteLine(string.Format("将要卸载服务【{0}】...", ServiceName));
            string uninstallCmd = string.Format("sc delete {0}", ServiceName);
            var rst = ExecuteCmd(uninstallCmd);
            if (rst == false)
            {
                Console.WriteLine("服务【{0}】卸载失败", ServiceName);
                return;
            }
            Console.WriteLine(string.Format("服务【{0}】卸载成功", ServiceName));
        }

        private void InstallService()
        {
            Console.WriteLine(string.Format("将要安装服务【{0}】...", ServiceName));
            var path = Process.GetCurrentProcess().MainModule.FileName + " s";
            string installCmd = string.Format("sc create {0} binpath= \"{1}\" displayName= {0} start= auto", ServiceName, path);//注意等号后面有空格
            bool rst = ExecuteCmd(installCmd);
            if (rst == false)
            {
                Console.WriteLine("服务【{0}】安装失败", ServiceName);
                return;
            }
            Console.WriteLine(string.Format("服务【{0}】已安装成功", ServiceName));
            StartService();
        }

        private bool StartService()
        {
            Console.WriteLine(string.Format("将要启动服务【{0}】...", ServiceName));
            string startCmd = string.Format("net start {0}", ServiceName);
            var rst = ExecuteCmd(startCmd);
            if (rst == true)
            {
                Console.WriteLine(string.Format("启动服务【{0}】成功", ServiceName));
            }
            return rst;
        }

        private bool StopService()
        {
            Console.WriteLine(string.Format("将要停止服务【{0}】...", ServiceName));
            string stopCmd = string.Format("net stop {0}", ServiceName);
            bool rst = ExecuteCmd(stopCmd);
            if (rst)
            {
                Console.WriteLine(string.Format("停止服务【{0}】成功", ServiceName));
            }
            return rst;
        }

        private void RestartService()
        {
            StopService();
            StartService();
        }

        private bool ExecuteCmd(string cmd)
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
                if (!string.IsNullOrEmpty(error))
                {
                    Console.WriteLine(error);
                    return false;
                }
            }
            return true;
        }
    }
}
