using Biz.PartyBuilding.YS.Client.PartyOrg.Models;
using MyNet.Client.Help;
using MyNet.Client.Pages;
using MyNet.Client.Public;
using MyNet.Components.WPF.Controls;
using MyNet.Components.WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// PartyMemberPage.xaml 的交互逻辑
    /// </summary>
    public partial class PartyMemberPage : BasePage
    {
        TreeViewData _gpTreeData = null;
        public PartyMemberPage()
        {
            InitializeComponent();
            _gpTreeData = (TreeViewData)gpTree.DataContext;

            CmbModel model = cmbSex.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.CmbItemsSex);

            model = cmbXL.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.CmbItemsXL);

            model = cmbAgeRange.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.CmbItemsAgeRange);

            model = cmbDyType.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.CmbItemsDyType);

            btnSearch.Click += (o, e) =>
            {
                Search();
            };
            btnAll.Click += (o, e) =>
            {
                Search(true);
            };
        }

        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            //组织树
            var nodes = TreeHelper.ParseGroupsTreeData(DataCacheHelper.AllGroups.Where(kvp => kvp.Value.gp_system == false).Select(kvp => kvp.Value));

            _gpTreeData.Bind(nodes);
        }

        private void menuTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Search(true);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new DetailPartyMemWindow().ShowDialog();
        }

        private void Search(bool all = false)
        {
            var items = PartyBuildingContext.partymembers;
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

            var mems = items.Where(p => p.party == node.Label);
            if (all)
            {
                dg.ItemsSource = mems;
                return;
            }

            if (cmbSex.SelectedItem != null)
            {
                mems = mems.Where(m => m.sex == (cmbSex.SelectedItem as CmbItem).Text);
            }
            var date = joinDate_Begin.Text;
            if (date.IsNotEmpty())
            {
                mems = mems.Where(m => !(string.Compare(m.join_in_time, date, true) < 0));
            }
            date = joinDate_End.Text;
            if (date.IsNotEmpty())
            {
                mems = mems.Where(m => !(string.Compare(m.join_in_time, date, true) > 0));
            }
            date = normalDate_Begin.Text;
            if (date.IsNotEmpty())
            {
                mems = mems.Where(m => !(string.Compare(m.zz_time, date, true) < 0));
            }
            date = normalDate_End.Text;
            if (date.IsNotEmpty())
            {
                mems = mems.Where(m => !(string.Compare(m.zz_time, date, true) > 0));
            }
            if (cmbXL.SelectedItem != null)
            {
                mems = mems.Where(m => m.xl == (cmbXL.SelectedItem as CmbItem).Text);
            }
            if (cmbAgeRange.SelectedItem != null)
            {
                var rangeTxt = (cmbAgeRange.SelectedValue as CmbItem).Text;
                string max = "200", min = "20";
                if (rangeTxt.Contains("以上"))//90以上
                {
                    min = "90";
                }
                else
                {
                    var arr = rangeTxt.Split('~');
                    min = arr[0];
                    max = arr[1];
                }
                mems = mems.Where(m => !(string.Compare(min, m.age) < 0) && !(string.Compare(min, m.age) > 0));
            }
            if (cmbDyType.SelectedItem != null)
            {
                mems = mems.Where(m => m.type == (cmbDyType.SelectedItem as CmbItem).Text);
            }

            dg.ItemsSource = mems;
        }

        private void btnZZ_Click(object sender, RoutedEventArgs e)
        {
            var mem = dg.SelectedItem as PartyMemberViewModel;
            if (mem == null || mem.type != "预备党员")
            {
                return;
            }
            mem.type = "正式党员";
            Search(true);

        }

        private void btnZybdw_Click(object sender, RoutedEventArgs e)
        {
            var mem = dg.SelectedItem as PartyMemberViewModel;
            if (mem == null || mem.type != "入党积极分子")
            {
                return;
            }
            mem.type = "预备党员";

            Search(true);
        }
    }
}
