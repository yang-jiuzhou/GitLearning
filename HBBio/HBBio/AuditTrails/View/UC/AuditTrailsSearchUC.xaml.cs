using System;
using System.Collections.Generic;
using System.Data;
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

namespace HBBio.AuditTrails
{
    /// <summary>
    /// AuditTrailsSearchUC.xaml 的交互逻辑
    /// </summary>
    public partial class AuditTrailsSearchUC : UserControl
    {
        #region 公开属性
        private DataTable _table = null;
        /// <summary>
        /// 数据表
        /// </summary>
        public DataTable Table
        {
            get
            {
                return _table;
            }
            set
            {
                _table = value;
                if (null != _table)
                {
                    TotalCount = _table.Rows.Count;
                    Bind();
                }
            }
        }

        private int _pageSize = 20;
        /// <summary>
        /// 每页显示记录数(默认20)
        /// </summary>
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (value > 0)
                {
                    _pageSize = value;
                }
                else
                {
                    _pageSize = 20;
                }
                PageCount = TotalCount / _pageSize;
                YuShu = _totalCount % PageSize;
            }
        }

        private int _currentPage = 1;
        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                if (value > 0)
                {
                    _currentPage = value;
                }
                else
                {
                    _currentPage = 1;
                }
            }
        }

        private int _totalCount = 0;
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount
        {
            get
            {
                return _totalCount;
            }
            set
            {
                if (value > 0)
                {
                    _totalCount = value;
                }
                else
                {
                    _totalCount = 0;
                }
                PageCount = _totalCount / PageSize;
                YuShu = _totalCount % PageSize;
                CurrentPage = 1;
            }
        }

        private int _pageCount = 0;
        /// <summary>
        /// 页数
        /// </summary>
        public int PageCount
        {
            get
            {
                return _pageCount;
            }
            set
            {
                if (value > 0)
                {
                    _pageCount = value;
                    intCurrPage.Maximum = _pageCount;
                }
                else
                {
                    _pageCount = 0;
                    intCurrPage.Maximum = 1;
                }
            }
        }

        private int _yuShu = 0;
        /// <summary>
        /// 最后一页剩余个数
        /// </summary>
        public int YuShu
        {
            get
            {
                return _yuShu;
            }
            set
            {
                if (value > 0)
                {
                    _yuShu = value;
                    PageCount++;
                }
                else
                {
                    _yuShu = 0;
                }
            }
        }

        public Visibility LoadingWaitVisibility
        {
            set
            {
                loadingWaitUC.Visibility = value;
            }
        }

        private int _startIndex = 0;    //当前页第一行序号
        private int _endIndex = 0;      //当前页最后一行序号
        private bool _first = true;
        private DataTable _curr = null;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public AuditTrailsSearchUC()
        {
            InitializeComponent();

            List<string> numList = new List<string>();
            numList.Add("20");
            numList.Add("50");
            numList.Add("100");
            cboxNum.ItemsSource = numList;
            cboxNum.SelectedIndex = 0;
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void Bind()
        {
            if (this.CurrentPage > this.PageCount)
            {
                this.CurrentPage = this.PageCount;
            }

            intCurrPage.Value = this.CurrentPage;
            labRowInfo.Text = GetRecordRegion();
            labPageCount.Text = this.PageCount.ToString();
            labRowCount.Text = this.TotalCount.ToString();

            if (null == Table)
            {
                return;
            }

            if (_first)
            {
                _curr = Table.Clone();
            }

            _curr.Rows.Clear();
            if (0 == this.PageCount)
            {
                Share.MessageBoxWin.Show(Share.ReadXaml.S_ErrorNoData);
                return;
            }
            else
            {
                for (int i = _startIndex - 1, j = 0; i < _endIndex; i++, j++)
                {
                    _curr.ImportRow(Table.Rows[i]);
                }
                dgv.ItemsSource = _curr.DefaultView;
            }

            if (_first)
            {
                _first = false;

                dgv.Columns[0].Visibility = Visibility.Collapsed;
                dgv.Columns[dgv.Columns.Count - 1].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }

            if (this.CurrentPage == 1)
            {
                this.btnBegin.IsEnabled = false;
                this.btnPrev.IsEnabled = false;
            }
            else
            {
                this.btnBegin.IsEnabled = true;
                this.btnPrev.IsEnabled = true;
            }

            if (this.CurrentPage == this.PageCount)
            {
                this.btnBack.IsEnabled = false;
                this.btnEnd.IsEnabled = false;
            }
            else
            {
                this.btnBack.IsEnabled = true;
                this.btnEnd.IsEnabled = true;
            }

            if (this.TotalCount == 0)
            {
                this.btnBegin.IsEnabled = false;
                this.btnPrev.IsEnabled = false;
                this.btnBack.IsEnabled = false;
                this.btnEnd.IsEnabled = false;
            }
        }

        /// <summary>
        /// 获取显示记录区间（格式如：1-20）
        /// </summary>
        /// <returns></returns>
        private string GetRecordRegion()
        {
            if (this.PageCount == 1) //只有一页
            {
                _startIndex = 1;
                _endIndex = this.TotalCount;
            }
            else  //有多页
            {
                if (this.CurrentPage == 1) //当前显示为第一页
                {
                    _startIndex = 1;
                    _endIndex = this.PageSize;
                }
                else if (this.CurrentPage == this.PageCount) //当前显示为最后一页
                {
                    _startIndex = (this.CurrentPage - 1) * this.PageSize + 1;
                    _endIndex = this.TotalCount;
                }
                else //中间页
                {
                    _startIndex = (this.CurrentPage - 1) * this.PageSize + 1;
                    _endIndex = this.CurrentPage * this.PageSize;
                }
            }

            return _startIndex + "-" + _endIndex;
        }

        /// <summary>
        /// 页数设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.PageSize = Convert.ToInt32(cboxNum.SelectedItem.ToString());
            this.Bind();
        }

        /// <summary>
        /// 第一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBegin_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentPage = 1;
            this.Bind();
        }

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentPage -= 1;
            this.Bind();
        }

        /// <summary>
        /// 跳转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentPage = (int)intCurrPage.Value;
            this.Bind();
        }

        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentPage += 1;
            this.Bind();
        }

        /// <summary>
        /// 最后一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnd_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentPage = this.PageCount;
            this.Bind();
        }
    }
}
