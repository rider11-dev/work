using Microsoft.Win32;
using MyNet.Client.Help;
using MyNet.Client.Pages;
using MyNet.Client.Public;
using MyNet.Components.Npoi;
using MyNet.Components.WPF.Controls;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Biz.PartyBuilding.YS.Client.PartyOrg
{
    /// <summary>
    /// PartymemAddrBookPage.xaml 的交互逻辑
    /// </summary>
    public partial class PartymemAddrBookPage : BasePage
    {
        public PartymemAddrBookPage()
        {
            InitializeComponent();

            _gpTreeData = (TreeViewData)gpTree.DataContext;
        }

        private void gpTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //dg.ItemsSource=PartyBuildingContext.DyPhones.Where()

            dg.ItemsSource = null;
            var node = (TreeViewData.TreeNode)e.NewValue;
            if (node == null)
            {
                return;
            }
            var phones = PartyBuildingContext.DyPhones.Where(p => p.dy_party == node.Label);
            dg.ItemsSource = phones;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new DetailPartyMemPhoneWindow().ShowDialog();
        }

        TreeViewData _gpTreeData = null;
        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            //组织树
            var nodes = TreeHelper.ParseGroupsTreeData(DataCacheHelper.AllGroups.Where(kvp => kvp.Value.gp_system == false).Select(kvp => kvp.Value));

            _gpTreeData.Bind(nodes);
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            var data = dg.ItemsSource as IEnumerable<dynamic>;
            if (data == null || data.Count() < 1)
            {
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog { Filter = "Excel Files|*.xls;*.xlsx", FileName = "党员通讯录" };
            var rst = saveFileDialog.ShowDialog();
            if (rst == null || ((bool)rst) == false)
            {
                return;
            }
            ExcelHelper.Export(saveFileDialog.FileName,
                () =>
                {
                    return new Dictionary<string, string>
                    {
                        {"dy_name","党员姓名"},
                        {"dy_phone","联系电话"},
                        {"dy_email","邮箱"},
                        {"dy_party","所属党组织"}
                    };
                }, data);
            MessageBoxResult dia = MessageBox.Show("文件已保存至" + saveFileDialog.FileName + Environment.NewLine + "是否打开？", "导出成功", MessageBoxButton.YesNo);
            if (dia == MessageBoxResult.Yes)
            {
                Process.Start(saveFileDialog.FileName);
            }
        }
    }
}
