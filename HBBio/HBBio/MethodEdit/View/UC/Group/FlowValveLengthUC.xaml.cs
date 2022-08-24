using HBBio.Communication;
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
    /// ColumnListUC.xaml 的交互逻辑
    /// </summary>
    public partial class FlowValveLengthUC : UserControl
    {
        public new object DataContext
        {
            get
            {
                return m_flowValveLength;
            }
            set
            {
                base.DataContext = value;
                m_flowValveLength = (FlowValveLengthVM)value;
                if (null != m_flowValveLength)
                {
                    this.dgv.ItemsSource = m_flowValveLength.MList;
                }
            }
        }
        private FlowValveLengthVM m_flowValveLength = null;

        
        /// <summary>
        /// 构造函数
        /// </summary>
        public FlowValveLengthUC()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置可见性
        /// </summary>
        /// <param name="inA"></param>
        /// <param name="inB"></param>
        /// <param name="inC"></param>
        /// <param name="inD"></param>
        /// <param name="bpv"></param>
        /// <param name="outlet"></param>
        /// <param name="pumpB"></param>
        /// <param name="pumpC"></param>
        /// <param name="pumpD"></param>
        public void SetVisibility(Visibility inA, Visibility inB, Visibility inC, Visibility inD
            , Visibility bpv, Visibility outlet
            , Visibility pumpB, Visibility pumpC, Visibility pumpD)
        {
            colInA.Visibility = inA;
            colInB.Visibility = inB;
            colInC.Visibility = inC;
            colInD.Visibility = inD;
            colBPV.Visibility = bpv;
            colOut.Visibility = outlet;

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
            m_flowValveLength.Add();
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
                m_flowValveLength.Del(dgv.SelectedIndex);
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
                m_flowValveLength.Up(dgv.SelectedIndex);
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
                m_flowValveLength.Down(dgv.SelectedIndex);
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
                m_flowValveLength.Copy(dgv.SelectedIndex);
            }
        }

        /// <summary>
        /// 粘贴行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPaste_Click(object sender, RoutedEventArgs e)
        {
            m_flowValveLength.Paste();
            dgv.SelectedIndex = dgv.Items.Count - 1;
        }

        /// <summary>
        /// 编辑完成的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (5 <= e.Column.DisplayIndex && e.Column.DisplayIndex <= 10)
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
            else if (12 == e.Column.DisplayIndex || e.Column.DisplayIndex == 16)
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
            else if (13 == e.Column.DisplayIndex)
            {
                TextBox obj = (TextBox)dgv.Columns[e.Column.DisplayIndex].GetCellContent(dgv.Items[e.Row.GetIndex()]);
                if (TextLegal.DoubleLegal(obj.Text))
                {
                    if (Convert.ToDouble(obj.Text) > StaticValue.s_maxFlowVol)
                    {
                        obj.Text = StaticValue.s_maxFlowVol.ToString();
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
