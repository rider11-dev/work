using OneCardSln.OneCardClient.Public;
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
using OneCardSln.Components.WPF.Extension;
using OneCardSln.Components;
using OneCardSln.OneCardClient.Models;
using OneCardSln.Components.Result;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OneCardSln.Model.Auth;
using OneCardSln.Components.WPF.Controls;
using OneCardSln.Components.Extensions;
using OneCardSln.Components.Mapper;
using OneCardSln.OneCardClient.Command;
using OneCardSln.Components.Logger;

namespace OneCardSln.OneCardClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : BaseWindow
    {
        private TreeViewData _menuTreeData = null;
        private OpenFuncCmd _cmdOpenFunc = new OpenFuncCmd();
        private ILogHelper<MainWindow> _logHelper = LogHelperFactory.GetLogHelper<MainWindow>();
        //TODO——MainWindow要修改的
        /*
         * 1、欢迎登陆，动态用户名 √
         * 2、当前位置，根据菜单动态设置√
         */
        public MainWindow()
        {
            InitializeComponent();

            _cmdOpenFunc.Container = framePage;
        }

        private void imgBtnUserInfo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageWindow.ShowMsg(MessageType.Info, "测试", "切换到用户信息界面");
        }

        private void imgBtnExit_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //追加退出前处理
            //TODO
            this.Close();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _menuTreeData = (TreeViewData)this.FindResource("menuTreeData");

            //TODO：需要优化
            imgBtnUserInfo.MouseLeftButtonDown += MiscExtension.HandleMouseButtonEvent;
            imgBtnExit.MouseLeftButtonDown += MiscExtension.HandleMouseButtonEvent;

            InitMenutTree();
        }

        private void InitMenutTree()
        {
            var rst = HttpHelper.GetResultByPost(ApiHelper.GetApiUrl(ApiKeys.GetPer), new { pk = "admin" }, Context.Token);
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Warning, "获取权限失败", rst.msg);
                return;
            }
            if (rst.data == null || rst.data.pers == null)
            {
                MessageWindow.ShowMsg(MessageType.Warning, "获取权限失败", "未能获得用户权限");
                return;
            }
            var pers = JsonConvert.DeserializeObject<IEnumerable<Permission>>(((JArray)rst.data.pers).ToString());
            Context.Pers = pers;

            //功能菜单
            var funcs = pers.Where(p => p.per_type.ToEnum<PermType>() == PermType.Func);
            if (funcs != null && funcs.Count() > 0)
            {
                List<TreeViewData.NodeViewModel> datas = new List<TreeViewData.NodeViewModel>();
                datas.Add(new TreeViewData.NodeViewModel { Id = "main", Label = "主页", Order = "0", Data = new Permission { per_uri = "MainPage.xaml" } });
                foreach (var p in funcs)
                {
                    datas.Add(new TreeViewData.NodeViewModel { Id = p.per_id, Label = p.per_name, Parent = p.per_parent, Order = p.per_sort, Data = p });
                }
                _menuTreeData.Bind(datas);
            }

            //选中第一个节点（主页）
            if (menuTree.Items != null && menuTree.Items.Count > 0)
            {
                TreeViewItem item = ((TreeViewItem)menuTree.ItemContainerGenerator.ContainerFromIndex(0));
                item.IsSelected = true;
                item.Focus();
            }
        }

        private void menuTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            e.Handled = true;
            TreeViewData.TreeNode node = e.NewValue as TreeViewData.TreeNode;
            if (node == null)
            {
                _logHelper.LogInfo(string.Format("{1}。{0}未能解析TreeViewData.TreeNodeData。", Environment.NewLine, OperationDesc.OpenFunc));
                MessageWindow.ShowMsg(MessageType.Error, OperationDesc.OpenFunc, MsgConst.Msg_ViewAppLog);
                return;
            }
            if (node.Data == null)
            {
                _logHelper.LogInfo(string.Format("{1}。{0}未能解析TreeViewData.TreeNodeData。{0}FuncName:{2}", Environment.NewLine, OperationDesc.OpenFunc, node.Label));
                MessageWindow.ShowMsg(MessageType.Error, OperationDesc.OpenFunc, MsgConst.Msg_ViewAppLog);
                return;
            }
            if (node.SubNodes != null && node.SubNodes.Count > 0)
            {
                return;
            }
            var per = node.Data as Permission;
            _cmdOpenFunc.Execute(new OpenFuncParam { PageUri = per.per_uri, FuncId = per.per_id });
            //报告当前位置
            lblCurrLocation.Text = node.GetNodePath();
        }
    }
}
