using Biz.PartyBuilding.YS.Client.PartyOrg.Models;
using LiveCharts;
using LiveCharts.Wpf;
using MyNet.Components.Misc;
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

namespace Biz.PartyBuilding.YS.Client.PartyOrg.Query
{
    /// <summary>
    /// org.xaml 的交互逻辑
    /// </summary>
    public partial class org : Page
    {
        IEnumerable<OrgStrucViewModel> allOrgs = PartyBuildingContext.orgs;
        public SeriesCollection ColSeries { get; set; }
        public SeriesCollection PieSeries { get; set; }
        public List<string> ColLabels { get; set; }
        public Func<ChartPoint, string> PiePointLabel { get; set; }

        public static NumberRange[] DyRanges = new NumberRange[]
           {
                new NumberRange {Max=10,Unit="人" },
                new NumberRange {Min=10,Max=30,Unit="人" },
                new NumberRange {Min=30,Max=50,Unit="人" },
                new NumberRange {Min=50,Max=100,Unit="人" },
                new NumberRange {Min=100,Unit="人"}
           };

        public org()
        {
            InitializeComponent();
            DataContext = this;

            ColSeries = new SeriesCollection();
            PieSeries = new SeriesCollection();
            ColLabels = new List<string>();
            PiePointLabel = ChartHelper.PiePointLabel;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitTotal();
            InitColChart();

            radioType.Command.Execute("type");
            radioType.IsChecked = true;
        }

        private void InitTotal()
        {
            txtOrgTotal.Text = allOrgs.Count().ToString();
        }

        private void InitColChart()
        {
            var groups = allOrgs.GroupBy(m => m.town);

            ChartValues<int> values = new ChartValues<int>();
            foreach (var gp in groups)
            {
                ColLabels.Add(gp.Key);
                values.Add(gp.Count());
            }

            ColSeries.Add(new ColumnSeries
            {
                Title = "党组织个数",
                Values = values,
                Fill = new SolidColorBrush(Color.FromRgb(0, 255, 0))
            });
        }

        private ICommand _cmdLoadPies;
        public ICommand CmdLoadPies
        {
            get
            {
                if (_cmdLoadPies == null)
                {
                    _cmdLoadPies = new DelegateCommand(LoadPiesAction);
                }
                return _cmdLoadPies;
            }
        }
        private void LoadPiesAction(object parameter)
        {
            if (parameter == null)
            {
                return;
            }

            PieSeries.Clear();
            string pieType = parameter.ToString();
            switch (pieType)
            {
                case "town":
                    ChartHelper.LoadPies(PieSeries, allOrgs.GroupBy(m => m.town), PiePointLabel);
                    break;
                case "type":
                    ChartHelper.LoadPies(PieSeries, allOrgs.GroupBy(m => m.org_type), PiePointLabel);
                    break;
                case "dy_zs":
                    double min, max;
                    foreach (var range in DyRanges)
                    {
                        min = range.Min.HasValue ? (double)range.Min : 0;
                        max = range.Max.HasValue ? (double)range.Max : 100000;
                        ChartHelper.AddAPie(PieSeries, range.Title,
                            new ChartValues<int> { allOrgs.Count(m => m.dy_zs >= min && m.dy_zs < max) },
                            PiePointLabel);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
