using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace HBBio.Communication
{
    /// <summary>
    /// EditProcessPictureWin.xaml 的交互逻辑
    /// </summary>
    public partial class EditProcessPictureWin : Window
    {
        private InstrumentSize m_size = null;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="baseinstrumentList"></param>
        /// <param name="ipList"></param>
        /// <param name="size"></param>
        public EditProcessPictureWin(Window parent, List<BaseInstrument> baseinstrumentList, List<InstrumentPoint> ipList, InstrumentSize size, List<Point> listCircle)
        {
            InitializeComponent();

            this.Owner = parent;

            this.processPictureUC.UpdateItems(baseinstrumentList, ipList, listCircle);

            cboxType.ItemsSource = Enum.GetNames(typeof(EnumLineType));
            cboxType.SelectedIndex = 0;

            m_size = size;
            border.Width = m_size.MWidth;
            border.Height = m_size.MHeight;
            doubleWidth.Value = m_size.MWidth;
            doubleHeight.Value = m_size.MHeight;

            intCircle.Value = listCircle.Count;
        }

        private void txtLeft_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                intX.Value = Convert.ToInt32(txtLeft.Text);
            }
            catch { }
        }

        private void txtTop_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                intY.Value = Convert.ToInt32(txtTop.Text);
            }
            catch { }
        }

        private void btnSet_Click(object sender, RoutedEventArgs e)
        {
            this.processPictureUC.MPointPosition = new Point((double)intX.Value, (double)intY.Value);
        }

        private void btnHVAdd_Click(object sender, RoutedEventArgs e)
        {
            this.processPictureUC.MLineType = (EnumLineType)cboxType.SelectedIndex;
            this.processPictureUC.MConnHVPt = true;
        }

        private void btnVHAdd_Click(object sender, RoutedEventArgs e)
        {
            this.processPictureUC.MLineType = (EnumLineType)cboxType.SelectedIndex;
            this.processPictureUC.MConnVHPt = true;
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            this.processPictureUC.MDisConnPt = true;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            this.processPictureUC.ClearShape();
        }

        private void intCircle_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (null != this.processPictureUC)
            {
                this.processPictureUC.UpdateItemCircle((int)intCircle.Value);
            }
        }

        private void btnSetSize_Click(object sender, RoutedEventArgs e)
        {
             border.Width = (double)doubleWidth.Value;
             border.Height = (double)doubleHeight.Value;
             m_size.MWidth = (int)doubleWidth.Value;
             m_size.MHeight = (int)doubleHeight.Value;
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.processPictureUC.UpdateAllPt();

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
