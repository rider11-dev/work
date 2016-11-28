using Biz.PartyBuilding.YS.Client.Daily;
using MyNet.Client.Pages;
using MyNet.Components.Extensions;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Controls;
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
        TreeViewData _gpTreeData = null;
        public VillageCadresPage()
        {
            InitializeComponent();

            CmbModel model = cmbSex.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.CmbItemsSex);

            model = cmbAgeRange.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.CmbItemsAgeRange);

            model = cmbNation.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.CmbItemsNation);

            model = cmbXL.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.CmbItemsXL);

            _gpTreeData = (TreeViewData)gpTree.DataContext;


            btnSearch.Click += (o, e) =>
            {
                Search();
            };

            btnAll.Click += (o, e) =>
            {
                Search(true);
            };
        }

        private void Search(bool all = false)
        {
            var items = PartyBuildingContext.cadres;
            if (items.IsEmpty())
            {
                return;
            }

            var node = (TreeViewData.TreeNode)gpTree.SelectedValue;
            if (node == null)
            {
                return;
            }

            dg.ItemsSource = null;

            if (node.Level == 2)
            {
                items = items.Where(m => m.town == node.Label);
            }
            else if (node.Level == 3)
            {
                items = items.Where(m => m.village == node.Label);
            }
            if (all)
            {
                dg.ItemsSource = items;
                return;
            }

            if (txtName.Text.IsNotEmpty())
            {
                items = items.Where(m => ((string)m.name).Contains(txtName.Text));
            }
            if (cmbSex.SelectedItem != null)
            {
                items = items.Where(m => ((string)m.sex) == (cmbSex.SelectedValue as CmbItem).Text);
            }
            if (cmbNation.SelectedItem != null)
            {
                items = items.Where(m => ((string)m.nation) == (cmbNation.SelectedValue as CmbItem).Text);
            }
            if (cmbXL.SelectedItem != null)
            {
                items = items.Where(m => ((string)m.xl) == (cmbXL.SelectedValue as CmbItem).Text);
            }

            dg.ItemsSource = items;
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

        private void menuTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Search(true);
        }

        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            List<TreeViewData.NodeViewModel> nodes = new List<TreeViewData.NodeViewModel>();
            DailyContext.town_villages.ForEach(v =>
            {
                nodes.Add(new TreeViewData.NodeViewModel
                {
                    Id = v.id,
                    Label = v.name,
                    Parent = v.parent
                });
            });
            _gpTreeData.Bind(nodes);
        }
    }
}
