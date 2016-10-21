using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Components.WPF.Controls
{
    public class PagingArgs
    {
        public int PageSize { get; set; }
        /// <summary>
        /// 1:第一页
        /// </summary>
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public int RecordsCount { get; set; }

        /// <summary>
        /// 获取当前页的起始索引
        /// </summary>
        public int Start
        {
            get
            {
                int idx = (PageIndex - 1) * PageSize;
                if (idx < 0)
                {
                    idx = 0;
                }
                return idx;
            }
        }

        public PagingArgs()
        {
            PageIndex = 1;
            PageSize = 20;
        }
    }
}
