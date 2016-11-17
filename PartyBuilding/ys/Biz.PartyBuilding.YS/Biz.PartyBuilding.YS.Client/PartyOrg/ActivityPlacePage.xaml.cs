using Biz.PartyBuilding.YS.Client.Daily;
using Biz.PartyBuilding.YS.Client.Models;
using Microsoft.Win32;
using MyNet.Client.Help;
using MyNet.Client.Pages;
using MyNet.Client.Public;
using MyNet.Components;
using MyNet.Components.Npoi;
using MyNet.Components.Result;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Controls;
using MyNet.Components.WPF.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
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
    /// ActivityPlacePage.xaml 的交互逻辑
    /// </summary>
    public partial class ActivityPlacePage : BasePage
    {
        public ActivityPlacePage()
        {
            InitializeComponent();

            _gpTreeData = (TreeViewData)gpTree.DataContext;
        }

        private void gpTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            GetAreas();

        }


        TreeViewData _gpTreeData = null;
        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            //组织树
            //var nodes = TreeHelper.ParseGroupsTreeData(DataCacheHelper.AllGroups.Where(kvp => kvp.Value.gp_system == false).Select(kvp => kvp.Value));
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

            GetAreas();
        }

        void GetAreas()
        {
            var rst = HttpHelper.GetResultByGet(ApiHelper.GetApiUrl(PartyBuildingApiKeys.AreaGet, PartyBuildingApiKeys.Key_ApiProvider_Party));
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, OperationDesc.Search, rst.msg);
                return;
            }
            if (rst.data != null && rst.data.infos != null)
            {
                var areas = JsonConvert.DeserializeObject<IEnumerable<PartyActAreaModel>>(((JArray)rst.data.infos).ToString());
                //
                var node = gpTree.SelectedItem as TreeViewData.TreeNode;
                if (node != null)
                {
                    if (node.Level == 2)
                    {
                        areas = areas.Where(a => a.town == node.Label);
                    }
                    else if (node.Level == 3)
                    {
                        areas = areas.Where(a => a.village == node.Label);
                    }
                }

                dg.ItemsSource = areas;
            }
        }

        ICommand _viewPicCmd;
        public ICommand ViewPicCmd
        {
            get
            {
                if (_viewPicCmd == null)
                {
                    _viewPicCmd = new DelegateCommand(ViewPicAction);
                }
                return _viewPicCmd;
            }
        }

        void ViewPicAction(object parameter)
        {
            var area = (PartyActAreaModel)parameter;
            if (area == null || area.pic == null || area.pic.Count < 1)
            {
                return;
            }

            var picContent = area.pic[0];
            if (!string.IsNullOrEmpty(picContent))
            {
                System.Drawing.Image img = ImageHelper.Base64Decode(picContent);
                string file = AppDomain.CurrentDomain.BaseDirectory + "temp.jpg";
                img.Save(file, ImageFormat.Jpeg);
                img.Dispose();
                img = null;
                Process.Start(file);
            }

        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            var area = dg.SelectedItem as PartyActAreaModel;
            if (area == null)
            {
                return;
            }
            new DetailActivityPlaceWindow(Daily.InfoOptType.View, area).ShowDialog();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var rst = new DetailActivityPlaceWindow(Daily.InfoOptType.InsertOrUpdate).ShowDialog();
            if (rst != null && (bool)rst == true)
            {
                GetAreas();
            }
        }
    }
}
