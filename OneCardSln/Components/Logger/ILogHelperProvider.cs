using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneCardSln.Components.Logger
{
    public interface ILogHelperProvider
    {
        ILogHelper<T> GetLogHelper<T>();
    }
}
