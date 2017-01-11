using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Model
{
    public class CardInfo
    {
        public string id { get; set; }
        public string idcard { get; set; }
        public string number { get; set; }
        public string state { get; set; }
        public DateTime issuetime { get; set; }
    }
}
