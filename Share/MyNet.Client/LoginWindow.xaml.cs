using MyNet.Components;
using MyNet.Client.Models;
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
        static string LoginFile = MyContext.BaseDirectory + "login";
        /*
         * TODO：改进点
         * 1、passwordbox，模型验证不起作用
         * 2、输入框placeholder
         * 3、图片按钮模板改成image？（工具栏样式可以这么干，图片+文字）
         * 4、分页进度条！！！
         */
        LoginViewModel vmLogin;
        public LoginWindow()
        {
            vmLogin = LoadLoginViewModel();

            this.Resources.Add("model", vmLogin);

            InitializeComponent();
            //允许拖拽
            this.DragWhenLeftMouseDown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            vmLogin.CanValidate = true;
            GenerateVerifyCode();
            SetInputImgAddOn();

            //设置焦点
            if (vmLogin.RememberMe)
            {
                FocusManager.SetFocusedElement(this, txtVerifyCode);
            }
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
        }

        private void SetInputImgAddOn()
        {
            txtUsrName.BindImageAddOn();
            txtPwd.BindImageAddOn();
            txtVerifyCode.BindImageAddOn();
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
            var url = ApiHelper.GetApiUrl(ApiKeys.Login);
            var rst = HttpHelper.GetResultByPost(url, new { username = vmLogin.UserName, pwd = vmLogin.Pwd });
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Warning, OperationDesc.Login, rst.msg);
                return;
            }
            //登录成功，记录token
            MyContext.Token = rst.data.token;
            //获取用户信息
            rst = HttpHelper.GetResultByPost(ApiHelper.GetApiUrl(ApiKeys.GetUsr), new { pk = rst.data.usrid }, MyContext.Token);
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Warning, OperationDesc.GetUsr, rst.msg);
                return;
            }
            var user = JsonConvert.DeserializeObject<User>(((JObject)rst.data).ToString());
            MyContext.CurrentUser = OOMapper.Map<User, UserViewModel>(user);
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
            var loginData = OOMapper.Map<LoginViewModel, LoginData>(vmLogin);
            var buffer = Serializer.BinaryByteSerialize(loginData);
            using (FileStream fs = new FileStream(LoginFile, FileMode.Create, FileAccess.Write))
            {
                await fs.WriteAsync(buffer, 0, buffer.Length);
            }
        }

        private LoginViewModel LoadLoginViewModel()
        {
            LoginViewModel vm = new LoginViewModel();
            if (File.Exists(LoginFile))
            {
                var bytes = File.ReadAllBytes(LoginFile);
                var loginData = Serializer.BinaryByteDeserialize<LoginData>(bytes);
                vm = OOMapper.Map<LoginData, LoginViewModel>(loginData);
            }

            return vm;
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
