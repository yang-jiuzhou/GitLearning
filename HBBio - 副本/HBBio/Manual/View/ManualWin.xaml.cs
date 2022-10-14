using HBBio.Communication;
using HBBio.Collection;
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
using HBBio.Administration;

namespace HBBio.Manual
{
    /// <summary>
    /// ManualWin.xaml 的交互逻辑
    /// </summary>
    public partial class ManualWin : Window, WindowPermission
    {
        public string MCompareInfo
        {
            get
            {
                StringBuilderSplit sb = new StringBuilderSplit("\n");
                foreach (TextBlock it in listOper.Items)
                {
                    sb.Append(it.Text);
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// 设置标记的基本单位
        /// </summary>
        public string MStrBase
        {
            set
            {
                labMarkerTVCV.Text = value;
            }
        }

        private ManualValue m_manualValue = null;
        private List<ManualItem> m_listItem = new List<ManualItem>();       //需要执行的信息列表

        private int m_indexAS = -1;

        private System.Windows.Threading.DispatcherTimer m_timer = new System.Windows.Threading.DispatcherTimer();

        /// <summary>
        /// 自定义事件，更新紫外设置时触发
        /// </summary>
        public static readonly RoutedEvent MUpdateEvent =
             EventManager.RegisterRoutedEvent("MUpdateManual", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ManualWin));
        public event RoutedEventHandler MUpdateManual
        {
            add { AddHandler(MUpdateEvent, value); }
            remove { RemoveHandler(MUpdateEvent, value); }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        public ManualWin(ManualValue manualValue)
        {
            InitializeComponent();

            m_manualValue = manualValue;

            //初始化泵
            treeItemPump.Visibility = Visibility.Collapsed;
            treeItemPumpSystem.Visibility = ItemVisibility.s_listPump[ENUMPumpName.FITA];
            if (Visibility.Visible == treeItemPumpSystem.Visibility)
            {
                treeItemPump.Visibility = Visibility.Visible;

                cboxLengthUnit.ItemsSource = EnumBaseString.GetItemsSource();
                cboxFlowUnit.ItemsSource = EnumFlowRateString.GetItemsSource();
                if (Visibility.Visible != ItemVisibility.s_listPump[ENUMPumpName.FITB])
                {
                    gridPump.RowDefinitions[4].Height = new GridLength(0);
                }
                if (Visibility.Visible != ItemVisibility.s_listPump[ENUMPumpName.FITC])
                {
                    gridPump.RowDefinitions[6].Height = new GridLength(0);
                }
                if (Visibility.Visible != ItemVisibility.s_listPump[ENUMPumpName.FITD])
                {
                    gridPump.RowDefinitions[8].Height = new GridLength(0);
                }
            }

            treeItemPumpSample.Visibility = ItemVisibility.s_listPump[ENUMPumpName.FITS];
            if (Visibility.Visible == treeItemPumpSample.Visibility)
            {
                treeItemPump.Visibility = Visibility.Visible;

                cboxSampleLengthUnit.ItemsSource = EnumBaseString.GetItemsSource();
                cboxSampleFlowUnit.ItemsSource = EnumFlowRateString.GetItemsSource();
            }

            //初始化阀
            int indexValve = 0;
            for (int i = 0; i < ItemVisibility.s_listValve.Count; i++)
            {
                if (Visibility.Visible == ItemVisibility.s_listValve[(ENUMValveName)i])
                {
                    string header = null;
                    if (((ENUMValveName)i).ToString().Contains("_"))
                    {
                        header = Share.ReadXaml.GetResources("lab" + ((ENUMValveName)i).ToString().Substring(0, ((ENUMValveName)i).ToString().IndexOf('_')));
                    }
                    else
                    {
                        header = Share.ReadXaml.GetResources("lab" + ((ENUMValveName)i).ToString());
                    }

                    bool isExist = false;
                    foreach (TreeViewItem it in this.treeItemValve.Items)
                    {
                        if (it.Header.ToString().Equals(header))
                        {
                            isExist = true;
                            break;
                        }
                    }
                    if (!isExist)
                    {
                        treeItemValve.Items.Insert(indexValve++, new TreeViewItem() { Name = treeItemValve.Name + ((ENUMValveName)i).ToString(), Header = header });
                    }
                }
            }
            treeItemCollectionValve.Visibility = ItemVisibility.s_listValve[ENUMValveName.Out];
            treeItemCollectionCollector.Visibility = ItemVisibility.s_listCollector[ENUMCollectorName.Collector01];
            treeItemValve.Visibility = (2 < this.treeItemValve.Items.Count || Visibility.Visible == treeItemCollectionValve.Visibility || Visibility.Visible == treeItemCollectionCollector.Visibility) ? Visibility.Visible : Visibility.Collapsed;

            //初始化监控
            for (int i = 0; i < ItemVisibility.s_listAS.Count; i++)
            {
                if (Visibility.Visible == ItemVisibility.s_listAS[(ENUMASName)i])
                {
                    treeItemMonitors.Items.Add(new TreeViewItem() { Name = treeItemMonitors.Name + ((ENUMASName)i).ToString(), Header = ((ENUMASName)i).ToString() });
                }
            }
            foreach (var it in EnumMonitorInfo.NameList)
            {
                treeItemMonitors.Items.Add(new TreeViewItem() { Name = treeItemMonitors.Name + it, Header = it });
            }
            cboxMonitorAction.ItemsSource = EnumString<EnumMonitorActionManual>.GetEnumStringList("EnumMonitorAction_");
            treeItemMonitors.Visibility = 0 < treeItemMonitors.Items.Count ? Visibility.Visible : Visibility.Collapsed;

            //初始化警报警告
            treeItemAlarmWarning.Visibility = 0 < StaticAlarmWarning.SAlarmWarning.MList.Count ? Visibility.Visible : Visibility.Collapsed;
            if (Visibility.Visible == treeItemAlarmWarning.Visibility)
            {
                foreach (var it in StaticAlarmWarning.SAlarmWarning.MList)
                {
                    treeItemAlarmWarning.Items.Add(new TreeViewItem() { Name = treeItemAlarmWarning.Name + it.MName, Header = it.MName });
                }
                cboxHH.ItemsSource = EnumString<EnumAlarmWarningMode>.GetEnumStringList("Com_AW_");
                cboxLL.ItemsSource = EnumString<EnumAlarmWarningMode>.GetEnumStringList("Com_AW_");
                cboxH.ItemsSource = EnumString<EnumAlarmWarningMode>.GetEnumStringList("Com_AW_");
                cboxL.ItemsSource = EnumString<EnumAlarmWarningMode>.GetEnumStringList("Com_AW_");
            }

            //初始化其它
            treeItemUV.Visibility = ItemVisibility.s_listUV[ENUMUVName.UV01];
            treeItemRI.Visibility = ItemVisibility.s_listRI[ENUMRIName.RI01];
            treeItemMixer.Visibility = ItemVisibility.s_listMixer[ENUMMixerName.Mixer01];

            //默认设置选中泵页面
            if (Visibility.Visible == treeItemPumpSystem.Visibility)
            {
                treeItemPumpSystem.IsSelected = true;
            }
            else if (Visibility.Visible == treeItemPumpSample.Visibility)
            {
                treeItemPumpSample.IsSelected = true;
            }
            else
            {
                treeItemMarker.IsSelected = true;
            }
        }

        /// <summary>
        /// 设置各模块是否可用
        /// </summary>
        /// <param name="info"></param>
        public bool SetPermission(PermissionInfo info)
        {
            if (info.MList[(int)EnumPermission.Manual])
            {
                return true;
            }
            else
            {
                this.Close();

                return false;
            }
        }

        /// <summary>
        /// 加载界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            m_timer.Interval = TimeSpan.FromMilliseconds(500);
            m_timer.Tick += timer1_Tick;
            m_timer.Start();
        }

        /// <summary>
        /// 关闭界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            m_timer.Stop();

            if (null != this.Owner)
            {
                this.Owner.Focus();
            }
        }

        /// <summary>
        /// 查找序号
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private int FindIndex(string name)
        {
            for (int i = 0; i < m_listItem.Count; i++)
            {
                if (m_listItem[i].MNameCurr.Equals(name) || (null != m_listItem[i].MNameParent && m_listItem[i].MNameParent.Equals(treeItemMonitors.Name) && !m_listItem[i].MNameCurr.Contains("AS")))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// 设置当前选中Tab页
        /// </summary>
        /// <param name="item"></param>
        private void ShowTab(TabItem item)
        {
            foreach (TabItem it in tabControl.Items)
            {
                it.Visibility = Visibility.Collapsed;
            }

            if (null != item)
            {
                item.Visibility = Visibility.Visible;
                tabControl.SelectedItem = item;
            }
            else
            {
                tabControl.SelectedItem = null;
            }
        }

        private void ShowTabPumpSystem(int index)
        {
            ShowTab(tabPumpSystem);

            PumpSystemValue temp = null;
            if (-1 == index)
            {
                temp = Share.DeepCopy.DeepCopyByXml(m_manualValue.m_pumpSystemValue);
            }
            else
            {
                temp = (PumpSystemValue)m_listItem[index].MValue;
            }

            doubleLength.DataContext = temp;
            cboxLengthUnit.DataContext = temp;
            doubleFlow.DataContext = temp;
            cboxFlowUnit.DataContext = temp;
            doubleBS.DataContext = temp;
            doubleBE.DataContext = temp;
            doubleCS.DataContext = temp;
            doubleCE.DataContext = temp;
            doubleDS.DataContext = temp;
            doubleDE.DataContext = temp;
        }

        private void ShowTabPumpSample(int index)
        {
            ShowTab(tabPumpSample);

            PumpSampleValue temp = null;
            if (-1 == index)
            {
                temp = Share.DeepCopy.DeepCopyByXml(m_manualValue.m_pumpSampleValue);
            }
            else
            {
                temp = (PumpSampleValue)m_listItem[index].MValue;
            }

            doubleSampleLength.DataContext = temp;
            cboxSampleLengthUnit.DataContext = temp;
            doubleSampleFlow.DataContext = temp;
            cboxSampleFlowUnit.DataContext = temp;
        }

        private void ShowTabValve(int index, string header, string name)
        {
            tabValve.Header = header;
            ShowTab(tabValve);

            cboxPosition.DataContext = null;
            cboxPosition.ItemsSource = null;
            cboxPosition.ItemsSource = StaticValue.GetNameList((ENUMValveName)Enum.Parse(typeof(ENUMValveName), name));

            StringInt temp = null;
            if (-1 == index)
            {
                temp = m_manualValue.m_valveValue.MListValave[(int)(ENUMValveName)Enum.Parse(typeof(ENUMValveName), name)].Clone();
            }
            else
            {
                temp = (StringInt)m_listItem[index].MValue;
            }

            cboxPosition.DataContext = temp;
        }

        private void ShowTabCollection(int index, string header, EnumCollectionType type)
        {
            tabCollection.Header = header;
            ShowTab(tabCollection);

            CollectionValve temp = m_manualValue.m_collValveValue;
            if (-1 != index)
            {
                temp = (CollectionValve)m_listItem[index].MValue;
            }
            collectionUC.MCollectionValve = DeepCopy.DeepCopyByXml(temp);
            CollectionCollector tempCollector = m_manualValue.m_collCollectorValue;
            if (-1 != index)
            {
                tempCollector = (CollectionCollector)m_listItem[index].MValue;
            }
            collectionUC.MCollectionCollector = DeepCopy.DeepCopyByXml(tempCollector);
            collectionUC.MType = type;
        }

        private void ShowTabAS(int index, string header, string name)
        {
            tabAS.Header = header;
            ShowTab(tabAS);

            if (-1 == index)
            {
                m_indexAS = (int)System.Enum.Parse(typeof(ENUMASName), name);
                asParaUC.DataContext = new ASManualParaVM(DeepCopy.DeepCopyByXml(m_manualValue.m_ASValue.MList[m_indexAS]));
            }
            else
            {
                asParaUC.DataContext = new ASManualParaVM((ASManualPara)m_listItem[index].MValue);
            }
        }

        private void ShowTabpHCdUV(int index, string header, string name)
        {
            tabpHCdUV.Header = header.Replace("_", "__");//无法显示“_”
            ShowTab(tabpHCdUV);

            MonitorValue temp = null;
            if (-1 == index)
            {
                if (name.Equals(m_manualValue.m_MonitorValue.MValue.MName))
                {
                    temp = m_manualValue.m_MonitorValue.Clone();
                }
                else
                {
                    temp = new MonitorValue();
                    temp.MValue.MName = name;
                }
            }
            else
            {
                if (name.Equals(m_manualValue.m_MonitorValue.MValue.MName))
                {
                    temp = (MonitorValue)m_listItem[index].MValue;
                }
                else
                {
                    temp = new MonitorValue();
                    temp.MValue.MName = name;
                }
            }

            cboxMonitorAction.DataContext = temp;
            monitorParaUC.MMonitroPara = temp.MValue;
        }

        private void ShowTabAlarmWarning(int index, string header, string name)
        {
            tabAlarmWarning.Header = header.Replace("_", "__");//无法显示“_”
            ShowTab(tabAlarmWarning);

            AlarmWarningItem temp = null;
            
            if (-1 == index)
            {
                foreach (var it in StaticAlarmWarning.SAlarmWarning.MList)
                {
                    if (name.Equals(it.MName))
                    {
                        temp = Share.DeepCopy.DeepCopyByXml(it);
                        break;
                    }
                }
            }
            else
            {
                temp = (AlarmWarningItem)m_listItem[index].MValue;
            }

            AlarmWarningItemVM tempVM = new AlarmWarningItemVM(temp);
            foreach (FrameworkElement it in gridAlarmWarning.Children)
            {
                if (it is TextBlock)
                {
                    continue;
                }
                it.DataContext = tempVM;
            }

            labHHRange.Text = "[" + temp.MValMin + "-" + temp.MValMax + "]";
            labHHUnit.Text = temp.MUnit;
        }

        private void ShowTabMarker(int index, string header)
        {
            tabMarker.Header = header;
            ShowTab(tabMarker);

            MarkerValue temp = null;
            if (-1 == index)
            {
                temp = new MarkerValue();
            }
            else
            {
                temp = (MarkerValue)m_listItem[index].MValue;
            }

            foreach (FrameworkElement it in gridMarker.Children)
            {
                if (it is TextBlock)
                {
                    continue;
                }
                it.DataContext = temp;
            }
        }

        private void ShowTabPauseStop(int index, string header, PauseStopValue temp)
        {
            tabPauseStop.Header = header;
            ShowTab(tabPauseStop);

            if (-1 == index)
            {
                temp = temp.Clone();
            }
            else
            {
                temp = (PauseStopValue)m_listItem[index].MValue;
            }

            foreach (FrameworkElement it in gridPauseStop.Children)
            {
                if (it is TextBlock)
                {
                    continue;
                }
                it.DataContext = temp;
            }
        }

        private void ShowTabUV(int index, string header)
        {
            tabUV.Header = header;
            ShowTab(tabUV);

            if (-1 == index)
            {
                ucUV.DataContext = new UVValueVM() { MItem = DeepCopy.DeepCopyByXml(m_manualValue.m_uvValue) };
            }
            else
            {
                ucUV.DataContext = new UVValueVM() { MItem = (UVValue)m_listItem[index].MValue };
            }
        }

        private void ShowTabRI(int index, string header)
        {
            tabRI.Header = header;
            ShowTab(tabRI);

            RIValue temp = null;
            if (-1 == index)
            {
                temp = m_manualValue.m_riValue.Clone();
            }
            else
            {
                temp = (RIValue)m_listItem[index].MValue;
            }

            ucRI.MRIValueOld = temp;
        }

        private void ShowTabMixer(int index, string header)
        {
            tabMixer.Header = header;
            ShowTab(tabMixer);

            if (-1 == index)
            {
                ucMixer.DataContext = new MixerValueVM() { MItem = DeepCopy.DeepCopyByXml(m_manualValue.m_mixerValue) };
            }
            else
            {
                ucMixer.DataContext = new MixerValueVM() { MItem = (MixerValue)m_listItem[index].MValue };
            }
        }

        private string AddTabPumpSystem(string nameCurr, string headerCurr, string nameParent = null, string headerParent = null)
        {
            string error = StaticValue.CheckData((EnumFlowRate)cboxFlowUnit.SelectedIndex, doubleFlow.Value, doubleBS.Value, doubleBE.Value, doubleCS.Value, doubleCE.Value, doubleDS.Value, doubleDE.Value);
            if (!string.IsNullOrEmpty(error))
            {
                MessageBoxWin.Show(error);
                return "";
            }

            m_listItem.Add(new ManualItem((PumpSystemValue)doubleLength.DataContext, nameCurr, headerCurr, nameParent, headerParent));

            StringBuilderSplit sb = new StringBuilderSplit("\n");
            sb.Append(((TabItem)tabControl.SelectedItem).Header.ToString());
            sb.AppendLeftBracket();
            sb.Append(labFlow.Text + doubleFlow.Value + cboxFlowUnit.Text);
            sb.Append(labLength.Text + doubleLength.Value + cboxLengthUnit.Text);
            if (Visibility.Visible == ItemVisibility.s_listPump[ENUMPumpName.FITB])
            {
                sb.Append(labBS.Text + doubleBS.Value + " " + labBE.Text + doubleBE.Value);
            }
            if (Visibility.Visible == ItemVisibility.s_listPump[ENUMPumpName.FITC])
            {
                sb.Append(labCS.Text + doubleCS.Value + " " + labCE.Text + doubleCE.Value);
            }
            if (Visibility.Visible == ItemVisibility.s_listPump[ENUMPumpName.FITD])
            {
                sb.Append(labDS.Text + doubleDS.Value + " " + labDE.Text + doubleDE.Value);
            }
            sb.AppendRightBracket();
            return sb.ToString();
        }

        private string AddTabPumpSample(string nameCurr, string headerCurr, string nameParent = null, string headerParent = null)
        {
            m_listItem.Add(new ManualItem((PumpSampleValue)doubleSampleLength.DataContext, nameCurr, headerCurr, nameParent, headerParent));

            StringBuilderSplit sb = new StringBuilderSplit("\n");
            sb.Append(((TabItem)tabControl.SelectedItem).Header.ToString());
            sb.AppendLeftBracket();
            sb.Append(labSampleFlow.Text + doubleSampleFlow.Value + cboxSampleFlowUnit.Text);
            sb.Append(labSampleLength.Text + doubleSampleLength.Value + cboxSampleLengthUnit.Text);
            sb.AppendRightBracket();
            return sb.ToString();
        }

        private string AddTabValve(string nameCurr, string headerCurr, string nameParent = null, string headerParent = null)
        {
            m_listItem.Add(new ManualItem((StringInt)cboxPosition.DataContext, nameCurr, headerCurr, nameParent, headerParent));

            StringBuilderSplit sb = new StringBuilderSplit("\n");
            sb.Append(((TabItem)tabControl.SelectedItem).Header.ToString());
            sb.Append("{" + cboxPosition.SelectedItem.ToString() + "}");
            return sb.ToString();
        }

        private string AddTabCollectionValve(string nameCurr, string headerCurr, string nameParent = null, string headerParent = null)
        {
            m_listItem.Add(new ManualItem(DeepCopy.DeepCopyByXml(collectionUC.MCollectionValve), nameCurr, headerCurr, nameParent, headerParent));

            StringBuilderSplit sb = new StringBuilderSplit();
            sb.Append(((TabItem)tabControl.SelectedItem).Header.ToString());
            sb.AppendLeftBracket();
            foreach (var it in collectionUC.MCollectionValve.MList)
            {
                sb.Append(it.MShowInfo);
            }
            sb.AppendRightBracket();
            return sb.ToString();
        }

        private string AddTabCollectionCollector(string nameCurr, string headerCurr, string nameParent = null, string headerParent = null)
        {
            m_listItem.Add(new ManualItem(DeepCopy.DeepCopyByXml(collectionUC.MCollectionCollector), nameCurr, headerCurr, nameParent, headerParent));

            StringBuilderSplit sb = new StringBuilderSplit("\n");
            sb.Append(((TabItem)tabControl.SelectedItem).Header.ToString());
            sb.AppendLeftBracket();
            foreach (var it in collectionUC.MCollectionCollector.MList)
            {
                sb.Append(it.MShowInfo);
            }
            sb.AppendRightBracket();
            return sb.ToString();
        }

        private string AddTabAS(string nameCurr, string headerCurr, string nameParent = null, string headerParent = null)
        {
            StringBuilderSplit sb = new StringBuilderSplit();
            string info = asParaUC.GetLog(m_manualValue.m_ASValue.MList[m_indexAS], false);
            if (!string.IsNullOrEmpty(info))
            {
                m_listItem.Add(new ManualItem(((ASManualParaVM)asParaUC.DataContext).MItem, nameCurr, headerCurr, nameParent, headerParent));

                sb.Append(((TabItem)tabControl.SelectedItem).Header.ToString());
                sb.AppendLeftBracket();
                sb.Append(info);
                sb.AppendRightBracket();
            }

            return sb.ToString();
        }

        private string AddTabpHCdUV(string nameCurr, string headerCurr, string nameParent = null, string headerParent = null)
        {
            m_listItem.Add(new ManualItem((MonitorValue)cboxMonitorAction.DataContext, nameCurr, headerCurr, nameParent, headerParent));

            StringBuilderSplit sb = new StringBuilderSplit("\n");
            sb.Append(((TabItem)tabControl.SelectedItem).Header.ToString().Replace("__", "_"));
            sb.AppendLeftBracket();
            sb.Append(labMonitorAction.Text + cboxMonitorAction.Text);
            sb.Append(monitorParaUC.GetLog());
            sb.AppendRightBracket();
            return sb.ToString();
        }

        private string AddTabAlarmWarning(string nameCurr, string headerCurr, string nameParent = null, string headerParent = null)
        {
            AlarmWarningItem val = ((AlarmWarningItemVM)doubleHH.DataContext).MItem;
            m_listItem.Add(new ManualItem(val, nameCurr, headerCurr, nameParent, headerParent));

            StringBuilderSplit sb = new StringBuilderSplit("\n");
            sb.Append(((TabItem)tabControl.SelectedItem).Header.ToString().Replace("__", "_"));
            sb.AppendLeftBracket();
            switch (val.MCheckHH)
            {
                case EnumAlarmWarningMode.Disabled: sb.Append(labHH.Text + cboxHH.Text); break;
                case EnumAlarmWarningMode.Dly: sb.Append(labHH.Text + doubleHH.Value); break;
            }
            switch (val.MCheckLL)
            {
                case EnumAlarmWarningMode.Disabled: sb.Append(labLL.Text + cboxLL.Text); break;
                case EnumAlarmWarningMode.Dly: sb.Append(labLL.Text + doubleLL.Value); break;
            }
            switch (val.MCheckH)
            {
                case EnumAlarmWarningMode.Disabled: sb.Append(labH.Text + cboxH.Text); break;
                case EnumAlarmWarningMode.Dly: sb.Append(labH.Text + doubleH.Value); break;
            }
            switch (val.MCheckL)
            {
                case EnumAlarmWarningMode.Disabled: sb.Append(labL.Text + cboxL.Text); break;
                case EnumAlarmWarningMode.Dly: sb.Append(labL.Text + doubleL.Value); break;
            }
            sb.AppendRightBracket();
            return sb.ToString();
        }

        private string AddTabMarker(string nameCurr, string headerCurr, string nameParent = null, string headerParent = null)
        {
            m_listItem.Add(new ManualItem((MarkerValue)rbtnNow.DataContext, nameCurr, headerCurr, nameParent, headerParent));

            StringBuilderSplit sb = new StringBuilderSplit("\n");
            sb.Append(((TabItem)tabControl.SelectedItem).Header.ToString());
            if (true == rbtnNow.IsChecked)
            {
                sb.Append("{" + txtMarker.Text + " " + rbtnNow.Content.ToString() + "}");
            }
            else
            {
                sb.Append("{" + txtMarker.Text + " " + rbtnPast.Content.ToString() + doubelMarkerTVCV.Value + "}");
            }
            return sb.ToString();
        }

        private string AddTabPauseStop(string nameCurr, string headerCurr, string nameParent = null, string headerParent = null)
        {
            m_listItem.Add(new ManualItem((PauseStopValue)rbtnT.DataContext, nameCurr, headerCurr, nameParent, headerParent));

            StringBuilderSplit sb = new StringBuilderSplit("\n");
            sb.Append(((TabItem)tabControl.SelectedItem).Header.ToString());
            if (true == rbtnT.IsChecked)
            {
                sb.Append("{" + rbtnT.Content.ToString() + doubleT.Value + "}");
            }
            else if (true == rbtnV.IsChecked)
            {
                sb.Append("{" + rbtnV.Content.ToString() + doubleV.Value + "}");
            }
            else
            {
                sb.Append("{" + rbtnCV.Content.ToString() + doubleCV.Value + "}");
            }
            return sb.ToString();
        }

        private string AddTabUV(string nameCurr, string headerCurr, string nameParent = null, string headerParent = null)
        {
            StringBuilderSplit sb = new StringBuilderSplit();
            string info = ucUV.GetLog(m_manualValue.m_uvValue, false);
            if (!string.IsNullOrEmpty(info))
            {
                m_listItem.Add(new ManualItem(((UVValueVM)ucUV.DataContext).MItem, nameCurr, headerCurr, nameParent, headerParent));

                sb.Append(((TabItem)tabControl.SelectedItem).Header.ToString());
                sb.AppendLeftBracket();
                sb.Append(info);
                sb.AppendRightBracket();
            }

            return sb.ToString();
        }

        private string AddTabRI(string nameCurr, string headerCurr, string nameParent = null, string headerParent = null)
        {
            string info = ucRI.GetLog();
            if (!string.IsNullOrEmpty(info))
            {
                m_listItem.Add(new ManualItem(ucRI.MRIValueNew, nameCurr, headerCurr, nameParent, headerParent));
            }

            return info;
        }

        private string AddTabMixer(string nameCurr, string headerCurr, string nameParent = null, string headerParent = null)
        {
            StringBuilderSplit sb = new StringBuilderSplit("\n");
            string info = ucMixer.GetLog(m_manualValue.m_mixerValue, false);
            if (!string.IsNullOrEmpty(info))
            {
                m_listItem.Add(new ManualItem(((MixerValueVM)ucMixer.DataContext).MItem, nameCurr, headerCurr, nameParent, headerParent));

                sb.Append(((TabItem)tabControl.SelectedItem).Header.ToString());
                sb.AppendLeftBracket();
                sb.Append(info);
                sb.AppendRightBracket();
            }

            return sb.ToString();
        }

        /// <summary>
        /// 切换当前tab页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            string currName = ((TreeViewItem)tree.SelectedItem).Name;
            string currHeader = ((TreeViewItem)tree.SelectedItem).Header.ToString();
            int index = FindIndex(currName);
            if (currName.Equals(treeItemPump.Name))
            {
                //泵
                ShowTab(null);
            }
            else if (currName.Equals(treeItemValve.Name))
            {
                //阀
                ShowTab(null);
            }
            else if (currName.Equals(treeItemMonitors.Name))
            {
                //监控
                ShowTab(null);
            }
            else if (currName.Equals(treeItemAlarmWarning.Name))
            {
                //警报警告
                ShowTab(null);
            }
            else if (currName.Equals(treeItemOther.Name))
            {
                //其它
                ShowTab(null);
            }
            else
            {
                string parentName = ((TreeViewItem)((TreeViewItem)tree.SelectedItem).Parent).Name;
                string parentHeader = ((TreeViewItem)((TreeViewItem)tree.SelectedItem).Parent).Header.ToString();
                if (parentName.Equals(treeItemPump.Name))
                {
                    //泵的下级
                    if (currName.Equals(treeItemPumpSystem.Name))
                    {
                        ShowTabPumpSystem(index);
                    }
                    else if (currName.Equals(treeItemPumpSample.Name))
                    {
                        ShowTabPumpSample(index);
                    }
                    else
                    {

                    }
                }
                else if (parentName.Equals(treeItemValve.Name))
                {
                    //阀的下级
                    if (currName.Equals(treeItemCollectionValve.Name))
                    {
                        ShowTabCollection(index, parentHeader + "-" + currHeader, EnumCollectionType.Valve);
                    }
                    else if (currName.Equals(treeItemCollectionCollector.Name))
                    {
                        ShowTabCollection(index, parentHeader + "-" + currHeader, EnumCollectionType.Collector);
                    }
                    else
                    {
                        ShowTabValve(index, parentHeader + "-" + currHeader, currName.Replace(parentName, ""));
                    }
                }
                else if (parentName.Equals(treeItemMonitors.Name))
                {
                    //监控的下级
                    if (currName.Contains("AS"))
                    {
                        ShowTabAS(index, parentHeader + "-" + currHeader, currName.Replace(parentName, ""));
                    }
                    else
                    {
                        ShowTabpHCdUV(index, parentHeader + "-" + currHeader, currName.Replace(parentName, ""));
                    }
                }
                else if (parentName.Equals(treeItemAlarmWarning.Name))
                {
                    //警报警告的下级
                    ShowTabAlarmWarning(index, parentHeader + "-" + currHeader, currName.Replace(parentName, ""));
                }
                else if (parentName.Equals(treeItemOther.Name))
                {
                    //其它的下级
                    if (currName.Equals(treeItemMarker.Name))
                    {
                        ShowTabMarker(index, parentHeader + "-" + currHeader);
                    }
                    else if (currName.Equals(treeItemPause.Name))
                    {
                        ShowTabPauseStop(index, parentHeader + "-" + currHeader, m_manualValue.m_pauseValue);
                    }
                    else if (currName.Equals(treeItemStop.Name))
                    {
                        ShowTabPauseStop(index, parentHeader + "-" + currHeader, m_manualValue.m_stopValue);
                    }
                    else if (currName.Equals(treeItemUV.Name))
                    {
                        ShowTabUV(index, parentHeader + "-" + currHeader);
                    }
                    else if (currName.Equals(treeItemRI.Name))
                    {
                        ShowTabRI(index, parentHeader + "-" + currHeader);
                    }
                    else if (currName.Equals(treeItemMixer.Name))
                    {
                        ShowTabMixer(index, parentHeader + "-" + currHeader);
                    }
                    else
                    {    

                    }
                }
            }
        }

        /// <summary>
        /// 泵的流速单位发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboxFlowUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch ((EnumFlowRate)cboxFlowUnit.SelectedIndex)
            {
                case EnumFlowRate.MLMIN:
                    labFlowMax.Text = StaticValue.s_maxFlowVolUnit;
                    doubleFlow.Maximum = StaticValue.s_maxFlowVol;
                    break;
                case EnumFlowRate.CMH:
                    labFlowMax.Text = StaticValue.s_maxFlowLenUnit;
                    doubleFlow.Maximum = StaticValue.s_maxFlowLen;
                    break;
            }

            if (doubleFlow.Value > doubleFlow.Maximum)
            {
                doubleFlow.Value = doubleFlow.Maximum;
            }
        }

        /// <summary>
        /// 泵的流速单位发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboxSampleFlowUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch ((EnumFlowRate)cboxFlowUnit.SelectedIndex)
            {
                case EnumFlowRate.MLMIN:
                    labSampleFlowMax.Text = StaticValue.s_maxFlowSVolUnit;
                    doubleSampleFlow.Maximum = StaticValue.s_maxFlowSVol;
                    break;
                case EnumFlowRate.CMH:
                    labSampleFlowMax.Text = StaticValue.s_maxFlowSLenUnit;
                    doubleSampleFlow.Maximum = StaticValue.s_maxFlowSLen;
                    break;
            }

            if (doubleSampleFlow.Value > doubleSampleFlow.Maximum)
            {
                doubleSampleFlow.Value = doubleSampleFlow.Maximum;
            }
        }

        /// <summary>
        /// 定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            txtPause.Text = m_manualValue.m_pauseValue.GetInfo();
            if (string.IsNullOrEmpty(txtPause.Text))
            {
                labPause.Visibility = Visibility.Hidden;
                txtPause.Visibility = Visibility.Hidden;
            }
            else
            {
                labPause.Visibility = Visibility.Visible;
                txtPause.Visibility = Visibility.Visible;
            }

            txtStop.Text = m_manualValue.m_stopValue.GetInfo();
            if (string.IsNullOrEmpty(txtStop.Text))
            {
                labStop.Visibility = Visibility.Hidden;
                txtStop.Visibility = Visibility.Hidden;
            }
            else
            {
                labStop.Visibility = Visibility.Visible;
                txtStop.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string currName = ((TreeViewItem)tree.SelectedItem).Name;
            string currHeader = ((TreeViewItem)tree.SelectedItem).Header.ToString();

            int index = FindIndex(currName);
            if (-1 != index)
            {
                m_listItem.RemoveAt(index);
                listOper.Items.RemoveAt(index);
            }

            TextBlock textBlock = new TextBlock();
            textBlock.Height = 35;
            if (currName.Equals(treeItemPump.Name))
            {
                
            }
            else if (currName.Equals(treeItemValve.Name))
            {

            }
            else if (currName.Equals(treeItemMonitors.Name))
            {

            }
            else if (currName.Equals(treeItemAlarmWarning.Name))
            {

            }
            else if (currName.Equals(treeItemOther.Name))
            {

            }
            else
            {
                string parentName = ((TreeViewItem)((TreeViewItem)tree.SelectedItem).Parent).Name;
                string parentHeader = ((TreeViewItem)((TreeViewItem)tree.SelectedItem).Parent).Header.ToString();
                if (parentName.Equals(treeItemPump.Name))
                {
                    if (currName.Equals(treeItemPumpSystem.Name))
                    {
                        textBlock.Text = AddTabPumpSystem(currName, currHeader, parentName, parentHeader);
                    }
                    else if (currName.Equals(treeItemPumpSample.Name))
                    {
                        textBlock.Text = AddTabPumpSample(currName, currHeader, parentName, parentHeader);
                    }
                    else
                    {

                    }
                }
                else if (parentName.Equals(treeItemValve.Name))
                {
                    if (currName.Equals(treeItemCollectionValve.Name))
                    {
                        textBlock.Text = AddTabCollectionValve(currName, currHeader, parentName, parentHeader);
                    }
                    else if (currName.Equals(treeItemCollectionCollector.Name))
                    {
                        textBlock.Text = AddTabCollectionCollector(currName, currHeader, parentName, parentHeader);
                    }
                    else
                    {
                        textBlock.Text = AddTabValve(currName, currHeader, parentName, parentHeader);
                    }
                }
                else if (parentName.Equals(treeItemMonitors.Name))
                {
                    if (currName.Contains("AS"))
                    {
                        textBlock.Text = AddTabAS(currName, currHeader, parentName, parentHeader);
                    }
                    else
                    {
                        textBlock.Text = AddTabpHCdUV(currName, currHeader, parentName, parentHeader);
                    }
                }
                else if (parentName.Equals(treeItemAlarmWarning.Name))
                {
                    textBlock.Text = AddTabAlarmWarning(currName, currHeader, parentName, parentHeader);
                }
                else if (parentName.Equals(treeItemOther.Name))
                {
                    if (currName.Equals(treeItemMarker.Name))
                    {
                        textBlock.Text = AddTabMarker(currName, currHeader, parentName, parentHeader);
                    }
                    else if(currName.Equals(treeItemPause.Name))
                    {
                        textBlock.Text = AddTabPauseStop(currName, currHeader, parentName, parentHeader);
                    }
                    else if (currName.Equals(treeItemStop.Name))
                    {
                        textBlock.Text = AddTabPauseStop(currName, currHeader, parentName, parentHeader);
                    }
                    else if (currName.Equals(treeItemUV.Name))
                    {
                        textBlock.Text = AddTabUV(currName, currHeader, parentName, parentHeader);
                    }
                    else if (currName.Equals(treeItemRI.Name))
                    {
                        textBlock.Text = AddTabRI(currName, currHeader, parentName, parentHeader);
                    }
                    else if (currName.Equals(treeItemMixer.Name))
                    {
                        textBlock.Text = AddTabMixer(currName, currHeader, parentName, parentHeader);
                    }
                    else
                    {
                        
                    }
                }
            }

            if (string.IsNullOrEmpty(textBlock.Text))
            {
                return;
            }

            textBlock.ToolTip = textBlock.Text;
            listOper.Items.Add(textBlock);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (-1 != listOper.SelectedIndex)
            {
                int index = listOper.SelectedIndex;
                m_listItem.RemoveAt(index);
                listOper.Items.RemoveAt(index);
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            lock (m_manualValue)
            {
                foreach (var it in m_listItem)
                {
                    if (it.MNameCurr.Equals(treeItemPump.Name))
                    {
                        
                    }
                    else if (it.MNameCurr.Equals(treeItemValve.Name))
                    {

                    }
                    else if (it.MNameCurr.Equals(treeItemMonitors.Name))
                    {

                    }
                    else if (it.MNameCurr.Equals(treeItemAlarmWarning.Name))
                    {

                    }
                    else if (it.MNameCurr.Equals(treeItemOther.Name))
                    {

                    }
                    else
                    {
                        if (it.MNameParent.Equals(treeItemPump.Name))
                        {
                            if (it.MNameCurr.Equals(treeItemPumpSystem.Name))
                            {
                                m_manualValue.m_pumpSystemValue = (PumpSystemValue)it.MValue;
                                m_manualValue.m_pumpSystemValue.m_update = true;
                            }
                            else if (it.MNameCurr.Equals(treeItemPumpSample.Name))
                            {
                                m_manualValue.m_pumpSampleValue = (PumpSampleValue)it.MValue;
                                m_manualValue.m_pumpSampleValue.m_update = true;
                            }
                            else
                            {
                           
                            }
                        }
                        else if (it.MNameParent.Equals(treeItemValve.Name))
                        {
                            if (it.MNameCurr.Equals(treeItemCollectionValve.Name))
                            {
                                m_manualValue.m_collValveValue = (CollectionValve)it.MValue;
                                m_manualValue.m_collValveValue.m_update = true;
                            }
                            else if (it.MNameCurr.Equals(treeItemCollectionCollector.Name))
                            {
                                m_manualValue.m_collCollectorValue = (CollectionCollector)it.MValue;
                                m_manualValue.m_collCollectorValue.m_update = true;
                            }
                            else
                            {
                                m_manualValue.m_valveValue.MListValave[(int)Enum.Parse(typeof(ENUMValveName), it.MNameCurr.Replace(it.MNameParent,""))] = (StringInt)it.MValue;
                                m_manualValue.m_valveValue.m_update = true;
                            }
                        }
                        else if (it.MNameParent.Equals(treeItemMonitors.Name))
                        {
                            if (it.MNameCurr.Contains("AS"))
                            {
                                m_manualValue.m_ASValue.MList[m_indexAS] = (ASManualPara)it.MValue;
                                m_manualValue.m_ASValue.MList[m_indexAS].m_update = true;
                            }
                            else
                            {
                                m_manualValue.m_MonitorValue = (MonitorValue)it.MValue;
                                m_manualValue.m_MonitorValue.m_update = true;
                            }
                        }
                        else if (it.MNameParent.Equals(treeItemAlarmWarning.Name))
                        {
                            bool exist = false;
                            foreach (var itAW in m_manualValue.m_alarmWarningValue.m_alarmWarning.MList)
                            {
                                if (itAW.MName.Equals(it.MNameCurr.Replace(it.MNameParent, "")))
                                {
                                    itAW.SetValue((AlarmWarningItem)it.MValue);
                                    exist = true;
                                    break;
                                }
                            }
                            if (!exist)
                            {
                                m_manualValue.m_alarmWarningValue.m_alarmWarning.MList.Add((AlarmWarningItem)it.MValue); 
                            }
                            m_manualValue.m_alarmWarningValue.m_update = true;
                        }
                        else if (it.MNameParent.Equals(treeItemOther.Name))
                        {
                            if (it.MNameCurr.Equals(treeItemMarker.Name))
                            {
                                m_manualValue.m_markerValue = (MarkerValue)it.MValue;
                                m_manualValue.m_markerValue.m_update = true;
                            }
                            else if (it.MNameCurr.Equals(treeItemPause.Name))
                            {
                                m_manualValue.m_pauseValue = (PauseStopValue)it.MValue;
                                m_manualValue.m_pauseValue.m_update = true;
                            }
                            else if (it.MNameCurr.Equals(treeItemStop.Name))
                            {
                                m_manualValue.m_stopValue = (PauseStopValue)it.MValue;
                                m_manualValue.m_stopValue.m_update = true;
                            }
                            else if (it.MNameCurr.Equals(treeItemUV.Name))
                            {
                                m_manualValue.m_uvValue = (UVValue)it.MValue;
                                m_manualValue.m_uvValue.m_update = true;

                                ucUV.DataContext = new UVValueVM() { MItem = DeepCopy.DeepCopyByXml(m_manualValue.m_uvValue) };
                            }
                            else if (it.MNameCurr.Equals(treeItemRI.Name))
                            {
                                m_manualValue.m_riValue = (RIValue)it.MValue;
                                m_manualValue.m_riValue.m_update = true;
                            }
                            else if (it.MNameCurr.Equals(treeItemMixer.Name))
                            {
                                m_manualValue.m_mixerValue = (MixerValue)it.MValue;
                                m_manualValue.m_mixerValue.m_update = true;

                                ucMixer.DataContext = new MixerValueVM() { MItem = DeepCopy.DeepCopyByXml(m_manualValue.m_mixerValue) };
                            }
                            else
                            {

                            }
                        }
                    }
                }
            }

            //DialogResult = true;
            RoutedEventArgs args = new RoutedEventArgs(MUpdateEvent, MCompareInfo);
            RaiseEvent(args);

            m_listItem.Clear();
            listOper.Items.Clear();
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