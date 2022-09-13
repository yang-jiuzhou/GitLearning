using HBBio.Share;
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
using System.Windows.Shapes;

namespace HBBio.Chromatogram
{
    /// <summary>
    /// CurveSetStyleWin.xaml 的交互逻辑
    /// </summary>
    public partial class CurveSetStyleWin : Window
    {
        private CurveSetStyleVM m_dataContext = null;
        public new object DataContext
        {
            get
            {
                return m_dataContext;
            }
            set
            {
                m_dataContext = (CurveSetStyleVM)value;

                spanel.DataContext = m_dataContext;
                dgv.ItemsSource = m_dataContext.MList;
                gridCheck.DataContext = m_dataContext;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public CurveSetStyleWin()
        {
            InitializeComponent();

            cboxXAxisScale.ItemsSource = EnumString<EnumAxisScale>.GetEnumStringList("Enum_");
            dgvSignalAxisScale.ItemsSource = EnumString<EnumAxisScale>.GetEnumStringList("Enum_");
        }

        /// <summary>
        /// 加载界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //dgv.ItemsSource = ((CurveSetStyleVM)this.DataContext).MList;
            //gridCheck.DataContext = this.DataContext;
        }

        /// <summary>
        /// 信号列表-选择实时数据颜色
        /// </summary>
        private void newColor_Click(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.ColorDialog dlg = new System.Windows.Forms.ColorDialog();
            dlg.Color = Share.ValueTrans.MediaToDraw(((SolidColorBrush)m_dataContext.MList[dgv.SelectedIndex].MModel.MBrush).Color);
            if (System.Windows.Forms.DialogResult.OK == dlg.ShowDialog())
            {
                m_dataContext.MList[dgv.SelectedIndex].MModel.MBrush = new SolidColorBrush(Share.ValueTrans.DrawToMedia(dlg.Color));
                MApp.DoEvents();
            }
        }
        
        /// <summary>
        /// 显示隐藏设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSignal_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (null != dgv.CurrentCell && 3 == dgv.CurrentCell.Column.DisplayIndex)
            {
                m_dataContext.MList[dgv.SelectedIndex].MModel.MShow = !m_dataContext.MList[dgv.SelectedIndex].MModel.MShow;
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (m_dataContext.MMin >= m_dataContext.MMax)
            {
                return;
            }

            int countShow = 0;
            foreach (var it in m_dataContext.MList)
            {
                if (it.MModel.MShow)
                {
                    countShow++;
                }

                if (it.MMin >= it.MMax)
                {
                    return;
                }
            }
            if (0 == countShow)
            {
                return;
            }

            DialogResult = true;
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
