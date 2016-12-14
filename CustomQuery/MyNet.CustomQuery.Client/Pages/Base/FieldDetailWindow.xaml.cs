using System.Windows;
using MyNet.CustomQuery.Client.Models;
using MyNet.Components.WPF.Windows;
using MyNet.Client.Public;
using MyNet.CustomQuery.Model;
using MyNet.Model.Base;

namespace MyNet.CustomQuery.Client.Pages.Base
{
    /// <summary>
    /// FieldDetailWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FieldDetailWindow : BaseWindow
    {
        private FieldDetailViewModel _vmField;

        private FieldDetailWindow()
        {
            InitializeComponent();

            _vmField = this.DataContext as FieldDetailViewModel;
            _vmField.Window = this;
        }

        public FieldDetailWindow(FieldViewModel vmField = null) : this()
        {
            if (vmField != null)
            {
                vmField.CopyTo(_vmField);
            }
            base.Title = _vmField.IsNew ? "新增查询字段" : "修改查询字段";
        }

        private void FieldDetailWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _vmField.CanValidate = true;
            //设置字段类型
            DataCacheHelper.SetCmbSource(cbFieldType, CqDictType.FieldType, _vmField.fieldtype);
            //设置可见性
            DataCacheHelper.SetCmbSource(cbVisible, DictType.Bool, _vmField.visible);
        }
    }
}
