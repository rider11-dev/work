using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneCardSln.Model
{
    /// <summary>
    /// 消费实体类
    /// </summary>
    public class PayEntity
    {
        /// <summary>
        /// 身份证号
        /// </summary>
        public string idcard { get; set; }

        /// <summary>
        /// 一卡通号码
        /// </summary>
        public string number { get; set; }

        /// <summary>
        /// 消费金额
        /// </summary>
        public decimal payamount { get; set; }

        /// <summary>
        /// 消费来源：手机端、电脑商城
        /// </summary>
        public string src { get; set; }
        /// <summary>
        /// 备注：手机端：deviceid，电脑商城：IP地址
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 消费订单号
        /// </summary>
        public string order { get; set; }

        public EnumPayPrior payprior { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string @operator { get; set; }
    }
}