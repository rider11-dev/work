using Biz.PartyBuilding.YS.Client.PartyOrg.Models;
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
using MyNet.Components.Extensions;

namespace Biz.PartyBuilding.YS.Client.PartyOrg
{
    /// <summary>
    /// Org2NewPage.xaml 的交互逻辑
    /// </summary>
    public partial class Org2NewPage : BasePage
    {
        public Org2NewPage()
        {
            InitializeComponent();

            CmbModel model = cmbIsEstablish.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.CmbItemsYesNo);

            model = cmbEstablishType.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.CmbItemsEstablishType);

            model = cmbHasActPlace.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.CmbItemsYesNo);

            btnSearch.Click += (o, e) =>
            {
                SearchCmd.Execute(null);
            };

            btnAll.Click += (o, e) =>
            {
                dg.ItemsSource = PartyBuildingContext.org2news;
            };
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
            var items = PartyBuildingContext.org2news;
            if (items == null || items.Count() < 1)
            {
                return;
            }
            if (txtCompName.Text.IsNotEmpty())
            {
                items = items.Where(m => m.comp_name.Contains(txtCompName.Text));
            }
            if (cmbIsEstablish.SelectedItem != null)
            {
                CmbItem sel = cmbIsEstablish.SelectedItem as CmbItem;
                items = items.Where(m => m.is_dzz_establish == sel.Text);
            }
            if (cmbEstablishType.SelectedItem != null)
            {
                CmbItem sel = cmbEstablishType.SelectedItem as CmbItem;
                items = items.Where(m => m.dzz_establish_type == sel.Text);
            }
            if (cmbHasActPlace != null)
            {
                CmbItem sel = cmbHasActPlace.SelectedItem as CmbItem;
                items = items.Where(m => m.has_atc_place == sel.Text);
            }

            dg.ItemsSource = items;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            new DetailOrg2NewWindow().ShowDialog();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            new DetailOrg2NewWindow().ShowDialog();

        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
