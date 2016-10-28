using MyNet.Client.Help;
using MyNet.Client.Pages;
using MyNet.Client.Public;
using MyNet.Components.WPF.Controls;
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

namespace Biz.PartyBuilding.Client.Pages.Base
{
    /// <summary>
    /// PartyOrgMngPage.xaml 的交互逻辑
    /// </summary>
    public partial class PartyOrgMngPage : BasePage
    {
        private TreeViewData _treeGroupData;
        public PartyOrgMngPage()
        {
            InitializeComponent();

            _treeGroupData = ctlTreeGroup.Tree.DataContext as TreeViewData;

            Loaded += (o, e) =>
            {
                var nodes = TreeHelper.ParseGroupsTreeData(DataCacheHelper.AllGroups.Select(kvp => kvp.Value));
                _treeGroupData.Bind(nodes);
            };
        }
    }
}
