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
    /// DetailOrg2NewWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DetailOrg2NewWindow : BaseWindow
    {
        private DetailOrg2NewWindow()
        {
            InitializeComponent();


            CmbModel model = cmbIsEstablish.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.CmbItemsYesNo);

            model = cmbEstablishType.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.CmbItemsEstablishType);

            model = cmbHasActPlace.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.CmbItemsYesNo);

            model = cmbSex.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.CmbItemsSex);

            model = cmbXL.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.CmbItemsXL);
        }

        public DetailOrg2NewWindow(Org2NewViewModel vm = null)
            : this()
        {
            base.Title = "两新组织";
        }

    }
}
