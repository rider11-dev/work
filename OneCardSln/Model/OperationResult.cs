using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneCardSln.Model
{
    /// <summary>
    /// 操作结果类
    /// </summary>
    public class OperationResult
    {
        public ResultCode Code { get; set; }
        public string Message { get; set; }

        public object Data { get; set; }

        public OperationResult(ResultCode code, string msg)
        {
            Code = code;
            Message = msg;
        }

        public OperationResult()
            : this(ResultCode.Unknown, "未识别的状态")
        {

        }
    }
}