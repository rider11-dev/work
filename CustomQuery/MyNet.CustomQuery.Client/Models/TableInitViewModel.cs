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

        private ICommand _getDbTablesCmd;
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
            var rst = HttpHelper.GetResultByGet(ApiHelper.GetApiUrl(CustomQueryApiKeys.TableDbTables, CustomQueryApiKeys.Key_ApiProvider_CustomQuery), MyContext.Token);
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, CustomQueryOptDesc.GetDbTables, rst.msg);
                return;
            }
            if (rst.data != null && rst.data.rows != null)
            {
                var dbTables = JsonConvert.DeserializeObject<IEnumerable<DbTable>>(((JArray)rst.data.rows).ToString());
                IList<TableViewModel> tbModels = new List<TableViewModel>();
                if (dbTables.IsNotEmpty())
                {
                    foreach (var tb in dbTables)
                    {
                        tbModels.Add(new TableViewModel { tbname = tb.Name, comment = tb.Comment });
                    }
                }
                base.Models = tbModels;
            }
        }

        private ICommand _clearCmd;
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

        private ICommand _saveCmd;
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
            if (base.Models.IsEmpty())
            {
                MessageWindow.ShowMsg(MessageType.Warning, OperationDesc.Save, "没有要保存的数据！");
                return;
            }
            var rst = HttpHelper.GetResultByPost(ApiHelper.GetApiUrl(CustomQueryApiKeys.TableInit, CustomQueryApiKeys.Key_ApiProvider_CustomQuery), base.Models, MyContext.Token);
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
                base.Models = base.Models.Where(m => m != model);
                return;
            }
            //工具栏删除（批量）
            var sels = base.GetSelectedModels();
            if (sels.IsEmpty())
            {
                return;
            }
            var newModels = base.Models;
            foreach (var sel in sels)
            {
                newModels = newModels.Where(m => m != sel);
            }
            base.Models = newModels;
        }

    }
}
