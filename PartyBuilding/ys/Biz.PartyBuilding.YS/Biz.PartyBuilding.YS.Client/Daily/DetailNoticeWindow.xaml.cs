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
    /// DetailNoticeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DetailNoticeWindow : BaseWindow
    {
        private DetailNoticeWindow()
        {
            InitializeComponent();


            CmbModel model = cmbNoticeType.DataContext as CmbModel;
            model.Bind(DailyContext.notice_types);

            model = cmbNoticeUrgency.DataContext as CmbModel;
            model.Bind(DailyContext.notice_urgency);

            model = cmbNoticeNeedReply.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.CmbItemsYesNo);


        }

        public DetailNoticeWindow(Org2NewViewModel vm = null)
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
    }
}
