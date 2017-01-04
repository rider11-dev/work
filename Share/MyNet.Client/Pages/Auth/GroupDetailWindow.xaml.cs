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
using MyNet.Components.WPF.Extension;

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
            model = new GroupDetailViewModel();
            this.DataContext = model;
            model.Window = this;

            this.AddModel(model);

            InitializeComponent();
        }

        public GroupDetailWindow(GroupDetailViewModel vmGroup = null)
            : this()
        {
            if (vmGroup != null)
            {
                vmGroup.CopyTo(model);
            }
            base.Title = model.IsNew ? "新增组织" : "修改组织";
            txtGpCode.IsReadOnly = model.groupdata.gp_code.IsNotEmpty();
        }

        private void GroupDetailWindow_Loaded(object sender, RoutedEventArgs e)
        {
            model.CanValidate = true;

            //是否系统下拉框
            DataCacheUtils.SetEnumCmbSource<BoolType>(cbIsSystem, model.groupdata.gp_system.ToString());
        }
    }
}
