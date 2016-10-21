using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyNet.Model
{
    /// <summary>
    /// 分页查询模型
    /// </summary>
    public class PageQuery
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }

        public long total { get; set; }

        public Dictionary<string, object> conditions { get; set; }

        public void Verify()
        {
            if (pageIndex < 0)
            {
                pageIndex = 0;
            }
            if (pageSize <= 0)
            {
                pageSize = 20;
            }
        }
    }
}