using Newtonsoft.Json;
using MyNet.Components;
using MyNet.Components.Result;
using MyNet.Components.WPF.Command;
using OneCardSln.OneCardClient.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNet.Components.WPF.Windows;
using MyNet.Components.WPF.Models;

namespace OneCardSln.OneCardClient.Models.Auth
{
    public class PermDetailViewModel : PermViewModel
    {
        [JsonIgnore]
        public bool IsNew
        {
            get { return string.IsNullOrEmpty(base.per_id); }
        }

        [JsonIgnore]
        public BaseWindow Window { get; set; }

        [JsonIgnore]
        private DelegateCommand _saveCmd;
        [JsonIgnore]
        public DelegateCommand SaveCmd
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

        private void SaveAction(object parameter)
        {
            if (!this.IsValid)
            {
                MessageWindow.ShowMsg(MessageType.Warning, OperationDesc.Validate, this.Error);
                return;
            }
            var url = ApiHelper.GetApiUrl(this.IsNew ? ApiKeys.AddPer : ApiKeys.EditPer);
            var rst = HttpHelper.GetResultByPost(url, (PermViewModel)this, Context.Token);
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, this.IsNew ? OperationDesc.Add : OperationDesc.Edit, rst.msg);
                return;
            }
            MessageWindow.ShowMsg(MessageType.Info, this.IsNew ? OperationDesc.Add : OperationDesc.Edit, MsgConst.Msg_Succeed);
            if (Window != null)
            {
                Window.DialogResult = true;
                Window.CloseCmd.Execute(null);
            }
        }

        CmbItem _selectedPermType;
        [JsonIgnore]
        public CmbItem SelectedPermType
        {
            get { return _selectedPermType; }
            set
            {
                if (_selectedPermType != value)
                {
                    _selectedPermType = value;
                    if (_selectedPermType != null && base.per_type != _selectedPermType.Id)
                    {
                        base.per_type = _selectedPermType.Id;
                    }
                    base.RaisePropertyChanged("SelectedPermType");
                }
            }
        }
    }
}
