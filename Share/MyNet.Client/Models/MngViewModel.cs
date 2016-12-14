using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Controls;
using MyNet.Components.WPF.Models;
using MyNet.Components.WPF.Windows;
using MyNet.Client.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyNet.Client.Models
{
    public abstract class MngViewModel : CheckableModelCollection
    {
        ControlPagination _ctlPage;
        /// <summary>
        /// 分页查询控件
        /// </summary>
        public ControlPagination CtlPage
        {
            get
            {
                return _ctlPage;
            }
            set
            {
                _ctlPage = value;
                _ctlPage.QueryHandler = (e) =>
                {
                    SearchAction?.Invoke(e);
                };
            }
        }


        #region 所有命令
        private ICommand _addCmd;
        public ICommand AddCmd
        {
            get
            {
                if (_addCmd == null)
                {
                    _addCmd = new DelegateCommand(AddAction);
                }
                return _addCmd;
            }
        }

        private ICommand _delCmd;
        public ICommand DelCmd
        {
            get
            {
                if (_delCmd == null)
                {
                    _delCmd = new DelegateCommand(DelAction);
                }
                return _delCmd;
            }
        }

        private ICommand _editCmd;
        public ICommand EditCmd
        {
            get
            {
                if (_editCmd == null)
                {
                    _editCmd = new DelegateCommand(EditAction);
                }
                return _editCmd;
            }
        }

        private ICommand _searchCmd;
        /// <summary>
        /// 分页查询命令
        /// </summary>
        public ICommand SearchCmd
        {
            get
            {
                if (_searchCmd == null)
                {
                    _searchCmd = new DelegateCommand(o => { CtlPage.Bind(); });
                }
                return _searchCmd;
            }
        }

        #endregion

        #region 命令对应动作
        protected virtual void AddAction(object param)
        {

        }
        protected virtual void EditAction(object param)
        {

        }

        protected virtual void DelAction(object param)
        {

        }
        protected abstract Action<PagingArgs> SearchAction { get; }

        #endregion

        protected bool GetSelectedOne(out CheckableModel model, string optDesc)
        {
            model = null;
            if (Models == null || Models.Count() < 1)
            {
                return false;
            }
            var items = Models.Where(m => m.IsChecked == true);
            var count = items.Count();
            if (count < 1)
            {
                MessageWindow.ShowMsg(MessageType.Warning, optDesc, MsgConst.Msg_SelectData);
                return false;
            }
            if (count > 1)
            {
                MessageWindow.ShowMsg(MessageType.Warning, optDesc, MsgConst.Msg_SelectOnlyone);
                return false;
            }
            model = items.First();
            return true;
        }

        protected bool BeforeDelete(out IEnumerable<CheckableModel> items)
        {
            items = base.GetSelectedModels();
            if (items == null || items.Count() < 1)
            {
                MessageWindow.ShowMsg(MessageType.Warning, OperationDesc.Delete, MsgConst.Msg_SelectData);
                return false;
            }
            var ask = MessageWindow.ShowMsg(MessageType.Ask, OperationDesc.Delete, MsgConst.Msg_AskWhenDeleteSel);
            if (ask == null || ask != true)
            {
                return false;
            }
            return true;
        }
    }
}
