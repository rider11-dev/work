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

namespace Card.Client
{
    public class CardQueryViewModel : BaseModel
    {
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
        private void QueryAction(object obj)
        {
            if (IdcardReader == null)
            {
                return;
            }
            var idcard = IdcardReader.Model.Idcard;
            Reset();
            if (idcard.IsEmpty())
            {
                MessageWindow.ShowMsg(MessageType.Info, "提示", "请输入身份证号或放置身份证到指定位置");
                return;
            }
            //1、查询账户信息
            var rst = HttpUtils.PostResult(ApiUtils.GetApiUrl(CardApiKeys.GetCardAccount, CardApiKeys.Key_ApiProvider_Card), new { idcard = idcard });
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, OperationDesc.Search, rst.msg);
                return;
            }
            if (rst.data == null)
            {
                return;
            }
            var acc = JsonConvert.DeserializeObject(((JObject)rst.data).ToString(), CardAccount.GetType()) as ICardAccountVM;
            if (acc == null)
            {
                return;
            }
            acc.CopyTo(CardAccount);

            //2、查询卡信息
            rst = HttpUtils.PostResult(ApiUtils.GetApiUrl(CardApiKeys.GetCards, CardApiKeys.Key_ApiProvider_Card), new { idcard = idcard });
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, OperationDesc.Search, rst.msg);
                return;
            }
            if (rst.data != null && rst.data.rows != null)
            {
                Cards = JsonConvert.DeserializeObject<IList<CardInfoVM>>(((JArray)rst.data.rows).ToString());
            }
        }

        private void Reset()
        {
            CardAccount.id = "";
            CardAccount.number = "";
            CardAccount.username = "";
            CardAccount.idcard = "";
            CardAccount.govmoney = 0;
            CardAccount.mymoney = 0;
            CardAccount.state = "";
            CardAccount.@operator = "";
            CardAccount.phone = "";
            CardAccount.remark = "";

            Cards = null;
        }

        public ICardAccountVM CardAccount { get; private set; }
        private IList<CardInfoVM> _cards;
        public IList<CardInfoVM> Cards
        {
            get { return _cards; }
            set
            {
                if (_cards != value)
                {
                    _cards = value;
                    base.RaisePropertyChanged("Cards");
                }
            }
        }

        public CardQueryViewModel(bool needValidate = false) : base(needValidate)
        {
            CardAccount = DynamicModelBuilder.GetInstance<ICardAccountVM>(parent: typeof(BaseModel), ctorArgs: needValidate);
        }
    }
}
