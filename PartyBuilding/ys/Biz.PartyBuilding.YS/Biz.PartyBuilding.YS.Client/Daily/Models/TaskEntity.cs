using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.PartyBuilding.YS.Client.Daily.Models
{
    public class TaskEntity
    {
        public string name { get; set; }
        public string priority { get; set; }
        public string content { get; set; }
        public string issue_time { get; set; }
        public string expire_time { get; set; }
        public string rec_party { get; set; }
        public string issue_party { get; set; }
        public string progress { get; set; }
        public string state { get; set; }

        public TaskCompleteDetail complete_detail { get; set; }
    }
}
