using MyNet.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientFrame
{
    public class FrameContext
    {
        public const int ExitCode = -1;

        public static string StartupWindow
        {
            get
            {
                return AppSettingUtils.Get("startWindow");
            }
        }

        public static string BasePath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory.TrimEnd('/', '\\');
            }
        }

        public static string ResConfFileName
        {
            get
            {
                return "resconf.json";
            }
        }

        public static string PluginPath
        {
            get
            {
                return BasePath + "/plugin";
            }
        }

        public static bool CheckUpdate
        {
            get
            {
                bool ck = true;
                Boolean.TryParse(AppSettingUtils.Get("checkupdate"), out ck);
                return ck;
            }
        }
    }
}
