using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.Components.Logger
{
    public interface ILogHelper<T>
    {
        void LogError(string msg, Exception ex = null);
        void LogError(Exception ex);
        void LogInfo(string msg, Exception ex = null);
        void LogInfo(Exception ex);
        void LogWarning(string msg, Exception ex = null);
        void LogWarning(Exception ex);
    }
}
