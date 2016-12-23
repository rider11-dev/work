using Microsoft.Win32;
using MyNet.Components.Office;
using MyNet.Components.WPF.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MyNet.Components.WPF.Misc
{
    public class ExcelHelper
    {
        public static void Export(DataGrid dg, string defaultFileName, Dictionary<string, string> colHeaders = null)
        {
            if (dg == null)
            {
                return;
            }
            if (colHeaders == null)
            {
                colHeaders = new Dictionary<string, string>();
                foreach (var col in dg.Columns)
                {
                    if (col is DataGridTextColumn && col.Visibility == Visibility.Visible)
                    {
                        colHeaders.Add(((col as DataGridTextColumn).Binding as Binding).Path.Path, col.Header.ToString());
                    }
                }
            }

            var data = dg.ItemsSource as IEnumerable<object>;
            if (data == null || data.Count() < 1)
            {
                MessageWindow.ShowMsg(MessageType.Info, "导出", "没有数据");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog { Filter = "Excel Files|*.xls;*.xlsx", FileName = defaultFileName };
            var rst = saveFileDialog.ShowDialog();
            if (rst == null || rst == false)
            {
                return;
            }


            ExcelUtils.Export(saveFileDialog.FileName, colHeaders, data);
            MessageBoxResult dia = MessageBox.Show("文件已保存至" + saveFileDialog.FileName + Environment.NewLine + "是否打开？", "导出成功", MessageBoxButton.YesNo);
            if (dia == MessageBoxResult.Yes)
            {
                Process.Start(saveFileDialog.FileName);
            }
        }
    }
}
