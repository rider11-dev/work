using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Service.Card.Models
{
    /// <summary>
    /// 接口实体类
    /// </summary>
    public class API
    {
        /// <summary>
        /// 接口名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 接口url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 接口提供方
        /// </summary>
        public string Provider { get; set; }
    }
}
