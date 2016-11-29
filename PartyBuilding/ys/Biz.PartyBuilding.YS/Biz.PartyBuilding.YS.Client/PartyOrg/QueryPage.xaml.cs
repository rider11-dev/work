using MyNet.Client.Pages;
using MyNet.Components.WPF.Command;
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

namespace Biz.PartyBuilding.YS.Client.PartyOrg
{
    /// <summary>
    /// QueryPage.xaml 的交互逻辑
    /// </summary>
    public partial class QueryPage : BasePage
    {
        public QueryPage()
        {
            InitializeComponent();

            DataContext = this;
        }

        ICommand _cmdQuery;
        public ICommand CmdQuery
        {
            get
            {
                if (_cmdQuery == null)
                {
                    _cmdQuery = new DelegateCommand(QueryAction);
                }
                return _cmdQuery;
            }
        }
        private string uriTemplate = "pack://application:,,,/Biz.PartyBuilding.YS.Client;component/PartyOrg/Query/{0}.xaml";
        public void QueryAction(object parameter)
        {
            var type = (string)parameter;
            if (string.IsNullOrEmpty(type))
            {
                return;
            }
            queryFrame.Source = new Uri(string.Format(uriTemplate, type), UriKind.Absolute);
        }

        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            radioOrg2New.Command.Execute("org2new");
            radioOrg2New.IsChecked = true;
        }
    }
}
