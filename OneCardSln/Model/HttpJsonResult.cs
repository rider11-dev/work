using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneCardSln.Model
{
    /// <summary>
    /// http请求json响应数据实体
    /// </summary>
    public class HttpJsonResult
    {
        /// <summary>
        /// 结果代码
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 结果说明
        /// </summary>
        public string msg { get; set; }
    }
}
