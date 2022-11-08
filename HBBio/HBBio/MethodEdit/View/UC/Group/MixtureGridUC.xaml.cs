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
    /// MixtureGridUC.xaml 的交互逻辑
    /// </summary>
    public partial class MixtureGridUC : UserControl
    {
        public new object DataContext
        {
            get
            {
                return m_mixtureGrid;
            }
            set
            {
                base.DataContext = value;
                m_mixtureGrid = (MixtureGridVM)value;
                if (null != m_mixtureGrid)
                {
                    this.dgv.ItemsSource = m_mixtureGrid.MList;
                }
            }
        }
        private MixtureGridVM m_mixtureGrid = null;

        public static List<EnumString<EnumMonitorActionMethod>> MListAS = EnumString<EnumMonitorActionMethod>.GetEnumStringList("EnumMonitorAction_");

        /// <summary>
        /// 构造函数
        /// </summary>
        public MixtureGridUC()
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
        public void SetVisibility(Visibility pumpS, Visibility pumpA
            , Visibility pumpB, Visibility pumpC, Visibility pumpD
            , Visibility inS, Visibility inA, Visibility inB, Visibility inC, Visibility inD
            , Visibility ijv, Visibility bpv, Visibility cpv, Visibility outlet
            , Visibility as01, Visibility as02, Visibility as03, Visibility as04, Visibility mixer, Visibility uv)
        {
            colSampleFlowRate.Visibility = pumpS;
            colSystemFlowRate.Visibility = pumpA;

            colBS.Visibility = pumpB;
            colBE.Visibility = pumpB;
            colCS.Visibility = pumpC;
            colCE.Visibility = pumpC;
            colDS.Visibility = pumpD;
            colDE.Visibility = pumpD;

            colInS.Visibility = inS;
            colInA.Visibility = inA;
            colInB.Visibility = inB;
            colInC.Visibility = inC;
            colInD.Visibility = inD;
            colIJV.Visibility = ijv;
            colBPV.Visibility = bpv;
            colCPV.Visibility = cpv;
            colOut.Visibility = outlet;

            colAS01.Visibility = as01;
            colAS02.Visibility = as02;
            colAS03.Visibility = as03;
            colAS04.Visibility = as04;
            colMixer.Visibility = mixer;
            colUVClear.Visibility = uv;
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
            m_mixtureGrid.Add();
            dgv.SelectedIndex = dgv.Items.Count - 1;
        }

        /// <summary>
        /// 编辑行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (-1 != dgv.SelectedIndex)
            {
                MixtureGridItemWin dlg = new MixtureGridItemWin();
                dlg.DataContext = new MixtureGridItemVM(m_mixtureGrid.MMethodBaseValue) { MItem = DeepCopy.DeepCopyByXml(m_mixtureGrid.MList[dgv.SelectedIndex].MItem) };
                dlg.MLabTVCV = m_mixtureGrid.MBaseStr + "(" + m_mixtureGrid.MBaseUnitStr + ") : ";
                dlg.MLabSampleFlowRate = Share.ReadXaml.GetResources("labSampleFlowRate") + "(" + m_mixtureGrid.MFlowRateUnitStr + ") : ";
                dlg.MLabSystemFlowRate = Share.ReadXaml.GetResources("labSampleFlowRate") + "(" + m_mixtureGrid.MFlowRateUnitStr + ") : ";
                dlg.SetVisibility(colSampleFlowRate.Visibility, colSystemFlowRate.Visibility
                    , colBS.Visibility, colCS.Visibility, colDS.Visibility
                    , colInS.Visibility, colInA.Visibility, colInB.Visibility, colInC.Visibility, colInD.Visibility
                    , colIJV.Visibility, colBPV.Visibility, colCPV.Visibility, colOut.Visibility
                    , colMixer.Visibility, colUVClear.Visibility);
                if (true == dlg.ShowDialog())
                {
                    m_mixtureGrid.MList[dgv.SelectedIndex].MItem = ((MixtureGridItemVM)dlg.DataContext).MItem;
                    MixtureGridItemVM tmpValue = m_mixtureGrid.MList[dgv.SelectedIndex];
                    int tmpIndex = dgv.SelectedIndex;
                    m_mixtureGrid.MItem.MList.RemoveAt(tmpIndex);
                    m_mixtureGrid.MList.RemoveAt(tmpIndex);
                    m_mixtureGrid.MItem.MList.Insert(tmpIndex, tmpValue.MItem);
                    m_mixtureGrid.MList.Insert(tmpIndex, tmpValue);
                    dgv.SelectedIndex = tmpIndex;
                }
            }
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
                m_mixtureGrid.Del(dgv.SelectedIndex);
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
                m_mixtureGrid.Up(dgv.SelectedIndex);
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
                m_mixtureGrid.Down(dgv.SelectedIndex);
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
                m_mixtureGrid.Copy(dgv.SelectedIndex);
            }
        }

        /// <summary>
        /// 粘贴行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPaste_Click(object sender, RoutedEventArgs e)
        {
            m_mixtureGrid.Paste();
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
