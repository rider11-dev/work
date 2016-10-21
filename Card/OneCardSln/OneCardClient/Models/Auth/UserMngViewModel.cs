using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MyNet.Components;
using MyNet.Components.Misc;
using MyNet.Components.Result;
using MyNet.Components.WPF.Command;
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
    public class UserMngViewModel : MngViewModel
    {
        public override Dictionary<string, ICommand> Commands
        {
            get
            {
                return new Dictionary<string, ICommand>
                {
                    {"Add",base.AddCmd},
                    {"Delete",base.DelCmd},
                    {"Edit",base.EditCmd},
                    {"Assign",AssignCmd},
                };
            }
        }

        private ICommand _assignCmd;
        public ICommand AssignCmd
        {
            get
            {
                if (_assignCmd == null)
                {
                    _assignCmd = new DelegateCommand(AssignAction);
                }
                return _assignCmd;
            }
        }

        private void AssignAction(object param)
        {
            MessageWindow.ShowMsg(MessageType.Info, OperationDesc.Assign, "分配用户权限");

        }


        #region 基类命令对应动作重写
        protected override void AddAction(object param)
        {
            AddOrEdit(null);
        }
        protected override void EditAction(object param)
        {
            CheckableModel vm;
            if (base.GetSelectedOne(out vm))
            {
                AddOrEdit(vm as UserViewModel);
            }
        }

        private void AddOrEdit(UserViewModel vmUsr)
        {
            var win = new UserDetailWindow(vmUsr);
            var rst = win.ShowDialog();
            if (rst != null && rst == true)
            {
                base.SearchCmd.Execute(null);
            }
        }
        protected override void DelAction(object param)
        {
            IEnumerable<CheckableModel> items = null;
            if (!base.BeforeDelete(out items))
            {
                return;
            }
            var ids = items.Select(m => ((UserViewModel)m).user_id);
            var rst = HttpHelper.GetResultByPost(ApiHelper.GetApiUrl(ApiKeys.MultiDeleteUsr),
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

        protected override Action<PagingArgs> SearchAction
        {
            get
            {
                return this.Search;
            }
        }
        private void Search(PagingArgs page)
        {
            var rst = HttpHelper.GetResultByPost(ApiHelper.GetApiUrl(ApiKeys.GetUsrByPage),
               new
               {
                   pageIndex = page.PageIndex,
                   pageSize = page.PageSize,
                   conditions = new Dictionary<string, string>
                    {
                        {"regioncode",Filter_RegionCode},
                        {"username",Filter_UserName},
                        {"truename",Filter_TrueName},
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

                var models = JsonConvert.DeserializeObject<IEnumerable<UserViewModel>>(((JArray)rst.data.rows).ToString());
                //
                base.PageStart = page.Start;
                base.Models = models;
            }
        }
        #endregion

        #region 查询条件
        string _filter_regioncode;
        public string Filter_RegionCode
        {
            get { return _filter_regioncode; }
            set
            {
                if (_filter_regioncode != value)
                {
                    _filter_regioncode = value;
                    base.RaisePropertyChanged("Filter_RegionCode");
                }
            }
        }
        string _filter_username;
        public string Filter_UserName
        {
            get { return _filter_username; }
            set
            {
                if (_filter_username != value)
                {
                    _filter_username = value;
                    base.RaisePropertyChanged("Filter_UserName");
                }
            }
        }
        string _filter_truename;
        public string Filter_TrueName
        {
            get { return _filter_truename; }
            set
            {
                if (_filter_truename != value)
                {
                    _filter_truename = value;
                    base.RaisePropertyChanged("Filter_TrueName");
                }
            }
        }


        #endregion


    }
}
