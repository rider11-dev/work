using MyNet.Client.Public;
using MyNet.Components;
using MyNet.Components.Extensions;
using MyNet.Components.Http;
using MyNet.Components.Result;
using MyNet.Components.Validation;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Windows;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyNet.Client.Models.CustomQuery
{
    public class TableDetailViewModel : TableViewModel
    {

        [JsonIgnore]
        [ValidateIgnore]
        public BaseWindow Window { get; set; }

        [JsonIgnore]
        [ValidateIgnore]
        public bool IsNew
        {
            get { return base.id.IsEmpty(); }
        }

        [JsonIgnore]
        [ValidateIgnore]
        private ICommand _saveCmd;
        [ValidateIgnore]
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
            var url = ApiUtils.GetApiUrl(this.IsNew ? ApiKeys.Cq_TableAdd : ApiKeys.Cq_TableUpdate);
            var rst = HttpUtils.PostResult(url, (TableViewModel)this, ClientContext.Token);
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
