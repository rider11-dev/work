using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Model.Auth
{
    /// <summary>
    /// 权限类别
    /// </summary>
    public enum PermType
    {
        [Description("功能权限")]
        PermTypeFunc = 0,
        [Description("操作权限")]
        PermTypeOpt = 1
    }
}
