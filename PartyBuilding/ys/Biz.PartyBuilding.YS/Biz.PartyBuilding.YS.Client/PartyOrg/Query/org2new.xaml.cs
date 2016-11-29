using LiveCharts;
using LiveCharts.Wpf;
using MyNet.Components.Misc;
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
    /// org2new.xaml 的交互逻辑
    /// </summary>
    public partial class org2new : Page
    {
        public SeriesCollection ColSeries { get; private set; }
        public SeriesCollection PieSeries { get; private set; }
        public List<string> ColLabels { get; private set; }
        public Func<ChartPoint, string> PiePointLabel { get; set; }
        public NumberRange[] NumRanges = new NumberRange[]
        {
            new NumberRange {Max=10 },
            new NumberRange {Min=10,Max=20 },
            new NumberRange {Min=20,Max=30 },
            new NumberRange {Min=30}
        };

        public org2new()
        {
            InitializeComponent();
            ColLabels = new List<string>();
            ColSeries = new SeriesCollection();
            PieSeries = new SeriesCollection();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitColChart();

            PiePointLabel = chartPoint => { return string.Format("{0}（{1:P}）", chartPoint.Y, chartPoint.Participation); };

            pieByDyCount.IsChecked = true;

            DataContext = this;
        }

        private void InitColChart()
        {
            ChartValues<int> memValues = new ChartValues<int>();
            ChartValues<int> empValues = new ChartValues<int>();
            foreach (var org in PartyBuildingContext.org2news)
            {
                ColLabels.Add(org.comp_name);
                memValues.Add(org.mem_count_dy);
                empValues.Add(org.emp_count);
            }

            ColSeries.Add(new ColumnSeries
            {
                Title = "党员数",
                Values = memValues,
                Fill = new SolidColorBrush(Color.FromRgb(0, 255, 0))
            });
            ColSeries.Add(new ColumnSeries
            {
                Title = "职工数",
                Values = empValues,
                Fill = new SolidColorBrush(Color.FromRgb(0, 0, 255))
            });
        }

        private void LoadPieChart(int flag = 0)
        {
            PieSeries.Clear();

            PieSeries pie = null;
            foreach (var range in NumRanges)
            {
                double min = range.Min.HasValue ? (double)range.Min : 0;
                double max = range.Max.HasValue ? (double)range.Max : 100000;
                pie = new PieSeries
                {
                    Title = range.Title,
                    Values = new ChartValues<int>
                    {
                        PartyBuildingContext.org2news.Count(
                            o =>flag==0?
                            o.mem_count_dy >= min&& o.mem_count_dy < max:
                            o.emp_count >= min && o.emp_count < max
                            )
                    },
                    DataLabels = true,
                    PushOut = 2,
                    LabelPoint = PiePointLabel
                };
                PieSeries.Add(pie);
            }
        }

        private void pieByDyCount_Click(object sender, RoutedEventArgs e)
        {
            LoadPieChart(0);
        }

        private void pieByEmpCount_Click(object sender, RoutedEventArgs e)
        {
            LoadPieChart(1);

        }
    }
}
