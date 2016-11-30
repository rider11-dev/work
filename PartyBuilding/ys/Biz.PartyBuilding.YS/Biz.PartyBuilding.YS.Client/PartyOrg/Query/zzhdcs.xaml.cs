using Biz.PartyBuilding.YS.Client.Models;
using LiveCharts;
using LiveCharts.Wpf;
using MyNet.Client.Public;
using MyNet.Components;
using MyNet.Components.Misc;
using MyNet.Components.Result;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    /// zzhdcs.xaml 的交互逻辑
    /// </summary>
    public partial class zzhdcs : Page
    {
        IEnumerable<PartyActAreaModel> allActPlaces = new List<PartyActAreaModel>();
        public SeriesCollection ColSeries { get; set; }
        public SeriesCollection PieSeries { get; set; }
        public List<string> ColLabels { get; set; }
        public Func<ChartPoint, string> PiePointLabel { get; set; }


        public static NumberRange[] LevelRanges = new NumberRange[]
           {
                new NumberRange {Min=1,Max=2,Unit="层" },
                new NumberRange {Min=2,Max=3,Unit="层" },
                new NumberRange {Min=3,Unit="层"}
           };

        public static NumberRange[] RoomRanges = new NumberRange[]
          {
                new NumberRange {Min=1,Max=2,Unit="间" },
                new NumberRange {Min=2,Max=5,Unit="间" },
                new NumberRange {Min=5,Unit="间"}
          };

        public static NumberRange[] AreaRanges = new NumberRange[]
         {
                new NumberRange {Max=60,Unit="㎡" },
                new NumberRange {Min=60,Max=80,Unit="㎡" },
                new NumberRange {Min=80,Max=100,Unit="㎡" },
                new NumberRange {Min=100,Unit="㎡"}
         };

        public zzhdcs()
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
            var rst = HttpHelper.GetResultByGet(ApiHelper.GetApiUrl(PartyBuildingApiKeys.AreaGet, PartyBuildingApiKeys.Key_ApiProvider_Party));
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, OperationDesc.Search, rst.msg);
                return;
            }
            if (rst.data != null && rst.data.infos != null)
            {
                allActPlaces = JsonConvert.DeserializeObject<IEnumerable<PartyActAreaModel>>(((JArray)rst.data.infos).ToString());
            }

            InitTotal();

            InitColChart();

            radioTown.Command.Execute("town");
            radioTown.IsChecked = true;
        }

        private void InitTotal()
        {
            txtCsTotal.Text = allActPlaces.Count().ToString();
        }

        private void InitColChart()
        {
            var groups = allActPlaces.GroupBy(m => m.town);

            ChartValues<int> values = new ChartValues<int>();
            foreach (var gp in groups)
            {
                ColLabels.Add(gp.Key);
                values.Add(gp.Count());
            }

            ColSeries.Add(new ColumnSeries
            {
                Title = "场所数",
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
            double min, max;
            switch (pieType)
            {
                case "town":
                    ChartHelper.LoadPies(PieSeries, allActPlaces.GroupBy(m => (string)m.town), PiePointLabel);
                    break;
                case "levels":
                    foreach (var range in LevelRanges)
                    {
                        min = range.Min.HasValue ? (double)range.Min : 0;
                        max = range.Max.HasValue ? (double)range.Max : 100000;
                        ChartHelper.AddAPie(PieSeries, range.Title,
                            new ChartValues<int> { allActPlaces.Count(m => Convert.ToInt32(m.levels) >= min && Convert.ToInt32(m.levels) < max) },
                            PiePointLabel);
                    }
                    break;
                case "rooms":
                    foreach (var range in RoomRanges)
                    {
                        min = range.Min.HasValue ? (double)range.Min : 0;
                        max = range.Max.HasValue ? (double)range.Max : 100000;
                        ChartHelper.AddAPie(PieSeries, range.Title,
                            new ChartValues<int> { allActPlaces.Count(m => Convert.ToInt32(m.rooms) >= min && Convert.ToInt32(m.rooms) < max) },
                            PiePointLabel);
                    }
                    break;
                case "area_jz":
                    foreach (var range in AreaRanges)
                    {
                        min = range.Min.HasValue ? (double)range.Min : 0;
                        max = range.Max.HasValue ? (double)range.Max : 100000;
                        ChartHelper.AddAPie(PieSeries, range.Title,
                            new ChartValues<int> { allActPlaces.Count(m => Convert.ToInt32(m.floor_area) >= min && Convert.ToInt32(m.floor_area) < max) },
                            PiePointLabel);
                    }
                    break;
                case "area_yl":
                    foreach (var range in AreaRanges)
                    {
                        min = range.Min.HasValue ? (double)range.Min : 0;
                        max = range.Max.HasValue ? (double)range.Max : 100000;
                        ChartHelper.AddAPie(PieSeries, range.Title,
                            new ChartValues<int> { allActPlaces.Count(m => Convert.ToInt32(m.courtyard_area) >= min && Convert.ToInt32(m.courtyard_area) < max) },
                            PiePointLabel);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
