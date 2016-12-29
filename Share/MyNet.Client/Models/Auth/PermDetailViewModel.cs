﻿using Newtonsoft.Json;
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
using MyNet.Components.WPF.Models;
using MyNet.Model.Auth;
using MyNet.Components.Mapper;

using System.Windows.Input;
using MyNet.Components.WPF.Controls;
using MyNet.Client.Help;
using MyNet.Model.Dto.Auth;
using MyNet.Components.Http;
using MyNet.Model.Interface.Auth;

namespace MyNet.Client.Models.Auth
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
            var url = ApiUtils.GetApiUrl(this.IsNew ? ApiKeys.AddPer : ApiKeys.EditPer);
            var rst = HttpUtils.PostResult(url, (PermViewModel)this, ClientContext.Token);
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, this.IsNew ? OperationDesc.Add : OperationDesc.Edit, rst.msg);
                return;
            }
            MessageWindow.ShowMsg(MessageType.Info, this.IsNew ? OperationDesc.Add : OperationDesc.Edit, MsgConst.Msg_Succeed);
            //如果保存的是功能权限，则添加或更新缓存
            if (base.per_type == PermType.Func.ToString())
            {
                var funcPermDto = OOMapper.Map<PermViewModel, PermissionCacheDto>(this);
                if (DataCacheUtils.AllFuncs.ContainsKey(base.per_code))
                {
                    DataCacheUtils.AllFuncs[base.per_code] = funcPermDto;
                }
                else
                {
                    DataCacheUtils.AllFuncs.Add(base.per_code, funcPermDto);
                }
            }
            if (Window != null)
            {
                Window.DialogResult = true;
                Window.CloseCmd.Execute(null);
            }
        }


        [JsonIgnore]
        private ICommand _permParentHelpCmd;
        [JsonIgnore]
        public ICommand PermParentHelpCmd
        {
            get
            {
                if (_permParentHelpCmd == null)
                {
                    _permParentHelpCmd = new DelegateCommand(OpenPermParentHelp);
                }
                return _permParentHelpCmd;
            }
        }

        private void OpenPermParentHelp(object parameter)
        {
            TreeHelper.OpenAllFuncsHelp(false, node =>
            {
                var tNode = (TreeViewData.TreeNode)node;
                base.per_parent_name = tNode.Label;
                base.per_parent = tNode.Id;
            });
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
                    if (_selectedIsSystem != null && base.per_type != _selectedIsSystem.Id)
                    {
                        base.per_system = _selectedIsSystem.Id;
                    }
                    base.RaisePropertyChanged("SelectedIsSystem");
                }
            }
        }
    }
}
