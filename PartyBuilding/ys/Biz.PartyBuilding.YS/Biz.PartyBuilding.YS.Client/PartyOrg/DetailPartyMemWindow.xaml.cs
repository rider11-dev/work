using Biz.PartyBuilding.YS.Client.PartyOrg.Models;
using MyNet.Components.WPF.Models;
using MyNet.Components.WPF.Windows;
using System;
using System.Collections.Generic;
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

namespace Biz.PartyBuilding.YS.Client.PartyOrg
{
    /// <summary>
    /// DetailPartyMemWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DetailPartyMemWindow : BaseWindow
    {
        public DetailPartyMemWindow()
        {
            InitializeComponent();


            CmbModel model = cmbType.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.CmbItemsDyType);

            model = cmbSex.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.CmbItemsSex);

            model = cmbNation.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.CmbItemsNation);

            model = cmbDnzw.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.CmbItemsDnzw);

            model = cmbXL.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.CmbItemsXL);

            model = cmbNowGzgw.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.CmbItemsNowGzgw);
        }
    }
}
