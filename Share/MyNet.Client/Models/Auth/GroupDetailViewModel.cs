using MyNet.Client.Help;
using MyNet.Client.Public;
using MyNet.Components;
using MyNet.Components.Emit;
using MyNet.Components.Extensions;
using MyNet.Components.Http;
using MyNet.Components.Mapper;
using MyNet.Components.Misc;
using MyNet.Components.Result;
using MyNet.Components.Validation;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Controls;
using MyNet.Components.WPF.Models;
using MyNet.Components.WPF.Windows;
using MyNet.Model.Auth;
using MyNet.ViewModel.Auth.Group;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyNet.Client.Models.Auth
{
    public partial class GroupDetailViewModel : CheckableModel
    {
        public IGroupVM groupdata { get; private set; }

        public GroupDetailViewModel(bool needValidate = true) : base(needValidate)
        {
            groupdata = DynamicModelBuilder.GetInstance<IGroupVM>(parent: typeof(BaseModel), ctorArgs: needValidate);
            groupdata.ValidateMetadataType = typeof(GroupVM);
        }
        [JsonIgnore]
        [ValidateIgnore]
        public bool IsNew
        {
            get { return string.IsNullOrEmpty(groupdata.gp_id); }
        }

        [JsonIgnore]
        public BaseWindow Window { get; set; }

        [JsonIgnore]
        private DelegateCommand _saveCmd;
        [JsonIgnore]
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

        private void SaveAction(object parameter)
        {
            if (!this.IsValid)
            {
                MessageWindow.ShowMsg(MessageType.Warning, OperationDesc.Validate, this.Error);
                return;
            }
            var url = ApiUtils.GetApiUrl(this.IsNew ? ApiKeys.AddGroup : ApiKeys.EditGroup);
            var rst = HttpUtils.PostResult(url, groupdata, ClientContext.Token);
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, this.IsNew ? OperationDesc.Add : OperationDesc.Edit, rst.msg);
                return;
            }
            MessageWindow.ShowMsg(MessageType.Info, this.IsNew ? OperationDesc.Add : OperationDesc.Edit, MsgConst.Msg_Succeed);
            //如果保存成功，则添加或更新缓存
            var group = OOMapper.Map<IGroupVM, Group>(groupdata);
            if (DataCacheUtils.AllGroups.ContainsKey(groupdata.gp_code))
            {
                DataCacheUtils.AllGroups[groupdata.gp_code] = group;
            }
            else
            {
                DataCacheUtils.AllGroups.Add(groupdata.gp_code, group);
            }
            if (Window != null)
            {
                Window.DialogResult = true;
                Window.CloseCmd.Execute(null);
            }
        }


        [JsonIgnore]
        private ICommand _groupParentHelpCmd;
        [JsonIgnore]
        [ValidateIgnore]
        public ICommand GroupParentHelpCmd
        {
            get
            {
                if (_groupParentHelpCmd == null)
                {
                    _groupParentHelpCmd = new DelegateCommand(OpenGroupParentHelp);
                }
                return _groupParentHelpCmd;
            }
        }

        private void OpenGroupParentHelp(object parameter)
        {
            TreeHelper.OpenAllGroupsHelp(false, node =>
            {
                var tNode = (TreeViewData.TreeNode)node;
                gp_parent_name = tNode.Label;
                groupdata.gp_parent = tNode.Id;
            });
        }

        CmbItem _selectedIsSystem;
        [JsonIgnore]
        [ValidateIgnore]
        public CmbItem SelectedIsSystem
        {
            get { return _selectedIsSystem; }
            set
            {
                if (_selectedIsSystem != value)
                {
                    _selectedIsSystem = value;
                    if (_selectedIsSystem != null)
                    {
                        groupdata.gp_system = Convert.ToBoolean(_selectedIsSystem.Id);
                    }
                    base.RaisePropertyChanged("SelectedIsSystem");
                }
            }
        }
    }

    public partial class GroupDetailViewModel : ICopyable
    {
        private string _gp_parent_name;
        public string gp_parent_name
        {
            get { return _gp_parent_name; }
            set
            {
                if (_gp_parent_name != value)
                {
                    _gp_parent_name = value;
                    if (_gp_parent_name.IsEmpty())
                    {
                        groupdata.gp_parent = "";
                    }
                    base.RaisePropertyChanged("gp_parent_name");
                }
            }
        }

        public void CopyTo(object target)
        {
            if (target == null)
            {
                return;
            }
            var vmGroup = (GroupDetailViewModel)target;
            groupdata.CopyTo(vmGroup.groupdata);
            vmGroup.gp_parent_name = this.gp_parent_name;
        }
    }
}
