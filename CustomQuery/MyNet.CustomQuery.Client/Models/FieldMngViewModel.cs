using MyNet.Client.Models;
using MyNet.Client.Public;
using MyNet.Components;
using MyNet.Components.Misc;
using MyNet.Components.Result;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Controls;
using MyNet.Components.WPF.Models;
using MyNet.Components.WPF.Windows;
using MyNet.CustomQuery.Client.Pages.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyNet.CustomQuery.Client.Models
{
    public class FieldMngViewModel : MngViewModel
    {
        public FieldMngViewModel()
        {
            TbMngModel = new TableMngViewModel();
        }

        TableMngViewModel _tbMngModel;
        public TableMngViewModel TbMngModel
        {
            get { return _tbMngModel; }
            set
            {
                if (_tbMngModel != value)
                {
                    _tbMngModel = value;
                    base.RaisePropertyChanged("TbMngModel");
                }
            }
        }

        public DataGrid DgTables { get; set; }

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
                AddOrEdit(vm as FieldViewModel);
            }
        }

        private void AddOrEdit(FieldViewModel vmField)
        {
            var win = new FieldDetailWindow(vmField);
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
            var ids = items.Select(m => ((FieldViewModel)m).id);
            var rst = HttpHelper.GetResultByPost(ApiHelper.GetApiUrl(CustomQueryApiKeys.FieldDel, CustomQueryApiKeys.Key_ApiProvider_CustomQuery),
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
            //当前选中的表数据
            var selTb = DgTables.SelectedItem as TableViewModel;
            var tbid = selTb == null ? "" : selTb.id;
            var rst = HttpHelper.GetResultByPost(ApiHelper.GetApiUrl(CustomQueryApiKeys.FieldPage, CustomQueryApiKeys.Key_ApiProvider_CustomQuery),
               new
               {
                   pageIndex = page.PageIndex,
                   pageSize = page.PageSize,
                   conditions = new Dictionary<string, string>
                    {
                        {"tbid",tbid},
                        {"fieldname",Filter_Fieldname},
                        {"displayname",Filter_DisplayName}
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

                var models = JsonConvert.DeserializeObject<IList<FieldDetailViewModel>>(((JArray)rst.data.rows).ToString());
                //
                base.PageStart = page.Start;
                base.Models = models;
            }
        }
        #region 查询条件
        string _filter_fieldname;
        public string Filter_Fieldname
        {
            get { return _filter_fieldname; }
            set
            {
                if (_filter_fieldname != value)
                {
                    _filter_fieldname = value;
                    base.RaisePropertyChanged("Filter_Fieldname");
                }
            }
        }
        string _filter_displayname;
        public string Filter_DisplayName
        {
            get { return _filter_displayname; }
            set
            {
                if (_filter_displayname != value)
                {
                    _filter_displayname = value;
                    base.RaisePropertyChanged("Filter_DisplayName");
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
            //var rst = new TableInitWindow().ShowDialog();
            //if (rst.HasValue && rst.Value == true)
            //{
            //    base.SearchCmd.Execute(null);
            //}
        }
    }
}
