using MyNet.ClientFrame.Help;
using MyNet.ClientFrame.Public;
using MyNet.Components;
using MyNet.Components.Mapper;
using MyNet.Components.Result;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Controls;
using MyNet.Components.WPF.Models;
using MyNet.Components.WPF.Windows;
using MyNet.Model.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyNet.ClientFrame.Models.Auth
{
    public class GroupDetailViewModel : GroupViewModel
    {
        [JsonIgnore]
        public bool IsNew
        {
            get { return string.IsNullOrEmpty(base.gp_id); }
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
            var url = ApiHelper.GetApiUrl(this.IsNew ? ApiKeys.AddGroup : ApiKeys.EditGroup);
            var rst = HttpHelper.GetResultByPost(url, (GroupViewModel)this, Context.Token);
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, this.IsNew ? OperationDesc.Add : OperationDesc.Edit, rst.msg);
                return;
            }
            MessageWindow.ShowMsg(MessageType.Info, this.IsNew ? OperationDesc.Add : OperationDesc.Edit, MsgConst.Msg_Succeed);
            //如果保存成功，则添加或更新缓存
            var group = OOMapper.Map<GroupViewModel, Group>(this);
            if (DataCacheHelper.AllGroups.ContainsKey(base.gp_code))
            {
                DataCacheHelper.AllGroups[base.gp_code] = group;
            }
            else
            {
                DataCacheHelper.AllGroups.Add(base.gp_code, group);
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
            TreeHelpHelper.OpenAllGroupsHelp(false, node =>
            {
                var tNode = (TreeViewData.TreeNode)node;
                base.gp_parent = tNode.Id;
            });
        }

        CmbItem _selectedIsSystem;
        [JsonIgnore]
        public CmbItem SelectedIsSystem
        {
            get { return _selectedIsSystem; }
            set
            {
                if (_selectedIsSystem != value)
                {
                    _selectedIsSystem = value;
                    if (_selectedIsSystem != null && base.gp_system != _selectedIsSystem.Id)
                    {
                        base.gp_system = _selectedIsSystem.Id;
                    }
                    base.RaisePropertyChanged("SelectedIsSystem");
                }
            }
        }
    }
}
