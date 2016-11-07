using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.PartyBuilding.YS.Client.Daily.Models
{
    public class NoticeEntity
    {
        public string title { get; set; }
        public string type { get; set; }
        public string content { get; set; }
        public string attach { get; set; }
        public string urgency { get; set; }
        public string issue_party { get; set; }
        public string issue_time { get; set; }
        public string receive_party { get; set; }
        public string state { get; set; }
        public string need_reply { get; set; }
        public string reply_expire_time { get; set; }

        public List<ReplyDetail> reply_details { get; set; }

        public List<ViewDetail> view_details { get; set; }


    }

    public class ViewDetail
    {
        public string party { get; set; }
        public string time { get; set; }
        public string isviewed { get; set; }
    }

    public class ReplyDetail
    {
        public string party { get; set; }
        public string time { get; set; }
        public string reply_content { get; set; }
        public string isreplied { get; set; }
    }
}
