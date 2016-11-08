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

namespace Biz.PartyBuilding.YS.Client.Evaluation
{
    /// <summary>
    /// EvaluateWindow.xaml 的交互逻辑
    /// </summary>
    public partial class EvaluateWindow : BaseWindow
    {
        private EvaluateWindow()
        {
            InitializeComponent();


        }

        public EvaluateWindow(bool passornot)
            : this()
        {
            if (passornot)
            {
                tab_passsornot.Visibility = Visibility.Visible;
                tab_passsornot.IsSelected = true;

                tab_goodbad.Visibility = Visibility.Collapsed;
            }
            else
            {
                tab_passsornot.Visibility = Visibility.Collapsed;

                tab_goodbad.Visibility = Visibility.Visible;
                tab_goodbad.IsSelected = true;
            }
        }

    }
}
