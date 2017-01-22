using MyNet.Client.Pages;
using MyNet.Components.Extensions;
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

namespace  AllInOne.Client
{
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : BasePage
    {
        //MainViewModel model = null;
        public MainPage()
        {
            //model = new MainViewModel();
            //this.DataContext = model;
            InitializeComponent();
        }

        //private void Page_Loaded(object sender, RoutedEventArgs e)
        //{
        //    model.NavService = this.NavigationService;
        //}

        //private ICommand _navigateCmd;
        //public ICommand NavigateCmd
        //{
        //    get
        //    {
        //        if (_navigateCmd == null)
        //        {
        //            _navigateCmd = new DelegateCommand(NavigateAction);
        //        }
        //        return _navigateCmd;
        //    }
        //}

        //private void NavigateAction(object obj)
        //{
        //    if (obj == null)
        //    {
        //        return;
        //    }
        //    var uriStr = obj.ToString();
        //    if (uriStr.IsEmpty())
        //    {
        //        return;
        //    }
        //    Uri uri = new Uri(uriStr, UriKind.RelativeOrAbsolute);
        //    this.NavigationService.Navigate(uri);
        //}
    }
}
