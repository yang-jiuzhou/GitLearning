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
    /// CurveColorWin.xaml 的交互逻辑
    /// </summary>
    public partial class CurveColorWin : Window
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CurveColorWin()
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
            dgv.ItemsSource = ((CurveSetStyleVM)this.DataContext).MList;
        }

        /// <summary>
        /// 信号列表-选择实时数据颜色
        /// </summary>
        private void newColor_Click(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.ColorDialog dlg = new System.Windows.Forms.ColorDialog();
            dlg.Color = Share.ValueTrans.MediaToDraw(((SolidColorBrush)((CurveSetStyleVM)this.DataContext).MList[dgv.SelectedIndex].MModel.MBrush).Color);
            if (System.Windows.Forms.DialogResult.OK == dlg.ShowDialog())
            {
                ((CurveSetStyleVM)this.DataContext).MList[dgv.SelectedIndex].MModel.MBrush = new SolidColorBrush(Share.ValueTrans.DrawToMedia(dlg.Color));
                MApp.DoEvents();
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
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
