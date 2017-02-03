using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MyNet.Components.Result;
using MyNet.Components.WPF.Controls;
using MyNet.Components.WPF.Models;
using MyNet.Client.Pages.Auth;
using MyNet.Client.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MyNet.Components.WPF.Windows;
using MyNet.Model.Auth;
using MyNet.Components.WPF.Command;
using MyNet.Client.Help;
using MyNet.Components.Http;
using MyNet.Components.Extensions;
using MyNet.ViewModel.Auth.Permission;

namespace MyNet.Client.Models.Auth
{
    public class PermMngViewModel : MngViewModel
    {
        public override Dictionary<string, ICommand> Commands
        {
            get
            {
                return new Dictionary<string, ICommand>
                {
                    {"Add",base.AddCmd},
                    {"Delete",base.DelCmd},
                    {"Edit",base.EditCmd}
                };
            }
        }

        private ICommand _permParentHelpCmd;
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
                Filter_PerParent_Name = tNode.Label;
                Filter_PerParent = tNode.Id;
            });
        }

        #region 基类命令对应动作重写
        protected override void AddAction(object parameter)
        {
            AddOrEdit(null);
        }
        protected override void EditAction(object parameter)
        {
            CheckableModel vm;
            if (base.GetSelectedOne(out vm, OperationDesc.Edit))
            {
                AddOrEdit(vm as PermDetailViewModel);
            }
        }
        protected override void DelAction(object parameter)
        {
            IEnumerable<CheckableModel> items = null;
            if (!base.BeforeDelete(out items))
            {
                return;
            }
            var ids = items.Select(m => ((PermDetailViewModel)m).permdata.per_id);
            var rst = HttpUtils.PostResult(ApiUtils.GetApiUrl(ApiKeys.MultiDeletePer),
                new
                {
                    pks = ids.ToArray()
                }, ClientContext.Token);
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, OperationDesc.Delete, rst.msg);
                return;
            }
            MessageWindow.ShowMsg(MessageType.Info, OperationDesc.Delete, MsgConst.Msg_Succeed);
            //清除垃圾缓存
            var funcCodes = items.Where(m => ((PermDetailViewModel)m).permdata.per_type == PermType.Func.ToString())
                                .Select(m => ((PermDetailViewModel)m).permdata.per_code);
            if (funcCodes != null && funcCodes.Count() > 0 && DataCacheUtils.AllFuncs.Count > 0)
            {
                foreach (var code in funcCodes)
                {
                    if (DataCacheUtils.AllFuncs.ContainsKey(code))
                    {
                        DataCacheUtils.AllFuncs.Remove(code);
                    }
                }
            }
            base.SearchCmd.Execute(null);
        }
        private void AddOrEdit(PermDetailViewModel vmPerm)
        {
            var win = new PermissionDetailWindow(vmPerm);
            var rst = win.ShowDialog();
            if (rst != null && rst == true)
            {
                base.SearchCmd.Execute(null);
            }
        }
        protected override Action<PagingArgs> SearchAction
        {
            get { return this.Search; }
        }
        private void Search(PagingArgs page)
        {
            base.Models = null;
            var rst = HttpUtils.PostResult(ApiUtils.GetApiUrl(ApiKeys.GetPerByPage),
               new
               {
                   pageIndex = page.PageIndex,
                   pageSize = page.PageSize,
                   conditions = new Dictionary<string, string>
                    {
                        {"per_code",Filter_PerCode},
                        {"per_name",Filter_PerName},
                        {"per_type",Filter_PerType==null?"":Filter_PerType.Id},
                        {"per_parent",Filter_PerParent},
                        {"per_parent_name",Filter_PerParent_Name},
                    }
               },
               ClientContext.Token);
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, OperationDesc.Search, rst.msg);
                return;
            }
            if (rst.data != null && rst.data.total != null)
            {
                page.RecordsCount = (int)rst.data.total;
                if (page.RecordsCount == 0)
                {
                    page.PageCount = 0;
                    page.PageIndex = 1;
                    return;
                }
                page.PageCount = Convert.ToInt32(Math.Ceiling(page.RecordsCount * 1.0 / page.PageSize));

                var datas = rst.data.rows as JArray;
                if (datas.IsNotEmpty())
                {
                    IEnumerable<PermDetailViewModel> perms = datas.Select(obj =>
                    {
                        PermDetailViewModel permVM = new PermDetailViewModel(needValidate: false);
                        var ins = JsonConvert.DeserializeObject(obj.ToString(), permVM.permdata.GetType());
                        (ins as IPermVM).CopyTo(permVM.permdata);
                        permVM.per_parent_name = obj["per_parent_name"].Value<string>();
                        permVM.per_type_name = obj["per_type_name"].Value<string>();
                        return permVM;
                    });

                    base.PageStart = page.Start;
                    base.Models = (perms as IEnumerable<CheckableModel>).ToList();
                }
            }
        }
        #endregion

        #region 查询条件
        string _filter_percode;
        public string Filter_PerCode
        {
            get { return _filter_percode; }
            set
            {
                if (_filter_percode != value)
                {
                    _filter_percode = value;
                    base.RaisePropertyChanged("Filter_PerCode");
                }
            }
        }
        string _filter_pername;
        public string Filter_PerName
        {
            get { return _filter_pername; }
            set
            {
                if (_filter_pername != value)
                {
                    _filter_pername = value;
                    base.RaisePropertyChanged("Filter_PerName");
                }
            }
        }
        CmbItem _filter_pertype;
        public CmbItem Filter_PerType
        {
            get { return _filter_pertype; }
            set
            {
                if (_filter_pertype != value)
                {
                    _filter_pertype = value;
                    base.RaisePropertyChanged("Filter_PerType");
                }
            }
        }

        string _filter_parent;
        public string Filter_PerParent
        {
            get { return _filter_parent; }
            set
            {
                if (_filter_parent != value)
                {
                    _filter_parent = value;
                    base.RaisePropertyChanged("Filter_PerParent");
                }
            }
        }

        string _filter_parent_name;
        public string Filter_PerParent_Name
        {
            get { return _filter_parent_name; }
            set
            {
                if (_filter_parent_name != value)
                {
                    _filter_parent_name = value;
                    base.RaisePropertyChanged("Filter_PerParent_Name");
                    //上级名称变了，说明是手动输入，此时应该将Filter_PerParent置空，因为二者已经不匹配了
                    Filter_PerParent = "";
                }
            }
        }
        #endregion
    }
}
