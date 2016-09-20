using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneCardSln.Model
{
    public enum ResultCode
    {
        Success = 1,
        Fail = 0,
        Repeat = 2,
        Unknown = -1
    }
}