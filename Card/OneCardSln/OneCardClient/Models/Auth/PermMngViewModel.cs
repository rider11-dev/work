using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MyNet.Components;
using MyNet.Components.Result;
using MyNet.Components.WPF.Controls;
using MyNet.Components.WPF.Models;
using OneCardSln.OneCardClient.Pages.Auth;
using OneCardSln.OneCardClient.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MyNet.Components.WPF.Windows;

namespace OneCardSln.OneCardClient.Models.Auth
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


        #region 基类命令对应动作重写
        protected override void AddAction(object parameter)
        {
            AddOrEdit(null);
        }
        protected override void EditAction(object parameter)
        {
            CheckableModel vm;
            if (base.GetSelectedOne(out vm))
            {
                AddOrEdit(vm as PermViewModel);
            }
        }
        protected override void DelAction(object parameter)
        {
            IEnumerable<CheckableModel> items = null;
            if (!base.BeforeDelete(out items))
            {
                return;
            }
            var ids = items.Select(m => ((PermViewModel)m).per_id);
            var rst = HttpHelper.GetResultByPost(ApiHelper.GetApiUrl(ApiKeys.MultiDeletePer),
                new
                {
                    pks = ids.ToArray()
                }, Context.Token);
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, OperationDesc.Delete, rst.msg);
                return;
            }
            MessageWindow.ShowMsg(MessageType.Info, OperationDesc.Delete, MsgConst.Msg_Succeed);
            base.SearchCmd.Execute(null);
        }
        private void AddOrEdit(PermViewModel vmPerm)
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
            var rst = HttpHelper.GetResultByPost(ApiHelper.GetApiUrl(ApiKeys.GetPerByPage),
               new
               {
                   pageIndex = page.PageIndex,
                   pageSize = page.PageSize,
                   conditions = new Dictionary<string, string>
                    {
                        {"per_code",Filter_PerCode},
                        {"per_name",Filter_PerName},
                        {"per_type",Filter_PerType},
                        {"per_parent",Filter_PerParent},
                    }
               },
               Context.Token);
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

                var models = JsonConvert.DeserializeObject<IEnumerable<PermViewModel>>(((JArray)rst.data.rows).ToString());

                base.PageStart = page.Start;
                base.Models = models;
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
        string _filter_type;
        public string Filter_PerType
        {
            get { return _filter_type; }
            set
            {
                if (_filter_type != value)
                {
                    _filter_type = value;
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
        #endregion
    }
}
