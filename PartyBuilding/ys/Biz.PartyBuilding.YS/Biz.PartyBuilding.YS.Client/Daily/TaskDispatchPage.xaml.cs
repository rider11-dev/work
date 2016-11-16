using Biz.PartyBuilding.YS.Client.Daily.Models;
using Biz.PartyBuilding.YS.Client.Models;
using MyNet.Client.Models;
using MyNet.Client.Pages;
using MyNet.Client.Public;
using MyNet.Components;
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
    /// TaskDispatchPage.xaml 的交互逻辑
    /// </summary>
    public partial class TaskDispatchPage : BasePage
    {
        public TaskDispatchPage()
        {
            InitializeComponent();

            CmbModel model = cmbTaskPriority.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.task_priority);

            model = cmbTaskState.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.task_state);

            model = cmbTaskCompleteState.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.task_complete_state);
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            new DetailTaskWindow().ShowDialog();
            GetTasks();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var model = (dg.ItemsSource as IEnumerable<TaskModel>).FirstOrDefault();
            new DetailTaskWindow(model).ShowDialog();


        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnIssue_Click(object sender, RoutedEventArgs e)
        {
            var model = (dg.ItemsSource as IEnumerable<TaskModel>).FirstOrDefault();
            if(model==null)
            {
                return;
            }
            var rst = HttpHelper.GetResultByPost(ApiHelper.GetApiUrl(PartyBuildingApiKeys.TaskRelease, PartyBuildingApiKeys.Key_ApiProvider_Party), new { id = model.id });
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, "任务发布", rst.msg);
                return;
            }
            GetTasks();
        }

        private void btnCanel_Click(object sender, RoutedEventArgs e)
        {
            dg.ItemsSource = null;
            var task = PartyBuildingContext.task_dispatch.Where(t => t.state == "已发布").FirstOrDefault();
            if (task != null)
            {
                task.state = "已取消";
            }
            dg.ItemsSource = PartyBuildingContext.task_dispatch;
        }

        private void btnCompleteDetail_Click(object sender, RoutedEventArgs e)
        {
            new DetailTaskWindow().ShowDialog();
        }

        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            GetTasks();
        }

        private void GetTasks()
        {
            var rst = HttpHelper.GetResultByGet(ApiHelper.GetApiUrl(PartyBuildingApiKeys.TaskGet,PartyBuildingApiKeys.Key_ApiProvider_Party));
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, OperationDesc.Search, rst.msg);
                return;
            }
            if (rst.data != null&&rst.data.tasks!=null)
            {
                var tasks = JsonConvert.DeserializeObject<IEnumerable<TaskModel>>(((JArray)rst.data.tasks).ToString());

                dg.ItemsSource = tasks;
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            GetTasks();
        }
    }
}
