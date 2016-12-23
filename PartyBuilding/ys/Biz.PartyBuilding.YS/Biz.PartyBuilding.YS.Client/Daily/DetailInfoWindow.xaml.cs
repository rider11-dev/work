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
    /// DetailInfoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DetailInfoWindow : BaseWindow
    {
        InfoOptType _type;
        InfoModel _model;
        private DetailInfoWindow()
        {
            InitializeComponent();

            _model = this.DataContext as InfoModel;
        }

        public DetailInfoWindow(InfoOptType type, InfoModel vm = null)
            : this()
        {
            _type = type;
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
            if (this._type == InfoOptType.View)
            {
                base.CloseCmd.Execute(null);
                return;
            }

            //保存
            _model.party = "曹县县委组织部";
            var url = ApiUtils.GetApiUrl(PartyBuildingApiKeys.InfoSave, PartyBuildingApiKeys.Key_ApiProvider_Party);
            var rst = HttpUtils.PostResult(url, _model);
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, OperationDesc.Save, rst.msg);
                return;
            }
            this.DialogResult = true;
            base.CloseCmd.Execute(null);
        }
    }

    public enum InfoOptType
    {
        InsertOrUpdate,
        View
    }
}
