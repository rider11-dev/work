using MyNet.Components.Extensions;
using MyNet.Components.Misc;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyNet.Components.WPF.Windows
{
    public class GridHelpViewModel : CheckableModelCollection
    {
        public BaseWindow Window { get; set; }
        public bool MultiSelect { get; set; }
        public DataGrid DataGrid { get; set; }
        public Action<CheckableModel> SingleSelectCallback { get; set; }
        public Action<IEnumerable<CheckableModel>> MultiSelectCallback { get; set; }
        public Func<IEnumerable<CheckableModel>> DataProvider { get; set; }

        private ICommand _selectCmd;
        public ICommand SelectCmd
        {
            get
            {
                if (_selectCmd == null)
                {
                    _selectCmd = new DelegateCommand(SelectAction);
                }
                return _selectCmd;
            }
        }

        private void SelectAction(object parameter)
        {
            bool rst = MultiSelect ? SelectMulti() : SelectOne();
            if (rst != true)
            {
                return;
            }
            this.Window.DialogResult = true;
            this.Window.Close();
        }

        private bool SelectOne()
        {
            var sel = (CheckableModel)DataGrid.SelectedItem;
            if (sel == null)
            {
                MessageWindow.ShowMsg(MessageType.Warning, "帮助选择", "请选择一条数据");
                return false;
            }
            SingleSelectCallback?.Invoke(sel);
            return true;
        }

        private bool SelectMulti()
        {
            if (base.Models.IsEmpty())
            {
                MessageWindow.ShowMsg(MessageType.Warning, "帮助选择", "没有数据");
                return false;
            }
            var sels = base.Models.Where(m => ((ICheckable)m).IsChecked == true);
            if (sels.IsEmpty())
            {
                MessageWindow.ShowMsg(MessageType.Warning, "帮助选择", "请选择至少一条数据");
                return false;
            }
            MultiSelectCallback?.Invoke(sels as IList<CheckableModel>);
            return true;
        }

        private ICommand _refreshCmd;
        public ICommand RefreshCmd
        {
            get
            {
                if (_refreshCmd == null)
                {
                    _refreshCmd = new DelegateCommand(RefreshAction);
                }
                return _refreshCmd;
            }
        }

        private void RefreshAction(object parameter)
        {
            if (DataProvider != null)
            {
                var data = DataProvider();
                if (data.IsNotEmpty())
                {
                    Models = data.ToList();
                }
            }
        }
    }
}
