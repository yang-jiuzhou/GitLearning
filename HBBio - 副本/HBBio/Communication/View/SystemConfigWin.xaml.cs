using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace HBBio.Communication
{
    /// <summary>
    /// SystemConfigWin.xaml 的交互逻辑
    /// </summary>
    public partial class SystemConfigWin : Window
    {
        public SystemConfig MSystemConfig { get; set; }
        private SystemConfigVM MSystemConfigShow { get; set; }
        public AlarmWarning MAlarmWarning { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public SystemConfigWin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置是否可编辑
        /// </summary>
        /// <param name="enabled"></param>
        public void SetPermission(bool enabled)
        {
            warpPanel.IsEnabled = enabled;
        }

        /// <summary>
        /// 获取审计跟踪对比信息
        /// </summary>
        /// <returns></returns>
        public string GetCompareInfo()
        {
            Share.StringBuilderSplit sb = new Share.StringBuilderSplit("\n");

            //色谱柱参数
            if (MSystemConfig.MConfColumn.MColumnVol != MSystemConfigShow.MConfColumn.MColumnVol)
            {
                sb.Append(labColumnVol1.Text + MSystemConfig.MConfColumn.MColumnVol + " -> " + MSystemConfigShow.MConfColumn.MColumnVol);
                MSystemConfig.MConfColumn.MColumnVol = MSystemConfigShow.MConfColumn.MColumnVol;
            }

            if (MSystemConfig.MConfColumn.MColumnDiameter != MSystemConfigShow.MConfColumn.MColumnDiameter)
            {
                sb.Append(labColumnDiameter1.Text + MSystemConfig.MConfColumn.MColumnDiameter + " -> " + MSystemConfigShow.MConfColumn.MColumnDiameter);
                MSystemConfig.MConfColumn.MColumnDiameter = MSystemConfigShow.MConfColumn.MColumnDiameter;
            }

            if (MSystemConfig.MConfColumn.MColumnHeight != MSystemConfigShow.MConfColumn.MColumnHeight)
            {
                sb.Append(labColumnHeight1.Text + MSystemConfig.MConfColumn.MColumnHeight + " -> " + MSystemConfigShow.MConfColumn.MColumnHeight);
                MSystemConfig.MConfColumn.MColumnHeight = MSystemConfigShow.MConfColumn.MColumnHeight;
            }    

            //冲洗参数
            if (MSystemConfig.MConfWash.MWashTime != MSystemConfigShow.MConfWash.MWashTime)
            {
                sb.Append(labWashTime1.Text + MSystemConfig.MConfWash.MWashTime + " -> " + MSystemConfigShow.MConfWash.MWashTime);
                MSystemConfig.MConfWash.MWashTime = MSystemConfigShow.MConfWash.MWashTime;
            }

            if (MSystemConfig.MConfWash.MWashFlowPer != MSystemConfigShow.MConfWash.MWashFlowPer)
            {
                sb.Append(labWashFlowPer1.Text + MSystemConfig.MConfWash.MWashFlowPer + " -> " + MSystemConfigShow.MConfWash.MWashFlowPer);
                MSystemConfig.MConfWash.MWashFlowPer = MSystemConfigShow.MConfWash.MWashFlowPer;
            }

            //组分收集器
            if (MSystemConfig.MConfCollector.MGLTJ != MSystemConfigShow.MConfCollector.MGLTJ)
            {
                sb.Append(labGLTJ.Text + MSystemConfig.MConfCollector.MGLTJ + " -> " + MSystemConfigShow.MConfCollector.MGLTJ);
                MSystemConfig.MConfCollector.MGLTJ = MSystemConfigShow.MConfCollector.MGLTJ;
            }

            MSystemConfig.MConfCollector.MVolL = MSystemConfigShow.MConfCollector.MVolL;
            MSystemConfig.MConfCollector.MVolR = MSystemConfigShow.MConfCollector.MVolR;
            MSystemConfig.MConfCollector.MCountL = MSystemConfigShow.MConfCollector.MCountL;
            MSystemConfig.MConfCollector.MCountR = MSystemConfigShow.MConfCollector.MCountR;
            MSystemConfig.MConfCollector.MModeL = MSystemConfigShow.MConfCollector.MModeL;
            MSystemConfig.MConfCollector.MModeR = MSystemConfigShow.MConfCollector.MModeR;

            //检测器延迟体积
            if (MSystemConfig.MDelayVol != MSystemConfigShow.MDelayVol)
            {
                sb.Append(labDefault.Text + "(" + Share.DlyBase.SC_VUNITML + ") :" + MSystemConfig.MDelayVol + " -> " + MSystemConfigShow.MDelayVol);
                MSystemConfig.MDelayVol = MSystemConfigShow.MDelayVol;
            }
            for (int i = 0; i < MSystemConfig.MListConfpHCdUV.Count; i++)
            {
                if (MSystemConfig.MListConfpHCdUV[i].MVol != MSystemConfigShow.MListConfpHCdUV[i].MVol)
                {
                    sb.Append(MSystemConfig.MListConfpHCdUV[i].MName +"("+ Share.DlyBase.SC_VUNITML+") :" + MSystemConfig.MListConfpHCdUV[i].MVol + " -> " + MSystemConfigShow.MListConfpHCdUV[i].MVol);
                    MSystemConfig.MListConfpHCdUV[i].MVol = MSystemConfigShow.MListConfpHCdUV[i].MVol;
                }
            }

            //气泡传感器
            foreach (FrameworkElement it in wrapAS.Children)
            {
                sb.Append(((ConfASUC)it).GetCompareInfo());
            }

            //其它
            if (MSystemConfig.MConfOther.MResetValve != MSystemConfigShow.MConfOtherVM.MResetValve)
            {
                sb.Append(chboxResetValve.Content.ToString() + " : " + (MSystemConfig.MConfOther.MResetValve ? Share.ReadXaml.S_Enabled : Share.ReadXaml.S_Disabled) + " -> " + (MSystemConfigShow.MConfOtherVM.MResetValve ? Share.ReadXaml.S_Enabled : Share.ReadXaml.S_Disabled));
                MSystemConfig.MConfOther.MResetValve = MSystemConfigShow.MConfOtherVM.MResetValve;
            }

            if (MSystemConfig.MConfOther.MCloseUV != MSystemConfigShow.MConfOtherVM.MCloseUV)
            {
                sb.Append(chboxCloseUV.Content.ToString() + " : " + (MSystemConfig.MConfOther.MCloseUV ? Share.ReadXaml.S_Enabled : Share.ReadXaml.S_Disabled) + " -> " + (MSystemConfigShow.MConfOtherVM.MCloseUV ? Share.ReadXaml.S_Enabled : Share.ReadXaml.S_Disabled));
                MSystemConfig.MConfOther.MCloseUV = MSystemConfigShow.MConfOtherVM.MCloseUV;
            }

            if (MSystemConfig.MConfOther.MOpenMixer != MSystemConfigShow.MConfOtherVM.MOpenMixer)
            {
                sb.Append(chboxOpenMixer.Content.ToString() + " : " + (MSystemConfig.MConfOther.MOpenMixer ? Share.ReadXaml.S_On : Share.ReadXaml.S_Off) + " -> " + (MSystemConfigShow.MConfOtherVM.MOpenMixer ? Share.ReadXaml.S_On : Share.ReadXaml.S_Off));
                MSystemConfig.MConfOther.MOpenMixer = MSystemConfigShow.MConfOtherVM.MOpenMixer;
            }

            return sb.ToString();
        }

        /// <summary>
        /// 界面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (null == MSystemConfig)
            {
                MSystemConfig = new SystemConfig();
            }
            MSystemConfigShow = new SystemConfigVM(Share.DeepCopy.DeepCopyByXml(MSystemConfig));

            foreach (FrameworkElement it in gridColumn.Children)
            {
                if (it is TextBlock)
                {
                    continue;
                }
                it.DataContext = MSystemConfigShow.MConfColumn;
            }

            foreach (FrameworkElement it in gridWash.Children)
            {
                if (it is TextBlock)
                {
                    continue;
                }
                it.DataContext = MSystemConfigShow.MConfWash;
            }

            if (Visibility.Visible == ItemVisibility.s_listCollector[ENUMCollectorName.Collector01])
            {
                foreach (FrameworkElement it in gridCollector.Children)
                {
                    if (it is TextBlock)
                    {
                        continue;
                    }
                    it.DataContext = MSystemConfigShow.MConfCollector;
                }

                btnCollector.Visibility = Visibility.Collapsed;
                foreach (BaseCommunication item in ComConfStatic.Instance().m_comList)
                {
                    if (item.MComConf.MModel.Equals(ENUMCollectorID.QBH_DLY.ToString()))
                    {
                        btnCollector.Visibility = Visibility.Visible;
                    }
                }
            }
            else
            {
                groupCollector.Visibility = Visibility.Collapsed;
            }

            if (0 == MSystemConfig.MListConfAS.Count)
            {
                groupAS.Visibility = Visibility.Collapsed;
            }
            else
            {
                int indexAS = 1;
                foreach (var it in MSystemConfig.MListConfAS)
                {
                    ConfASUC uc = new ConfASUC();
                    uc.MHeader = "AS0" + (indexAS++);
                    uc.MConfAS = it;
                    wrapAS.Children.Add(uc);
                }
            }

            if (0 == MSystemConfig.MListConfpHCdUV.Count)
            {
                grouppHCdUV.Visibility = Visibility.Collapsed;
                groupCal.Visibility = Visibility.Collapsed;
            }
            else
            {
                //默认延迟
                doubleDefaultDelayVol.DataContext = MSystemConfigShow;

                int indexpHCdUV = 1;
                foreach (var it in MSystemConfigShow.MListConfpHCdUV)
                {
                    gridpHCdUV.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                    gridpHCdUV.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(10) });

                    TextBlock tbk = new TextBlock() { Text = it.MName + " :" };
                    tbk.SetValue(Grid.RowProperty, indexpHCdUV * 2);
                    tbk.SetValue(Grid.ColumnProperty, 0);

                    DoubleUpDown val = new DoubleUpDown();
                    val.SetBinding(DoubleUpDown.ValueProperty, new Binding("MVol"));
                    val.DataContext = it;
                    val.SetValue(Grid.RowProperty, indexpHCdUV * 2);
                    val.SetValue(Grid.ColumnProperty, 2);

                    TextBlock labUnit = new TextBlock() { Text = Share.DlyBase.SC_VUNITML };
                    labUnit.SetValue(Grid.RowProperty, indexpHCdUV * 2);
                    labUnit.SetValue(Grid.ColumnProperty, 4);

                    gridpHCdUV.Children.Add(tbk);
                    gridpHCdUV.Children.Add(val);
                    gridpHCdUV.Children.Add(labUnit);

                    indexpHCdUV++;
                }

                indexpHCdUV = 0;
                foreach (var it in MSystemConfigShow.MListConfpHCdUV)
                {
                    if (it.MName.Contains("pH") || it.MName.Contains("Cd"))
                    {
                        gridCal.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                        gridCal.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(10) });

                        Button btn = new Button() { Content = it.MName };
                        btn.Click += new RoutedEventHandler(btnCal_Click);
                        btn.SetValue(Grid.RowProperty, indexpHCdUV * 2);
                        gridCal.Children.Add(btn);
                        indexpHCdUV++;
                    }
                }
                if (0 == gridCal.Children.Count)
                {
                    groupCal.Visibility = Visibility.Collapsed;
                }
            }

            chboxResetValve.Visibility = Visibility.Collapsed;
            foreach (var it in ItemVisibility.s_listValve)
            {
                if (Visibility.Visible == it.Value)
                {
                    chboxResetValve.Visibility = Visibility.Visible;
                }
            }
            chboxCloseUV.Visibility = ItemVisibility.s_listUV[ENUMUVName.UV01];
            chboxOpenMixer.Visibility = ItemVisibility.s_listMixer[ENUMMixerName.Mixer01];
            foreach (FrameworkElement it in gridOther.Children)
            {
                it.DataContext = MSystemConfigShow.MConfOtherVM;
            }
        }

        /// <summary>
        /// 组分收集器托盘设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCollector_Click(object sender, RoutedEventArgs e)
        {
            foreach (BaseCommunication item in ComConfStatic.Instance().m_comList)
            {
                if (item.MComConf.MModel.Equals(ENUMCollectorID.QBH_DLY.ToString()))
                {
                    CollectorQBHModeWin win = new CollectorQBHModeWin();
                    win.Owner = this;  
                    switch (item.MComConf.MCommunMode)
                    {
                        case EnumCommunMode.Com:
                            win.MItemCom = (ComCollectorQBH)item;
                            break;
                        case EnumCommunMode.TCP:
                            win.MItemTCP = (TCPCollectorQBH)item;
                            break;
                    }
                    win.DataContext = MSystemConfigShow.MConfCollector;
                    win.ShowDialog();
                    break;
                }
                else if (item.MComConf.MModel.Equals(ENUMCollectorID.HB_DLY_W.ToString()))
                {
                    CollectorHBModeWin win = new CollectorHBModeWin();
                    win.MMode = ENUMCollectorID.HB_DLY_W;
                    win.Owner = this;
                    switch (item.MComConf.MCommunMode)
                    {
                        case EnumCommunMode.Com:
                            win.MItemCom = (ComCollectorHB)item;
                            break;
                        case EnumCommunMode.TCP:
                            win.MItemTCP = (TCPCollectorHB)item;
                            break;
                    }
                    win.DataContext = MSystemConfigShow.MConfCollector;
                    win.ShowDialog();
                    break;
                }
                else if (item.MComConf.MModel.Equals(ENUMCollectorID.HB_DLY_B.ToString()))
                {
                    CollectorHBModeWin win = new CollectorHBModeWin();
                    win.MMode = ENUMCollectorID.HB_DLY_B;
                    win.Owner = this;
                    switch (item.MComConf.MCommunMode)
                    {
                        case EnumCommunMode.Com:
                            win.MItemCom = (ComCollectorHB)item;
                            break;
                        case EnumCommunMode.TCP:
                            win.MItemTCP = (TCPCollectorHB)item;
                            break;
                    }
                    win.DataContext = MSystemConfigShow.MConfCollector;
                    win.ShowDialog();
                    break;
                }
            }
        }

        /// <summary>
        /// 设置警报警告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAlarmWarning_Click(object sender, RoutedEventArgs e)
        {
            ConfAlarmWarningWin dlg = new ConfAlarmWarningWin();
            dlg.Owner = this;
            dlg.MAlarmWarning = MAlarmWarning;
            dlg.ShowDialog();
        }

        /// <summary>
        /// 校准设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCal_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Content.ToString().Contains("pH"))
            {
                foreach (BaseCommunication item in ComConfStatic.Instance().m_comList)
                {
                    if (item.MComConf.MModel.Equals(ENUMDetectorID.pHCdOEM.ToString()))
                    {
                        switch (item.MComConf.MCommunMode)
                        {
                            case EnumCommunMode.Com:
                                if (((ComPHCDOEM)item).MpHItem.MConstName.Equals(((Button)sender).Content.ToString()))
                                {
                                    CalOEMPHWin win = new CalOEMPHWin();
                                    win.Owner = this;
                                    win.MItemCom = (ComPHCDOEM)item;
                                    if (true == win.ShowDialog())
                                    {
                                        AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.Title, win.Title);
                                    }
                                    break;
                                }
                                break;
                            case EnumCommunMode.TCP:
                                if (((TCPPHCDOEM)item).MpHItem.MConstName.Equals(((Button)sender).Content.ToString()))
                                {
                                    CalOEMPHWin win = new CalOEMPHWin();
                                    win.Owner = this;
                                    win.MItemTCP = (TCPPHCDOEM)item;
                                    if (true == win.ShowDialog())
                                    {
                                        AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.Title, win.Title);
                                    }
                                    break;
                                }
                                break;
                        }
                    }
                    else if (item.MComConf.MModel.Equals(ENUMDetectorID.pHHamilton.ToString()))
                    {
                        try
                        {
                            item.ThreadStatus(ENUMThreadStatus.Abort);
                            while (ENUMCommunicationState.Over != item.m_communState && ENUMCommunicationState.Free != item.m_communState)
                            {
                                Thread.Sleep(DlyBase.c_sleep1);
                                DispatcherHelper.DoEvents();
                            }
                            AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.Title, ((Button)sender).Content.ToString());
                            Process.Start(@"ArcAir");
                        }
                        catch (Exception e1)
                        {
                            MessageBoxWin.Show(e1.Message);
                        }
                    }
                    else if (item.MComConf.MModel.Equals(ENUMDetectorID.pHCdHamilton.ToString()))
                    {
                        try
                        {
                            item.ThreadStatus(ENUMThreadStatus.Abort);
                            while (ENUMCommunicationState.Over != item.m_communState && ENUMCommunicationState.Free != item.m_communState)
                            {
                                Thread.Sleep(DlyBase.c_sleep1);
                                DispatcherHelper.DoEvents();
                            }
                            AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.Title, ((Button)sender).Content.ToString());
                            Process.Start(@"ArcAir");
                        }
                        catch (Exception e1)
                        {
                            MessageBoxWin.Show(e1.Message);
                        }
                    }
                }
            }
            else if (((Button)sender).Content.ToString().Contains("Cd"))
            {
                foreach (BaseCommunication item in ComConfStatic.Instance().m_comList)
                {
                    if (item.MComConf.MModel.Equals(ENUMDetectorID.pHCdOEM.ToString()))
                    {
                        switch (item.MComConf.MCommunMode)
                        {
                            case EnumCommunMode.Com:
                                if (((ComPHCDOEM)item).MCdItem.MConstName.Equals(((Button)sender).Content.ToString()))
                                {
                                    CalOEMCDWin win = new CalOEMCDWin();
                                    win.Owner = this;
                                    win.MItemCom = (ComPHCDOEM)item;
                                    if (true == win.ShowDialog())
                                    {
                                        AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.Title, win.Title);
                                    }

                                    break;
                                }
                                break;
                            case EnumCommunMode.TCP:
                                if (((TCPPHCDOEM)item).MCdItem.MConstName.Equals(((Button)sender).Content.ToString()))
                                {
                                    CalOEMCDWin win = new CalOEMCDWin();
                                    win.Owner = this;
                                    win.MItemTCP = (TCPPHCDOEM)item;
                                    if (true == win.ShowDialog())
                                    {
                                        AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.Title, win.Title);
                                    }

                                    break;
                                }
                                break;
                        }
                    }
                    else if (item.MComConf.MModel.Equals(ENUMDetectorID.CdHamilton.ToString()))
                    {
                        try
                        {
                            item.ThreadStatus(ENUMThreadStatus.Abort);
                            while (ENUMCommunicationState.Over != item.m_communState && ENUMCommunicationState.Free != item.m_communState)
                            {
                                Thread.Sleep(DlyBase.c_sleep1);
                                DispatcherHelper.DoEvents();
                            }
                            AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.Title, ((Button)sender).Content.ToString());
                            Process.Start(@"ArcAir");
                        }
                        catch (Exception e1)
                        {
                            MessageBoxWin.Show(e1.Message);
                        }
                    }
                    else if (item.MComConf.MModel.Equals(ENUMDetectorID.pHCdHamilton.ToString()))
                    {
                        try
                        {
                            item.ThreadStatus(ENUMThreadStatus.Abort);
                            while (ENUMCommunicationState.Over != item.m_communState && ENUMCommunicationState.Free != item.m_communState)
                            {
                                Thread.Sleep(DlyBase.c_sleep1);
                                DispatcherHelper.DoEvents();
                            }
                            AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.Title, ((Button)sender).Content.ToString());
                            Process.Start(@"ArcAir");
                        }
                        catch (Exception e1)
                        {
                            MessageBoxWin.Show(e1.Message);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (!Administration.AdministrationStatic.Instance().ShowSignerReviewerWin(this, Administration.EnumSignerReviewer.InstrumentParameters_Edit))
            {
                return;
            }

            string logInfo = GetCompareInfo();
            if (!string.IsNullOrEmpty(logInfo))
            {
                AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.Title, logInfo);
            }

            foreach (BaseCommunication item in ComConfStatic.Instance().m_comList)
            {
                if (ENUMCommunicationState.Over == item.m_communState || ENUMCommunicationState.Free == item.m_communState)
                {
                    item.ThreadStatus(ENUMThreadStatus.WriteOrRead);
                }
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
            foreach (BaseCommunication item in ComConfStatic.Instance().m_comList)
            {
                if (ENUMCommunicationState.Over == item.m_communState || ENUMCommunicationState.Free == item.m_communState)
                {
                    item.ThreadStatus(ENUMThreadStatus.WriteOrRead);
                }
            }

            DialogResult = false;
        }
    }
}