using Biz.PartyBuilding.YS.Client.Daily.Models;
using Microsoft.Win32;
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
            dg.ItemsSource = null;
            var task = PartyBuildingContext.tasks_ccbsc_receive.Where(t => t.complete_detail.comp_state == "未领").FirstOrDefault();
            if (task != null)
            {
                task.complete_detail.comp_state = "已领未完成";
            }
            dg.ItemsSource = PartyBuildingContext.tasks_ccbsc_receive;
        }

        private void btnComplete_Click(object sender, RoutedEventArgs e)
        {
            new DetailTaskCompleteWindow().ShowDialog();

            dg.ItemsSource = null;
            dg.ItemsSource = PartyBuildingContext.tasks_ccbsc_receive;
        }
    }
}
