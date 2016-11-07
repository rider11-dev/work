using Biz.PartyBuilding.YS.Client.Daily.Models;
using Biz.PartyBuilding.YS.Client.PartyOrg.Models;
using Microsoft.Win32;
using MyNet.Components.Extensions;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Models;
using MyNet.Components.WPF.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Biz.PartyBuilding.YS.Client.Daily
{
    /// <summary>
    /// DetailReplyNoticeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DetailReplyNoticeWindow : BaseWindow
    {
        NoticeEntity model;

        private DetailReplyNoticeWindow()
        {
            InitializeComponent();
        }

        public DetailReplyNoticeWindow(NoticeEntity vm = null)
            : this()
        {
            if (vm == null)
            {
                return;
            }
            model = DailyContext.notices.Where(n => n.title == vm.title).FirstOrDefault();
            if (model == null)
            {
                model = new NoticeEntity();
            }
            this.DataContext = model;

            if (vm != null)
            {
                base.Title = "通知回复——" + vm.title;
            }
            var reply = vm.reply_details.Where(r => r.party == "曹城办事处党组织").FirstOrDefault();
            if (reply != null)
            {
                txtReply.Text = reply.reply_content;
            }
        }

        protected override void BeforeClose()
        {
            base.BeforeClose();

            model.reply_details.Add(new ReplyDetail
            {
                party = "曹城办事处党组织",
                time = DateTime.Now.ToString("yyyy-MM-dd hh24:mm:ss"),
                reply_content = txtReply.Text,
                isreplied = "是"
            });
        }

    }
}
