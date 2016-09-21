using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneCardSln.Model
{
    public enum ResultCode
    {
        /// <summary>
        /// 位置操作
        /// </summary>
        Unknown = -1,
        /// <summary>
        /// 成功
        /// </summary>
        Success = 1,
        /// <summary>
        /// 失败
        /// </summary>
        Fail = 0,
        /// <summary>
        /// 重复操作
        /// </summary>
        Repeat = 2,
        /// <summary>
        /// 参数错误
        /// </summary>
        ParamError = 3,
    }
}