using OneCardSln.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OneCardSln.WebApi.Models.Card
{
    /// <summary>
    /// 消费vm
    /// </summary>
    public class PayViewModel
    {
        /// <summary>
        /// 身份证号
        /// </summary>
        [Required(ErrorMessageResourceName = "Idcard_Require", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string idcard { get; set; }

        /// <summary>
        /// 一卡通号码
        /// </summary>
        public string number { get; set; }

        /// <summary>
        /// 消费订单号
        /// </summary>
        [Required(ErrorMessageResourceName = "Order_Require", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string order { get; set; }
        /// <summary>
        /// 消费金额
        /// </summary>
        [Required(ErrorMessageResourceName = "Pay_Amount_Require", ErrorMessageResourceType = typeof(Resources.Resource))]
        public decimal amount { get; set; }
        /// <summary>
        /// 消费来源：手机端、电脑商城
        /// </summary>
        [MaxLength(255, ErrorMessageResourceName = "Pay_Src_Length", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string src { get; set; }
        /// <summary>
        /// 备注：手机端：deviceid，电脑商城：IP地址
        /// </summary>
        [MaxLength(255, ErrorMessageResourceName = "Remark_Length", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string remark { get; set; }

        /// <summary>
        /// 扣费优先级
        /// </summary>
        public MoneyEnum priority { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        [MaxLength(32, ErrorMessageResourceName = "Opt_Length", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string opt { get; set; }

    }
}