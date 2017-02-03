using System.Windows;
using MyNet.Components.WPF.Windows;
using MyNet.Client.Public;
using MyNet.Model.Base;
using MyNet.Components.WPF.Extension;
using MyNet.Client.Models.CustomQuery;
using MyNet.Model.CustomQuery;

namespace MyNet.Client.Pages.CustomQuery.Basic
{
    /// <summary>
    /// FieldDetailWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FieldDetailWindow : BaseWindow
    {
        private FieldDetailViewModel _vmField;

        private FieldDetailWindow()
        {
            _vmField = new FieldDetailViewModel() { Window = this };
            this.DataContext = _vmField;
            this.AddModel(_vmField);
            InitializeComponent();
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
            //_vmField.CanValidate = true;
            //设置字段类型
            DataCacheUtils.SetEnumCmbSource<FieldType>(cbFieldType, _vmField.fieldtype);
            //设置可见性
            DataCacheUtils.SetEnumCmbSource<BoolType>(cbVisible, _vmField.visible);
        }
    }
}
