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
    /// dy.xaml 的交互逻辑
    /// </summary>
    public partial class dy : Page
    {
        IEnumerable<PartyMemberViewModel> dyAll = PartyBuildingContext.dy;

        public SeriesCollection ColSeries { get; private set; }
        public SeriesCollection PieSeries { get; private set; }
        public List<string> ColLabels { get; private set; }
        public Func<ChartPoint, string> PiePointLabel { get; set; }


        public dy()
        {
            InitializeComponent();
            DataContext = this;
            ColSeries = new SeriesCollection();
            PieSeries = new SeriesCollection();
            ColLabels = new List<string>();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitTotal();

            InitColChart();

            PiePointLabel = ChartHelper.PiePointLabel;

            radioType.Command.Execute("type");
            radioType.IsChecked = true;
        }

        private void InitTotal()
        {
            txtDyTotal.Text = dyAll.Count().ToString();
            txtZsDy.Text = dyAll.Where(m => m.type == "正式党员").Count().ToString();
            txtYbDyTotal.Text = dyAll.Where(m => m.type == "预备党员").Count().ToString();
            txtJjfzTotal.Text = dyAll.Where(m => m.type == "入党积极分子").Count().ToString();
        }

        private void InitColChart()
        {
            var groups = dyAll.GroupBy(m => m.town);

            ChartValues<int> memValues = new ChartValues<int>();
            foreach (var gp in groups)
            {
                ColLabels.Add(gp.Key);
                memValues.Add(gp.Count());
            }

            ColSeries.Add(new ColumnSeries
            {
                Title = "党员人数",
                Values = memValues,
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
                case "sex":
                    ChartHelper.LoadPies(PieSeries, dyAll.GroupBy(m => m.sex), PiePointLabel);
                    break;
                case "nation":
                    ChartHelper.LoadPies(PieSeries, dyAll.GroupBy(m => m.nation), PiePointLabel);
                    break;
                case "dnzw":
                    ChartHelper.LoadPies(PieSeries, dyAll.GroupBy(m => m.dnzw), PiePointLabel);
                    break;
                case "xl":
                    ChartHelper.LoadPies(PieSeries, dyAll.GroupBy(m => m.xl), PiePointLabel);
                    break;
                case "type":
                    ChartHelper.LoadPies(PieSeries, dyAll.GroupBy(m => m.type), PiePointLabel);
                    break;
                case "age":
                    double min, max;
                    foreach (var range in ChartHelper.AgeRanges)
                    {
                        min = range.Min.HasValue ? (double)range.Min : 0;
                        max = range.Max.HasValue ? (double)range.Max : 100000;
                        ChartHelper.AddAPie(PieSeries, range.Title,
                            new ChartValues<int> { dyAll.Count(m => Convert.ToInt32(m.age) >= min && Convert.ToInt32(m.age) < max) },
                            PiePointLabel);
                    }
                    break;
                default:
                    break;
            }
        }

    }
}
