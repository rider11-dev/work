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
        public String id { get; set; }
        public String number { get; set; }
        public String idcard { get; set; }
        public Decimal agoallmoney { get; set; }
        public Decimal agogovmoney { get; set; }
        public Decimal agomymoney { get; set; }
        public Decimal changegov { get; set; }
        public Decimal changemy { get; set; }
        public Decimal nowgovmoney { get; set; }
        public Decimal nowmymoney { get; set; }
        public Decimal nowallmoney { get; set; }
        public String type { get; set; }
        public DateTime time { get; set; }
        public String rid { get; set; }

        /// <summary>
        /// 消费来源
        /// </summary>
        public String src { get; set; }
        /// <summary>
        /// 消费订单号
        /// </summary>
        public String order { get; set; }
        /// <summary>
        /// 消费备注：手机端：deviceid，电脑商城：IP地址
        /// </summary>
        public String remark { get; set; }
    }
}