using MyNet.Client.Models.Auth;
using MyNet.Components.WPF.Windows;
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

namespace MyNet.Client.Pages.Auth
{
    /// <summary>
    /// GroupDetailWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GroupDetailWindow : BaseWindow
    {
        GroupDetailViewModel model;
        private GroupDetailWindow()
        {
            InitializeComponent();

            model = this.DataContext as GroupDetailViewModel;
            model.Window = this;
        }

        public GroupDetailWindow(GroupViewModel vmGroup = null)
            : this()
        {
            if (vmGroup != null)
            {
                vmGroup.CopyTo(model);
            }
            base.Title = string.IsNullOrEmpty(model.gp_id) ? "新增组织" : "修改组织";
            txtGpCode.IsReadOnly = model.gp_code.IsNotEmpty();
        }

        private void GroupDetailWindow_Loaded(object sender, RoutedEventArgs e)
        {
            model.CanValidate = true;

            //是否系统下拉框
            DataCacheUtils.SetEnumCmbSource<BoolType>(cbIsSystem, model.gp_system);
        }
    }
}
