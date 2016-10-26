using MyNet.Components;
using MyNet.Components.Result;
using MyNet.Components.WPF.Windows;
using MyNet.ClientFrame.Models;
using MyNet.ClientFrame.Models.Account;
using MyNet.ClientFrame.Public;
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

namespace MyNet.ClientFrame.Pages.Account
{
    /// <summary>
    /// ChangePwdPage.xaml 的交互逻辑
    /// </summary>
    public partial class ChangePwdPage : BasePage
    {
        ChangePwdViewModel vmChangePwd = null;
        public ChangePwdPage()
        {
            vmChangePwd = new ChangePwdViewModel();
            this.Resources.Add("model", vmChangePwd);

            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            if (!vmChangePwd.IsValid)
            {
                MessageWindow.ShowMsg(MessageType.Warning, OperationDesc.Validate, vmChangePwd.Error);
                return;
            }
            string msg = "";
            if (!vmChangePwd.HandlyCheck(out msg))
            {
                MessageWindow.ShowMsg(MessageType.Warning, OperationDesc.Validate, msg);
                return;
            }
            var rst = HttpHelper.GetResultByPost(ApiHelper.GetApiUrl(ApiKeys.ChangePwd),
                new
                {
                    userid = Context.CurrentUser.user_id,
                    oldpwd = vmChangePwd.oldpwd,
                    newpwd = vmChangePwd.newpwd
                },
                Context.Token);
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Warning, OperationDesc.ChangePwd, rst.msg);
                return;
            }
            MessageWindow.ShowMsg(MessageType.Info, OperationDesc.ChangePwd, "密码修改成功");
            vmChangePwd.Reset();
            FocusManager.SetFocusedElement(this, txtOldPwd);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            vmChangePwd.CanValidate = true;
        }
    }
}
