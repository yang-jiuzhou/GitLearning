using HBBio.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace HBBio.MethodEdit
{
    /// <summary>
    /// FlowRatePerListUC.xaml 的交互逻辑
    /// </summary>
    public partial class FlowRatePerUC : UserControl
    {
        private FlowRatePerVM m_flowRatePer = null;
        public new object DataContext
        {
            get
            {
                return m_flowRatePer;
            }
            set
            {
                base.DataContext = value;
                m_flowRatePer = (FlowRatePerVM)value;
                if (null != m_flowRatePer)
                {
                    dgv.ItemsSource = m_flowRatePer.MList;
                }
            }
        }

        public static readonly DependencyProperty BaseStrProperty = DependencyProperty.Register("MBaseStr", typeof(string), typeof(FlowRatePerUC), new PropertyMetadata(""));
        public string MBaseStr
        {
            get
            {
                return (string)GetValue(BaseStrProperty);
            }
            set
            {
                SetValue(BaseStrProperty, value);
            }
        }

        public static readonly DependencyProperty BaseUnitStrProperty = DependencyProperty.Register("MBaseUnitStr", typeof(string), typeof(FlowRatePerUC), new PropertyMetadata(""));
        public string MBaseUnitStr
        {
            get
            {
                return (string)GetValue(BaseUnitStrProperty);
            }
            set
            {
                SetValue(BaseUnitStrProperty, value);
            }
        }



        /// <summary>
        /// 构造函数
        /// </summary>
        public FlowRatePerUC()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置可见性
        /// </summary>
        /// <param name="pumpB"></param>
        /// <param name="pumpC"></param>
        /// <param name="pumpD"></param>
        public void SetVisibility(Visibility pumpB, Visibility pumpC, Visibility pumpD)
        {
            colBS.Visibility = pumpB;
            colBE.Visibility = pumpB;
            colCS.Visibility = pumpC;
            colCE.Visibility = pumpC;
            colDS.Visibility = pumpD;
            colDE.Visibility = pumpD;
        }

        /// <summary>
        /// 界面初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 添加行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            m_flowRatePer.Add();
            dgv.SelectedIndex = dgv.Items.Count - 1;
        }

        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (-1 != dgv.SelectedIndex)
            {
                int temp = dgv.SelectedIndex;
                m_flowRatePer.Del(dgv.SelectedIndex);
                dgv.SelectedIndex = temp - 1;
            }
        }

        /// <summary>
        /// 上移行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            if (0 < dgv.SelectedIndex)
            {
                int temp = dgv.SelectedIndex;
                m_flowRatePer.Up(dgv.SelectedIndex);
                dgv.SelectedIndex = temp - 1;
            }
        }

        /// <summary>
        /// 下移行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            if (-1 != dgv.SelectedIndex && dgv.Items.Count - 1 > dgv.SelectedIndex)
            {
                int temp = dgv.SelectedIndex;
                m_flowRatePer.Down(dgv.SelectedIndex);
                dgv.SelectedIndex = temp + 1;
            }
        }

        /// <summary>
        /// 复制行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            if (-1 != dgv.SelectedIndex)
            {
                m_flowRatePer.Copy(dgv.SelectedIndex);
            }
        }

        /// <summary>
        /// 粘贴行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPaste_Click(object sender, RoutedEventArgs e)
        {
            m_flowRatePer.Paste();
            dgv.SelectedIndex = dgv.Items.Count - 1;
        }

        private void dgv_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (0 <= e.Column.DisplayIndex && e.Column.DisplayIndex <= 5)
            {
                TextBox obj = (TextBox)dgv.Columns[e.Column.DisplayIndex].GetCellContent(dgv.Items[e.Row.GetIndex()]);
                if (TextLegal.DoubleLegal(obj.Text))
                {
                    if (Convert.ToDouble(obj.Text) > 100)
                    {
                        obj.Text = "100";
                    }
                    else if (Convert.ToDouble(obj.Text) < 0)
                    {
                        obj.Text = "0";
                    }
                }
            }
            else if (7 == e.Column.DisplayIndex)
            {
                TextBox obj = (TextBox)dgv.Columns[e.Column.DisplayIndex].GetCellContent(dgv.Items[e.Row.GetIndex()]);
                if (TextLegal.DoubleLegal(obj.Text))
                {
                    if (Convert.ToDouble(obj.Text) > DlyBase.MAX)
                    {
                        obj.Text = DlyBase.MAX.ToString();
                    }
                    else if (Convert.ToDouble(obj.Text) < 0)
                    {
                        obj.Text = "0";
                    }
                }
            }
        }
    }
}
