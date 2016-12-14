using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.CustomQuery.Model
{
    public class Sort
    {
        public string Field { get; set; }
        public string DisplayName { get; set; }
        public SortType SortType { get; set; }
        public string FieldFullName
        {
            get
            {
                return ToString();
            }
        }

        public override string ToString()
        {
            return string.Format("{0}[{1}]", DisplayName, Field);
        }
    }

    /// <summary>
    /// 排序类型
    /// </summary>
    public enum SortType
    {
        [Description("升序")]
        Asc,
        [Description("降序")]
        Desc
    }
}
