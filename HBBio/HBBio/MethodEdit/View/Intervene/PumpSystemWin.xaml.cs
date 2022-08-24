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
    /// PumpSystemWin.xaml 的交互逻辑
    /// </summary>
    public partial class PumpSystemWin : Window
    {
        public MethodTempValue MMethodTempValue { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parent"></param>
        public PumpSystemWin(Window parent)
        {
            InitializeComponent();

            this.Owner = parent;

            sliderFlow.Maximum = StaticValue.s_maxFlowVol;
            doubleFlow.Maximum = StaticValue.s_maxFlowVol;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sliderFlow.Value = MMethodTempValue.MFlowSystem;
            sliderB.Value = MMethodTempValue.MPerB;
            sliderC.Value = MMethodTempValue.MPerC;
            sliderD.Value = MMethodTempValue.MPerD;
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
        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            (sender as Slider).Value = Math.Round(e.NewValue, 2);
        }

        /// <summary>
        /// 设置流速
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFlow_Click(object sender, RoutedEventArgs e)
        {
            if (sliderFlow.Value != MMethodTempValue.MFlowSystem)
            {
                AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.Title, labFlow.Text + ":" + MMethodTempValue.MFlowSystem + " -> " + sliderFlow.Value);
                MMethodTempValue.MFlowSystem = sliderFlow.Value;
                MMethodTempValue.MChange = true;
            }
        }

        /// <summary>
        /// 设置百分比
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPer_Click(object sender, RoutedEventArgs e)
        {
            if (Visibility.Visible == doubleB.Visibility)
            {
                if (sliderB.Value != MMethodTempValue.MPerB)
                {
                    AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.Title, "%B:" + MMethodTempValue.MPerB + " -> " + sliderB.Value);
                    MMethodTempValue.MPerB = sliderB.Value;
                    MMethodTempValue.MChange = true;
                }
            }
            if (Visibility.Visible == doubleC.Visibility)
            {
                if (sliderC.Value != MMethodTempValue.MPerC)
                {
                    AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.Title, "%C:" + MMethodTempValue.MPerC + " -> " + sliderC.Value);
                    MMethodTempValue.MPerC = sliderC.Value;
                    MMethodTempValue.MChange = true;
                }
            }
            if (Visibility.Visible == doubleD.Visibility)
            {
                if (sliderD.Value != MMethodTempValue.MPerD)
                {
                    AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.Title, "%D:" + MMethodTempValue.MPerD + " -> " + sliderD.Value);
                    MMethodTempValue.MPerD = sliderD.Value;
                    MMethodTempValue.MChange = true;
                }
            }
        }
    }
}