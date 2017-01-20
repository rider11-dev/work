using mshtml;
using MyNet.Client.Pages;
using MyNet.Components.Extensions;
using MyNet.Components.WPF.Command;
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

namespace Card.Client
{
    /// <summary>
    /// LinkPage.xaml 的交互逻辑
    /// </summary>
    public partial class LinkPage : BasePage
    {
        public LinkPage()
        {
            InitializeComponent();

            browser.Navigating += (o, e) =>
            {
                (o as WebBrowser).SuppressScriptErrors();
            };
            browser.Navigated += (o, e) =>
            {
                (o as WebBrowser).SuppressScriptErrors();
            };
            browser.OpenInSelfWindowOnly();
        }

        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            browser.Navigate(CardContext.Conf.linkurl);
        }


        private ICommand _browserNavCmd;
        public ICommand BrowserNavCmd
        {
            get
            {
                if (_browserNavCmd == null)
                {
                    _browserNavCmd = new DelegateCommand(BrowserNavigateAction);
                }
                return _browserNavCmd;
            }
        }

        private void BrowserNavigateAction(object obj)
        {
            var uriStr = obj.ToString();
            if (uriStr == "0" && browser.CanGoBack)
            {
                try
                {
                    browser.GoBack();
                }
                catch { }
                return;
            }

            if (uriStr == "1" && browser.CanGoForward)
            {
                try
                {
                    browser.GoForward();
                }
                catch { }
            }
        }
    }
}
