using MyNet.Client.Help;
using MyNet.Client.Public;
using MyNet.Components;
using MyNet.Components.Extensions;
using MyNet.Components.Result;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Extension;
using MyNet.Components.WPF.Models;
using MyNet.Components.WPF.Windows;
using MyNet.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyNet.CustomQuery.Client.Models
{
    public class FieldDetailViewModel : FieldViewModel
    {
        IList<DataGridColModel> Cols = new List<DataGridColModel>
                {
                    new DataGridColModel { Field="comment",Header="表注释"},
                    new DataGridColModel { Field="tbname",Header="表名称"}
                };
        [JsonIgnore]
        public BaseWindow Window { get; set; }

        [JsonIgnore]
        public bool IsNew
        {
            get { return base.id.IsEmpty(); }
        }

        CmbItem _selFieldType;
        [JsonIgnore]
        public CmbItem SelFieldType
        {
            get { return _selFieldType; }
            set
            {
                if (_selFieldType != value)
                {
                    _selFieldType = value;
                    if (_selFieldType != null && base.fieldtype != _selFieldType.Id)
                    {
                        base.fieldtype = _selFieldType.Id;
                    }
                    base.RaisePropertyChanged("SelFieldType");
                }
            }
        }

        [JsonIgnore]
        private ICommand _saveCmd;
        [JsonIgnore]
        public ICommand SaveCmd
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
            var url = ApiHelper.GetApiUrl(this.IsNew ? CustomQueryApiKeys.FieldAdd : CustomQueryApiKeys.FieldUpdate, CustomQueryApiKeys.Key_ApiProvider_CustomQuery);
            var rst = HttpHelper.GetResultByPost(url, (FieldViewModel)this, MyContext.Token);
            string opt = this.IsNew ? OperationDesc.Add : OperationDesc.Edit;
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, opt, rst.msg);
                return;
            }
            MessageWindow.ShowMsg(MessageType.Info, opt, MsgConst.Msg_Succeed);
            if (Window != null)
            {
                Window.DialogResult = true;
                Window.CloseCmd.Execute(null);
            }
        }

        [JsonIgnore]
        private ICommand _tableHelpCmd;
        [JsonIgnore]
        public ICommand TableHelpCmd
        {
            get
            {
                if (_tableHelpCmd == null)
                {
                    _tableHelpCmd = new DelegateCommand(OpenTableHelp);
                }
                return _tableHelpCmd;
            }
        }

        private void OpenTableHelp(object parameter)
        {
            GridHelpWindow.ShowHelp(
                title: "查询表信息帮助",
                multiSel: false,
                dataProvider: GetTables,
                singleSelAction: AfterSelect,
                cols: Cols);
        }

        private IList<CheckableModel> GetTables()
        {
            return TableMngViewModel.GetTables(new PageQuery
            {
                pageIndex = 1,
                pageSize = 1000
            }) as IList<CheckableModel>;
        }

        private void AfterSelect(CheckableModel m)
        {
            var table = (TableViewModel)m;
            base.tbid = table.id;
            this.tbname = table.comment;
        }
    }
}
