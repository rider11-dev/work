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
        UserDetailViewModel vmUsr;
        public DetailPage()
        {
            vmUsr = new UserDetailViewModel();
            ClientContext.CurrentUser.CopyTo(vmUsr.userdata);

            this.DataContext = vmUsr;

            InitializeComponent();
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
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            vmUsr.CanValidate = true;
        }
    }
}
