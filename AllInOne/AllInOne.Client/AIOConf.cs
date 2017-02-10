using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  AllInOne.Client
{
    public class AIOConf
    {
        /// <summary>
        /// 政府在线url
        /// </summary>
        public string url_gov { get; set; }
        /// <summary>
        /// 服务购买url
        /// </summary>
        public string url_buy { get; set; }
        public int card_read_interval { get; set; }
    }
}
