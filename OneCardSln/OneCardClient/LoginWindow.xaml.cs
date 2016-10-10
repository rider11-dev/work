using OneCardSln.Components;
using OneCardSln.OneCardClient.Models;
using System;
using System.Collections.Generic;
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
using OneCardSln.Components.WPF.Extension;
using OneCardSln.OneCardClient.Public;

namespace OneCardSln.OneCardClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : BaseWindow
    {

        LoginViewModel vmLogin;
        public LoginWindow()
        {
            vmLogin = new LoginViewModel();
            this.Resources.Add("vmLogin", vmLogin);

            InitializeComponent();
            //允许拖拽
            this.DragWhenLeftMouseDown();
        }


        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLogin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;//终止事件路由，否则会进入Window_MouseLeftButtonDown，导致MouseLeftButtonUp不触发
        }

        private void btnLogin_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (!vmLogin.IsValid)
            {
                var rst = new MessageWindow(MessageType.Error, "出人命了", "有人跳楼了有人跳楼了有人跳楼了有人跳楼了有人跳楼了有人跳楼了有人跳楼了有人跳楼了有人跳楼了有人跳楼了有人跳楼了有人跳楼了有人跳楼了有人跳楼了有人跳楼了有人跳楼了有人跳楼了").ShowDialog();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            vmLogin.CanValidate = true;

            //获取验证码
            string file = AppDomain.CurrentDomain.BaseDirectory + "download/img/verifycode.jpg";
            //var img = HttpHelper.GetImage("http://localhost/aaa/api/common/misc/verifycode");
            //img.Save(file, ImageFormat.Jpeg);
            txtVerifyCode.Tag = file;
        }
    }
}
