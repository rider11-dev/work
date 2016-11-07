using Biz.PartyBuilding.YS.Client.Daily.Models;
using Biz.PartyBuilding.YS.Client.PartyOrg.Models;
using MyNet.Components.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.PartyBuilding.YS.Client
{
    public class PartyBuildingContext
    {
        public static List<OrgStrucViewModel> orgs = new List<OrgStrucViewModel>
        {
            new OrgStrucViewModel{org_code="cxxwzzb",org_name="曹县县委组织部",org_contacts="张老师",org_phone="13525008945",org_addr="县委前街1号"},
            new OrgStrucViewModel{org_code="cxccbsc",org_name="曹城办事处党组织",org_parent_name="曹县县委组织部",org_contacts="王晓辉",org_phone="05301234567",org_addr="曹城办事处"}
        };

        public static List<Org2NewViewModel> org2news = new List<Org2NewViewModel>
        {
            new Org2NewViewModel{comp_name="长江科技发展有限公司",mem_count_dy=10,emp_count=67,is_dzz_establish="是",dzz_establish_type="单独建立",zbsj_name="王长江",zbsj_sex="男",zbsj_age=50,zbsj_xl="大专",zbsj_joinin_time="1996-10-12",has_atc_place="是",atc_place_area="50"},
            new Org2NewViewModel{comp_name="曹县一中",mem_count_dy=20,emp_count=43,is_dzz_establish="是",dzz_establish_type="联合建立",zbsj_name="付守国",zbsj_sex="男",zbsj_age=56,zbsj_xl="本科",zbsj_joinin_time="1990-01-20",has_atc_place="是",atc_place_area="60"},
        };

        public static List<CmbItem> CmbItemsYesNo = new List<CmbItem>
        {
            new CmbItem{Id="yes",Text="是"},
            new CmbItem{Id="no",Text="否"},
        };

        public static List<CmbItem> CmbItemsEstablishType = new List<CmbItem>
        {
            new CmbItem{Id="alone",Text="单独建立"},
            new CmbItem{Id="union",Text="联合建立"},
        };

        public static List<CmbItem> CmbItemsSex = new List<CmbItem>
        {
            new CmbItem{Id="male",Text="男"},
            new CmbItem{Id="female",Text="女"},
        };

        public static List<CmbItem> CmbItemsXL = new List<CmbItem>
        {
            new CmbItem{Id="xx",Text="小学"},
            new CmbItem{Id="cz",Text="初中"},
            new CmbItem{Id="gz",Text="高中"},
            new CmbItem{Id="dx",Text="大学"},
            new CmbItem{Id="yjs",Text="研究生"},
            new CmbItem{Id="bs",Text="博士"},
        };

        public static List<CmbItem> CmbItemsDyType = new List<CmbItem>
        {
            new CmbItem{Id="zs",Text="正式党员"},
            new CmbItem{Id="yb",Text="预备党员"},
            new CmbItem{Id="jjfz",Text="入党积极分子"}
        };

        public static List<CmbItem> CmbItemsNation = new List<CmbItem>
        {
            new CmbItem{Id="1",Text="汉族"},
            new CmbItem{Id="2",Text="回族"},
            new CmbItem{Id="3",Text="苗族"},
            new CmbItem{Id="4",Text="维吾尔族"},
            new CmbItem{Id="5",Text="壮族"},
            new CmbItem{Id="6",Text="土家"},
            new CmbItem{Id="7",Text="高山族"},
            new CmbItem{Id="8",Text="傣族"},
            new CmbItem{Id="9",Text="羌族"},
        };

        /// <summary>
        /// 党内职务
        /// </summary>
        public static List<CmbItem> CmbItemsDnzw = new List<CmbItem>
        {
            new CmbItem{Id="1",Text="委员"},
            new CmbItem{Id="2",Text="常委"},
            new CmbItem{Id="3",Text="书记"},
            new CmbItem{Id="4",Text="副书记"}
        };

        /// <summary>
        /// 现工作岗位
        /// </summary>
        public static List<CmbItem> CmbItemsNowGzgw = new List<CmbItem>
        {
            new CmbItem{Id="1",Text="学生"},
            new CmbItem{Id="2",Text="企事业单位管理人员"},
            new CmbItem{Id="3",Text="企事业单位专业技术人员"},
            new CmbItem{Id="4",Text="工勤人员"},
            new CmbItem{Id="5",Text="退休人员"}
        };

        /// <summary>
        /// 政治面貌
        /// </summary>
        public static List<CmbItem> CmbItemsZzmm = new List<CmbItem>
        {
            new CmbItem{Id="1",Text="学生"},
            new CmbItem{Id="2",Text="企事业单位管理人员"},
            new CmbItem{Id="3",Text="企事业单位专业技术人员"},
            new CmbItem{Id="4",Text="工勤人员"},
            new CmbItem{Id="5",Text="退休人员"}
        };

        public static List<PartyMemberViewModel> partymembers = new List<PartyMemberViewModel>
        {
            new PartyMemberViewModel{
                type="正式党员",name="张小虎",sex="男",nation="汉族",party="曹县县委组织部",dnzw="委员",age="27",jg="山东曹县",
                join_in_time="2014-10-24",zz_time="2015-12-12",idcard="372924199912661212",xl="本科",
                remark="作风优良",phone="13789561234",now_gzgw="企事业单位专业技术人员",month_salary="4578",
                month_party_money="45"
            },
             new PartyMemberViewModel{
                type="正式党员",name="王文哲",sex="女",nation="回族",party="曹县县委组织部",dnzw="副书记",age="50",jg="山东曹县",
                join_in_time="1999-10-24",zz_time="2001-12-12",idcard="37292412345661212",xl="大专",
                remark="",phone="15989561234",now_gzgw="企事业单位管理人员",month_salary="6000",
                month_party_money="90"
            },
            new PartyMemberViewModel{
                type="入党积极分子",name="陈晓冉",sex="女",nation="汉族",party="曹城办事处党组织",dnzw="委员",age="35",jg="山东菏泽",
                join_in_time="2014-10-24",zz_time="2015-12-12",idcard="372924199912661212",xl="本科",
                remark="",phone="13789561234",now_gzgw="企事业单位专业技术人员",month_salary="3000",
                month_party_money="15"
            },
             new PartyMemberViewModel{
                type="预备党员",name="隋庆亮",sex="男",nation="壮族",party="曹城办事处党组织",dnzw="委员",age="25",jg="山东成武",
                join_in_time="2010-10-24",zz_time="2012-12-12",idcard="37292412345661212",xl="本科",
                remark="",phone="15989561234",now_gzgw="企事业单位管理人员",month_salary="3000",
                month_party_money="15"
            }
        };

        public static List<dynamic> Families = new List<dynamic>
        {
            new{cw="儿子",name="王建国",age="23",zzmm="群众",work_unit="设计院",zw="职员"},
            new{cw="妻子",name="程琳琳",age="49",zzmm="党员",work_unit="水利局",zw="职员"}
        };

        public static List<dynamic> DyPhones = new List<dynamic>
        {
            new{dy_name="张明",dy_phone="18754115622",dy_email="zhagnm@163.com",dy_party="曹县县委组织部"},
            new{dy_name="李欣然",dy_phone="18956565656",dy_email="lixr@126.com",dy_party="曹县县委组织部"},
            new{dy_name="王爱国",dy_phone="13451235689",dy_email="wangag@sina.com",dy_party="曹县县委组织部"},

            new{dy_name="隋长英",dy_phone="15005052323",dy_email="suicy@gmail.com",dy_party="曹城办事处党组织"},
            new{dy_name="吴玲新",dy_phone="13835353626",dy_email="wulx@126.com",dy_party="曹城办事处党组织"},
            new{dy_name="赵新民",dy_phone="14912457896",dy_email="zhaoxm@126.com",dy_party="曹城办事处党组织"},
        };

        /// <summary>
        /// 党费执行标准
        /// </summary>
        public static List<CmbItem> CmbItemsDfZxbz = new List<CmbItem>
        {
            new CmbItem{Id="1",Text="在职工"},
            new CmbItem{Id="2",Text="离退休党员"},
            new CmbItem{Id="3",Text="学生党员"},
        };

        public static List<dynamic> Df = new List<dynamic>
        {
             new{dy_name="张明",dy_party="曹县县委组织部",df_zxbz="在职工",df_base="3000",df_month=20,df_year_plan=240,df_year_actual=240,df_year="2015"},
             new{dy_name="吴玲",dy_party="曹县县委组织部",df_zxbz="离退休党员",df_base="2000",df_month=10,df_year_plan=120,df_year_actual=100,df_year="2016"},

              new{dy_name="赵明亮",dy_party="曹城办事处党组织",df_zxbz="在职工",df_base="4000",df_month=40,df_year_plan=480,df_year_actual=240,df_year="2015"},
             new{dy_name="吴玲",dy_party="曹城办事处党组织",df_zxbz="学生党员",df_base="0",df_month=2,df_year_plan=24,df_year_actual=22,df_year="2016"},
        };

        /// <summary>
        /// 干部管理
        /// </summary>
        public static List<dynamic> cadres = new List<dynamic>
        {
            new{name="张三梁",sex="男",age="40",nation="汉族",xl="大专",idcard="372924198955223456",zw="村委会主任",rz_time="2014-12-20",rz_jl="",village="陈庄",lz_time="",lz_reason=""},
            new{name="吴思清",sex="男",age="34",nation="回族",xl="高中",idcard="372924193455221111",zw="村会计",rz_time="2015-01-24",rz_jl="",village="田庄",lz_time="",lz_reason=""},
            new{name="李新明",sex="男",age="25",nation="汉族",xl="本科",idcard="372924197855225390",zw="组织委员",rz_time="2010-06-03",rz_jl="",village="陆湾",lz_time="",lz_reason=""},
            new{name="王玲",sex="女",age="57",nation="汉族",xl="本科",idcard="372924196012221212",zw="宣传委员",rz_time="2009-12-26",rz_jl="",village="刘集",lz_time="2016-08-08",lz_reason="退休"}
        };

        public static List<dynamic> work_news = new List<dynamic>
        {
            new{time="2015-10-12",content="做好2015年征兵工作宣传",progress="10月底",remark=""},
            new{time="2015-11-11",content="功能区建设、进展情况的宣传报道",progress="11月底",remark=""},
            new{time="2015-12-13",content="2015乡村文艺大巡演文艺调演",progress="12月底",remark=""},
        };

        public static List<dynamic> act_place = new List<dynamic>
        {
            new{party="曹城街道马山庄党组织",floor_area="500",levels="2",rooms="3",courtyard_area="400",location="曹城街道办事处长江西路南200米"},
        };

        public static List<TaskEntity> task_dispatch = new List<TaskEntity>
        {
            new TaskEntity{name="任务2016-11-01",priority="高",content="道路路况汇总统计",issue_time="2016-11-01",expire_time="2016-11-10",
                rec_party="曹城办事处党组织",progress="100%",state="已完成",issue_party="曹县县委组织部",
            complete_detail=new TaskCompleteDetail{comp_state="已完成"}},
            new TaskEntity{name="任务2016-05-16",priority="高",content="开展2016年人口普查工作",issue_time="2016-05-16",expire_time="2016-06-16",rec_party="全部",progress="50%",state="已发布",issue_party="曹县县委组织部", complete_detail=new TaskCompleteDetail{comp_state="已完成"}},
            new TaskEntity{name="任务2016-1-12",priority="中",content="召开党组织年度计划会议并提交总结报告",issue_time="2016-1-12",expire_time="2016-1-19",rec_party="曹城办事处党组织",progress="0%",state="已发布",issue_party="曹县县委组织部", complete_detail=new TaskCompleteDetail{comp_state="未领"}},
            new TaskEntity{name="任务2016-10-16",priority="中",content="做好2016年供暖准备工作",issue_time="",expire_time="2016-11-10",rec_party="曹城办事处党组织",progress="0%",state="编辑",issue_party="曹县县委组织部", complete_detail=new TaskCompleteDetail{comp_state=""}},
        };

        static List<TaskEntity> _tasks_receive;
        public static List<TaskEntity> tasks_ccbsc_receive
        {
            get
            {
                if (_tasks_receive == null)
                {
                    _tasks_receive = task_dispatch.Where(t => t.state != "编辑").ToList();
                }
                return _tasks_receive;
            }
        }

        public static List<TaskCompleteDetail> task_complete_detail = new List<TaskCompleteDetail>
        {
            new TaskCompleteDetail{party="曹县王集镇党组织",comp_state="未完成",comp_remark="",comp_time="",attach=""},
            new TaskCompleteDetail{party="曹城办事处党组织",comp_state="已完成",comp_remark="任务已完成，请检阅",comp_time="2016-06-10",attach="2016人口普查——曹城办事处.docx"}
        };

        public static List<CmbItem> task_priority = new List<CmbItem>
        {
            new CmbItem{Id="1",Text="高"},
            new CmbItem{Id="2",Text="中"},
            new CmbItem{Id="3",Text="低"},
        };

        public static List<CmbItem> task_state = new List<CmbItem>
        {
            new CmbItem{Id="1",Text="编辑"},
            new CmbItem{Id="2",Text="已发布"},
            new CmbItem{Id="3",Text="已完成"},
            new CmbItem{Id="4",Text="已取消"}
        };

        public static List<CmbItem> task_complete_state = new List<CmbItem>
        {
            new CmbItem{Id="1",Text="未领"},
            new CmbItem{Id="2",Text="已领未完成"},
            new CmbItem{Id="3",Text="已完成"}
        };
    }
}
