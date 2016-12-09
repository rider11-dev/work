using MyNet.Client.Public;
using MyNet.Components;
using MyNet.Components.Extensions;
using MyNet.Components.Result;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Windows;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyNet.CustomQuery.Client.Models
{
    public class TableDetailViewModel : TableViewModel
    {

        [JsonIgnore]
        public BaseWindow Window { get; set; }

        [JsonIgnore]
        public bool IsNew
        {
            get { return base.id.IsEmpty(); }
        }

        [JsonIgnore]
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
        private void SaveAction(object parameter)
        {
            if (!this.IsValid)
            {
                MessageWindow.ShowMsg(MessageType.Warning, OperationDesc.Validate, this.Error);
                return;
            }
            var url = ApiHelper.GetApiUrl(this.IsNew ? CustomQueryApiKeys.TableAdd : CustomQueryApiKeys.TableUpdate, CustomQueryApiKeys.Key_ApiProvider_CustomQuery);
            var rst = HttpHelper.GetResultByPost(url, (TableViewModel)this, MyContext.Token);
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
    }
}
