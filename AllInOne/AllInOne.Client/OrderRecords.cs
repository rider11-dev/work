using MyNet.Components.Extensions;
using MyNet.Components.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class OrderObject
{
    public string code { get; set; }
    public Order[] rows { get; set; }
    public int total { get; set; }
    public string msg { get; set; }
}

public class Order : CheckableModel
{
    public string order_id { get; set; }
    /// <summary>
    /// 订单号
    /// </summary>
    public string order_sn { get; set; }
    /// <summary>
    /// 店铺
    /// </summary>
    public string store_name { get; set; }
    /// <summary>
    /// 买家
    /// </summary>
    public string buyer_name { get; set; }
    /// <summary>
    /// 下单时间
    /// </summary>
    public string add_time { get; set; }
    /// <summary>
    /// 付款时间
    /// </summary>
    public string payment_time { get; set; }
    /// <summary>
    /// 订单完成时间
    /// </summary>
    public string finnshed_time { get; set; }
    /// <summary>
    /// 商品总价
    /// </summary>
    public string goods_amount { get; set; }
    /// <summary>
    /// 订单总价
    /// </summary>
    public string order_amount { get; set; }
    /// <summary>
    /// 订单状态
    /// </summary>
    public string order_state { get; set; }
    /// <summary>
    /// 退款状态
    /// </summary>
    public string refund_state { get; set; }
    /// <summary>
    /// 退款金额
    /// </summary>
    public string refund_amount { get; set; }
    /// <summary>
    /// 消费方式
    /// </summary>
    public string door_service { get; set; }
    /// <summary>
    /// 支付现金
    /// </summary>
    public string down_payment { get; set; }
    /// <summary>
    /// 订单状态描述
    /// </summary>
    public string state_desc { get; set; }
    /// <summary>
    /// 支付方式
    /// </summary>
    public string payment_name { get; set; }
    public Order_Goods[] extend_order_goods { get; set; }

    public int goods_count
    {
        get
        {
            return extend_order_goods.IsEmpty() ? 0 : extend_order_goods.Length;
        }
    }

    public string goods_names
    {
        get
        {
            return extend_order_goods.IsEmpty() ? "" : string.Join(",", extend_order_goods.Select(og => og.goods_name));
        }
    }
}

/// <summary>
/// 订单信息扩展
/// </summary>
public class Order_Common
{
    public string reciver_name { get; set; }
    public Reciver reciver_info { get; set; }
    public string reciver_province_id { get; set; }
}

public class Reciver
{
    public string address { get; set; }
    public string phone { get; set; }
}

public class Order_Goods
{
    public string rec_id { get; set; }
    public string order_id { get; set; }
    public string goods_id { get; set; }
    public string goods_name { get; set; }
}
