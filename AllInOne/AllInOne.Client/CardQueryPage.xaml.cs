using MyNet.Client.Pages;
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
    /// CardQueryPage.xaml 的交互逻辑
    /// </summary>
    public partial class CardQueryPage : BasePage
    {
        CardQueryViewModel model = null;
        public CardQueryPage()
        {
            model = new CardQueryViewModel();
            this.DataContext = model;
            InitializeComponent();

            ucIdcardReader.ReadInterval = AIOContext.Conf.card_read_interval;
            model.IdcardReader = ucIdcardReader;
        }
    }
}
