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
        private int _idx;
        public int pageIndex
        {
            get { return _idx; }
            set
            {
                _idx = value;
                if (_idx <= 0)
                {
                    _idx = 1;
                }
            }
        }
        private int _size;
        public int pageSize
        {
            get { return _size; }
            set
            {
                _size = value;
                if (_size <= 0)
                {
                    _size = 20;
                }
            }
        }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int pageTotal { get; set; }

        public Dictionary<string, object> conditions { get; set; }

    }
}