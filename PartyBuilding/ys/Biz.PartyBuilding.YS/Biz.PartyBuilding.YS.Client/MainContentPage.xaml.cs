using Biz.PartyBuilding.YS.Client.Contacts;
using Biz.PartyBuilding.YS.Client.Daily;
using Biz.PartyBuilding.YS.Client.Models;
using MyNet.Client.Pages;
using MyNet.Client.Public;
using MyNet.Components;
using MyNet.Components.Result;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            if (contact == null)
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
            GetTasks();
            GetInfos();
        }

        void GetTasks()
        {
            var rst = HttpHelper.GetResultByGet(ApiHelper.GetApiUrl(PartyBuildingApiKeys.TaskGet, PartyBuildingApiKeys.Key_ApiProvider_Party));
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, OperationDesc.Search, rst.msg);
                return;
            }
            if (rst.data != null && rst.data.tasks != null)
            {
                var tasks = JsonConvert.DeserializeObject<IEnumerable<TaskModel>>(((JArray)rst.data.tasks).ToString());
                if (tasks != null && tasks.Count() > 0)
                {
                    tasks = tasks.Where(t => t.complete_state == "未领" || t.complete_state == "已领未完成");
                }
                dgTasks.ItemsSource = tasks;
            }
        }

        void GetInfos()
        {
            var rst = HttpHelper.GetResultByGet(ApiHelper.GetApiUrl(PartyBuildingApiKeys.InfoGet, PartyBuildingApiKeys.Key_ApiProvider_Party));
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, OperationDesc.Search, rst.msg);
                return;
            }
            if (rst.data != null && rst.data.infos != null)
            {
                var infos = JsonConvert.DeserializeObject<IEnumerable<InfoModel>>(((JArray)rst.data.infos).ToString());
                if (infos != null && infos.Count() > 0)
                {
                    dgNotice.ItemsSource = infos.Where(i => i.state == "已发布");
                }
            }
        }
    }
}
