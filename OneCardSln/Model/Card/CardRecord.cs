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
        public String rec_id { get; set; }
        public String rec_number { get; set; }
        public String rec_username { get; set; }
        public String rec_idcard { get; set; }
        public String rec_type { get; set; }
        public DateTime rec_time { get; set; }
        public String rec_operator { get; set; }
    }
}