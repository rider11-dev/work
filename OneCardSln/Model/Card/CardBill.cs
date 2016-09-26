using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneCardSln.Model
{
    /// <summary>
    /// 一卡通流水记录
    /// </summary>
    public class CardBill
    {
        public String bill_id { get; set; }
        public String bill_number { get; set; }
        public String bill_idcard { get; set; }
        public Decimal bill_agoall { get; set; }
        public Decimal bill_agogov { get; set; }
        public Decimal bill_agomy { get; set; }
        public Decimal bill_changegov { get; set; }
        public Decimal bill_changemy { get; set; }
        public Decimal bill_nowgov { get; set; }
        public Decimal bill_nowmy { get; set; }
        public Decimal bill_nowall { get; set; }
        public String bill_type { get; set; }
        public DateTime bill_time { get; set; }
        /// <summary>
        /// 操作记录id
        /// </summary>
        public String bill_record { get; set; }

        /// <summary>
        /// 消费来源
        /// </summary>
        public String bill_src { get; set; }
        /// <summary>
        /// 消费订单号
        /// </summary>
        public String bill_order { get; set; }
        /// <summary>
        /// 消费备注：手机端：deviceid，电脑商城：IP地址
        /// </summary>
        public String bill_remark { get; set; }
    }
}