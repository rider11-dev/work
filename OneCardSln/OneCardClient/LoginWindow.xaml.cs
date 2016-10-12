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
using OneCardSln.Components.Result;
using Newtonsoft.Json;
using OneCardSln.Model.Auth;
using Newtonsoft.Json.Linq;
using OneCardSln.Components.Mapper;
using System.IO;

namespace OneCardSln.OneCardClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : BaseWindow
    {
        /*
         * TODO：改进点
         * 1、passwordbox，模型验证不起作用
         */
        LoginViewModel vmLogin;
        public LoginWindow()
        {
            vmLogin = new LoginViewModel();
            this.Resources.Add("vmLogin", vmLogin);

            InitializeComponent();
            //允许拖拽
            this.DragWhenLeftMouseDown();
            btnLogin.MouseLeftButtonDown += MiscExtension.HandleMouseButtonEvent;
        }

        private void btnLogin_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (!vmLogin.IsValid)
            {
                MessageWindow.ShowMsg(MessageType.Warning, "输入验证失败", vmLogin.Error);
                return;
            }
            if (!vmLogin.CheckVerifyCode())
            {
                MessageWindow.ShowMsg(MessageType.Warning, "输入验证失败", "验证码错误");
                return;
            }
            //登录
            var url = ApiHelper.GetApiUrl(ApiKeys.Login);
            var rst = HttpHelper.GetResultByPost(url, new { username = vmLogin.UserName, pwd = vmLogin.Pwd });
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Warning, "登录失败", rst.msg);
                return;
            }
            //登录成功，记录token
            Context.Token = rst.data.token;
            //获取用户信息
            rst = HttpHelper.GetResultByPost(ApiHelper.GetApiUrl(ApiKeys.GetUsr), new { pk = rst.data.usrid }, Context.Token);
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Warning, "获取用户信息失败", rst.msg);
                return;
            }
            var user = JsonConvert.DeserializeObject<User>(((JObject)rst.data).ToString());
            Context.CurrentUser = OOMapper.Map<User, UserViewModel>(user);

            new MainWindow().Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            vmLogin.CanValidate = true;
            GenerateVerifyCode();
            SetInputImgAddOn();
        }

        private void GenerateVerifyCode()
        {
            //获取验证码
            string file = AppDomain.CurrentDomain.BaseDirectory + "download/img/verifycode.jpg";
            var code = VerificationCodeHelper.Create();
            vmLogin.VerifyCodeTarget = code.Code;
            using (var stream = new MemoryStream(code.ImageBytes))
            {
                var img = System.Drawing.Image.FromStream(stream);
                img.Save(file);
            }
            txtVerifyCode.Tag = file;//将验证码图片路径赋给Tag，是因为模板中将图片的Source属性绑定到了目标控件的Tag
        }

        private void SetInputImgAddOn()
        {
            txtUsrName.BindImageAddOn();
            txtPwd.BindImageAddOn();
            txtVerifyCode.BindImageAddOn();
        }
    }
}
