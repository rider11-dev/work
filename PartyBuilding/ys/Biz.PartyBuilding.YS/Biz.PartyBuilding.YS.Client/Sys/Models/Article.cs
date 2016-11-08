using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.PartyBuilding.YS.Client.Sys.Models
{
    public class Article
    {
        public string channel_code { get; set; }
        public string channel { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string state { get; set; }
        public string issue_time { get; set; }
        public string attach { get; set; }
        public string clicks { get; set; }
    }
}
