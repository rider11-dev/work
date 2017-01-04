using MyNet.Components;
using MyNet.Client.Models;
using System;
using System.Windows;
using System.Windows.Input;
using MyNet.Components.WPF.Extension;
using MyNet.Client.Public;
using MyNet.Components.Result;
using Newtonsoft.Json;
using MyNet.Model.Auth;
using Newtonsoft.Json.Linq;
using MyNet.Components.Mapper;
using System.IO;
using MyNet.Client.Models.Auth;
using MyNet.Components.Serialize;
using MyNet.Components.WPF.Windows;
using MyNet.Components.Logger;
using MyNet.Components.Http;
using MyNet.ViewModel.Auth.User;
using MyNet.Components.Emit;
using MyNet.Components.Misc;

namespace MyNet.Client
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : BaseWindow
    {
        /// <summary>
        /// 存储上次登录信息的文件
        /// </summary>
        static string LoginFile = ClientContext.BaseDirectory + "/login";
        /*
         * TODO：改进点
         * 1、passwordbox，模型验证不起作用
         * 2、输入框placeholder
         * 3、图片按钮模板改成image？（工具栏样式可以这么干，图片+文字）
         * 4、分页进度条！！！
         */
        LoginViewModel vmLogin;
        ILogHelper<LoginWindow> _logHelper = LogHelperFactory.GetLogHelper<LoginWindow>();
        public LoginWindow()
        {
            LoadLoginViewModel();

            this.DataContext = vmLogin;
            GenerateVerifyCode();
            InitializeComponent();
            //允许拖拽
            this.DragWhenLeftMouseDown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            vmLogin.CanValidate = true;
            //设置焦点
            if (vmLogin.RememberMe)
            {
                FocusManager.SetFocusedElement(this, txtVerifyCode);
            }
        }

        private void GenerateVerifyCode()
        {
            //获取验证码
            string file = new DirectoryInfo(ClientContext.BaseDirectory).Parent.Parent.FullName.TrimEnd() + "/verifycode.jpg";
            var code = VerificationCodeUtils.Create();
            vmLogin.VerifyCodeTarget = code.Code;
            using (var stream = new MemoryStream(code.ImageBytes))
            {
                var img = System.Drawing.Image.FromStream(stream);
                var fileInfo = new FileInfo(file);
                if (!fileInfo.Directory.Exists)
                {
                    fileInfo.Directory.Create();
                }
                img.Save(file);
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            if (!vmLogin.IsValid)
            {
                MessageWindow.ShowMsg(MessageType.Warning, OperationDesc.Validate, vmLogin.Error);
                return;
            }
            if (!vmLogin.CheckVerifyCode())
            {
                MessageWindow.ShowMsg(MessageType.Warning, OperationDesc.Validate, "验证码错误");
                return;
            }
            //登录
            var url = ApiUtils.GetApiUrl(ApiKeys.Login);
            var rst = HttpUtils.PostResult(url, new { username = vmLogin.logindata.username, pwd = vmLogin.logindata.pwd });
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Warning, OperationDesc.Login, rst.msg);
                return;
            }
            //登录成功，记录token
            ClientContext.Token = rst.data.token;
            //获取用户信息
            rst = HttpUtils.PostResult(ApiUtils.GetApiUrl(ApiKeys.GetUsr), new { pk = rst.data.usrid }, ClientContext.Token);
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Warning, OperationDesc.GetUsr, rst.msg);
                return;
            }
            var user = JsonConvert.DeserializeObject<User>(((JObject)rst.data).ToString());
            ClientContext.CurrentUser = new UserVM();
            OOMapper.Map(typeof(User), typeof(UserVM), user, ClientContext.CurrentUser);
            //记住我？
            RememberMe();

            new MainWindow().Show();
            this.Close();
        }

        private async void RememberMe()
        {
            if (!vmLogin.RememberMe)
            {
                if (File.Exists(LoginFile))
                {
                    File.Delete(LoginFile);
                }
                return;
            }
            //将vmLogin进行序列化，存储到指定文件
            var buffer = Serializer.BinaryByteSerialize(new LoginData
            {
                UserName = vmLogin.logindata.username,
                Pwd = vmLogin.logindata.pwd,
                RememberMe = vmLogin.RememberMe
            });
            using (FileStream fs = new FileStream(LoginFile, FileMode.Create, FileAccess.Write))
            {
                await fs.WriteAsync(buffer, 0, buffer.Length);
            }
        }

        private void LoadLoginViewModel()
        {
            vmLogin = new LoginViewModel();
            if (File.Exists(LoginFile))
            {
                var bytes = File.ReadAllBytes(LoginFile);
                var loginData = Serializer.BinaryByteDeserialize<LoginData>(bytes);
                if (vmLogin.logindata != null)
                {
                    vmLogin.logindata.username = loginData.UserName;
                    vmLogin.logindata.pwd = loginData.Pwd;
                    vmLogin.RememberMe = loginData.RememberMe;
                }
            }

        }

        [Serializable]
        public struct LoginData
        {
            public string UserName;
            public string Pwd;
            public bool RememberMe;
        }
    }
}
