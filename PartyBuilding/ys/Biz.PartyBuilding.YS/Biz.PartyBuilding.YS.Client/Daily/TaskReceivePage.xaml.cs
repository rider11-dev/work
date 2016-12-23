using Biz.PartyBuilding.YS.Client.Daily.Models;
using Biz.PartyBuilding.YS.Client.Models;
using Microsoft.Win32;
using MyNet.Client.Pages;
using MyNet.Client.Public;
using MyNet.Components;
using MyNet.Components.Extensions;
using MyNet.Components.Http;
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

            btnAll.Click += (o, e) =>
            {
                Search();
            };
            btnSearch.Click += (o, e) =>
            {
                Search(false);
            };
        }

        private void Search(bool all = true)
        {
            var rst = HttpUtils.GetResult(ApiUtils.GetApiUrl(PartyBuildingApiKeys.TaskGet, PartyBuildingApiKeys.Key_ApiProvider_Party));
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, OperationDesc.Search, rst.msg);
                return;
            }
            dg.ItemsSource = null;
            if (rst.data != null && rst.data.tasks != null)
            {
                var tasks = JsonConvert.DeserializeObject<IEnumerable<TaskModel>>(((JArray)rst.data.tasks).ToString());
                if (tasks.IsEmpty())
                {
                    return;
                }
                tasks = tasks.Where(t => t.state != "编辑");
                if (all)
                {
                    dg.ItemsSource = tasks;
                }
                else
                {
                    if (txtTaskName.Text.IsNotEmpty())
                    {
                        tasks = tasks.Where(t => t.name.Contains(txtTaskName.Text));
                    }
                    if (cmbTaskPriority.SelectedItem != null)
                    {
                        tasks = tasks.Where(t => t.priority == (cmbTaskPriority.SelectedValue as CmbItem).Text);
                    }

                    if (cmbTaskCompleteState.SelectedItem != null)
                    {
                        tasks = tasks.Where(t => t.complete_state == (cmbTaskCompleteState.SelectedValue as CmbItem).Text);
                    }
                    var date = dtExpire_Begin.Text;
                    if (date.IsNotEmpty())
                    {
                        tasks = tasks.Where(t => !(string.Compare(t.expire_time, date) < 0));
                    }
                    date = dtExpire_End.Text;
                    if (date.IsNotEmpty())
                    {
                        tasks = tasks.Where(t => !(string.Compare(t.expire_time, date) > 0));
                    }

                    dg.ItemsSource = tasks;
                }
            }
        }


        private void btnCompleteDetail_Click(object sender, RoutedEventArgs e)
        {
            var task = dg.SelectedItem as TaskModel;

            if (task != null)
            {
                new DetailTaskWindow(task).ShowDialog();
            }
        }

        private void btnReceive_Click(object sender, RoutedEventArgs e)
        {
            var task = dg.SelectedItem as TaskModel;
            if (task == null)
            {
                return;
            }
            string url = ApiUtils.GetApiUrl(PartyBuildingApiKeys.TaskTake, PartyBuildingApiKeys.Key_ApiProvider_Party);
            var rst = HttpUtils.PostResult(url, new { id = task.id });
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, OperationDesc.Search, rst.msg);
                return;
            }
            Search();
        }

        private void btnComplete_Click(object sender, RoutedEventArgs e)
        {
            var task = dg.SelectedItem as TaskModel;
            if (task == null)
            {
                return;
            }

            new DetailTaskCompleteWindow(task).ShowDialog();

            Search();
        }

        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            MyNet.Components.WPF.Misc.ExcelHelper.Export(dg, "我的任务");
        }
    }
}
