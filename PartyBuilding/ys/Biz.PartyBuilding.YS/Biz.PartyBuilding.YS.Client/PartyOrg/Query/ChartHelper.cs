using LiveCharts;
using LiveCharts.Wpf;
using MyNet.Components.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.PartyBuilding.YS.Client.PartyOrg.Query
{
    public class ChartHelper
    {
        public static NumberRange[] AgeRanges = new NumberRange[]
        {
            new NumberRange {Max=20,Unit="岁" },
            new NumberRange {Min=20,Max=30,Unit="岁" },
            new NumberRange {Min=30,Max=40,Unit="岁" },
            new NumberRange {Min=40,Max=50,Unit="岁" },
            new NumberRange {Min=50,Unit="岁"}
        };

        public static Func<ChartPoint, string> PiePointLabel = chartPoint =>
         {
             return string.Format("{0}（{1:P}）", chartPoint.Y, chartPoint.Participation);
         };

        public static void LoadPies(SeriesCollection seriesCollection, IEnumerable<IGrouping<string, dynamic>> groups, Func<ChartPoint, string> piePointLabel = null)
        {
            foreach (var gp in groups)
            {
                AddAPie(seriesCollection, gp.Key, new ChartValues<int> { gp.Count() }, piePointLabel);
            }
        }
        public static void AddAPie(SeriesCollection seriesCollection, string title, IChartValues values, Func<ChartPoint, string> piePointLabel = null)
        {
            PieSeries pie = new PieSeries
            {
                Title = title,
                Values = values,
                DataLabels = true,
                PushOut = 2,
                LabelPoint = piePointLabel
            };

            seriesCollection.Add(pie);
        }
    }
}
