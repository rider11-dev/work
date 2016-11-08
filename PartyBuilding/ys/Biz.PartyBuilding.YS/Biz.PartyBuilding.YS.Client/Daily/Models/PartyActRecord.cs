using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.PartyBuilding.YS.Client.Daily.Models
{
    public class PartyActRecord
    {
        public string party { get; set; }
        public string type { get; set; }
        public string theme { get; set; }
        public string time { get; set; }
        public string place { get; set; }
        public string host { get; set; }
        public string recorder { get; set; }
        public string cnt_yd { get; set; }
        public string cnt_sd { get; set; }
        public string cnt_qj { get; set; }
        public string cnt_qx { get; set; }
        public string cnt_chry { get; set; }
        public string content { get; set; }
        public List<PartyActRecordAttach> attaches { get; set; }
    }

    public class PartyActRecordAttach
    {
        public string att_name { get; set; }
        public string att_size { get; set; }
    }
}
