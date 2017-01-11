using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Model
{
    public class CardAccount
    {
        public string id { get; set; }
        public string number { get; set; }
        public string username { get; set; }
        public string idcard { get; set; }
        public decimal govmoney { get; set; }
        public decimal mymoney { get; set; }
        public string state { get; set; }
        public string @operator { get; set; }
        public string phone { get; set; }
        public string remark { get; set; }
    }
}
