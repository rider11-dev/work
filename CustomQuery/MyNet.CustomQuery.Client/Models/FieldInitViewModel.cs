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
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Controls;

namespace MyNet.CustomQuery.Client.Models
{
    public class FieldInitViewModel : MngViewModel
    {
        public FieldInitViewModel(IEnumerable<TableViewModel> tables)
        {
            Tables = tables;
        }

        [JsonIgnore]
        public IEnumerable<TableViewModel> Tables { get; private set; }
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
        private ICommand _getDbFieldsCmd;
        [JsonIgnore]
        public ICommand GetDbFieldsCmd
        {
            get
            {
                if (_getDbFieldsCmd == null)
                {
                    _getDbFieldsCmd = new DelegateCommand(GetDbFieldsAction);
                }
                return _getDbFieldsCmd;
            }
        }
        private void GetDbFieldsAction(object parameter)
        {
            if (Tables.IsEmpty())
            {
                MessageWindow.ShowMsg(MessageType.Error, CustomQueryOptDesc.GetDbFields, "未定义查询表！");
                return;
            }
            var url = ApiHelper.GetApiUrl(CustomQueryApiKeys.FieldDbFields, CustomQueryApiKeys.Key_ApiProvider_CustomQuery);
            var rst = HttpHelper.GetResultByPost(url, Tables.Select(t => t.id), MyContext.Token);
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, CustomQueryOptDesc.GetDbFields, rst.msg);
                return;
            }
            if (rst.data != null && rst.data.rows != null)
            {
                var fields = JsonConvert.DeserializeObject<IEnumerable<FieldViewModel>>(((JArray)rst.data.rows).ToString());
                base.Models = ((IEnumerable<CheckableModel>)fields).ToList();//
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
            var askRst = MessageWindow.ShowMsg(MessageType.Ask, CustomQueryOptDesc.InitFields, "初始化操作将清空当前查询字段，是否继续？");
            if (askRst != true)
            {
                return;
            }
            var rst = HttpHelper.GetResultByPost(ApiHelper.GetApiUrl(CustomQueryApiKeys.FieldInit, CustomQueryApiKeys.Key_ApiProvider_CustomQuery), sels, MyContext.Token);
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, CustomQueryOptDesc.InitFields, rst.msg);
                return;
            }
            MessageWindow.ShowMsg(MessageType.Info, CustomQueryOptDesc.InitFields, MsgConst.Msg_Succeed);
            if (Window != null)
            {
                Window.DialogResult = true;
                Window.CloseCmd.Execute(null);
            }
        }

        //客户端删除
        protected override void DelAction(object param)
        {
            var model = param as FieldViewModel;
            if (model != null)
            {
                //行内删除
                base.Models = base.Models.Except(new List<FieldViewModel> { model }).ToList();
                return;
            }
            //工具栏删除（批量删除）
            var dels = base.GetSelectedModels();
            if (dels.IsEmpty())
            {
                MessageWindow.ShowMsg(MessageType.Info, OperationDesc.Delete, "请选择要删除的数据！");
                return;
            }
            base.Models = base.Models.Except(dels).ToList();
            FindCmd.Execute(null);
        }

        [JsonIgnore]
        private string _filter_tbname;
        public string Filter_Tbname
        {
            get { return _filter_tbname; }
            set
            {
                if (_filter_tbname != value)
                {
                    _filter_tbname = value;
                    base.RaisePropertyChanged("Filter_Tbname");
                }
            }
        }
        [JsonIgnore]
        private ICommand _findCmd;
        [JsonIgnore]
        public ICommand FindCmd
        {
            get
            {
                if (_findCmd == null)
                {
                    _findCmd = new DelegateCommand(FindAction);
                }
                return _findCmd;
            }
        }

        private void FindAction(object obj)
        {
            if (base.Models.IsEmpty())
            {
                return;
            }
            ICollectionView view = GetView();
            if (Filter_Tbname.IsEmpty())
            {
                view.Filter = null;
            }
            else
            {
                view.Filter = model =>
                {
                    return (model as FieldViewModel).tbname.Contains(Filter_Tbname);
                };
            }
        }

        private ICollectionView GetView()
        {
            return CollectionViewSource.GetDefaultView(base.Models);
        }
    }
}
