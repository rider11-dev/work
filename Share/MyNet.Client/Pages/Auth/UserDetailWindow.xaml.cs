using MyNet.Client.Models;
using MyNet.Client.Models.Auth;
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
using MyNet.Components.Extensions;
using MyNet.Components.WPF.Windows;
using MyNet.ViewModel.Auth.User;

namespace MyNet.Client.Pages.Auth
{
    /// <summary>
    /// UserDetailWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UserDetailWindow : BaseWindow
    {
        UserDetailViewModel _vmUsrDetail;
        private UserDetailWindow()
        {
            InitializeComponent();

            _vmUsrDetail = new UserDetailViewModel();
            this.DataContext = _vmUsrDetail;
            _vmUsrDetail.Window = this;
        }

        public UserDetailWindow(UserDetailViewModel usrVM) : this()
        {
            if (usrVM != null)
            {
                usrVM.CopyTo(_vmUsrDetail);
            }
            base.Title = _vmUsrDetail.IsNew ? "新增用户" : "修改用户";
            txtUserName.IsReadOnly = _vmUsrDetail.userdata.user_name.IsNotEmpty();
        }

        private void UserDetailWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _vmUsrDetail.CanValidate = true;
        }
    }
}
