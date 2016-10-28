using Newtonsoft.Json;
using MyNet.Components;
using MyNet.Components.Result;
using MyNet.Components.WPF.Command;
using MyNet.Client.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNet.Components.WPF.Windows;
using System.Windows.Input;
using MyNet.Client.Help;
using MyNet.Components.WPF.Controls;

namespace MyNet.Client.Models.Auth
{
    public class UserDetailViewModel : UserViewModel
    {
        [JsonIgnore]
        public bool IsNew
        {
            get { return string.IsNullOrEmpty(base.user_id); }
        }

        [JsonIgnore]
        public BaseWindow Window { get; set; }

        [JsonIgnore]
        private DelegateCommand _saveCmd;
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

        [JsonIgnore]
        private ICommand _groupHelpCmd;
        [JsonIgnore]
        public ICommand GroupHelpCmd
        {
            get
            {
                if (_groupHelpCmd == null)
                {
                    _groupHelpCmd = new DelegateCommand(OpenGroupHelp);
                }
                return _groupHelpCmd;
            }
        }

        private void OpenGroupHelp(object parameter)
        {
            TreeHelper.OpenAllGroupsHelp(false, node =>
            {
                var tNode = (TreeViewData.TreeNode)node;
                base.user_group_name = tNode.Label;
                base.user_group = tNode.DataId;
            });
        }

        private void SaveAction(object parameter)
        {
            if (!this.IsValid)
            {
                MessageWindow.ShowMsg(MessageType.Warning, OperationDesc.Validate, this.Error);
                return;
            }
            var url = ApiHelper.GetApiUrl(this.IsNew ? ApiKeys.AddUsr : ApiKeys.EditUsr);
            var rst = HttpHelper.GetResultByPost(url, (UserViewModel)this, Context.Token);
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
    }
}
