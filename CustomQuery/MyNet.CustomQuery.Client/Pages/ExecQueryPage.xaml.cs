using MyNet.Client.Pages;
using MyNet.CustomQuery.Client.Models;
using MyNet.Model;
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

namespace MyNet.CustomQuery.Client.Pages
{
    /// <summary>
    /// ExecQueryPage.xaml 的交互逻辑
    /// </summary>
    public partial class ExecQueryPage : BasePage
    {
        ExecQueryModel model = null;
        public ExecQueryPage()
        {
            InitializeComponent();

            model = this.DataContext as ExecQueryModel;
        }

        private void ExecQueryPage_Loaded(object sender, RoutedEventArgs e)
        {
            model.InitDataSources();
        }
    }
}
