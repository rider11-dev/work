using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyNet.Components.WinForm
{
    public static class DataGridViewExtension
    {
        public static void SetColumnAlignment(this DataGridView grid)
        {
            if (grid == null)
            {
                return;
            }
            foreach (DataGridViewColumn col in grid.Columns)
            {
                col.DefaultCellStyle.Alignment = col.ValueType == typeof(decimal) ? DataGridViewContentAlignment.MiddleRight : DataGridViewContentAlignment.MiddleCenter;
            }
        }

        public static void SetCheckBoxCol(this DataGridView grid, string checkColName)
        {
            DataGridViewCheckBoxHeaderCell ckHeaderCell = new DataGridViewCheckBoxHeaderCell();
            ckHeaderCell.OnCheckBoxClicked += (o, e) =>
            {
                grid.SuspendLayout();
                for (int i = grid.Rows.Count - 1; i >= 0; i--)
                {
                    grid.Rows[i].Cells[checkColName].Value = e.CheckedState;
                }
                grid.EndEdit();//不加的话，焦点所在行的选中状态受表头checkbox控制
                grid.ResumeLayout();
            };
            grid.Columns[checkColName].HeaderCell = ckHeaderCell;
            grid.Columns[checkColName].HeaderCell.Value = null;
        }
    }
}
