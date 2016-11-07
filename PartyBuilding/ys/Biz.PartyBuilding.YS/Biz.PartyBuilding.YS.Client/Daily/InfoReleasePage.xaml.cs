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
    /// InfoReleasePage.xaml 的交互逻辑
    /// </summary>
    public partial class InfoReleasePage : BasePage
    {
        public InfoReleasePage()
        {
            InitializeComponent();

            CmbModel model = cmbInfoState.DataContext as CmbModel;
            model.Bind(DailyContext.notice_state);
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

        ICommand _viewCmd_Rec;
        ICommand _viewCmd_Sent;
        ICommand _addCmd_Sent;
        ICommand _editCmd_Sent;
        ICommand _issueCmd_Sent;
        ICommand _viewAttachCmd;

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
            var notice = (InfoEntity)parameter;
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
            var notice = DailyContext.infos.Where(n => n.state == "编辑").FirstOrDefault();
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
            dg_Sent.ItemsSource = DailyContext.infos;

            dg_Rec.ItemsSource = null;
            dg_Rec.ItemsSource = DailyContext.infos_rec;
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
        void ViewCmd_RecAction(object parameter)
        {
            ShowDetailWindow();
        }

        void ShowDetailWindow()
        {
            new DetailInfoWindow().ShowDialog();
        }
    }
}
