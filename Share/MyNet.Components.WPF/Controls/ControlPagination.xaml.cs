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
using MyNet.Components.WPF.Extension;

namespace MyNet.Components.WPF.Controls
{
    /// <summary>
    /// 分页获取数据委托
    /// </summary>
    /// <param name="page"></param>
    public delegate void PageQueryHandler(PagingArgs page);
    /// <summary>
    /// ControlPagination.xaml 的交互逻辑
    /// </summary>
    public partial class ControlPagination : UserControl
    {
        public PageQueryHandler QueryHandler { get; set; }
        private int _defaultPageSize = 20;
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
                    txtPageSize.Text = this._defaultPageSize.ToString();
                    return _defaultPageSize;
                }
            }
            set
            {
                txtPageSize.Text = value.ToString();
                SetPageCount();
            }
        }

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
                SetPageCount();
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
        private int _pageIndex = 1;
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex
        {
            get { return _pageIndex; }
            set
            {
                if (value > 0)
                {
                    _pageIndex = value;
                }
            }
        }

        Action<PagingArgs> _actionSetPagination = null;

        public ControlPagination()
        {
            InitializeComponent();

            _actionSetPagination = page =>
            {
                //总记录数、总页数、页索引可能会变化，此处更新下
                this.PageIndex = page.PageIndex;
                this.PageCount = page.PageCount;
                this.RecordsCount = page.RecordsCount;

                this.txtPageIdx.Text = this.PageIndex.ToString();

                this.btnFirst.IsEnabled = !(this.PageIndex <= 1);
                this.btnPrev.IsEnabled = !(this.PageIndex <= 1);

                this.btnNext.IsEnabled = !(this.PageIndex >= this.PageCount);
                this.btnLast.IsEnabled = !(this.PageIndex >= this.PageCount);
            };
            //限制文本框输入：只能输入正整数
            string pattern = @"^[1-9]\d*$";
            txtPageIdx.LimitInput(pattern);
            txtPageSize.LimitInput(pattern);
        }

        private void SetPageCount()
        {
            if (this.RecordsCount > 0)
            {
                this.PageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(this.RecordsCount) / Convert.ToDouble(this.PageSize)));
            }
            else
            {
                this.PageCount = 0;
            }
            lblPageCount.Content = PageCount;
            lblRecordCount.Content = this.RecordsCount;
        }

        /// <summary>
        /// 分页控件数据绑定
        /// </summary>
        public void Bind()
        {
            //调用委托，查询分页数据
            PagingArgs page = new PagingArgs { PageSize = this.PageSize, PageIndex = this.PageIndex, PageCount = this.PageCount, RecordsCount = this.RecordsCount };
            QueryHandler(page);
            //设置分页信息显示
            _actionSetPagination(page);
        }

        public void GoFirst()
        {
            this.PageIndex = 1;
            Bind();
        }

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            GoFirst();
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            PageIndex--;
            Bind();
        }

        private void btnGoToPage_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtPageIdx.Text, out _pageIndex))
            {
                Bind();
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            PageIndex++;
            if (PageIndex > PageCount)
            {
                PageIndex = PageCount;
            }
            Bind();
        }

        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            PageIndex = PageCount;
            Bind();
        }

    }
}
