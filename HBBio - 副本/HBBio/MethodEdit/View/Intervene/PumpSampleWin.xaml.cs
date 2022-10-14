using HBBio.Communication;
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

namespace HBBio.MethodEdit
{
    /// <summary>
    /// PumpSampleWin.xaml 的交互逻辑
    /// </summary>
    public partial class PumpSampleWin : Window
    {
        public MethodTempValue MMethodTempValue { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parent"></param>
        public PumpSampleWin(Window parent)
        {
            InitializeComponent();

            this.Owner = parent;

            sliderFlow.Maximum = StaticValue.s_maxFlowSVol;
            doubleFlow.Maximum = StaticValue.s_maxFlowSVol;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sliderFlow.Value = MMethodTempValue.MFlowSample;
        }

        /// <summary>
        /// 移动对话框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        /// <summary>
        /// 关闭对话框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 滑动条保留两位小数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sliderFlow_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            (sender as Slider).Value = Math.Round(e.NewValue, 2);
        }

        private void btnFlow_Click(object sender, RoutedEventArgs e)
        {
            if (sliderFlow.Value != MMethodTempValue.MFlowSample)
            {
                AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.Title, labFlow.Text + ":" + MMethodTempValue.MFlowSample + " -> " + sliderFlow.Value);
                MMethodTempValue.MFlowSample = sliderFlow.Value;
                MMethodTempValue.MChange = true;
            }
        }
    }
}
