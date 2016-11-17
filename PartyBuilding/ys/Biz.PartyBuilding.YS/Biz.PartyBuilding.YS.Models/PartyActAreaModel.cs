using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biz.PartyBuilding.YS.Models
{
    /// <summary>
    /// 组织活动场所
    /// </summary>
    public class PartyActAreaModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string town { get; set; }
        public string village { get; set; }
        /// <summary>
        /// 建筑面积
        /// </summary>
        public string floor_area { get; set; }
        /// <summary>
        /// 院落面积
        /// </summary>
        public string courtyard_area { get; set; }
        public string levels { get; set; }
        public string rooms { get; set; }
        /// <summary>
        /// 坐落位置
        /// </summary>
        public string location { get; set; }
        public string gps { get; set; }
        public List<string> pic { get; set; }
    }
}