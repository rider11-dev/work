using Biz.PartyBuilding.YS.Client.Sys;
using Biz.PartyBuilding.YS.Client.Sys.Models;
using MyNet.Client.Pages;
using MyNet.Client.Public;
using MyNet.Components.Extensions;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Biz.PartyBuilding.YS.Client.Learn
{
    /// <summary>
    /// PartyLearnPage.xaml 的交互逻辑
    /// </summary>
    public partial class PartyLearnPage : BasePage
    {
        public PartyLearnPage()
        {
            InitializeComponent();

        }


        ICommand _searchCmd;
        public ICommand SearchCmd
        {
            get
            {
                if (_searchCmd == null)
                {
                    _searchCmd = new DelegateCommand(SearchAction);
                }

                return _searchCmd;
            }
        }

        void SearchAction(object parameter)
        {

        }

        ICommand _viewAttachCmd;
        public ICommand ViewAttachCmd
        {
            get
            {
                if (_viewAttachCmd == null)
                {
                    _viewAttachCmd = new DelegateCommand(ViewAttachAction);
                }

                return _viewAttachCmd;
            }
        }

        void ViewAttachAction(object parameter)
        {
            var article = (Article)parameter;
            if (article == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(article.attach))
            {
                return;
            }
            string fullPath = "";
            if (FileExtension.GetFileFullPath(AppDomain.CurrentDomain.BaseDirectory, article.attach, out fullPath))
            {
                Process.Start(fullPath);

            }
        }


        private void btnView_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();

        }
        void LoadData()
        {
            var func = base.FuncCode;
            if (string.IsNullOrEmpty(func))
            {
                return;
            }
            var articles = SysContext.articles.Where(a => a.channel_code == func && a.state == "已发布");
            dg.ItemsSource = articles;
        }

    }
}
