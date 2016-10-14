using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.Components.WPF.Controls
{
    public class PagingArgs
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public int RecordsCount { get; set; }
    }
}
