using MyNet.Components.Extensions;
using MyNet.WindowsService;
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

            WindowsServiceManager svrManager = new WindowsServiceManager(HostContext.SvrName);
            svrManager.Manage();
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
    }
}
