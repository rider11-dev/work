using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.CustomQuery.Model
{
    public class Sort
    {
        public string Field { get; set; }
        public SortType SortType { get; set; }
    }

    /// <summary>
    /// 排序类型
    /// </summary>
    public enum SortType
    {
        Asc,
        Desc
    }
}
