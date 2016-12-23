using MyNet.Client.Models;
using MyNet.Components.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNet.Components.WPF.Controls;
using System.Windows.Input;
using MyNet.Components.WPF.Command;
using MyNet.Components;
using MyNet.Client.Public;
using MyNet.Components.WPF.Windows;
using MyNet.Components.Result;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MyNet.CustomQuery.Model;
using MyNet.Components.Mapper;
using EmitMapper.MappingConfiguration;
using MyNet.Components.Extensions;
using MyNet.Components.Http;

namespace MyNet.CustomQuery.Client.Models
{
    public class TableInitViewModel : MngViewModel
    {
        [JsonIgnore]
        public BaseWindow Window { get; set; }

        protected override Action<PagingArgs> SearchAction
        {
            get
            {
                return null;
            }
        }

        [JsonIgnore]
        private ICommand _getDbTablesCmd;
        [JsonIgnore]
        public ICommand GetDbTablesCmd
        {
            get
            {
                if (_getDbTablesCmd == null)
                {
                    _getDbTablesCmd = new DelegateCommand(GetDbTablesAction);
                }
                return _getDbTablesCmd;
            }
        }
        private void GetDbTablesAction(object parameter)
        {
            var rst = HttpUtils.PostResult(ApiUtils.GetApiUrl(CustomQueryApiKeys.TableDbTables, CustomQueryApiKeys.Key_ApiProvider_CustomQuery), ClientContext.Token);
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, CustomQueryOptDesc.GetDbTables, rst.msg);
                return;
            }
            if (rst.data != null && rst.data.rows != null)
            {
                var dbTables = JsonConvert.DeserializeObject<IEnumerable<DbTable>>(((JArray)rst.data.rows).ToString());
                if (dbTables.IsNotEmpty())
                {
                    base.Models = (dbTables
                        .Select(dt => new TableViewModel { tbname = dt.table_name, comment = dt.table_comment })
                            as IEnumerable<CheckableModel>)
                        .ToList();
                }
                else
                {
                    base.Models = null;
                }
            }
        }

        [JsonIgnore]
        private ICommand _clearCmd;
        [JsonIgnore]
        public ICommand ClearCmd
        {
            get
            {
                if (_clearCmd == null)
                {
                    _clearCmd = new DelegateCommand(ClearAction);
                }
                return _clearCmd;
            }
        }

        private void ClearAction(object obj)
        {
            base.Models = null;
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

        private void SaveAction(object obj)
        {
            var sels = base.GetSelectedModels();
            if (sels.IsEmpty())
            {
                MessageWindow.ShowMsg(MessageType.Warning, OperationDesc.Save, "没有要保存的数据！");
                return;
            }
            //
            var askRst = MessageWindow.ShowMsg(MessageType.Ask, CustomQueryOptDesc.InitTables, "初始化操作将清空当前查询表以及查询字段，是否继续？");
            if (askRst != true)
            {
                return;
            }
            var rst = HttpUtils.PostResult(ApiUtils.GetApiUrl(CustomQueryApiKeys.TableInit, CustomQueryApiKeys.Key_ApiProvider_CustomQuery), sels, ClientContext.Token);
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, CustomQueryOptDesc.InitTables, rst.msg);
                return;
            }
            MessageWindow.ShowMsg(MessageType.Info, CustomQueryOptDesc.InitTables, MsgConst.Msg_Succeed);
            if (Window != null)
            {
                Window.DialogResult = true;
                Window.CloseCmd.Execute(null);
            }
        }

        protected override void DelAction(object param)
        {
            var model = param as TableViewModel;
            if (model != null)
            {
                //行内删除
                base.Models = base.Models.Except(new List<TableViewModel> { model }).ToList();
                return;
            }
            //工具栏删除（批量）
            var sels = base.GetSelectedModels();
            if (sels.IsEmpty())
            {
                MessageWindow.ShowMsg(MessageType.Info, OperationDesc.Delete, "请选择要删除的数据！");
                return;
            }

            base.Models = base.Models.Except(sels).ToList();
        }

    }
}
