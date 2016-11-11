using Biz.PartyBuilding.YS.Client.Contacts;
using Biz.PartyBuilding.YS.Client.Daily;
using MyNet.Client.Pages;
using MyNet.Components.WPF.Command;
using System;
using System.Collections.Generic;
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

namespace Biz.PartyBuilding.YS.Client
{
    /// <summary>
    /// MainContentPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainContentPage : BasePage
    {
        public MainContentPage()
        {
            InitializeComponent();
        }

        ICommand _chatCmd;
        public ICommand ChatCmd
        {
            get
            {
                if (_chatCmd == null)
                {
                    _chatCmd = new DelegateCommand(ChatAction);
                }

                return _chatCmd;
            }
        }

        void ChatAction(object parameter)
        {
            var contact = (dynamic)parameter;
            if(contact==null)
            {
                return;
            }
            try
            {
                new ChatWindow(contact.name).ShowDialog();
            }
            catch { }
        }

        ICommand _viewNoticeCmd;
        public ICommand ViewNoticeCmd
        {
            get
            {
                if (_viewNoticeCmd == null)
                {
                    _viewNoticeCmd = new DelegateCommand(ViewNoticeAction);
                }

                return _viewNoticeCmd;
            }
        }

        void ViewNoticeAction(object parameter)
        {
          
            try
            {
                new DetailNoticeWindow().ShowDialog();
            }
            catch { }
        }

        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            var tasks = PartyBuildingContext.tasks_ccbsc_receive.Where(t => t.complete_detail.comp_state == "未领" || t.complete_detail.comp_state == "已领未完成");
            dgTasks.ItemsSource = tasks;
        }
    }
}
