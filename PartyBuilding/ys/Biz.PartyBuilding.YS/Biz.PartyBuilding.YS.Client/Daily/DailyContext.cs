using Biz.PartyBuilding.YS.Client.Daily.Models;
using MyNet.Components.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.PartyBuilding.YS.Client.Daily
{
    public class DailyContext : PartyBuildingContext
    {
        public static List<CmbItem> notice_types = new List<CmbItem>
        {
            new CmbItem{Id="1",Text="会议"},
        };

        public static List<CmbItem> notice_urgency = new List<CmbItem>
        {
            new CmbItem{Id="1",Text="高"},
            new CmbItem{Id="2",Text="中"},
            new CmbItem{Id="3",Text="低"},
        };

        public static List<CmbItem> notice_state = new List<CmbItem>
        {
            new CmbItem{Id="1",Text="编辑"},
            new CmbItem{Id="2",Text="已发布"}
        };

        public static List<NoticeEntity> notices = new List<NoticeEntity>
        {
            new NoticeEntity{
                title="会议通知",type="会议",content="请各乡镇级党组织，于2016年11月20日上午8点，到县委政府会议室开会。收到请回复！",
                attach="会议主题.docx",
                urgency="高",issue_party="曹县县委组织部",issue_time="2016-11-07 16:16",receive_party="全部",state="已发布",need_reply="是",reply_expire_time="2016-11-19 17:30",
                view_details=new List<ViewDetail>{
                    new ViewDetail{party="曹县王集镇党组织",time="2016-11-07 17:20",isviewed="是"},
                    new ViewDetail{party="曹城办事处党组织",time="2016-11-10 08:30",isviewed="是"},
                },
                reply_details=new List<ReplyDetail>{
                    new ReplyDetail{party="曹县王集镇党组织",time="2016-11-07 17:20",reply_content="收到，保证准时参会！",isreplied="是"},
                    new ReplyDetail{party="曹城办事处党组织",time="",reply_content="",isreplied="否"},
                }
            },
            new NoticeEntity{
                title="会议通知",type="会议",content="各乡镇党组织深入学习一号文件，并贯彻落实！",
                attach="一号文件.docx",
                urgency="中",issue_party="曹县县委组织部",issue_time="2016-10-07",receive_party="全部",state="编辑",need_reply="否",reply_expire_time="",
                view_details=new List<ViewDetail>{
                    new ViewDetail{party="曹县王集镇党组织",time="2016-11-07 17:20",isviewed="是"},
                    new ViewDetail{party="曹城办事处党组织",time="",isviewed="否"},
                }
            },
        };

        public static List<NoticeEntity> notices_rec
        {
            get
            {
                return notices.Where(n => n.receive_party == "全部" && n.state == "已发布").ToList();
            }
        }
    }
}
