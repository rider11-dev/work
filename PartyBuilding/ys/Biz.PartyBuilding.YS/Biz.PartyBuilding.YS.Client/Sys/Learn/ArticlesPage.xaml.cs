using Biz.PartyBuilding.YS.Client.Sys.Models;
using MyNet.Client.Pages;
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

namespace Biz.PartyBuilding.YS.Client.Sys.Learn
{
    /// <summary>
    /// ArticlesPage.xaml 的交互逻辑
    /// </summary>
    public partial class ArticlesPage : BasePage
    {
        public ArticlesPage()
        {
            InitializeComponent();

            var cmb = cmbChannel.DataContext as CmbModel;
            cmb.Bind(SysContext.channels);
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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {


        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnIssue_Click(object sender, RoutedEventArgs e)
        {
            var articles = SysContext.articles.Where(a => a.state == "编辑").ToList();
            articles.ForEach(a =>
            {
                a.state = "已发布";
                a.issue_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            });

            dg.ItemsSource = null;
            dg.ItemsSource = SysContext.articles;
        }
    }
}
