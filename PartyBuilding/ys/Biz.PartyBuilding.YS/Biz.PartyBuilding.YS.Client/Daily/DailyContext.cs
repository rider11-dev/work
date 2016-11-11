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
                }
            },
            new NoticeEntity{
                title="会议通知",type="会议",content="各乡镇党组织做好扶贫工作！",
                attach="",
                urgency="中",issue_party="曹县县委组织部",issue_time="2016-10-07",receive_party="全部",state="编辑",need_reply="否",reply_expire_time="",
                view_details=new List<ViewDetail>{
                    new ViewDetail{party="曹县王集镇党组织",time="2016-11-07 17:20",isviewed="是"},
                }
            },
            new NoticeEntity{
                title="会议通知",type="会议",content="各乡镇党组织深入乡村基层，了解农民收入情况！",
                attach="",
                urgency="中",issue_party="曹县县委组织部",issue_time="2016-10-07",receive_party="全部",state="编辑",need_reply="否",reply_expire_time="",
                view_details=new List<ViewDetail>{
                    new ViewDetail{party="曹县王集镇党组织",time="2016-11-07 17:20",isviewed="是"},
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

        public static List<InfoEntity> infos = new List<InfoEntity>
        {
           new InfoEntity{title="双十一购物攻略",
               content="看到很多朋友忙着为双十一大抢购做准备了，数字君作为一个小网购者，也都参加过几年的双十一，在这里有些小经验和大家分享一下，如果有什么不对，欢迎指导。",
               attach="双十一购物攻略.docx",issue_time="",party="曹县王集镇党组织",state="编辑"},
           new InfoEntity{title="大风警告",
               content="区内已出现或即将出现6级（蒲福风级，下同）或以上大风警告性的信息。通常，以气象电报、明愈广播或悬挂风球等方式发布。",
               attach="",issue_time="2016-11-01",party="曹县县委组织部",state="已发布"},
        };

        public static List<InfoEntity> infos_rec
        {
            get
            {
                return infos.Where(i => i.state == "已发布").ToList();
            }
        }

        public static List<CmbItem> party_act_types = new List<CmbItem>
        {
            new CmbItem{Id="1",Text="支部党员大会"},
            new CmbItem{Id="2",Text="支部委员会"},
            new CmbItem{Id="3",Text="党小组会"},
            new CmbItem{Id="4",Text="党课"},
        };

        public static List<PartyActRecord> party_acts = new List<PartyActRecord>
        {
            new PartyActRecord{party="曹县县委组织部",type="党课",theme="保持先进性，深入学习党课",time="2015-10-12",place="组织部党员活动室",
                host="李长江",recorder="王小明",cnt_yd="6",cnt_sd="4",cnt_qj="2",cnt_qx="0",cnt_chry="李长江，王雪青，赵爱国，王小明",
                content="通过这段时间校党课培训班的学习，对党的理论知识和党的光荣历史有了非常深刻的认识，对党的性质、宗旨、方针、政策、路线有了进一步地了解，对提高自己对党的认识有非常大的帮助，有利地提高了在思想上对党的认识，增强了自己的党性，坚定了一定要加入中国共产党的决心和信心。更深刻地对党在新时代新背景下所面临的挑战与机遇以及党在新时期的发展方向，在内心树立了共产主义的信念。同时，也意识到党组织性纪律性之强，入党的条件之高，发现自己身上尚有不足，距离真正的党员还有一定的差距，但一定会不断努力，提高自身的修养，以党员的标准来要求自己，让自己早日达到党员的条件，会以实际行动争取早日加入中国共产党。",
                attaches=new List<Attach>{
                     new Attach{att_name="党课学习照片.jpg",att_size="13KB"}
                }
            },
            new PartyActRecord{party="曹城办事处党组织",type="支部党员大会",theme=@"1、进一步学习学院“创先争优”活动推进会精神，温习王院长在会上的讲话;
　　2、集体学习《党章》关于党员标准的知识;
　　3、开展民主评议党员学习会;
　　4、学习党员发展流程，筹备11月12日召开信息工程系直属党支部党员发展会。",time="2015-01-09",place="组织部党员活动室",
                                        host="迟明霞",recorder="李宗良",cnt_yd="4",cnt_sd="4",cnt_qj="0",cnt_qx="0",cnt_chry="迟明霞，吴建明，赵新东，李宗良",
                content=@"　一、全系党员进一步学习10月9日召开的学院“创先争优”活动推进会的文件精神，再次温习王院长在会上的重要讲话。此次创先争优活动需贯穿落实党的十七大和十七届三中、四中全会精神，以邓小平理论和“三个代表”重要思想为指导，深入学习实践科学发展观;紧密结合我院“初创、民办、高职”院情，全面服务于学院的各项中心工作，进一步推进学院、系部各项工作的科学发展;切实领会学院基层党组织和党员在创先争优活动中的“五个好”、“五带头”的要求和标准。信息工程系直属党支部要结合本部门工作实际，进一步丰富主题内涵，把系部教师党员吸引到创先争优活动中来，加强信息工程系直属党支部建设，加强信息工程系广大师生党员的学习和教育，充分发挥信息工程系直属党支部的战斗堡垒作用和广大党员的先锋模范作用。
　　二、集体学习《党章》关于党员标准的知识，重温党章，进一步强化认识我党的行动指南、社会主义初级阶段的基本路线、纪律要求等基础知识，学习讨论中党员老师们均表示以实际行动履行好党员应尽的义务，保持党员的先进性，始终将以一名优秀共产党员的标准严格要求自己。
　　三、信息工程系直属党支部书记简冬秋老师，组织与会党员老师共同学习了《中共四川城市职业学院委员会关于开展2009-2010学年民主评议党员的通知》、《中共四川城市职业学院委员会关于民主评议党员暂行办法》，明确本次民主评议党员所坚持的原则、内容、方法和程序，并结合系部实际情况与到会党员老师共同拟定本系民主评议安排，强调通过本次民主评议和组织考察，旨在激励发挥党员的先锋模范作用，表彰优秀党员、共同帮助不合格党员，提高党员素质，增强党组织的凝聚力和战斗力，进一步推进系部、学院党组织建设。
　　四、认真做好本学期的党员发展教育工作是我们党组织的常规工作和基础工作，是党的组织建设的一个重要内容。为了让我系党员老师进一步深入到党员苗子发掘、培养、发展的工作中，各位党员老师学习了党员发展流程，并结合我系实际情况，分工筹备11月12日的信息工程系直属党支部党员发展会。
 
　　党支部党员大会会议记录2
　　时间：20xx年4月15日
　　地点：西北工业大学长安校区动力与能源学院楼225室
　　主持：王光豪记录：樊炫腾
　　出席：动力与能源学院09级党支部第二支部全体党员及预备党员
　　列席：王光豪(支部书记)缺席：0人
　　2012年4月15日星期日21时00分，动力与能源学院09级党支部第二支部全体党员及预备党员关于讨论发展本学期第三批预备党员会议在动力与能源学院学院楼225室隆重举行。本次会议的重点是关于发展预备党员的事宜以及本学年党支部的活动安排督促工作，会议得到了党支部领导的高度重视，动力与能源学院09级党支部第二支部全体党员及预备党员均出席了本次会议。本次会议由程明树同志主持。
　　发展党员，向党支部输入新成员，增加新的活力，历来是党支部重视的工作之一。首先，5名发展对象依次上台宣读了入党志愿书，介绍了自己的个人情况、家庭情况以及自己强烈希望加入中国共产党的愿望和申请。每个人都充分表达了自己的态度和决心。然后，发展对象的各自培养联系人又详细地介绍了发展对象在日常学习生活中学习、工作、生活及思想等方面表现出来的先进性，与会党员也对自己熟悉的发展对象的认识进行了各个方面的客观的评价。这一环节为是否吸收这5名发展对象成为中国共产党的一员作了一定的评断参考。紧接着，正式党员根据发展预备党员的各项标准对5名发展对象进行了一一核对，并以通过举手投票的方式进行了选举。
　　最终，经过各方面的综合评价认定，5名发展对象得到了大家的充分认定，成功地发展为预备党员。他们分别是陈勇同志(7291班)、谢佳骏同志(7291班)、杨弋同志(7292班)、叶虎同志(7292班)、闫波同志(7392班)。之后王光豪同志对此次会议作了简短总结，以及安排了党支部的近期活动。最后他主持安排落实了各个专业的党支部活动和党建博客的事宜。祝贺以上5名同志顺利地加入中国共产党。
　　在与会成员的阵阵掌声中，本次会议圆满地落下了帷幕。
　　补习班党支部会议纪要
　　11月25号，莘县一中补习班党支部在西校小会议室召开了全体党员大会，会议主要是讨论张玉霞老师的转正和王安平老师的纳新问题。本次会议特邀纪检书记张书显同志参加。会议有补习班党支部组织委员贾俊奎同志主持。会议首先讨论并通过了张玉霞老师的转正申请，接着参会人员根据王安平老师的申请，讨论并通过了其为中共预备党员。紧接着支部宣传委员黄守民同志带领张玉霞，王安平两位老师面对党旗进行了庄严的入党宣誓。
　　接下来补习班党支部书记陈朝银发表了讲话。首先他对张玉霞被转为正式党员和王安平被确定为预备党员表示衷心祝贺，并对他们提出了希望和要求，希望他们再接再厉，切实起到先锋带头作用。接着他带领全体与会人员重温了党章的有关内容。最后他讲了补习班目前所面临的问题并对全体党员提出要求，要求全体党员在工作中要起模范带头作用，要任劳任怨，要经得起考验，少一些抱怨，多一些沟通，为补习班的建设和一中的发展多做贡献
　　最后张书记作了重要讲话。他说党员会也是交流会，希望党员要多交流，相互学习，并多学习一些党章知识。最后他对全体党员提出了几点具体要求：
　　要增强党员的岗位意识，要爱岗敬业
　　要增强学习意识，加强党的知识和业务知识的学习;
　　要增强修养意识，加强包括政治、思想、道德、法规、文明行为的修养的学习;
　　至此，大会圆满结束。",
                attaches=new List<Attach>{
                     new Attach{att_name="支部党员大会.jpg",att_size="40KB"},
                     new Attach{att_name="会议记录2015-01-09.docx",att_size="17.1KB"},
                }
            },
        };
    }
}
