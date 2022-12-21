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
        public ComConfStatic MComconfStatic { get; set; }
        public WashSystem MWashSystem { get; set; }

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

        private System.Windows.Threading.DispatcherTimer m_timer = new System.Windows.Threading.DispatcherTimer();


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

            cboxWash.ItemsSource = EnumWashInfo.NameList;
            cboxWash.SelectedIndex = 1;
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
            m_timer.Stop();

            this.Close();
        }

        /// <summary>
        /// 定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (null != MWashSystem)
            {
                btnStart.IsEnabled = EnumWashStatus.No == MWashSystem.MListStatus[(int)ENUMPumpName.FITS]
                    && EnumWashStatus.No == MWashSystem.MListStatus[(int)ENUMPumpName.FITA]
                    && EnumWashStatus.No == MWashSystem.MListStatus[(int)ENUMPumpName.FITB]
                    && EnumWashStatus.No == MWashSystem.MListStatus[(int)ENUMPumpName.FITC]
                    && EnumWashStatus.No == MWashSystem.MListStatus[(int)ENUMPumpName.FITD];

                cbox.IsEnabled = btnStart.IsEnabled;
                cboxWash.IsEnabled = btnStart.IsEnabled;

                btnStop.IsEnabled = EnumWashStatus.Ing == MWashSystem.MListStatus[(int)ENUMPumpName.FITS];
            }
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
            }
            
            MPumpValueOld.m_update = true;
        }

        /// <summary>
        /// 清洗泵S
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            AuditTrails.AuditTrailsStatic.Instance().InsertRowManual(this.Title, btnStart.Content.ToString());

            WashStart(ENUMPumpName.FITS, ENUMValveName.InS, cbox.SelectedIndex, cboxWash.SelectedIndex);
        }

        /// <summary>
        /// 停止清洗泵S
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            AuditTrails.AuditTrailsStatic.Instance().InsertRowManual(this.Title, btnStop.Content.ToString());

            WashStop(ENUMPumpName.FITS);
        }

        /// <summary>
        /// 不能选择不清洗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboxWash_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (0 == cboxWash.SelectedIndex)
            {
                cboxWash.SelectedIndex = 1;
            }
        }

        /// <summary>
        /// 开始清洗
        /// </summary>
        /// <param name="pump"></param>
        /// <param name="valve"></param>
        /// <param name="index"></param>
        /// <param name="wash"></param>
        private void WashStart(ENUMPumpName pump, ENUMValveName valve, int index, int wash)
        {
            MComconfStatic.SetValve(valve, index);
            MWashSystem.Start(MComconfStatic, pump, wash);
        }

        /// <summary>
        /// 结束清洗
        /// </summary>
        /// <param name="pump"></param>
        private void WashStop(ENUMPumpName pump)
        {
            MWashSystem.Stop(MComconfStatic, pump);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer1_Tick(null, null);
            m_timer.Interval = TimeSpan.FromMilliseconds(DlyBase.c_sleep5);
            m_timer.Tick += timer1_Tick;
            m_timer.Start();
        }
    }
}
