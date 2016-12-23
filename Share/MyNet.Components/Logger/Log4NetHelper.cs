using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: log4net.Config.XmlConfigurator(Watch = true, ConfigFile = "log4net.config")]
namespace MyNet.Components.Logger
{
    /// <summary>
    /// 日志辅助类，使用log4net
    /// </summary>
    public class Log4NetHelper<T> : ILogHelper<T>
    {
        log4net.ILog logger = null;
        public Log4NetHelper()
        {
            logger = log4net.LogManager.GetLogger(typeof(T));
        }

        public void LogError(string msg, Exception ex = null)
        {
            if (logger == null)
            {
                return;
            }
            logger.Error(msg, ex);
        }

        public void LogError(Exception ex)
        {
            if (logger == null)
            {
                return;
            }
            logger.Error("Error:", ex);
        }

        public void LogInfo(string msg, Exception ex = null)
        {
            if (logger == null || !AppSettingUtils.Log)
            {
                return;
            }
            logger.Info(msg, ex);
        }

        public void LogInfo(Exception ex)
        {
            if (logger == null || !AppSettingUtils.Log)
            {
                return;
            }
            logger.Info("Info:", ex);
        }

        public void LogWarning(string msg, Exception ex = null)
        {
            if (logger == null || !AppSettingUtils.Log)
            {
                return;
            }
            logger.Warn(msg, ex);
        }

        public void LogWarning(Exception ex)
        {

            if (logger == null || !AppSettingUtils.Log)
            {
                return;
            }
            logger.Warn("Warn:", ex);
        }
    }
}
