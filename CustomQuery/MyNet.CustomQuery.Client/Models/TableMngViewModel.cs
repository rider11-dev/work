using MyNet.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNet.Components.WPF.Controls;
using System.Windows.Input;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Models;
using MyNet.Client.Public;
using MyNet.CustomQuery.Client.Pages.Base;
using MyNet.Components;
using MyNet.Components.Result;
using MyNet.Components.WPF.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MyNet.Model;

namespace MyNet.CustomQuery.Client.Models
{
    public class TableMngViewModel : MngViewModel
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
                    {"Init",InitCmd}
                };
            }
        }
        protected override void AddAction(object param)
        {
            AddOrEdit(null);
        }
        protected override void EditAction(object param)
        {
            CheckableModel vm;
            if (base.GetSelectedOne(out vm, OperationDesc.Edit))
            {
                AddOrEdit(vm as TableViewModel);
            }
        }

        private void AddOrEdit(TableViewModel vmTable)
        {
            var win = new TableDetailWindow(vmTable);
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
            var ids = items.Select(m => ((TableViewModel)m).id);
            var rst = HttpHelper.GetResultByPost(ApiHelper.GetApiUrl(CustomQueryApiKeys.TableDel, CustomQueryApiKeys.Key_ApiProvider_CustomQuery),
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
            PageQuery pageQuery = new PageQuery
            {
                pageIndex = page.PageIndex,
                pageSize = page.PageSize,
                conditions = new Dictionary<string, object>
                    {
                        {"tbname",Filter_Tbname},
                        {"alias",Filter_Alias}
                    }
            };
            base.Models = ((IEnumerable<CheckableModel>)GetTables(pageQuery)).ToList();
            page.RecordsCount = pageQuery.total;
            page.PageIndex = pageQuery.pageIndex;
            page.PageSize = pageQuery.pageSize;
            page.PageCount = pageQuery.pageTotal;
        }

        public static IEnumerable<TableViewModel> GetTables(PageQuery page)
        {
            IEnumerable<TableViewModel> empty = new List<TableViewModel>();
            var rst = HttpHelper.GetResultByPost(ApiHelper.GetApiUrl(CustomQueryApiKeys.TablePage, CustomQueryApiKeys.Key_ApiProvider_CustomQuery),
              new
              {
                  pageIndex = page.pageIndex,
                  pageSize = page.pageSize,
                  conditions = page.conditions
              },
              ClientContext.Token);
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, OperationDesc.Search, rst.msg);
                return null;
            }
            if (rst.data != null && rst.data.total != null)
            {
                page.total = (int)rst.data.total;
                if (page.total == 0)
                {
                    page.pageTotal = 0;
                    page.pageIndex = 1;
                    return empty;
                }
                page.pageTotal = Convert.ToInt32(Math.Ceiling(page.total * 1.0 / page.pageSize));
                var models = JsonConvert.DeserializeObject<IEnumerable<TableViewModel>>(((JArray)rst.data.rows).ToString());
                return models;
            }
            return empty;
        }

        #region 查询条件
        string _filter_tbname;
        public string Filter_Tbname
        {
            get { return _filter_tbname; }
            set
            {
                if (_filter_tbname != value)
                {
                    _filter_tbname = value;
                    base.RaisePropertyChanged("Filter_Tbname");
                }
            }
        }
        string _filter_alias;
        public string Filter_Alias
        {
            get { return _filter_alias; }
            set
            {
                if (_filter_alias != value)
                {
                    _filter_alias = value;
                    base.RaisePropertyChanged("Filter_Alias");
                }
            }
        }
        #endregion

        public ICommand _initCmd;
        public ICommand InitCmd
        {
            get
            {
                if (_initCmd == null)
                {
                    _initCmd = new DelegateCommand(InitAction);
                }
                return _initCmd;
            }
        }

        private void InitAction(object parameter)
        {
            var rst = new TableInitWindow().ShowDialog();
            if (rst.HasValue && rst.Value == true)
            {
                base.SearchCmd.Execute(null);
            }
        }
    }
}
