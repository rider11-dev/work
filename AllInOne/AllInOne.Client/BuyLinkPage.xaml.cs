using CefSharp;
using CefSharp.Wpf;
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

namespace AllInOne.Client
{
    /// <summary>
    /// BuyLinkPage.xaml 的交互逻辑
    /// </summary>
    public partial class BuyLinkPage : BasePage
    {
        ChromiumWebBrowser webView = null;

        public BuyLinkPage()
        {
            InitializeComponent();
        }

        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            InitWebBrowser();
        }

        private void InitWebBrowser()
        {
            WebBrowserUtils.Init();
            if (webView == null)
            {
                webView = new ChromiumWebBrowser();
                bdBrowser.Child = webView;
            }
            webView.SetOpenInSelfWindowOnly();
            webView.Address = AIOContext.Conf.url_buy;
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

            if (uriStr == "0" && webView.CanGoBack)
            {
                try
                {
                    webView.Back();
                }
                catch { }
                return;
            }

            if (uriStr == "1" && webView.CanGoForward)
            {
                try
                {
                    webView.Forward();
                }
                catch { }
            }
        }
    }
}
