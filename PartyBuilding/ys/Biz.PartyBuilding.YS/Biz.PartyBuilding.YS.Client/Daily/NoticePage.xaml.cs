using Biz.PartyBuilding.YS.Client.Daily.Models;
using MyNet.Client.Pages;
using MyNet.Components.Extensions;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Biz.PartyBuilding.YS.Client.Daily
{
    /// <summary>
    /// NoticePage.xaml 的交互逻辑
    /// </summary>
    public partial class NoticePage : BasePage
    {
        public NoticePage()
        {
            InitializeComponent();

            CmbModel model = cmbNoticePriority_Sent.DataContext as CmbModel;
            model.Bind(DailyContext.notice_urgency);

            model = cmbNoticeState_Sent.DataContext as CmbModel;
            model.Bind(DailyContext.notice_state);

            model = cmbNoticeType_Sent.DataContext as CmbModel;
            model.Bind(DailyContext.notice_types);

            model = cmbNoticePriority_Rec.DataContext as CmbModel;
            model.Bind(DailyContext.notice_urgency);

            model = cmbNoticeType_Rec.DataContext as CmbModel;
            model.Bind(DailyContext.notice_types);

            model = cmbNotice_IsReplied.DataContext as CmbModel;
            model.Bind(DailyContext.CmbItemsYesNo);

        }

        ICommand _searchCmd;
        public ICommand SearchCmd
        {
            get
            {
                if (_searchCmd == null)
                {
                    _searchCmd = new DelegateCommand(SearchAction);
                }

                return _searchCmd;
            }
        }

        void SearchAction(object parameter)
        {

        }

        ICommand _viewDetailsCmd;
        ICommand _replyDetailsCmd;
        ICommand _viewCmd_Rec;
        ICommand _replyCmd;
        ICommand _viewCmd_Sent;
        ICommand _addCmd_Sent;
        ICommand _editCmd_Sent;
        ICommand _issueCmd_Sent;
        ICommand _viewAttachCmd;

        public ICommand ViewDetailsCmd
        {
            get
            {
                if (_viewDetailsCmd == null)
                {
                    _viewDetailsCmd = new DelegateCommand(ViewDetailsAction);
                }

                return _viewDetailsCmd;
            }
        }

        public ICommand ReplyDetailsCmd
        {
            get
            {
                if (_replyDetailsCmd == null)
                {
                    _replyDetailsCmd = new DelegateCommand(ReplyDetailsAction);
                }

                return _replyDetailsCmd;
            }
        }

        public ICommand ViewCmd_Rec
        {
            get
            {
                if (_viewCmd_Rec == null)
                {
                    _viewCmd_Rec = new DelegateCommand(ViewCmd_RecAction);
                }

                return _viewCmd_Rec;
            }
        }

        public ICommand ReplyCmd
        {
            get
            {
                if (_replyCmd == null)
                {
                    _replyCmd = new DelegateCommand(ReplyAction);
                }

                return _replyCmd;
            }
        }

        public ICommand ViewCmd_Sent
        {
            get
            {
                if (_viewCmd_Sent == null)
                {
                    _viewCmd_Sent = new DelegateCommand(View_SentAction);
                }

                return _viewCmd_Sent;
            }
        }
        public ICommand AddCmd_Sent
        {
            get
            {
                if (_addCmd_Sent == null)
                {
                    _addCmd_Sent = new DelegateCommand(Add_SentAction);
                }

                return _addCmd_Sent;
            }
        }
        public ICommand EditCmd_Sent
        {
            get
            {
                if (_editCmd_Sent == null)
                {
                    _editCmd_Sent = new DelegateCommand(Edit_SentAction);
                }

                return _editCmd_Sent;
            }
        }
        public ICommand IssueCmd_Sent
        {
            get
            {
                if (_issueCmd_Sent == null)
                {
                    _issueCmd_Sent = new DelegateCommand(Issue_SentAction);
                }

                return _issueCmd_Sent;
            }
        }
        public ICommand ViewAttachCmd
        {
            get
            {
                if (_viewAttachCmd == null)
                {
                    _viewAttachCmd = new DelegateCommand(ViewAttachAction);
                }

                return _viewAttachCmd;
            }
        }
        void ViewAttachAction(object parameter)
        {
            var notice = (NoticeEntity)parameter;
            if (notice == null || string.IsNullOrEmpty(notice.attach))
            {
                return;
            }

            var fullPath = "";
            if (FileExtension.GetFileFullPath(AppDomain.CurrentDomain.BaseDirectory, notice.attach, out fullPath))
            {
                Process.Start(fullPath);
            }
        }
        void Issue_SentAction(object parameter)
        {
            var notice = DailyContext.notices.Where(n => n.state == "编辑").FirstOrDefault();
            if (notice == null)
            {
                return;
            }
            notice.state = "已发布";
            Reload();
        }

        private void Reload()
        {
            dg_Sent.ItemsSource = null;
            dg_Sent.ItemsSource = DailyContext.notices;

            dg_Rec.ItemsSource = null;
            dg_Rec.ItemsSource = DailyContext.notices_rec;
        }
        void Edit_SentAction(object parameter)
        {
            ShowDetailWindow();
        }
        void Add_SentAction(object parameter)
        {
            ShowDetailWindow();
        }
        void View_SentAction(object parameter)
        {
            ShowDetailWindow();
        }

        void ReplyAction(object parameter)
        {
            var notice = (NoticeEntity)parameter;
            if (notice == null)
            {
                return;
            }
            new DetailReplyNoticeWindow(notice).ShowDialog();
            Reload();
        }
        void ViewDetailsAction(object parameter)
        {
            var notice = (NoticeEntity)parameter;
            if (notice == null)
            {
                return;
            }
            new DetailViewDetailsWindow(notice).ShowDialog();
        }
        void ReplyDetailsAction(object parameter)
        {
            var notice = (NoticeEntity)parameter;
            if (notice == null)
            {
                return;
            }
            new DetailReplyDetailsWindow(notice).ShowDialog();
        }

        void ViewCmd_RecAction(object parameter)
        {
            ShowDetailWindow();
        }

        void ShowDetailWindow()
        {
            new DetailNoticeWindow().ShowDialog();
        }

        private void btnExport_Rec_Click(object sender, RoutedEventArgs e)
        {
            MyNet.Components.WPF.Misc.ExcelHelper.Export(dg_Rec, "已收通知");
        }

        private void btnExport_Sent_Click(object sender, RoutedEventArgs e)
        {
            MyNet.Components.WPF.Misc.ExcelHelper.Export(dg_Sent, "已发通知");
        }
    }
}
