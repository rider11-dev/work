using MyNet.Client.Public;
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
using MyNet.Components.WPF.Extension;
using MyNet.Components;
using MyNet.Client.Models;
using MyNet.Components.Result;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MyNet.Model.Auth;
using MyNet.Components.WPF.Controls;
using MyNet.Components.Extensions;
using MyNet.Components.Mapper;
using MyNet.Client.Command;
using MyNet.Components.Logger;
using MyNet.Components.WPF.Windows;
using MyNet.Model.Base;
using System.Threading;
using System.Windows.Threading;
using MyNet.Components.Http;
using MyNet.Model.Interface.Auth;

namespace MyNet.Client
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
            //设置header背景
            gridHeader.SetBackgroundImg(ClientContext.GetFullPath(ClientContext.Conf.header));
            panelFooter.SetBackgroundImg(ClientContext.GetFullPath(ClientContext.Conf.footer));
            imgTitle.SetSource(ClientContext.GetFullPath(ClientContext.Conf.title));
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
            //TODO：需要优化
            imgBtnUserInfo.MouseLeftButtonDown += MiscExtension.HandleMouseButtonEvent;
            imgBtnExit.MouseLeftButtonDown += MiscExtension.HandleMouseButtonEvent;

            InitMenutTree();
        }

        private void InitMenutTree()
        {
            _menuTreeData = (TreeViewData)menuTree.DataContext;

            var rst = HttpUtils.PostResult(ApiUtils.GetApiUrl(ApiKeys.GetPer), new { pk = ClientContext.CurrentUser.user_id }, ClientContext.Token);
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
            ClientContext.Pers = pers;

            //功能菜单
            var funcs = pers.Where(p => p.per_type == PermType.Func.ToString());
            if (funcs != null && funcs.Count() > 0)
            {
                List<TreeViewData.NodeViewModel> datas = new List<TreeViewData.NodeViewModel>();
                datas.Add(new TreeViewData.NodeViewModel
                {
                    Id = "main",
                    Label = "主页",
                    Order = "0",
                    Data = new Permission { per_uri = ClientContext.Conf.mainpage },
                    IconUri = "/MyNet.Client;component/Resources/img/icon_home.png",
                    HAlign = "Center"
                });
                foreach (var p in funcs)
                {
                    datas.Add(new TreeViewData.NodeViewModel
                    {
                        Id = p.per_code,
                        Label = p.per_name,
                        Parent = p.per_parent,
                        Order = p.per_sort,
                        Data = p,
                        DataId = p.per_id,
                        IconUri = p.per_icon,
                        HAlign = p.per_halign
                    });
                }
                _menuTreeData.Bind(datas);
            }

            //选中第一个节点（主页）
            menuTree.Select();
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
            _cmdOpenFunc.Execute(new OpenFuncParam { PageUri = per.per_uri, FuncCode = per.per_code });
            //报告当前位置
            lblCurrLocation.Text = node.GetNodePath();
        }

        private void gridHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                base.Resize();
                e.Handled = true;
            }
        }

    }
}
