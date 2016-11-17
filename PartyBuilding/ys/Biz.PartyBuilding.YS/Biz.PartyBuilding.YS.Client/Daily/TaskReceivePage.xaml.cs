using Biz.PartyBuilding.YS.Client.Daily.Models;
using Biz.PartyBuilding.YS.Client.Models;
using Microsoft.Win32;
using MyNet.Client.Pages;
using MyNet.Client.Public;
using MyNet.Components;
using MyNet.Components.Extensions;
using MyNet.Components.Result;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Models;
using MyNet.Components.WPF.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    /// TaskReceivePage.xaml 的交互逻辑
    /// </summary>
    public partial class TaskReceivePage : BasePage
    {
        public TaskReceivePage()
        {
            InitializeComponent();

            CmbModel model = cmbTaskPriority.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.task_priority);

            model = cmbTaskCompleteState.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.task_complete_state);

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


        private void btnCompleteDetail_Click(object sender, RoutedEventArgs e)
        {
            new DetailTaskWindow().ShowDialog();
        }

        private void btnReceive_Click(object sender, RoutedEventArgs e)
        {
            var task = dg.SelectedItem as TaskModel;
            if (task == null)
            {
                return;
            }
            string url = ApiHelper.GetApiUrl(PartyBuildingApiKeys.TaskTake, PartyBuildingApiKeys.Key_ApiProvider_Party);
            var rst = HttpHelper.GetResultByPost(url, new { id = task.id });
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, OperationDesc.Search, rst.msg);
                return;
            }
            GetTasks();
        }

        private void btnComplete_Click(object sender, RoutedEventArgs e)
        {
            var task = dg.SelectedItem as TaskModel;
            if (task == null)
            {
                return;
            }

            new DetailTaskCompleteWindow(task).ShowDialog();

            GetTasks();
        }

        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            GetTasks();
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
                    dg.ItemsSource = tasks.Where(t => t.state != "编辑");
                }
            }
        }
    }
}
