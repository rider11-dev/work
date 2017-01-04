using MyNet.Components;
using MyNet.Components.Mapper;
using MyNet.Components.Result;
using MyNet.Components.WPF.Windows;
using MyNet.Client.Models;
using MyNet.Client.Models.Account;
using MyNet.Client.Models.Auth;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MyNet.Components.Http;
using MyNet.ViewModel.Auth.User;

namespace MyNet.Client.Pages.Account
{
    /*
     * TODO
     * 1、区域使用弹出帮助选择
     */
    /// <summary>
    /// DetailPage.xaml 的交互逻辑
    /// </summary>
    public partial class DetailPage : BasePage
    {
        private UserDetailViewModel vmUsr;
        public DetailPage()
        {
            vmUsr = new UserDetailViewModel();
            SetUserData();

            this.DataContext = vmUsr;

            InitializeComponent();
        }

        private void SetUserData()
        {
            vmUsr.userdata.user_id = ClientContext.CurrentUser.user_id;
            vmUsr.userdata.user_name = ClientContext.CurrentUser.user_name;
            vmUsr.userdata.user_idcard = ClientContext.CurrentUser.user_idcard;
            vmUsr.userdata.user_truename = ClientContext.CurrentUser.user_truename;
            vmUsr.userdata.user_regioncode = ClientContext.CurrentUser.user_regioncode;
            vmUsr.userdata.user_remark = ClientContext.CurrentUser.user_remark;
            vmUsr.userdata.user_group = ClientContext.CurrentUser.user_group;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            DoChg();

        }

        private void DoChg()
        {
            if (!vmUsr.IsValid)
            {
                MessageWindow.ShowMsg(MessageType.Warning, OperationDesc.Validate, vmUsr.Error);
                return;
            }
            var rst = HttpUtils.PostResult(ApiUtils.GetApiUrl(ApiKeys.EditUsr), vmUsr.userdata, ClientContext.Token);
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Warning, OperationDesc.Edit, rst.msg);
                return;
            }
            MessageWindow.ShowMsg(MessageType.Info, OperationDesc.Edit, "修改成功");
            //更新用户缓存
            UpdateUsrCache();
        }

        private void UpdateUsrCache()
        {
            ClientContext.CurrentUser.user_idcard = vmUsr.userdata.user_idcard;
            ClientContext.CurrentUser.user_truename = vmUsr.userdata.user_truename;
            ClientContext.CurrentUser.user_regioncode = vmUsr.userdata.user_regioncode;
            ClientContext.CurrentUser.user_remark = vmUsr.userdata.user_remark;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            vmUsr.CanValidate = true;
        }
    }
}
