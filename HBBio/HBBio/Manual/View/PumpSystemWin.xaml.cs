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

        /// <summary>
        /// 自定义事件，开始清洗时触发
        /// </summary>
        public static readonly RoutedEvent MWashStartEvent =
             EventManager.RegisterRoutedEvent("MWashStartClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PumpSystemWin));
        public event RoutedEventHandler MWashStartClick
        {
            add { AddHandler(MWashStartEvent, value); }
            remove { RemoveHandler(MWashStartEvent, value); }
        }
        /// <summary>
        /// 自定义事件，结束清洗时触发
        /// </summary>
        public static readonly RoutedEvent MWashStopEvent =
             EventManager.RegisterRoutedEvent("MWashStopClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PumpSystemWin));
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

            gridSystemWash.Visibility = ItemVisibility.s_listValve[ENUMValveName.CPV_1];
            gridSystemWash.Visibility = Visibility.Collapsed;
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
            if (MPumpValueNew.MFlow != MPumpValueOld.MFlow)
            {
                string error = StaticValue.CheckData(MPumpValueOld.MFlowUnit, MPumpValueNew.MFlow, MPumpValueOld.MBS, MPumpValueOld.MCS, MPumpValueOld.MDS);
                if (!string.IsNullOrEmpty(error))
                {
                    MessageBoxWin.Show(error);
                    return;
                }

                AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.Title, labFlow.Text + ":" + MPumpValueOld.MFlow + " -> " + MPumpValueNew.MFlow);

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

            AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.Title, sb.ToString());

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
            RoutedEventArgs args = new RoutedEventArgs(MWashStartEvent, new WashPara(ENUMPumpName.FITA, ENUMValveName.InA, cboxA.SelectedIndex));
            RaiseEvent(args);
        }

        /// <summary>
        /// 清洗泵B
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartB_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(MWashStartEvent, new WashPara(ENUMPumpName.FITB, ENUMValveName.InB, cboxB.SelectedIndex));
            RaiseEvent(args);
        }

        /// <summary>
        /// 清洗泵C
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartC_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(MWashStartEvent, new WashPara(ENUMPumpName.FITC, ENUMValveName.InC, cboxC.SelectedIndex));
            RaiseEvent(args);
        }

        /// <summary>
        /// 清洗泵D
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartD_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(MWashStartEvent, new WashPara(ENUMPumpName.FITD, ENUMValveName.InD, cboxD.SelectedIndex));
            RaiseEvent(args);
        }

        /// <summary>
        /// 清洗系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartSystem_Click(object sender, RoutedEventArgs e)
        {
            if (true == rbtnSystemOut.IsChecked)
            {
                RoutedEventArgs args = new RoutedEventArgs(MWashStartEvent, new WashPara(ENUMPumpName.FITA, ENUMValveName.InA, cboxA.SelectedIndex));
                RaiseEvent(args);
                args = new RoutedEventArgs(MWashStartEvent, new WashPara(ENUMPumpName.FITB, ENUMValveName.InB, cboxB.SelectedIndex));
                RaiseEvent(args);
                args = new RoutedEventArgs(MWashStartEvent, new WashPara(ENUMPumpName.FITC, ENUMValveName.InC, cboxC.SelectedIndex));
                RaiseEvent(args);
                args = new RoutedEventArgs(MWashStartEvent, new WashPara(ENUMPumpName.FITD, ENUMValveName.InD, cboxD.SelectedIndex));
                RaiseEvent(args);
            }
            else if (true == rbtnInjectionValve.IsChecked)
            {
                RoutedEventArgs args = new RoutedEventArgs(MWashStartEvent, new WashPara(ENUMPumpName.FITA, ENUMValveName.InA, cboxA.SelectedIndex));
                RaiseEvent(args);
                args = new RoutedEventArgs(MWashStartEvent, new WashPara(ENUMPumpName.FITB, ENUMValveName.InB, cboxB.SelectedIndex));
                RaiseEvent(args);
                args = new RoutedEventArgs(MWashStartEvent, new WashPara(ENUMPumpName.FITC, ENUMValveName.InC, cboxC.SelectedIndex));
                RaiseEvent(args);
                args = new RoutedEventArgs(MWashStartEvent, new WashPara(ENUMPumpName.FITD, ENUMValveName.InD, cboxD.SelectedIndex));
                RaiseEvent(args);
            }
        }

        /// <summary>
        /// 停止清洗泵A
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopA_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(MWashStopEvent, ENUMPumpName.FITA);
            RaiseEvent(args);
        }

        /// <summary>
        /// 停止清洗泵B
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopB_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(MWashStopEvent, ENUMPumpName.FITB);
            RaiseEvent(args);
        }

        /// <summary>
        /// 停止清洗泵C
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopC_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(MWashStopEvent, ENUMPumpName.FITC);
            RaiseEvent(args);
        }

        /// <summary>
        /// 停止清洗泵D
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopD_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(MWashStopEvent, ENUMPumpName.FITD);
            RaiseEvent(args);
        }

        /// <summary>
        /// 停止清洗系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopSystem_Click(object sender, RoutedEventArgs e)
        {
            if (true == rbtnSystemOut.IsChecked)
            {
                RoutedEventArgs args = new RoutedEventArgs(MWashStopEvent, ENUMPumpName.FITA);
                RaiseEvent(args);
                args = new RoutedEventArgs(MWashStopEvent, ENUMPumpName.FITB);
                RaiseEvent(args);
                args = new RoutedEventArgs(MWashStopEvent, ENUMPumpName.FITC);
                RaiseEvent(args);
                args = new RoutedEventArgs(MWashStopEvent, ENUMPumpName.FITD);
                RaiseEvent(args);
            }
            else if (true == rbtnInjectionValve.IsChecked)
            {
                RoutedEventArgs args = new RoutedEventArgs(MWashStopEvent, ENUMPumpName.FITA);
                RaiseEvent(args);
                args = new RoutedEventArgs(MWashStopEvent, ENUMPumpName.FITB);
                RaiseEvent(args);
                args = new RoutedEventArgs(MWashStopEvent, ENUMPumpName.FITC);
                RaiseEvent(args);
                args = new RoutedEventArgs(MWashStopEvent, ENUMPumpName.FITD);
                RaiseEvent(args);
            }
        }
    }
}