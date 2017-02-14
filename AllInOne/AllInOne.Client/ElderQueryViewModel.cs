using Card.ViewModel;
using MyNet.Client.Public;
using MyNet.Components.Emit;
using MyNet.Components.Extensions;
using MyNet.Components.Http;
using MyNet.Components.Misc;
using MyNet.Components.Result;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Controls;
using MyNet.Components.WPF.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AllInOne.Client
{
    public class ElderQueryViewModel : BaseModel
    {
        public ElderInfoVM ElderInfo { get; private set; }
        private ICommand _queryCmd;
        public ICommand QueryCmd
        {
            get
            {
                if (_queryCmd == null)
                {
                    _queryCmd = new DelegateCommand(QueryAction);
                }
                return _queryCmd;
            }
        }
        public UCIdcardReader IdcardReader { get; set; }

        public ElderQueryViewModel(bool needValidate = false) : base(needValidate)
        {
            ElderInfo = new ElderInfoVM();
        }

        private void QueryAction(object obj)
        {
            if (IdcardReader == null)
            {
                return;
            }
            var idcard = IdcardReader.Model.Idcard;
            ElderInfo.Reset();
            if (idcard.IsEmpty())
            {
                MessageWindow.ShowMsg(MessageType.Info, "提示", "请输入身份证号或放置身份证到指定位置");
                return;
            }
            //1、查询老年人信息
            var url = ApiUtils.GetApiUrl(AIOApiKeys.GetElderInfo, AIOApiKeys.Key_ApiProvider_Card) + string.Format("&idcard={0}", idcard);
            //var rst = HttpUtils.GetResult(url);
            var rstStr = HttpUtils.Get(url);
            var rst = JsonConvert.DeserializeObject<OptResultElder>(rstStr);
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, OperationDesc.Search, rst.msg);
                return;
            }
            if (rst.rows.IsEmpty())
            {
                MessageWindow.ShowMsg(MessageType.Info, OperationDesc.Search, "没有找到指定身份证号的老人信息");
                return;
            }

            rst.rows[0].CopyTo(ElderInfo);

        }

    }
}
