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

namespace HBBio.Manual
{
    /// <summary>
    /// PumpSampleWin.xaml 的交互逻辑
    /// </summary>
    public partial class PumpSampleWin : Window
    {
        /// <summary>
        /// 属性，旧值
        /// </summary>
        public PumpSampleValue MPumpValueOld { get; set; }
        /// <summary>
        /// 属性，新值
        /// </summary>
        public PumpSampleValue MPumpValueNew { get; set; }
        /// <summary>
        /// 属性，设置是否只可泵洗
        /// </summary>
        public bool MEnabledFlow
        {
            set
            {
                btnFlow.IsEnabled = value;
            }
        }

        /// <summary>
        /// 自定义事件，开始清洗时触发
        /// </summary>
        public static readonly RoutedEvent MWashStartEvent =
             EventManager.RegisterRoutedEvent("MWashStartClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PumpSampleWin));
        public event RoutedEventHandler MWashStartClick
        {
            add { AddHandler(MWashStartEvent, value); }
            remove { RemoveHandler(MWashStartEvent, value); }
        }
        /// <summary>
        /// 自定义事件，结束清洗时触发
        /// </summary>
        public static readonly RoutedEvent MWashStopEvent =
             EventManager.RegisterRoutedEvent("MWashStopClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PumpSampleWin));
        public event RoutedEventHandler MWashStopClick
        {
            add { AddHandler(MWashStopEvent, value); }
            remove { RemoveHandler(MWashStopEvent, value); }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="pumpValue"></param>
        /// <param name="valveValue"></param>
        public PumpSampleWin(Window parent, PumpSampleValue pumpValue, ValveListValue valveValue)
        {
            InitializeComponent();

            this.Owner = parent;

            if (null != pumpValue)
            {
                MPumpValueOld = pumpValue;
            }
            else
            {
                MPumpValueOld = new PumpSampleValue();
            }
            MPumpValueNew = Share.DeepCopy.DeepCopyByXml(MPumpValueOld);

            switch (MPumpValueNew.MFlowUnit)
            {
                case EnumFlowRate.CMH:
                    sliderFlow.Maximum = StaticValue.s_maxFlowLen;
                    doubleFlow.Maximum = StaticValue.s_maxFlowLen;
                    break;
                default:
                    sliderFlow.Maximum = StaticValue.s_maxFlowSVol;
                    doubleFlow.Maximum = StaticValue.s_maxFlowSVol;
                    break;
            }

            sliderFlow.DataContext = MPumpValueNew;

            btnFlow.IsEnabled = MPumpValueNew.m_signal;

            cbox.ItemsSource = EnumInSInfo.NameList;
            cbox.SelectedIndex = valveValue.MListValave[(int)ENUMValveName.InS].MIndex;

            cbox.Visibility = ItemVisibility.s_listValve[ENUMValveName.InS];
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
            if (MPumpValueNew.MFlow != MPumpValueOld.MFlow)
            {
                AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.Title, labFlow.Text + ":" + MPumpValueOld.MFlow + " -> " + MPumpValueNew.MFlow);

                MPumpValueOld.MFlow = MPumpValueNew.MFlow;
                MPumpValueOld.m_update = true;
            }
        }

        /// <summary>
        /// 清洗泵S
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(MWashStartEvent, new WashPara(ENUMPumpName.FITS, ENUMValveName.InS, cbox.SelectedIndex));
            RaiseEvent(args);
        }

        /// <summary>
        /// 停止清洗泵S
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(MWashStopEvent, ENUMPumpName.FITS);
            RaiseEvent(args);
        }
    }
}
