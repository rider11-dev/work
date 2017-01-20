using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Components.Handlers
{
    /// <summary>
    /// 值改变处理器
    /// </summary>
    /// <param name="oldValue">旧值</param>
    /// <param name="newValue">新值</param>
    public delegate void ValueCangedHandler(string oldValue, string newValue);
}
