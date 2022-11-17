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
    /// PumpSystemWin.xaml 的交互逻辑
    /// </summary>
    public partial class PumpSystemWin : Window
    {
        public ComConfStatic MComconfStatic { get; set; }
        public WashSystem MWashSystem { get; set; }

        /// <summary>
        /// 属性，旧值
        /// </summary>
        public PumpSystemValue MPumpValueOld { get; set; }
        /// <summary>
        /// 属性，新值
        /// </summary>
        public PumpSystemValue MPumpValueNew { get; set; }
        /// <summary>
        /// 属性，设置是否只可泵洗
        /// </summary>
        public bool MEnabledFlow
        {
            set
            {
                btnFlow.IsEnabled = value;
                btnPer.IsEnabled = value;
            }
        }

        private System.Windows.Threading.DispatcherTimer m_timer = new System.Windows.Threading.DispatcherTimer();


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="pumpValue"></param>
        /// <param name="valveValue"></param>
        public PumpSystemWin(Window parent, PumpSystemValue pumpValue, ValveListValue valveValue)
        {
            InitializeComponent();

            this.Owner = parent;

            if (null != pumpValue)
            {
                MPumpValueOld = pumpValue;
            }
            else
            {
                MPumpValueOld = new PumpSystemValue();
            }
            MPumpValueNew = Share.DeepCopy.DeepCopyByXml(MPumpValueOld);

            switch (MPumpValueNew.MFlowUnit)
            {
                case EnumFlowRate.CMH:
                    sliderFlow.Maximum = StaticValue.s_maxFlowLen;
                    doubleFlow.Maximum = StaticValue.s_maxFlowLen;
                    break;
                default:
                    sliderFlow.Maximum = StaticValue.s_maxFlowVol;
                    doubleFlow.Maximum = StaticValue.s_maxFlowVol;
                    break;
            }

            sliderFlow.DataContext = MPumpValueNew;
            sliderB.DataContext = MPumpValueNew;
            sliderC.DataContext = MPumpValueNew;
            sliderD.DataContext = MPumpValueNew;

            cboxA.ItemsSource = EnumInAInfo.NameList;
            cboxA.SelectedIndex = valveValue.MListValave[(int)ENUMValveName.InA].MIndex;
            cboxB.ItemsSource = EnumInBInfo.NameList;
            cboxB.SelectedIndex = valveValue.MListValave[(int)ENUMValveName.InB].MIndex;
            cboxC.ItemsSource = EnumInCInfo.NameList;
            cboxC.SelectedIndex = valveValue.MListValave[(int)ENUMValveName.InC].MIndex;
            cboxD.ItemsSource = EnumInDInfo.NameList;
            cboxD.SelectedIndex = valveValue.MListValave[(int)ENUMValveName.InD].MIndex;

            cboxA.Visibility = ItemVisibility.s_listValve[ENUMValveName.InA];
            cboxB.Visibility = ItemVisibility.s_listValve[ENUMValveName.InB];
            cboxC.Visibility = ItemVisibility.s_listValve[ENUMValveName.InC];
            cboxD.Visibility = ItemVisibility.s_listValve[ENUMValveName.InD];

            btnStartB.Visibility = ItemVisibility.s_listPump[ENUMPumpName.FITB];
            btnStartC.Visibility = ItemVisibility.s_listPump[ENUMPumpName.FITC];
            btnStartD.Visibility = ItemVisibility.s_listPump[ENUMPumpName.FITD];

            btnStopB.Visibility = ItemVisibility.s_listPump[ENUMPumpName.FITB];
            btnStopC.Visibility = ItemVisibility.s_listPump[ENUMPumpName.FITC];
            btnStopD.Visibility = ItemVisibility.s_listPump[ENUMPumpName.FITD];

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
                btnStartA.IsEnabled = EnumWashStatus.No == MWashSystem.MListStatus[(int)ENUMPumpName.FITA] && EnumWashStatus.No == MWashSystem.MListStatus[(int)ENUMPumpName.FITS];
                btnStartB.IsEnabled = EnumWashStatus.No == MWashSystem.MListStatus[(int)ENUMPumpName.FITB] && EnumWashStatus.No == MWashSystem.MListStatus[(int)ENUMPumpName.FITS];
                btnStartC.IsEnabled = EnumWashStatus.No == MWashSystem.MListStatus[(int)ENUMPumpName.FITC] && EnumWashStatus.No == MWashSystem.MListStatus[(int)ENUMPumpName.FITS];
                btnStartD.IsEnabled = EnumWashStatus.No == MWashSystem.MListStatus[(int)ENUMPumpName.FITD] && EnumWashStatus.No == MWashSystem.MListStatus[(int)ENUMPumpName.FITS];
                btnStartAll.IsEnabled = btnStartA.IsEnabled && btnStartB.IsEnabled && btnStartC.IsEnabled && btnStartD.IsEnabled;

                cboxA.IsEnabled = btnStartA.IsEnabled;
                cboxB.IsEnabled = btnStartB.IsEnabled;
                cboxC.IsEnabled = btnStartC.IsEnabled;
                cboxD.IsEnabled = btnStartD.IsEnabled;
                cboxWash.IsEnabled = btnStartAll.IsEnabled;

                btnStopA.IsEnabled = EnumWashStatus.Ing == MWashSystem.MListStatus[(int)ENUMPumpName.FITA];
                btnStopB.IsEnabled = EnumWashStatus.Ing == MWashSystem.MListStatus[(int)ENUMPumpName.FITB];
                btnStopC.IsEnabled = EnumWashStatus.Ing == MWashSystem.MListStatus[(int)ENUMPumpName.FITC];
                btnStopD.IsEnabled = EnumWashStatus.Ing == MWashSystem.MListStatus[(int)ENUMPumpName.FITD];
                btnStopAll.IsEnabled = btnStopA.IsEnabled && btnStopB.IsEnabled && btnStopC.IsEnabled && btnStopD.IsEnabled;
            }
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
            if (MPumpValueNew.MFlow != MPumpValueOld.MFlow)
            {
                string error = StaticValue.CheckData(MPumpValueOld.MFlowUnit, MPumpValueNew.MFlow, MPumpValueOld.MBS, MPumpValueOld.MCS, MPumpValueOld.MDS);
                if (!string.IsNullOrEmpty(error))
                {
                    MessageBoxWin.Show(error);
                    return;
                }

                AuditTrails.AuditTrailsStatic.Instance().InsertRowManual(this.Title, labFlow.Text + ":" + MPumpValueOld.MFlow + " -> " + MPumpValueNew.MFlow);

                MPumpValueOld.MFlow = MPumpValueNew.MFlow;
                MPumpValueOld.m_update = true;
            }
        }

        /// <summary>
        /// 设置百分比
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPer_Click(object sender, RoutedEventArgs e)
        {
            string error = StaticValue.CheckData(MPumpValueOld.MFlowUnit, MPumpValueNew.MFlow, MPumpValueNew.MBS, MPumpValueNew.MCS, MPumpValueNew.MDS);
            if (!string.IsNullOrEmpty(error))
            {
                MessageBoxWin.Show(error);
                return;
            }

            MPumpValueNew.MBE = MPumpValueNew.MBS;
            MPumpValueNew.MCE = MPumpValueNew.MCS;
            MPumpValueNew.MDE = MPumpValueNew.MDS;

            StringBuilderSplit sb = new StringBuilderSplit();
            sb.Append(labConc.Text + ":");
            if (Visibility.Visible == doubleB.Visibility)
            {
                sb.Append(MPumpValueNew.MBS + labBPer.Text);
            }
            if (Visibility.Visible == doubleC.Visibility)
            {
                sb.Append(MPumpValueNew.MCS + labCPer.Text);
            }
            if (Visibility.Visible == doubleD.Visibility)
            {
                sb.Append(MPumpValueNew.MDS + labDPer.Text);
            }

            AuditTrails.AuditTrailsStatic.Instance().InsertRowManual(this.Title, sb.ToString());

            MPumpValueOld.MBS = MPumpValueNew.MBS;
            MPumpValueOld.MBE = MPumpValueNew.MBE;
            MPumpValueOld.MCS = MPumpValueNew.MCS;
            MPumpValueOld.MCE = MPumpValueNew.MCE;
            MPumpValueOld.MDS = MPumpValueNew.MDS;
            MPumpValueOld.MDE = MPumpValueNew.MDE;
            MPumpValueOld.m_update = true;
        }

        /// <summary>
        /// 清洗泵A
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartA_Click(object sender, RoutedEventArgs e)
        {
            AuditTrails.AuditTrailsStatic.Instance().InsertRowManual(this.Title, btnStartA.Content.ToString());

            WashStart(ENUMPumpName.FITA, ENUMValveName.InA, cboxA.SelectedIndex, cboxWash.SelectedIndex);
        }

        /// <summary>
        /// 清洗泵B
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartB_Click(object sender, RoutedEventArgs e)
        {
            AuditTrails.AuditTrailsStatic.Instance().InsertRowManual(this.Title, btnStartB.Content.ToString());

            WashStart(ENUMPumpName.FITB, ENUMValveName.InB, cboxB.SelectedIndex, cboxWash.SelectedIndex);
        }

        /// <summary>
        /// 清洗泵C
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartC_Click(object sender, RoutedEventArgs e)
        {
            AuditTrails.AuditTrailsStatic.Instance().InsertRowManual(this.Title, btnStartC.Content.ToString());

            WashStart(ENUMPumpName.FITC, ENUMValveName.InC, cboxC.SelectedIndex, cboxWash.SelectedIndex);
        }

        /// <summary>
        /// 清洗泵D
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartD_Click(object sender, RoutedEventArgs e)
        {
            AuditTrails.AuditTrailsStatic.Instance().InsertRowManual(this.Title, btnStartD.Content.ToString());

            WashStart(ENUMPumpName.FITD, ENUMValveName.InD, cboxD.SelectedIndex, cboxWash.SelectedIndex);
        }

        /// <summary>
        /// 清洗系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartAll_Click(object sender, RoutedEventArgs e)
        {
            AuditTrails.AuditTrailsStatic.Instance().InsertRowManual(this.Title, btnStartAll.Content.ToString());

            WashStart(ENUMPumpName.FITA, ENUMValveName.InA, cboxA.SelectedIndex, cboxWash.SelectedIndex);
            WashStart(ENUMPumpName.FITB, ENUMValveName.InB, cboxB.SelectedIndex, cboxWash.SelectedIndex);
            WashStart(ENUMPumpName.FITC, ENUMValveName.InC, cboxC.SelectedIndex, cboxWash.SelectedIndex);
            WashStart(ENUMPumpName.FITD, ENUMValveName.InD, cboxD.SelectedIndex, cboxWash.SelectedIndex);
        }

        /// <summary>
        /// 停止清洗泵A
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopA_Click(object sender, RoutedEventArgs e)
        {
            AuditTrails.AuditTrailsStatic.Instance().InsertRowManual(this.Title, btnStopA.Content.ToString());

            WashStop(ENUMPumpName.FITA);
        }

        /// <summary>
        /// 停止清洗泵B
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopB_Click(object sender, RoutedEventArgs e)
        {
            AuditTrails.AuditTrailsStatic.Instance().InsertRowManual(this.Title, btnStopB.Content.ToString());

            WashStop(ENUMPumpName.FITB);
        }

        /// <summary>
        /// 停止清洗泵C
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopC_Click(object sender, RoutedEventArgs e)
        {
            AuditTrails.AuditTrailsStatic.Instance().InsertRowManual(this.Title, btnStopC.Content.ToString());

            WashStop(ENUMPumpName.FITC);
        }

        /// <summary>
        /// 停止清洗泵D
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopD_Click(object sender, RoutedEventArgs e)
        {
            AuditTrails.AuditTrailsStatic.Instance().InsertRowManual(this.Title, btnStopD.Content.ToString());

            WashStop(ENUMPumpName.FITD);
        }

        /// <summary>
        /// 停止清洗系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopAll_Click(object sender, RoutedEventArgs e)
        {
            AuditTrails.AuditTrailsStatic.Instance().InsertRowManual(this.Title, btnStopAll.Content.ToString());

            WashStop(ENUMPumpName.FITA);
            WashStop(ENUMPumpName.FITB);
            WashStop(ENUMPumpName.FITC);
            WashStop(ENUMPumpName.FITD);
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