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
using OneCardSln.OneCardClient.Public;

namespace OneCardSln.OneCardClient.Pages.Auth
{
    /// <summary>
    /// PermissionDetailWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PermissionDetailWindow : BaseWindow
    {
        PermDetailViewModel _vmPermDetail;
        private PermissionDetailWindow()
        {
            InitializeComponent();

            _vmPermDetail = this.Resources["model"] as PermDetailViewModel;
            _vmPermDetail.Window = this;

            base.VmWindow = this.Resources["win"] as WindowViewModel;
        }

        public PermissionDetailWindow(PermViewModel vm = null)
            : this()
        {
            if (vm != null)
            {
                vm.CopyTo(_vmPermDetail);
            }
            base.Title = base.VmWindow.Title = string.IsNullOrEmpty(_vmPermDetail.per_id) ? "新增权限" : "修改权限";
            txtPerCode.IsReadOnly = _vmPermDetail.per_code.IsNotEmpty();
        }

        private void PermissionDetailWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _vmPermDetail.CanValidate = true;

            //设置权限类型
            DictHelper.SetSource(cbPermType, DictType.permtype);
        }
    }
}
