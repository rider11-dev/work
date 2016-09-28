using OneCardSln.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneCardSln.Service.Card.Models
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
        /// 消费订单号
        /// </summary>
        public string order { get; set; }
        /// <summary>
        /// 消费金额
        /// </summary>
        public decimal amount { get; set; }
        /// <summary>
        /// 消费来源：手机端、电脑商城
        /// </summary>
        public string src { get; set; }
        /// <summary>
        /// 备注：手机端：deviceid，电脑商城：IP地址
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 扣费优先级
        /// </summary>
        public MoneyEnum priority { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string opt { get; set; }

        public bool Check(out string msg)
        {
            bool rst = true;
            msg = "";
            if (string.IsNullOrEmpty(idcard))
            {
                rst = false;
                msg = "身份证号不能为空";
                return rst;
            }
            if (string.IsNullOrEmpty(order))
            {
                rst = false;
                msg = "订单号不能为空";
                return rst;
            }
            if (amount < 0)
            {
                rst = false;
                msg = "金额应大于0";
                return rst;
            }
            return rst;
        }
    }
}