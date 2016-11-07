using Biz.PartyBuilding.YS.Client.Daily.Models;
using MyNet.Client.Pages;
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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            new DetailTaskWindow().ShowDialog();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            new DetailTaskWindow().ShowDialog();


        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnIssue_Click(object sender, RoutedEventArgs e)
        {
            dg.ItemsSource = null;
            var task = PartyBuildingContext.task_dispatch.Where(t => t.state == "编辑").FirstOrDefault();
            if (task != null)
            {
                task.state = "已发布";
            }
            dg.ItemsSource = PartyBuildingContext.task_dispatch;
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
    }
}
