using MyNet.Components;
using MyNet.Components.Mapper;
using MyNet.Components.Result;
using MyNet.Components.WPF.Windows;
using MyNet.ClientFrame.Models;
using MyNet.ClientFrame.Models.Account;
using MyNet.ClientFrame.Models.Auth;
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
    /*
     * TODO
     * 1、区域使用弹出帮助选择
     */
    /// <summary>
    /// DetailPage.xaml 的交互逻辑
    /// </summary>
    public partial class DetailPage : BasePage
    {
        UserViewModel vmUsr;
        public DetailPage()
        {
            vmUsr = Context.CurrentUser;
            this.Resources.Add("model", vmUsr);
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            if (!vmUsr.IsValid)
            {
                MessageWindow.ShowMsg(MessageType.Warning, OperationDesc.Validate, vmUsr.Error);
                return;
            }
            var rst = HttpHelper.GetResultByPost(ApiHelper.GetApiUrl(ApiKeys.EditUsr),
                new
                {
                    user_id = vmUsr.user_id,
                    user_truename = vmUsr.user_truename,
                    user_idcard = vmUsr.user_idcard,
                    user_regioncode = vmUsr.user_regioncode,
                    user_remark = vmUsr.user_remark
                },
                Context.Token);
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
