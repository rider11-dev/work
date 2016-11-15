using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biz.PartyBuilding.YS.WebApi.Models
{
    public class TaskModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string content { get; set; }
        /// <summary>
        /// 优先级：高、中、低
        /// </summary>
        public string priority { get; set; }
        public string receiver { get; set; }
        public string issue_time { get; set; }
        public string expire_time { get; set; }
        public string progress { get; set; }
        /// <summary>
        /// 状态：编辑、已发布、已完成、已取消
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// 完成标志:未领、已领未完成、已完成
        /// </summary>
        public string complete_state { get; set; }
    }
}