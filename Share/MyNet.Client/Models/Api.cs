using MyNet.Client.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Client.Models
{
    /// <summary>
    /// 接口实体类
    /// </summary>
    public class Api
    {
        /// <summary>
        /// 接口名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 接口相对url
        /// </summary>
        public string RelativeUrl { get; set; }

        /// <summary>
        /// 接口绝对url
        /// </summary>
        public string AbsoluteUrl
        {
            get
            {
                return ClientContext.Conf.srvroot.TrimEnd('/') + "/" + RelativeUrl;
            }
        }

        /// <summary>
        /// 接口提供方
        /// </summary>
        public string Provider { get; set; }
    }
}
