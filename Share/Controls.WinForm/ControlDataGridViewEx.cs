using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyNet.Components.WinForm
{
    /// <summary>
    /// DataGridView扩展类
    /// </summary>
    public class ControlDataGridViewEx : DataGridView
    {
        /// <summary>
        /// 设置合并的列
        /// </summary>
        private List<string> MergeCols = new List<string>();
        public ControlDataGridViewEx()
        {
            InitGrid();
        }

        private void InitGrid()
        {
            this.CellPainting += dgv_CellPainting;
            this.RowPostPaint += dgv_RowPostPaint;
            this.AutoGenerateColumns = false;
            this.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        public void AddMergeCol(string colName)
        {
            if (!MergeCols.Contains(colName))
            {
                MergeCols.Add(colName);
            }
        }

        public void AddMergeColRange(string[] cols)
        {
            MergeCols.AddRange(cols);
        }

        private void dgv_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0 || !MergeCols.Contains(this.Columns[e.ColumnIndex].Name))
            {
                return;
            }

            // 对第1列相同单元格进行合并

            using (Brush gridBrush = new SolidBrush(this.GridColor), backColorBrush = new SolidBrush(e.CellStyle.BackColor))
            {
                using (Pen gridLinePen = new Pen(gridBrush))
                {
                    // 清除单元格
                    e.Graphics.FillRectangle(backColorBrush, e.CellBounds);

                    // 画 Grid 边线（仅画单元格的底边线和右边线）
                    // 如果下一行和当前行的数据不同，则在当前的单元格画一条底边线
                    if (e.RowIndex < this.Rows.Count - 1 &&
                    this.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Value.ToString() != e.Value.ToString())
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left,
                        e.CellBounds.Bottom - 1, e.CellBounds.Right - 1,
                        e.CellBounds.Bottom - 1);
                    // 画右边线
                    e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1,
                        e.CellBounds.Top, e.CellBounds.Right - 1,
                        e.CellBounds.Bottom);

                    // 画（填写）单元格内容，相同的内容的单元格只填写第一个
                    if (e.Value != null)
                    {
                        if (e.RowIndex > 0 && this.Rows[e.RowIndex - 1].Cells[e.ColumnIndex].Value.ToString() == e.Value.ToString())
                        { }
                        else
                        {
                            e.Graphics.DrawString((String)e.Value, e.CellStyle.Font,
                                Brushes.Black, e.CellBounds.X + 2,
                                e.CellBounds.Y + 5, StringFormat.GenericDefault);
                        }
                    }
                    e.Handled = true;
                }
            }
        }

        private void dgv_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                              e.RowBounds.Location.Y,
                              this.RowHeadersWidth,
                              e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                this.RowHeadersDefaultCellStyle.Font,
                rectangle,
                //this.dataGrid_dyxx.RowHeadersDefaultCellStyle.ForeColor,
                Color.Black,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }
    }
}
