using Biz.PartyBuilding.YS.Client.Daily.Models;
using Biz.PartyBuilding.YS.Client.Models;
using Biz.PartyBuilding.YS.Client.PartyOrg.Models;
using MyNet.Client.Models;
using MyNet.Client.Public;
using MyNet.Components;
using MyNet.Components.Extensions;
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
    /// DetailTaskWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DetailTaskWindow : BaseWindow
    {
        TaskModel _model;

        public bool IsNew
        {
            get { return string.IsNullOrEmpty(_model.id); }
        }
        private DetailTaskWindow()
        {
            InitializeComponent();

            _model = this.DataContext as TaskModel;
        }

        public DetailTaskWindow(TaskModel vm = null)
            : this()
        {
            if(vm!=null)
            {
                vm.CopyTo(_model);
            }
        }

        ICommand _saveCmd;
        public ICommand SaveCmd
        {
            get
            {
                if (_saveCmd == null)
                {
                    _saveCmd = new DelegateCommand(SaveAction);
                }
                return _saveCmd;
            }
        }

        void SaveAction(object parameter)
        {
            //var taskCompleteDetail = (TaskCompleteDetail)parameter;
            //if (taskCompleteDetail == null || string.IsNullOrEmpty(taskCompleteDetail.attach))
            //{
            //    return;
            //}

            //var fullPath = "";
            //if (FileExtension.GetFileFullPath(AppDomain.CurrentDomain.BaseDirectory, taskCompleteDetail.attach, out fullPath))
            //{
            //    Process.Start(fullPath);

            //}
            var url = ApiHelper.GetApiUrl(PartyBuildingApiKeys.TaskSave,PartyBuildingApiKeys.Key_ApiProvider_Party);
            var rst = HttpHelper.GetResultByPost(url, _model);
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, this.IsNew ? OperationDesc.Add : OperationDesc.Edit, rst.msg);
                return;
            }
            this.DialogResult = true;
            base.CloseCmd.Execute(null);
        }
    }
}
