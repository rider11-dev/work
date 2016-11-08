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
    /// DetailActRecordWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DetailActRecordWindow : BaseWindow
    {
        private DetailActRecordWindow()
        {
            InitializeComponent();


        }

        public DetailActRecordWindow(Org2NewViewModel vm = null)
            : this()
        {
            var act = DailyContext.party_acts.Where(a => a.party == "曹县县委组织部").FirstOrDefault();
            this.DataContext = act;

            if (act != null)
                dgAttaches.ItemsSource = act.attaches;
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

        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            var atts = dgAttaches.ItemsSource as List<PartyActRecordAttach>;
            if (atts == null || atts.Count < 1)
            {
                return;
            }
            var att = atts[0];
            if (string.IsNullOrEmpty(att.att_name))
            {
                return;
            }
            string fullPath = "";
            if (FileExtension.GetFileFullPath(AppDomain.CurrentDomain.BaseDirectory, att.att_name, out fullPath))
            {
                Process.Start(fullPath);
            }

        }
    }
}
