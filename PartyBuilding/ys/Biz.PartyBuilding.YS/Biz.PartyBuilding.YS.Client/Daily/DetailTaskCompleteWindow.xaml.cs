using Biz.PartyBuilding.YS.Client.Daily.Models;
using Biz.PartyBuilding.YS.Client.Models;
using Biz.PartyBuilding.YS.Client.PartyOrg.Models;
using Microsoft.Win32;
using MyNet.Client.Public;
using MyNet.Components;
using MyNet.Components.Extensions;
using MyNet.Components.Http;
using MyNet.Components.Result;
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
    /// DetailTaskCompleteWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DetailTaskCompleteWindow : BaseWindow
    {
        TaskModel _model = null;
        private DetailTaskCompleteWindow()
        {
            InitializeComponent();
            this._model = this.DataContext as TaskModel;
        }

        public DetailTaskCompleteWindow(TaskModel vm = null)
            : this()
        {
            if (vm != null)
            {
                vm.CopyTo(_model);
            }
        }

        ICommand _uploadAttachCmd;
        public ICommand UploadAttachCmd
        {
            get
            {
                if (_uploadAttachCmd == null)
                {
                    _uploadAttachCmd = new DelegateCommand(UploadAttachAction);
                }
                return _uploadAttachCmd;
            }
        }

        void UploadAttachAction(object parameter)
        {
            OpenFileDialog dia = new OpenFileDialog();
            var rst = dia.ShowDialog();
            if (rst == null || (bool)rst == false)
            {
                return;
            }

            ctlHelpButton.Content = dia.FileName;

        }

        ICommand _completeCmd;
        public ICommand CompleteCmd
        {
            get
            {
                if (_completeCmd == null)
                {
                    _completeCmd = new DelegateCommand(CompleteAction);
                }
                return _completeCmd;
            }
        }

        void CompleteAction(object parameter)
        {
            string url = ApiUtils.GetApiUrl(PartyBuildingApiKeys.TaskComplete, PartyBuildingApiKeys.Key_ApiProvider_Party);
            var rst = HttpUtils.PostResult(url, new { id = _model.id, progress = _model.progress });
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, "任务完成", rst.msg);
                return;
            }
            this.DialogResult = true;

            base.Close();
        }
    }
}
