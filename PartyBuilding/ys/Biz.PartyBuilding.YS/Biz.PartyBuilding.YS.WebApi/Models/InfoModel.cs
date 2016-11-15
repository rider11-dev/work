using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biz.PartyBuilding.YS.WebApi.Models
{
    public class InfoModel
    {
        public string id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string issue_time { get; set; }
        public string party { get; set; }
        /// <summary>
        /// 状态：编辑、已发布
        /// </summary>
        public string state { get; set; }
        /// <summary>
        /// 阅读状态：已读、未读
        /// </summary>
        public string read_state { get; set; }
    }
}