using MyNet.Components;
using MyNet.Components.Result;
using MyNet.Components.WPF.Models;
using MyNet.Components.WPF.Windows;

using MyNet.Model.Auth;
using MyNet.Model.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MyNet.Client.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MyNet.Components.Extensions;
using MyNet.Model.Dto.Auth;

namespace MyNet.Client.Public
{
    /// <summary>
    /// 缓存辅助类
    /// </summary>
    public class DataCacheHelper
    {
        /// <summary>
        /// 字典缓存
        /// </summary>
        private static Dictionary<DictType, IList<CmbItem>> DictSource = new Dictionary<DictType, IList<CmbItem>>();
        private static Dictionary<Type, IList<CmbItem>> EnumDictSource = new Dictionary<Type, IList<CmbItem>>();

        private static Dictionary<string, PermissionCacheDto> _allFuncs = null;
        /// <summary>
        /// 所有功能权限缓存
        /// </summary>
        public static Dictionary<string, PermissionCacheDto> AllFuncs
        {
            get
            {
                if (_allFuncs == null)
                {
                    _allFuncs = new Dictionary<string, PermissionCacheDto>();
                    LoadPerms(ApiKeys.GetAllFuncs, _allFuncs);
                }
                return _allFuncs;
            }
        }

        private static Dictionary<string, PermissionCacheDto> _allOpts = null;
        /// <summary>
        /// 所有操作权限缓存
        /// </summary>
        public static Dictionary<string, PermissionCacheDto> AllOpts
        {
            get
            {
                if (_allOpts == null)
                {
                    _allOpts = new Dictionary<string, PermissionCacheDto>();
                    LoadPerms(ApiKeys.GetAllOpts, _allOpts);
                }
                return _allOpts;
            }
        }

        private static Dictionary<string, Group> _allGroups = null;
        /// <summary>
        /// 所有组织缓存
        /// </summary>
        public static Dictionary<string, Group> AllGroups
        {
            get
            {
                if (_allGroups == null)
                {
                    _allGroups = new Dictionary<string, Group>();
                    LoadGroups(ApiKeys.GetAllGroups, _allGroups);
                }
                return _allGroups;
            }
        }

        static DataCacheHelper()
        {
            PreLoadSomeDicts();
        }

        /// <summary>
        /// 预加载部分字典
        /// </summary>
        private static void PreLoadSomeDicts()
        {
            GetEnumCmbSource<BoolType>();
        }

        /// <summary>
        /// 设置ComboBox数据源
        /// </summary>
        /// <param name="cmb">ComboBox控件</param>
        /// <param name="dictType">字典类别</param>
        /// <param name="selectedTypeCode">设置要选中的子项</param>
        /// <param name="setSelect">是否设置选中项</param>
        /// <param name="needBlankItem">是否需要一个空项</param>
        public static void SetCmbSource(ComboBox cmb, DictType dictType, string selectedTypeCode = "", bool setSelect = true, bool needBlankItem = false)
        {
            if (cmb == null)
            {
                return;
            }

            var dicts = GetCmbSource(dictType);
            CmbModel model = cmb.DataContext as CmbModel;
            model.Bind(dicts, selectedTypeCode, setSelect, needBlankItem);
        }
        /// <summary>
        /// 设置ComboBox数据源（解析枚举）
        /// </summary>
        /// <param name="cmb">ComboBox控件</param>
        /// <param name="dictType">枚举类型</param>
        /// <param name="selectedTypeCode">设置要选中的子项</param>
        /// <param name="setSelect">是否设置选中项</param>
        /// <param name="needBlankItem">是否需要一个空项</param>
        public static void SetEnumCmbSource<TEnum>(ComboBox cmb, string selectedTypeCode = "", bool setSelect = true, bool needBlankItem = false)
        {
            if (cmb == null)
            {
                return;
            }

            var dicts = GetEnumCmbSource<TEnum>();
            CmbModel model = cmb.DataContext as CmbModel;
            model.Bind(dicts, selectedTypeCode, setSelect, needBlankItem);
        }
        /// <summary>
        /// 获取数据库字典数据
        /// </summary>
        /// <param name="dictType"></param>
        /// <returns></returns>
        public static IList<CmbItem> GetCmbSource(DictType dictType)
        {
            if (DataCacheHelper.DictSource.ContainsKey(dictType))
            {
                //取缓存数据
                return DictSource[dictType];
            }
            else
            {
                //从服务器获取
                var rst = HttpHelper.GetResultByPost(ApiHelper.GetApiUrl(ApiKeys.GetDict), new
                {
                    dict_type = dictType.type_code
                }, ClientContext.Token);
                if (rst.code != ResultCode.Success)
                {
                    MessageWindow.ShowMsg(MessageType.Error, OperationDesc.Search, rst.msg);
                    return null;
                }
                if (rst.data != null && rst.data.rows != null)
                {
                    var dicts = JsonConvert.DeserializeObject<IList<CmbItem>>(((JArray)rst.data.rows).ToString());
                    DataCacheHelper.DictSource.Add(dictType, dicts);
                    return dicts;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取枚举字典数据
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public static IList<CmbItem> GetEnumCmbSource<TEnum>()
        {
            var type = typeof(TEnum);
            if (EnumDictSource.ContainsKey(type))
            {
                return EnumDictSource[type];
            }
            var dicts = EnumExtension.ConvertEnumToDict<TEnum>()
                 .Select(kvp => new CmbItem { Id = kvp.Key, Text = kvp.Value })
                 .ToList();
            EnumDictSource.Add(type, dicts);
            return dicts;
        }

        private static void LoadPerms(string apiKey, Dictionary<string, PermissionCacheDto> target)
        {
            //从服务器获取
            var rst = HttpHelper.GetResultByPost(url: ApiHelper.GetApiUrl(apiKey), token: ClientContext.Token);
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, OperationDesc.Search, rst.msg);
                return;
            }
            if (rst.data != null)
            {
                var opts = JsonConvert.DeserializeObject<IList<PermissionCacheDto>>(((JArray)rst.data).ToString());
                if (opts != null && opts.Count > 0)
                {
                    foreach (var func in opts)
                    {
                        target.Add(func.per_code, func);
                    }
                }
            }
        }
        private static void LoadGroups(string apiKey, Dictionary<string, Group> target)
        {
            //从服务器获取
            var rst = HttpHelper.GetResultByPost(url: ApiHelper.GetApiUrl(apiKey), token: ClientContext.Token);
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, OperationDesc.Search, rst.msg);
                return;
            }
            if (rst.data != null)
            {
                var groups = JsonConvert.DeserializeObject<IList<Group>>(((JArray)rst.data).ToString());
                if (groups != null && groups.Count > 0)
                {
                    foreach (var func in groups)
                    {
                        target.Add(func.gp_code, func);
                    }
                }
            }
        }
    }
}
