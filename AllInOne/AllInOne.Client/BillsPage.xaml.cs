using MyNet.Client.Pages;
using MyNet.Components.WPF.Extension;
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

namespace AllInOne.Client
{
    /// <summary>
    /// BillsPage.xaml 的交互逻辑
    /// </summary>
    public partial class BillsPage : BasePage
    {
        OrderRecordsViewModel model = new OrderRecordsViewModel();
        public BillsPage()
        {
            this.DataContext = model;
            InitializeComponent();
            model.CtlPage = ctlPagination;
            model.IdcardReader = ucIdcardReader;
        }

        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            gridRecords.ShowRowNumber();
        }
    }
}
