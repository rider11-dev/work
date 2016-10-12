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

namespace OneCardSln.OneCardClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : BaseWindow
    {
        private TreeViewData _menuTreeData = null;
        //TODO——MainWindow要修改的
        /*
         * 1、欢迎登陆，动态用户名 √
         * 2、当前位置，根据菜单动态设置
         */
        public MainWindow()
        {
            InitializeComponent();
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
                List<TreeViewData.NodeData> datas = new List<TreeViewData.NodeData>();
                datas.Add(new TreeViewData.NodeData { Id = "main", Label = "主页", Order = "0" });
                foreach (var p in funcs)
                {
                    datas.Add(new TreeViewData.NodeData { Id = p.per_id, Label = p.per_name, Parent = p.per_parent, Order = p.per_sort, Data = p });
                }
                _menuTreeData.Bind(datas);
            }
        }
    }
}
