using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyNet.Components.Extensions;

namespace MyNet.Model.Card
{
    /// <summary>
    /// 一卡通信息
    /// </summary>
    public class CardInfo
    {
        public String card_id { get; set; }
        public String card_number { get; set; }
        public String card_idcard { get; set; }
        public String card_username { get; set; }
        public String card_phone { get; set; }
        public Decimal card_govmoney { get; set; }
        public Decimal card_mymoney { get; set; }
        public string card_state { get; set; }

        public CardState State
        {
            get
            {
                return card_state.ToEnum<CardState>();
            }
        }
        public String card_creator { get; set; }
        public DateTime card_createtime { get; set; }
        public String card_modifier { get; set; }
        public DateTime card_modifytime { get; set; }
        public String card_remark { get; set; }

        public override string ToString()
        {
            return string.Format("card_id:{0},card_number:{1},card_idcard:{2},card_username:{3},card_phone:{4},card_govmoney:{5},card_state:{6},card_creator:{7},card_createtime:{8},card_modifier:{9},card_modifytime:{10},card_remark:{10}",
                card_id, card_number, card_idcard, card_username, card_govmoney, card_mymoney, State.GetDescription(), card_creator, card_createtime.ToString("yyyy-MM-dd HH:mm:ss"), card_modifier, card_modifytime, card_remark);
        }
    }
}