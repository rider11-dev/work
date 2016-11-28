using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyNet.Components.Logger
{
    public class LogHelperFactory
    {
        public static ILogHelperProvider LogHelperProvider;
        public static ILogHelper<T> GetLogHelper<T>()
        {
            if (LogHelperProvider == null)
            {
                //使用默认日志组件，如使用扩展日志组件，请实现ILogHelper<T>接口并给LogHelperProvider赋值
                LogHelperProvider = new LogHelperProvider();
            }
            return LogHelperProvider.GetLogHelper<T>();
        }
    }
}
