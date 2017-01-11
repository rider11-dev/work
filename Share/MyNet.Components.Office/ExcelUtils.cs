using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNet.Components.Extensions;
using NPOI.XSSF.UserModel;
using System.Dynamic;
using System.Windows;

namespace MyNet.Components.Office
{
    public class ExcelUtils
    {
        public static bool Export(string filename, Func<Dictionary<string, string>> colHeadersFunc, IEnumerable<object> data)
        {
            Dictionary<string, string> colHeaders = null;
            if (colHeadersFunc != null)
            {
                colHeaders = colHeadersFunc();
            }
            return Export(filename, colHeaders, data);
        }

        public static bool Export(string filename, Dictionary<string, string> colHeaders, IEnumerable<object> data)
        {
            if (data == null || data.Count() < 1 || string.IsNullOrEmpty(filename) || colHeaders == null || colHeaders.Count < 1)
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

        public static List<dynamic> ReadFile(string filename,string sheetname="",Dictionary<int,string> cols=null)
        {
            List<dynamic> datas = new List<dynamic>();
            IWorkbook workbook=null;
            ISheet sheet = null;
            FileStream fs = null;
            try
            {
                int startRow = 1;
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                if (filename.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (filename.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);

                if (sheetname.IsNotEmpty())
                {
                    sheet = workbook.GetSheet(sheetname);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstDataRow = sheet.GetRow(startRow);
                    int cellCount = firstDataRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null)
                        {
                            continue; //没有数据的行默认是null　　　　　　　
                        }

                        ExpandoObject obj = new ExpandoObject();
                        var dict = obj as ICollection<KeyValuePair<string, object>>;
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                            {
                                dict.Add(new KeyValuePair<string, object>(cols == null ? j.ToString() : cols[j], row.GetCell(j).ToString()));
                            }
                        }
                        datas.Add(obj);
                    }
                }

                return datas;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "读取excel文件错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            finally
            {
                if(fs!=null)
                {
                    fs.Close();
                    fs.Dispose();
                    fs = null;
                }
                if(workbook!=null)
                {
                    workbook.Close();
                    workbook = null;
                }
            }
        }
    }
}
