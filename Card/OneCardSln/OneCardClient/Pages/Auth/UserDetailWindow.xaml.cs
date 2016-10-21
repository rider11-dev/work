using OneCardSln.OneCardClient.Models;
using OneCardSln.OneCardClient.Models.Auth;
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

namespace OneCardSln.OneCardClient.Pages.Auth
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

            _vmUsrDetail = this.Resources["model"] as UserDetailViewModel;
            _vmUsrDetail.Window = this;

            base.VmWindow = this.Resources["win"] as WindowViewModel;
        }

        public UserDetailWindow(UserViewModel vm = null)
            : this()
        {
            if (vm != null)
            {
                vm.CopyTo(_vmUsrDetail);
            }

            base.Title = base.VmWindow.Title = string.IsNullOrEmpty(_vmUsrDetail.user_id) ? "新增用户" : "修改用户";
            txtUserName.IsReadOnly = _vmUsrDetail.user_name.IsNotEmpty();
        }

        private void UserDetailWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _vmUsrDetail.CanValidate = true;
        }
    }
}
