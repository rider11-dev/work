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
using MyNet.Client.Public;
using MyNet.Model.Base;
using MyNet.Components.WPF.Windows;
using MyNet.Components.WPF.Extension;

namespace MyNet.Client.Pages.Auth
{
    /// <summary>
    /// PermissionDetailWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PermissionDetailWindow : BaseWindow
    {
        PermDetailViewModel _vmPermDetail;
        private PermissionDetailWindow()
        {
            _vmPermDetail = new PermDetailViewModel();
            _vmPermDetail.Window = this;
            this.DataContext = _vmPermDetail;
            this.AddModel(_vmPermDetail);//PermDetailViewModel.SelectedPermType绑定时，需要指定source，故添加

            InitializeComponent();
        }

        public PermissionDetailWindow(PermDetailViewModel vm = null)
            : this()
        {
            if (vm != null)
            {
                vm.CopyTo(_vmPermDetail);
            }
            base.Title = _vmPermDetail.IsNew ? "新增权限" : "修改权限";
            txtPerCode.IsReadOnly = _vmPermDetail.permdata.per_code.IsNotEmpty();
        }

        private void PermissionDetailWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _vmPermDetail.CanValidate = true;

            //设置权限类型
            DataCacheUtils.SetCmbSource(cbPermType, DictType.Perm, _vmPermDetail.permdata.per_type);
            //是否系统下拉框
            DataCacheUtils.SetEnumCmbSource<BoolType>(cbIsSystem, _vmPermDetail.permdata.per_system.ToString());
        }
    }
}
