using LiveCharts;
using LiveCharts.Wpf;
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
    /// dysj.xaml 的交互逻辑
    /// </summary>
    public partial class dysj : Page
    {
        IEnumerable<dynamic> allDysj = PartyBuildingContext.cadres;
        public SeriesCollection ColSeries { get; set; }
        public SeriesCollection PieSeries { get; set; }
        public List<string> ColLabels { get; set; }
        public Func<ChartPoint, string> PiePointLabel { get; set; }
        public dysj()
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
            txtDysjTotal.Text = allDysj.Count().ToString();
            txtMale.Text = allDysj.Where(m => m.sex == "男").Count().ToString();
            txtFemale.Text = allDysj.Where(m => m.sex == "女").Count().ToString();
        }

        private void InitColChart()
        {
            var groups = allDysj.GroupBy(m => m.town);

            ChartValues<int> values = new ChartValues<int>();
            foreach (var gp in groups)
            {
                ColLabels.Add((string)gp.Key);
                values.Add(gp.Count());
            }

            ColSeries.Add(new ColumnSeries
            {
                Title = "第一书记人数",
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
                case "type":
                    ChartHelper.LoadPies(PieSeries, allDysj.GroupBy(m => (string)m.type), PiePointLabel);
                    break;
                case "town":
                    ChartHelper.LoadPies(PieSeries, allDysj.GroupBy(m => (string)m.town), PiePointLabel);
                    break;
                case "sex":
                    ChartHelper.LoadPies(PieSeries, allDysj.GroupBy(m => (string)m.sex), PiePointLabel);
                    break;
                case "age":
                    double min, max;
                    foreach (var range in ChartHelper.AgeRanges)
                    {
                        min = range.Min.HasValue ? (double)range.Min : 0;
                        max = range.Max.HasValue ? (double)range.Max : 100000;
                        ChartHelper.AddAPie(PieSeries, range.Title,
                            new ChartValues<int> { allDysj.Count(m => Convert.ToInt32(m.age) >= min && Convert.ToInt32(m.age) < max) },
                            PiePointLabel);
                    }
                    break;
                case "nation":
                    ChartHelper.LoadPies(PieSeries, allDysj.GroupBy(m => (string)m.nation), PiePointLabel);
                    break;
                case "xl":
                    ChartHelper.LoadPies(PieSeries, allDysj.GroupBy(m => (string)m.xl), PiePointLabel);
                    break;
                default:
                    break;
            }
        }

    }
}
