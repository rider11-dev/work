using MyNet.Client.Pages;
using MyNet.Components.WPF.Models;
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

namespace Biz.PartyBuilding.YS.Client.Evaluation
{
    /// <summary>
    /// EvaluateScorePage.xaml 的交互逻辑
    /// </summary>
    public partial class EvaluateScorePage : BasePage
    {
        public EvaluateScorePage()
        {
            InitializeComponent();

            CmbModel model = cmbSeason.DataContext as CmbModel;
            model.Bind(EvaluationContext.seasons);

            model = cmbMonth.DataContext as CmbModel;
            model.Bind(EvaluationContext.months);

        }

        private void dgRank_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var rows = e.AddedItems;
            if (rows == null || rows.Count < 1)
            {
                return;
            }
            var row = (dynamic)rows[0];
            try
            {
                var party = row.party;
                var details = EvaluationContext.score_check_result.Where(r => r.party == party);
                dgDetail.ItemsSource = details;
            }
            catch { }
        }
    }
}
