using MyNet.Components.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.PartyBuilding.YS.Client.Evaluation
{
    public class EvaluationContext : PartyBuildingContext
    {
        public static List<Attach> upload_attaches = new List<Attach>
        {
            new Attach{att_name="开展党建工作述职评议工作汇报.docx",att_size="18.6KB"},
            new Attach{att_name="党委书记公开承诺书.docx",att_size="16.1KB"},
        };

        public static List<dynamic> check_details = new List<dynamic>
        {
            new {party="曹城办事处党组织",proj="开展述职评议",expire_time="2016-12-28",check_type="季度考核",upload_state="未上传",upload_time="",check_time="",check_result=""},
            new {party="曹县王集镇党组织",proj="开展书记公开承诺",expire_time="2015-11-28",check_type="月度考核",upload_state="已考核",upload_time="2015-11-20",check_time="2015-11-27",check_result="通过"},
        };

        public static List<CmbItem> seasons = new List<CmbItem>
        {
            new CmbItem{Id="1",Text="第一季度"},
            new CmbItem{Id="2",Text="第二季度"},
            new CmbItem{Id="3",Text="第三季度"},
            new CmbItem{Id="4",Text="第四季度"},
        };

        public static List<CmbItem> months = new List<CmbItem>
        {
            new CmbItem{Id="1",Text="1月"},
            new CmbItem{Id="2",Text="2月"},
            new CmbItem{Id="3",Text="3月"},
            new CmbItem{Id="4",Text="4月"},
            new CmbItem{Id="11",Text="5月"},
            new CmbItem{Id="12",Text="6月"},
            new CmbItem{Id="13",Text="7月"},
            new CmbItem{Id="14",Text="8月"},
            new CmbItem{Id="21",Text="9月"},
            new CmbItem{Id="22",Text="10月"},
            new CmbItem{Id="23",Text="11月"},
            new CmbItem{Id="24",Text="12月"},
        };

        public static List<dynamic> score_rank = new List<dynamic>
        {
            new{party="曹县县委组织部",score="100",rank=1},
            new{party="曹县王集镇党组织",score="90",rank=2},
            new{party="曹城办事处党组织",score="80",rank=3},
        };

        public static List<dynamic> score_check_result = new List<dynamic>
        {
            new{proj="开展述职评议",party="曹县县委组织部",result="通过",score="50",check_score="50"},
            new{proj="开展书记公开承诺",party="曹县县委组织部",result="通过",score="50",check_score="50"},

            new{proj="抓好基层党建工作年度考评",party="曹县王集镇党组织",result="好",score="50",check_score="50"},
            new{proj="党的群众路线教育实践活动",party="曹县王集镇党组织",result="较好",score="50",check_score="40"},

            new{proj="“强化两项职能”、争当“五个书记”活动",party="曹城办事处党组织",result="通过",score="50",check_score="50"},
            new{proj="党内激励关怀帮扶",party="曹城办事处党组织",result="一般",score="50",check_score="30"},

        };

    }
}
