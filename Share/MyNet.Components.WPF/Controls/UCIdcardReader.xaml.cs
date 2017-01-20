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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyNet.Components.WPF.Controls
{
    /// <summary>
    /// UCIdcardReader.xaml 的交互逻辑
    /// </summary>
    public partial class UCIdcardReader : UserControl
    {
        /// <summary>
        /// 是否自动读卡
        /// </summary>
        public bool AutoRead
        {
            get { return (bool)GetValue(AutoReadProperty); }
            set
            {
                SetValue(AutoReadProperty, value);
            }
        }

        public static readonly DependencyProperty AutoReadProperty = DependencyProperty.Register("AutoRead", typeof(bool), typeof(UCIdcardReader), new PropertyMetadata(false, null));
        /// <summary>
        /// 自动读卡时间间隔
        /// </summary>
        public int ReadInterval
        {
            get { return (int)GetValue(ReadIntervalProperty); }
            set
            {
                SetValue(ReadIntervalProperty, value);
                Model.SetInterval(value);
            }
        }

        public static readonly DependencyProperty ReadIntervalProperty = DependencyProperty.Register("ReadInterval", typeof(int), typeof(UCIdcardReader), new PropertyMetadata(5, null));

        public IdcardReaderViewModel Model = new IdcardReaderViewModel();
        public UCIdcardReader()
        {
            this.DataContext = Model;
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            btnRead.Visibility = AutoRead ? Visibility.Collapsed : Visibility.Visible;
            if (AutoRead)
            {
                Model.AutoRead();
            }
        }
    }
}
