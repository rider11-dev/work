using Biz.PartyBuilding.YS.Client.Daily.Models;
using Biz.PartyBuilding.YS.Client.PartyOrg.Models;
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
    /// DetailViewDetailsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DetailViewDetailsWindow : BaseWindow
    {
        private DetailViewDetailsWindow()
        {
            InitializeComponent();

        }

        public DetailViewDetailsWindow(NoticeEntity vm = null)
            : this()
        {
            dg.ItemsSource = vm.view_details;
        }
    }
}
