using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace OneCardSln.Model
{
    public enum ResultCode
    {
        [Description("未知操作")]
        Unknown = -1,
        [Description("成功")]
        Success = 1,
        [Description("失败")]
        Fail = 0,
        [Description("重复操作")]
        Repeat = 2,
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
    }
}