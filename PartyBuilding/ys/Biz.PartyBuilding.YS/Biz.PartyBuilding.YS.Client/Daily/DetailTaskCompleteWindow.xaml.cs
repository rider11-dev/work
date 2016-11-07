using Biz.PartyBuilding.YS.Client.Daily.Models;
using Biz.PartyBuilding.YS.Client.PartyOrg.Models;
using Microsoft.Win32;
using MyNet.Components.Extensions;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Models;
using MyNet.Components.WPF.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Biz.PartyBuilding.YS.Client.Daily
{
    /// <summary>
    /// DetailTaskCompleteWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DetailTaskCompleteWindow : BaseWindow
    {
        TaskEntity _model = null;
        private DetailTaskCompleteWindow()
        {
            _model = PartyBuildingContext.tasks_ccbsc_receive.Where(t => t.complete_detail.comp_state == "已领未完成").FirstOrDefault();
            InitializeComponent();
            this.DataContext = _model;
        }

        public DetailTaskCompleteWindow(Org2NewViewModel vm = null)
            : this()
        {
        }

        ICommand _uploadAttachCmd;
        public ICommand UploadAttachCmd
        {
            get
            {
                if (_uploadAttachCmd == null)
                {
                    _uploadAttachCmd = new DelegateCommand(UploadAttachAction);
                }
                return _uploadAttachCmd;
            }
        }

        void UploadAttachAction(object parameter)
        {
            OpenFileDialog dia = new OpenFileDialog();
            var rst = dia.ShowDialog();
            if (rst == null || (bool)rst == false)
            {
                return;
            }

            ctlHelpButton.Content = dia.FileName;

        }

        protected override void BeforeClose()
        {
            _model.complete_detail.comp_state = "已完成";
        }
    }
}
