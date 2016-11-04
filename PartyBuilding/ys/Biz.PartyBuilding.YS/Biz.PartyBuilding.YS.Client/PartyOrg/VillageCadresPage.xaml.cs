using MyNet.Client.Pages;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Biz.PartyBuilding.YS.Client.PartyOrg
{
    /// <summary>
    /// VillageCadresPage.xaml 的交互逻辑
    /// </summary>
    public partial class VillageCadresPage : BasePage
    {
        public VillageCadresPage()
        {
            InitializeComponent();

            CmbModel model = cmbSex.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.CmbItemsSex);

            model = cmbNation.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.CmbItemsNation);

            model = cmbXL.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.CmbItemsXL);
        }

        ICommand _searchCmd;
        public ICommand SearchCmd
        {
            get
            {
                if (_searchCmd == null)
                {
                    _searchCmd = new DelegateCommand(SearchAction);
                }

                return _searchCmd;
            }
        }

        void SearchAction(object parameter)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            new DetailVillageCadresWindow().ShowDialog();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            new DetailVillageCadresWindow().ShowDialog();

        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
