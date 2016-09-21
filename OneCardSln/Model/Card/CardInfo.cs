using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneCardSln.Model
{
    /// <summary>
    /// 一卡通信息
    /// </summary>
    public class CardInfo
    {
        public String id { get; set; }
        public String number { get; set; }
        public String idcard { get; set; }
        public String username { get; set; }
        public Decimal govmoney { get; set; }
        public Decimal mymoney { get; set; }
        public String state { get; set; }
        public DateTime updatetime { get; set; }
        public String @operator { get; set; }
        public String remark { get; set; }
        public String phone { get; set; }

        public override string ToString()
        {
            return string.Format("id:{0},number:{1},idcard:{2},username:{3},govmoney:{4},mymoney:{5},state:{6},updatetime:{7},remark:{8},phone:{9}",
                id, number, idcard, username, govmoney, mymoney, state, updatetime.ToString("yyyy-MM-dd HH:mm:ss"), remark, phone);
        }
    }
}