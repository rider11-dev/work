using MyNet.Components;
using MyNet.Components.Result;
using MyNet.Components.WPF.Windows;
using MyNet.Client.Models;
using MyNet.Client.Models.Account;
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

namespace MyNet.Client.Pages.Account
{
    /// <summary>
    /// ChangePwdPage.xaml 的交互逻辑
    /// </summary>
    public partial class ChangePwdPage : BasePage
    {
        ChgPwdDetailViewModel vmChgPwd = null;
        public ChangePwdPage()
        {
            vmChgPwd = new ChgPwdDetailViewModel();
            vmChgPwd.chgpwddata.userid = ClientContext.CurrentUser.user_id;

            this.DataContext = vmChgPwd;

            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            DoChgPwd();
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            vmChgPwd.CanValidate = true;
        }

        private void DoChgPwd()
        {
            if (!vmChgPwd.IsValid)
            {
                MessageWindow.ShowMsg(MessageType.Warning, OperationDesc.Validate, vmChgPwd.Error);
                return;
            }
            string msg = "";
            if (!vmChgPwd.HandlyCheck(out msg))
            {
                MessageWindow.ShowMsg(MessageType.Warning, OperationDesc.Validate, msg);
                return;
            }
            var rst = HttpUtils.PostResult(ApiUtils.GetApiUrl(ApiKeys.ChangePwd), vmChgPwd.chgpwddata, ClientContext.Token);
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Warning, OperationDesc.ChangePwd, rst.msg);
                return;
            }
            MessageWindow.ShowMsg(MessageType.Info, OperationDesc.ChangePwd, "密码修改成功");
            vmChgPwd.CanValidate = false;
            vmChgPwd.Reset();
            vmChgPwd.CanValidate = true;
            FocusManager.SetFocusedElement(this, txtOldPwd);
        }
    }
}
