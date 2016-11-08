using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.PartyBuilding.YS.Client.Sys.Models
{
    public class EvaluateProject
    {
        public string id { get; set; }
        public string name { get; set; }
        public string score { get; set; }
        public string remark { get; set; }
        public string time_type { get; set; }
        public string time_day { get; set; }
        public string party_type { get; set; }
        public string check_type { get; set; }
        public string score_type { get; set; }
        public string parent { get; set; }
        public string order { get; set; }

        public void CopyTo(EvaluateProject target)
        {
            if (target == null)
            {
                return;
            }
            target.id = id;
            target.name = name;
            target.score = score;
            target.remark = remark;
            target.time_type = time_type;
            target.time_day = time_day;
            target.party_type = party_type;
            target.check_type = check_type;
            target.score_type = score_type;
            target.parent = parent;
            target.order = order;
        }
    }

}
