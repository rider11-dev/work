using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MyNet.Components.Result
{
    public enum ResultCode
    {
        [Description("异常")]
        Exception = -1,
        [Description("成功")]
        Success = 1,
        [Description("失败")]
        Fail = 0,
        [Description("重复操作")]
        OptRepeat = 2,
        [Description("参数错误")]
        ParamError = 3,
        [Description("未找到token")]
        Tokenless = 4,
        [Description("token已失效")]
        TokenExpired = 5,
        [Description("非法token")]
        TokenIllegal = 6,
        [Description("数据不存在")]
        DataNotFound = 7,
        [Description("数据已被使用")]
        DataInUse = 8,
        [Description("数据重复")]
        DataRepeat = 9,
        [Description("系统预制数据")]
        DataSystem = 10,
        [Description("数据库操作错误")]
        DbError = 11,
        [Description("非法操作")]
        IllegalOpt = 12,
        [Description("管理员数据")]
        DataAddmin = 13,
    }
}