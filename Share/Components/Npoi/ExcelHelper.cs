using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNet.Components.Extensions;

namespace MyNet.Components.Npoi
{
    public class ExcelHelper
    {
        public static bool Export(string filename, Func<Dictionary<string, string>> colHeadersFunc, IEnumerable<object> data)
        {
            if (data == null || data.Count() < 1 || string.IsNullOrEmpty(filename) || colHeadersFunc == null)
            {
                return false;
            }
            var colHeaders = colHeadersFunc();
            if (colHeaders == null || colHeaders.Count() < 1)
            {
                return false;
            }
            var colCount = colHeaders.Count;

            HSSFWorkbook book = new HSSFWorkbook();
            ISheet sheet = book.CreateSheet();

            int rowIdx = 0;
            IRow row = sheet.CreateRow(rowIdx);
            int colIdx = 0;
            foreach (var kvp in colHeaders)
            {
                row.CreateCell(colIdx).SetCellValue(kvp.Value);

                colIdx++;
            }
            rowIdx++;

            foreach (var item in data)
            {
                row = sheet.CreateRow(rowIdx++);
                colIdx = 0;
                foreach (var col in colHeaders)
                {
                    var val = item.GetPropertyValue(col.Key);
                    row.CreateCell(colIdx++).SetCellValue(val.IsEmpty() ? "" : val.ToString());
                }
            }

            using (MemoryStream ms = new MemoryStream())
            {
                book.Write(ms);
                using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
                {
                    byte[] bytes = ms.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Flush();
                }
            }
            book = null;
            return true;
        }

    }
}
