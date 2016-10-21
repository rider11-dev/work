using MyNet.Components;
using MyNet.Components.Result;
using MyNet.Components.WPF.Models;
using MyNet.Components.WPF.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OneCardSln.OneCardClient.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OneCardSln.OneCardClient.Public
{
    /// <summary>
    /// 字典辅助类
    /// </summary>
    public class DictHelper
    {
        /// <summary>
        /// 字典缓存
        /// </summary>
        private static Dictionary<DictType, ObservableCollection<CmbItem>> DictSource = new Dictionary<DictType, ObservableCollection<CmbItem>>();

        public static void SetSource(ComboBox cmb, DictType dictType)
        {
            if (cmb == null)
            {
                return;
            }
            CmbModel model = cmb.FindResource("cbVm") as CmbModel;
            if (DictHelper.DictSource.ContainsKey(dictType))
            {
                //取缓存数据
                model.Bind(DictSource[dictType]);
            }
            else
            {
                //从服务器获取
                var rst = HttpHelper.GetResultByPost(ApiHelper.GetApiUrl(ApiKeys.GetDict), new
                 {
                     dict_type = dictType.ToString()
                 }, Context.Token);
                if (rst.code != ResultCode.Success)
                {
                    MessageWindow.ShowMsg(MessageType.Error, OperationDesc.Search, rst.msg);
                    return;
                }
                if (rst.data != null && rst.data.rows != null)
                {
                    var dicts = JsonConvert.DeserializeObject<ObservableCollection<CmbItem>>(((JArray)rst.data.rows).ToString());
                    model.Bind(dicts);
                    DictHelper.DictSource.Add(dictType, dicts);
                }
            }
        }
    }
}
