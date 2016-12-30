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
using MyNet.Components.WPF.Models;
using MyNet.Model.Auth;
using MyNet.Components.Mapper;

using System.Windows.Input;
using MyNet.Components.WPF.Controls;
using MyNet.Client.Help;
using MyNet.Model.Dto.Auth;
using MyNet.Components.Http;
using MyNet.Model.Interface.Auth;
using MyNet.ViewModel.Auth.Permission;
using MyNet.Components.Emit;
using MyNet.Components.Misc;
using MyNet.Components.Validation;

namespace MyNet.Client.Models.Auth
{
    public partial class PermDetailViewModel : CheckableModel
    {
        public IPermDetailVM permdata { get; private set; }

        public PermDetailViewModel(bool needValidate = true) : base(needValidate)
        {
            permdata = DynamicModelBuilder.GetInstance<IPermDetailVM>(parent: typeof(BaseModel), ctorArgs: needValidate);
            permdata.ValidateMetadataType = typeof(PermDetailVM);
        }

        [JsonIgnore]
        public bool IsNew
        {
            get { return string.IsNullOrEmpty(permdata.per_id); }
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
            var url = ApiUtils.GetApiUrl(this.IsNew ? ApiKeys.AddPer : ApiKeys.EditPer);
            var rst = HttpUtils.PostResult(url, permdata, ClientContext.Token);
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, this.IsNew ? OperationDesc.Add : OperationDesc.Edit, rst.msg);
                return;
            }
            MessageWindow.ShowMsg(MessageType.Info, this.IsNew ? OperationDesc.Add : OperationDesc.Edit, MsgConst.Msg_Succeed);
            //如果保存的是功能权限，则添加或更新缓存
            if (permdata.per_type == PermType.Func.ToString())
            {
                var funcPermDto = OOMapper.Map<IPermDetailVM, PermissionCacheDto>(permdata);
                if (DataCacheUtils.AllFuncs.ContainsKey(permdata.per_code))
                {
                    DataCacheUtils.AllFuncs[permdata.per_code] = funcPermDto;
                }
                else
                {
                    DataCacheUtils.AllFuncs.Add(permdata.per_code, funcPermDto);
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
        [ValidateIgnore]
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
                this.per_parent_name = tNode.Label;
                permdata.per_parent = tNode.Id;
            });
        }

        CmbItem _selectedPermType;
        [JsonIgnore]
        [ValidateIgnore]
        public CmbItem SelectedPermType
        {
            get { return _selectedPermType; }
            set
            {
                if (_selectedPermType != value)
                {
                    _selectedPermType = value;
                    if (_selectedPermType != null && permdata.per_type != _selectedPermType.Id)
                    {
                        permdata.per_type = _selectedPermType.Id;
                    }
                    base.RaisePropertyChanged("SelectedPermType");
                }
            }
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
                    if (_selectedIsSystem != null && permdata.per_type != _selectedIsSystem.Id)
                    {
                        permdata.per_system = Convert.ToBoolean(_selectedIsSystem.Id);
                    }
                    base.RaisePropertyChanged("SelectedIsSystem");
                }
            }
        }
    }

    public partial class PermDetailViewModel : ICopyable
    {
        string _per_type_name;
        public string per_type_name
        {
            get { return _per_type_name; }
            set
            {
                if (_per_type_name != value)
                {
                    _per_type_name = value;
                    base.RaisePropertyChanged("per_type_name");
                }
            }
        }

        string _per_parent_name;
        public string per_parent_name
        {
            get { return _per_parent_name; }
            set
            {
                if (_per_parent_name != value)
                {
                    _per_parent_name = value;
                    base.RaisePropertyChanged("per_parent_name");
                }
            }
        }

        public void CopyTo(object target)
        {
            if (target == null)
            {
                return;
            }
            var vmPerm = (PermDetailViewModel)target;
            this.permdata.CopyTo(vmPerm.permdata);
            vmPerm.per_type_name = this.per_type_name;
            vmPerm.per_parent_name = this.per_parent_name;
        }
    }
}
