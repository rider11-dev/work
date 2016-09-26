using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OneCardSln.Components.Controls.WinForm
{
    public partial class ControlPagination : UserControl
    {
        /// <summary>
        /// 分页事件——获取分页数据
        /// </summary>
        public event GetDataByPageEventHandler GetDataByPageEvent;

        private int defaultPageSize = 50;

        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize
        {
            get
            {
                try
                {
                    return Convert.ToInt32(txtPageSize.Text);
                }
                catch
                {
                    txtPageSize.Text = this.defaultPageSize.ToString();
                    return defaultPageSize;
                }
            }
            set
            {
                txtPageSize.Text = value.ToString();
                GetPageCount();
            }
        }

        /// <summary>
        /// 总记录数
        /// </summary>
        private int _recordsCount = 0;

        /// <summary>
        /// 总记录数
        /// </summary>
        public int RecordsCount
        {
            get { return _recordsCount; }
            set
            {
                _recordsCount = value;
                GetPageCount();
            }
        }
        /// <summary>
        /// 总页数
        /// </summary>
        private int _pageCount = 0;
        /// <summary>
        /// 总页数=总记录数/每页显示记录数 
        /// </summary>
        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = value; }
        }

        /// <summary>
        /// 当前页
        /// </summary>
        private int _pageIndex = 0;
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex
        {
            get { return _pageIndex; }
            set { _pageIndex = value; }
        }

        public ControlPagination()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置页面大小
        /// </summary>
        private void GetPageCount()
        {
            if (this.RecordsCount > 0)
            {
                this.PageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(this.RecordsCount) / Convert.ToDouble(this.PageSize)));
            }
            else
            {
                this.PageCount = 0;
            }
            lblPageCount.Text = string.Format(" / 共 {0} 页", PageCount.ToString());
            lblTotalRecords.Text = string.Format("总计： {0} 条", this.RecordsCount);
        }

        /// <summary>
        /// 翻页控件数据绑定
        /// </summary>
        public void Bind()
        {
            Action<PagingArgs> actionUpdatePagination = pagination =>
            {
                //总记录数、总页数、页索引可能会变化，此处更新下
                this.PageIndex = pagination.PageIndex;
                this.PageCount = pagination.PageCount;
                this.RecordsCount = pagination.RecordsCount;

                this.txtPageIndex.Text = this.PageIndex.ToString();

                this.btnMoveFirst.Enabled = !(this.PageIndex <= 1);
                this.btnMovePrevious.Enabled = !(this.PageIndex <= 1);

                this.btnMoveNext.Enabled = !(this.PageIndex >= this.PageCount);
                this.btnMoveLast.Enabled = !(this.PageIndex >= this.PageCount);
            };
            //触发事件，查询分页数据
            PagingArgs args = new PagingArgs { PageSize = this.PageSize, PageIndex = this.PageIndex, PageCount = this.PageCount, RecordsCount = this.RecordsCount };
            this.GetDataByPageEvent(args);
            if (this.InvokeRequired)
            {
                this.Invoke(actionUpdatePagination, args);
            }
            else
            {
                actionUpdatePagination(args);
            }
        }

        public void QueryFirstPage()
        {
            this.PageIndex = 1;
            this.Bind();
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMoveFirst_Click(object sender, EventArgs e)
        {
            PageIndex = 1;
            this.Bind();
        }
        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMovePrevious_Click(object sender, EventArgs e)
        {
            PageIndex--;
            this.Bind();
        }
        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMoveNext_Click(object sender, EventArgs e)
        {
            PageIndex++;
            if (PageIndex > PageCount)
            {
                PageIndex = PageCount;
            }
            this.Bind();
        }
        /// <summary>
        /// 最后一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMoveLast_Click(object sender, EventArgs e)
        {
            PageIndex = PageCount;
            this.Bind();
        }
        /// <summary>
        /// 指定页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGoToPage_Click(object sender, EventArgs e)
        {
            if (int.TryParse(this.txtPageIndex.Text, out this._pageIndex))
            {
                this.Bind();
            }
        }
    }

    /// <summary>
    /// 分页委托——获取分页数据
    /// </summary>
    public delegate void GetDataByPageEventHandler(PagingArgs pagingArgs);

}
