using MyNet.Client.Help;
using MyNet.Client.Pages.Auth;
using MyNet.Client.Public;
using MyNet.Components;
using MyNet.Components.Result;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Controls;
using MyNet.Components.WPF.Models;
using MyNet.Components.WPF.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyNet.Client.Models.Auth
{
    public class GroupMngViewModel : MngViewModel
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

        private ICommand _gpParentHelpCmd;
        public ICommand GpParentHelpCmd
        {
            get
            {
                if (_gpParentHelpCmd == null)
                {
                    _gpParentHelpCmd = new DelegateCommand(OpenGpParentHelp);
                }
                return _gpParentHelpCmd;
            }
        }

        private void OpenGpParentHelp(object parameter)
        {
            TreeHelper.OpenAllGroupsHelp(false, node =>
            {
                var tNode = (TreeViewData.TreeNode)node;
                Filter_GpParent_Name = tNode.Label;
                Filter_GpParent = tNode.Id;
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
                AddOrEdit(vm as GroupViewModel);
            }
        }
        protected override void DelAction(object parameter)
        {
            IEnumerable<CheckableModel> items = null;
            if (!base.BeforeDelete(out items))
            {
                return;
            }
            var ids = items.Select(m => ((GroupViewModel)m).gp_id);
            var rst = HttpHelper.GetResultByPost(ApiHelper.GetApiUrl(ApiKeys.MultiDeleteGroup),
                new
                {
                    pks = ids.ToArray()
                }, MyContext.Token);
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, OperationDesc.Delete, rst.msg);
                return;
            }
            MessageWindow.ShowMsg(MessageType.Info, OperationDesc.Delete, MsgConst.Msg_Succeed);
            //清除垃圾缓存
            var gpCodes = items.Select(m => ((GroupViewModel)m).gp_code);
            if (gpCodes != null && gpCodes.Count() > 0 && DataCacheHelper.AllGroups.Count > 0)
            {
                foreach (var code in gpCodes)
                {
                    if (DataCacheHelper.AllGroups.ContainsKey(code))
                    {
                        DataCacheHelper.AllGroups.Remove(code);
                    }
                }
            }
            base.SearchCmd.Execute(null);
        }
        private void AddOrEdit(GroupViewModel vmGroup)
        {
            var win = new GroupDetailWindow(vmGroup);
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
            var rst = HttpHelper.GetResultByPost(ApiHelper.GetApiUrl(ApiKeys.GetGroupByPage),
               new
               {
                   pageIndex = page.PageIndex,
                   pageSize = page.PageSize,
                   conditions = new Dictionary<string, string>
                    {
                        {"gp_code",Filter_GpCode},
                        {"gp_name",Filter_GpName},
                        {"gp_parent",Filter_GpParent},
                        {"gp_parent_name",Filter_GpParent_Name}
                    }
               },
               MyContext.Token);
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
                    base.Models = null;
                    return;
                }
                page.PageCount = Convert.ToInt32(Math.Ceiling(page.RecordsCount * 1.0 / page.PageSize));

                var models = JsonConvert.DeserializeObject<IEnumerable<GroupViewModel>>(((JArray)rst.data.rows).ToString());

                base.PageStart = page.Start;
                base.Models = (models as IEnumerable<CheckableModel>).ToList();
            }
        }
        #endregion

        #region 查询条件
        string _filter_percode;
        public string Filter_GpCode
        {
            get { return _filter_percode; }
            set
            {
                if (_filter_percode != value)
                {
                    _filter_percode = value;
                    base.RaisePropertyChanged("Filter_GpCode");
                }
            }
        }
        string _filter_pername;
        public string Filter_GpName
        {
            get { return _filter_pername; }
            set
            {
                if (_filter_pername != value)
                {
                    _filter_pername = value;
                    base.RaisePropertyChanged("Filter_GpName");
                }
            }
        }

        string _filter_parent;
        public string Filter_GpParent
        {
            get { return _filter_parent; }
            set
            {
                if (_filter_parent != value)
                {
                    _filter_parent = value;
                    base.RaisePropertyChanged("Filter_GpParent");
                }
            }
        }

        string _filter_parent_name;
        public string Filter_GpParent_Name
        {
            get { return _filter_parent_name; }
            set
            {
                if (_filter_parent_name != value)
                {
                    _filter_parent_name = value;
                    base.RaisePropertyChanged("Filter_GpParent_Name");
                    //上级名称变了，说明是手动输入，此时应该将Filter_GpParent置空，因为二者已经不匹配了
                    Filter_GpParent = "";
                }
            }
        }
        #endregion
    }
}
