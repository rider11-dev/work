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
using MyNet.Components.Http;
using MyNet.ViewModel.Auth.User;
using MyNet.Components.Emit;
using MyNet.Components.Misc;
using MyNet.Components.WPF.Models;
using MyNet.Components.Validation;

namespace MyNet.Client.Models.Auth
{
    public partial class UserDetailViewModel : CheckableModel
    {
        public IUserDetailVM userdata { get; private set; }

        public UserDetailViewModel(bool needValidate = true) : base(needValidate)
        {
            userdata = DynamicModelBuilder.GetInstance<IUserDetailVM>(parent: typeof(BaseModel), ctorArgs: needValidate);
            userdata.ValidateMetadataType = typeof(UserDetailVM);
        }

        [JsonIgnore]
        [ValidateIgnore]
        public bool IsNew
        {
            get { return string.IsNullOrEmpty(userdata.user_id); }
        }

        [JsonIgnore]
        [ValidateIgnore]
        public BaseWindow Window { get; set; }

        [JsonIgnore]
        private DelegateCommand _saveCmd;
        [ValidateIgnore]
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
        [ValidateIgnore]
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
                var tNode = node;
                user_group_name = tNode.Label;
                userdata.user_group = tNode.DataId;
            });
        }

        private void SaveAction(object parameter)
        {
            if (!this.IsValid)
            {
                MessageWindow.ShowMsg(MessageType.Warning, OperationDesc.Validate, this.Error);
                return;
            }
            var url = ApiUtils.GetApiUrl(this.IsNew ? ApiKeys.AddUsr : ApiKeys.EditUsr);
            var rst = HttpUtils.PostResult(url, this.userdata, ClientContext.Token);
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

    //扩展属性
    public partial class UserDetailViewModel : ICopyable
    {
        string _user_group_name;
        public string user_group_name
        {
            get { return _user_group_name; }
            set
            {
                if (_user_group_name != value)
                {
                    _user_group_name = value;
                    base.RaisePropertyChanged("user_group_name");
                }
            }
        }

        public void CopyTo(object target)
        {
            if (target == null)
            {
                return;
            }
            var vmUsr = (UserDetailViewModel)target;
            this.userdata.CopyTo(vmUsr.userdata);
            vmUsr.user_group_name = this.user_group_name;
        }
    }
}
