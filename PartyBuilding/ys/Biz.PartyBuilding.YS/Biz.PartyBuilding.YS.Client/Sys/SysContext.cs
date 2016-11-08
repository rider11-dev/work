using Biz.PartyBuilding.YS.Client.Sys.Models;
using MyNet.Components.WPF.Controls;
using MyNet.Components.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.PartyBuilding.YS.Client.Sys
{
    public class SysContext : PartyBuildingContext
    {
        public static List<CmbItem> CmbItemsTimeType = new List<CmbItem>
        {
            new CmbItem{Id="1",Text="月末"},
            new CmbItem{Id="2",Text="季度月末"},
            new CmbItem{Id="2",Text="年度月末"},
        };

        public static List<CmbItem> CmbItemsPartyType = new List<CmbItem>
        {
            new CmbItem{Id="1",Text="党委"},
            new CmbItem{Id="2",Text="机关工委"},
            new CmbItem{Id="3",Text="基层党委"},
            new CmbItem{Id="4",Text="党总支部"},
            new CmbItem{Id="5",Text="党支部"}
        };

        public static List<EvaluateProject> projects = new List<EvaluateProject>
        {
            new EvaluateProject{id="01",name="乡镇党委"},
            new EvaluateProject{id="0101",name="落实党管职责",parent="01"},
            new EvaluateProject{id="010101",name="开展述职评议",parent="0101",score="100",
                remark="党委书记向县委和乡镇党委及所属党组织负责人进行述职，有述职报告，有述职评议考评方案，有会议记录及测评结果等资料",
                time_type="季度末月",time_day="30",party_type="所有单位",order="010101"
            },
            new EvaluateProject{id="010102",name="开展书记公开承诺",parent="0101",score="100",
                remark="公开承诺书内容公开公示，承诺事项逐一兑现落实；督导所属村、社区党组织书记开展公开承诺、兑现落实承诺事项，有台账资料。",
                time_type="年度末月",time_day="30",party_type="基层党委",order="010102"
            },
        };

        public static IList<TreeViewData.NodeViewModel> ParseProjectsTreeData(IEnumerable<EvaluateProject> projs)
        {
            if (projs == null || projs.Count() < 1)
            {
                return null;
            }

            List<TreeViewData.NodeViewModel> datas = new List<TreeViewData.NodeViewModel>();
            foreach (var pro in projs)
            {
                datas.Add(new TreeViewData.NodeViewModel { Id = pro.id, Label = pro.name, Parent = pro.parent, Order = pro.order, Data = pro, DataId = pro.id });
            }
            return datas;
        }

        public static Dictionary<string, List<string>> party_projects = new Dictionary<string, List<string>>
        {
            {"曹城办事处党组织",new List<string>{
                "01","0101","010101"
            }},
             {"曹县王集镇党组织",new List<string>{
                "01","0101","010101","010102"
            }},
        };

        public static List<CmbItem> channels = new List<CmbItem>
        {
            new CmbItem{Id="lzjs",Text="廉政建设"},
            new CmbItem{Id="llzd",Text="理论制度"},
            new CmbItem{Id="wsdx",Text="网上党校"},
            new CmbItem{Id="xcjy",Text="宣传教育"},
        };


        public static List<Article> articles = new List<Article>
        {
            new Article{channel_code="func_party_learn_cleangov",channel="廉政建设", title="压紧压实“两个责任” 推动党风廉政建设",
                content=@"为认真贯彻落实中央、省市委关于加强党风廉政建设的要求，鲅鱼圈区委组织部主动聚焦风险性、苗头性、倾向性问题，积极落实责任、开展教育、完善制度、强化管理，推动组织部门党风廉政建设取得新成效。
年初以来，区委组织部将加强党风廉政建设列入部机关建设重要议事日程。部领导率先垂范，认真履行“一岗双责”，在抓好部内全面工作的同时，特别注重抓好支部党风廉政建设工作，带头研究制定支部党风廉政建设相关制度，带头学习中央和省市委关于加强党风廉政建设的相关要求，带头严格遵守党员领导干部廉洁自律相关规范，切实为全体机关干部树起标杆。
区委组织部始终坚持以加强教育、事前预防为治本之策。今年以来，结合“两学一做”学习教育，区委组织部将党风廉政教育列入支部学习计划，通过组织全体党员干部深入学习《中国共产党党内监督条例》、《中国共产党纪律处分条例》等，使机关全体党员在加强纪律意识和法制观念上形成思想共识，自觉做到廉洁从政；结合“讲规矩、有纪律”专题，支部书记带头讲党课，通过分析讲解大量历史和现实的正反典型，使党员干部真正洗洗脑，出出汗，筑牢思想防线。
为加大从源头上预防和治理腐败的工作力度，区委组织部认真抓好重大事项报告、出差纪律要求、公务用车管理等相关制度的有效执行。在公务接待、公务用车、差旅费报销等方面，严格监督和把关，确保把各项制度不折不扣贯彻落实到位。通过严格的监督把关机制，形成用制度管人、用制度管事、用制度管物的管理机制，为防止和减少贪腐的发生起到了有效的防范作用。
区委组织部还将廉政提醒谈话作为支部书记与支部党员谈心谈话的重要内容。每逢重要节日、出差公务等关键时间节点，支部书记都会向支部党员传达廉洁自律要求，并结合支部建设实际和党员思想认识状况，与支部党员进行一对一谈心谈话，把好节日、差旅廉洁关，营造廉洁自律浓厚氛围。同时，强化日常监督，净化八小时外的生活圈，严禁支部党员参与带有赌博性质、沾染不良风气的娱乐活动。",
            state="已发布",issue_time="2016-11-01",attach="压紧压实“两个责任” 推动党风廉政建设.docx",clicks="45"},
            new Article{channel_code="func_party_learn_theory",channel="理论制度", title="2015党建工作制度",
                content="一是认真开展学习实践科学发展观活动。在全党开展深入学习实践科学发展观活动，是党的十七大做出的一项重大战略部署，也是今年我委机关党的建设的一项中心工作。要紧密结合工作实际，把活动的重点放在“实践”上，以开展学习实践活动为契机，引导广大党员干部牢固树立科学发展的理念，自觉运用科学发展观武装头脑，提高应对复杂经济形势的能力，积极有效地推动我市经济平稳较快发展。认真参加市里组织的系列活动，并结合我委工作实际，采取多种形式扎实开展学习实践活动。二是认真开展日常政治学习。充分发挥党支部集中学习、党小组学习、党员自学等多种形式的作用，保证政治学习的时间和质量，确保政治学习与业务工作同抓共促。三是积极组织支部书记和入党积极分子参加各种培训活动。力争通过培训，打牢支部书记的思想理论基础，不断提高其领导党建工作的能力素质；进一步端正入党积极分子入党动机，加深对党的认识，提高入党积极分子的政治素质，使其早日达到党组织的培养要求。",
                state="已发布",issue_time="2015-01-10",attach="2015党建工作制度.docx",clicks="10"},
            new Article{channel_code="func_party_learn_school",channel="网上党校", title="关于集中开展学习贯彻党的十八大精神轮训的通知",
                content="见附件",
                state="已发布",issue_time="2015-12-12",attach="关于集中开展学习贯彻党的十八大精神轮训的通知.docx",clicks="5"},
            new Article{channel_code="func_party_learn_pubedu",channel="宣传教育", title="党建宣传方案、党建工作思路及党建工作重点",content="见附件",state="编辑",issue_time="",attach="党建宣传方案、党建工作思路及党建工作重点.docx",clicks=""},
        };
    }
}
