using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneCardSln.Model
{
    /// <summary>
    /// 操作记录
    /// </summary>
    public class CardRecord
    {
        public String id { get; set; }
        public String number { get; set; }
        public String username { get; set; }
        public String idcard { get; set; }
        public String type { get; set; }
        public DateTime time { get; set; }
        public String @operator { get; set; }
    }
}