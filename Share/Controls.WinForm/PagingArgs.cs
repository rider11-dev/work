using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyNet.Components.WinForm
{
    /// <summary>
    /// 分页参数类
    /// </summary>
    public class PagingArgs
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public int RecordsCount { get; set; }
    }
}
