using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Controls;
using MyNet.Components.WPF.Models;
using MyNet.Components.WPF.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyNet.Components.WPF.Windows
{
    public class HelpWindowViewModel : BaseModel
    {
        public BaseWindow Window { get; set; }
        public ControlTree TreeHelpControl { get; set; }
        public Action<dynamic> NodeSelCallback { get; set; }

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
            var treeView = TreeHelpControl.FindName("treeView") as TreeView;
            var selNode = treeView.SelectedValue;
            if (selNode == null)
            {
                MessageWindow.ShowMsg(MessageType.Warning, "帮助选择", "请选择节点数据");
                return;
            }
            this.Window.DialogResult = true;
            this.Window.Close();
            if (NodeSelCallback != null)
            {
                NodeSelCallback(selNode);
            }
        }
    }
}
