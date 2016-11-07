using Biz.PartyBuilding.YS.Client.Daily.Models;
using Biz.PartyBuilding.YS.Client.PartyOrg.Models;
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
    /// DetailTaskWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DetailTaskWindow : BaseWindow
    {
        private DetailTaskWindow()
        {
            InitializeComponent();


            CmbModel model = cmbTaskPriority.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.task_priority);

            model = cmbTaskState.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.task_state);
        }

        public DetailTaskWindow(Org2NewViewModel vm = null)
            : this()
        {
        }

        ICommand _viewAttachCmd;
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
            var taskCompleteDetail = (TaskCompleteDetail)parameter;
            if (taskCompleteDetail == null || string.IsNullOrEmpty(taskCompleteDetail.attach))
            {
                return;
            }

            var fullPath = "";
            if (FileExtension.GetFileFullPath(AppDomain.CurrentDomain.BaseDirectory, taskCompleteDetail.attach, out fullPath))
            {
                Process.Start(fullPath);

            }
        }
    }
}
