using MyNet.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNet.Components.WPF.Controls;
using MyNet.Components.Http;
using MyNet.Client.Public;
using MyNet.Components.Extensions;
using MyNet.Components.WPF.Windows;
using Newtonsoft.Json;
using MyNet.Components.WPF.Models;

namespace Card.Client
{
    public class OrderRecordsViewModel : MngViewModel
    {
        public UCIdcardReader IdcardReader { get; set; }

        protected override Action<PagingArgs> SearchAction
        {
            get
            {
                return Search;
            }
        }

        private void Search(PagingArgs page)
        {
            if (IdcardReader.Model.Idcard.IsEmpty())
            {
                MessageWindow.ShowMsg(MessageType.Info, "提示", "请输入身份证号或放置身份证到指定位置");
                return;
            }
            base.Models = null;
            var rst = HttpUtils.Post(ApiUtils.GetApiUrl(CardApiKeys.GetOrderRecords, CardApiKeys.Key_ApiProvider_Card),
               new
               {
                   page = page.PageIndex,
                   rows = page.PageSize,
                   idcard = IdcardReader.Model.Idcard,
                   state = "40"
               });
            var rstObj = JsonConvert.DeserializeObject<OrderObject>(rst);
            if (rstObj == null)
            {
                return;
            }
            if (rstObj.code != "1")
            {
                MessageWindow.ShowMsg(MessageType.Info, "提示", rstObj.msg);
                page.RecordsCount = 0;
                page.PageCount = 0;
                page.PageIndex = 1;
                return;
            }
            page.RecordsCount = rstObj.total;
            if (page.RecordsCount == 0)
            {
                page.PageCount = 0;
                page.PageIndex = 1;
                base.Models = null;
                return;
            }
            page.PageCount = Convert.ToInt32(Math.Ceiling(page.RecordsCount * 1.0 / page.PageSize));
            if (rstObj.rows.IsNotEmpty())
            {
                base.PageStart = page.Start;
                base.Models = (rstObj.rows as IEnumerable<CheckableModel>).ToList();
            }
        }
    }
}
