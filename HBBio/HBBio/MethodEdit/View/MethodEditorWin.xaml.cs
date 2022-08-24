using HBBio.ColumnList;
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
    /// MethodEditorWin.xaml 的交互逻辑
    /// </summary>
    public partial class MethodEditorWin : Window
    {
        private MethodVM MMethod { get; set; }
        private Label m_lastBtn = null;

        private object m_labMove = null;
        private bool m_isPressMove = false;
        private Point m_clickPtMove = new Point(0, 0);

        private object m_labCopy = null;
        private bool m_isPressCopy = false;
        private Point m_clickPtCopy = new Point(0, 0);

        private bool m_doubleClick = false;

        private bool m_clickSave = false;
        public bool MClickSave
        {
            get
            {
                return m_clickSave;
            }
        }


        /// <summary>
        /// 自定义事件，发送方法或者方法序列时触发
        /// </summary>
        public static readonly RoutedEvent MSelectEvent =
             EventManager.RegisterRoutedEvent("MSelectItem", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MethodEditorWin));
        public event RoutedEventHandler MSelectItem
        {
            add { AddHandler(MSelectEvent, value); }
            remove { RemoveHandler(MSelectEvent, value); }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="method"></param>
        public MethodEditorWin(Window parent, Method method)
        {
            InitializeComponent();
            InitPersonalPhases();
            InitDynamicUI();

            this.Owner = parent;

            //赋值方法对象
            MMethod = new MethodVM();
            MMethod.MItem = method;
            MMethod.InitHandler();

            //Tab全部隐藏
            foreach (TabItem item in tabControl.Items)
            {
                item.Visibility = Visibility.Collapsed;
            }

            TabItemVisibility();
            ItemsSourceMethodSettings();
            DataContextMethodSettings(MMethod.MMethodSetting);


            
        }

        /// <summary>
        /// 方法是否可以编辑发送
        /// </summary>
        /// <param name="save"></param>
        /// <param name="send"></param>
        public void SaveSend(bool save, bool send)
        {
            btnSave.IsEnabled = save;
            btnSend.IsEnabled = send;
        }

        /// <summary>
        /// 资源初始化-方法设置
        /// </summary>
        private void ItemsSourceMethodSettings()
        {
            ColumnManager columnManager = new ColumnManager();
            List<ColumnItem> listColName = null;
            columnManager.GetNameList(out listColName);
            cboxColumnName.ItemsSource = listColName;
            cboxCPV.ItemsSource = EnumCPVInfo.NameList;

            cboxMBU.ItemsSource = EnumBaseString.GetItemsSource();
            cboxFRU.ItemsSource = EnumFlowRateString.GetItemsSource();

            cboxInletA.ItemsSource = EnumInAInfo.NameList;
            cboxInletB.ItemsSource = EnumInBInfo.NameList;
            cboxInletC.ItemsSource = EnumInCInfo.NameList;
            cboxInletD.ItemsSource = EnumInDInfo.NameList;
            cboxBPV.ItemsSource = EnumBPVInfo.NameList;
        }

        /// <summary>
        /// 初始化数量不固定的界面
        /// </summary>
        private void InitDynamicUI()
        {
            foreach (ENUMASName it in Enum.GetValues(typeof(ENUMASName)))
            {
                if (Visibility.Visible == ItemVisibility.s_listAS[it])
                {
                    stackAirSensorAlarm.Children.Add(new ASMethodParaUC());
                }
            }
        }

        /// <summary>
        /// 初始化自定义用户阶段
        /// </summary>
        private void InitPersonalPhases()
        {
            while (gridPhase.Children.Count > Enum.GetNames(typeof(EnumPhaseType)).GetLength(0) - 1)
            {
                gridPhase.Children.RemoveAt(gridPhase.Children.Count - 1);
            }
            MethodManager methodManager = new MethodManager();
            List<string> listName = new List<string>();
            if (null == methodManager.GetPhaseNameList(out listName))
            {
                foreach (var it in listName)
                {
                    Label label = new Label();
                    label.Style = (Style)this.Resources["labCopyPhase"];
                    label.Template = (ControlTemplate)this.Resources["btnPhase"];
                    label.Content = it;
                    gridPhase.Children.Add(label);
                }
            }
        }

        /// <summary>
        /// 控件可见性-方法设置
        /// </summary>
        private void TabItemVisibility()
        {
            //方法设置
            labCPV.Visibility = ItemVisibility.s_listValve[ENUMValveName.CPV_1];

            if (Visibility.Visible != ItemVisibility.s_listValve[ENUMValveName.InA]
                && Visibility.Visible != ItemVisibility.s_listValve[ENUMValveName.InB]
                && Visibility.Visible != ItemVisibility.s_listValve[ENUMValveName.InC]
                && Visibility.Visible != ItemVisibility.s_listValve[ENUMValveName.InD]
                && Visibility.Visible != ItemVisibility.s_listValve[ENUMValveName.BPV])
            {
                groupValveSelection.Visibility = Visibility.Collapsed;
            }
            else
            {
                labInletA.Visibility = ItemVisibility.s_listValve[ENUMValveName.InA];
                labInletB.Visibility = ItemVisibility.s_listValve[ENUMValveName.InB];
                labInletC.Visibility = ItemVisibility.s_listValve[ENUMValveName.InC];
                labInletD.Visibility = ItemVisibility.s_listValve[ENUMValveName.InD];
                labBPV.Visibility = ItemVisibility.s_listValve[ENUMValveName.BPV];
            }

            groupMonitorSettings.Visibility = ItemVisibility.s_listUV[ENUMUVName.UV01];

            groupAirSensorAlarm.Visibility = ItemVisibility.s_listAS[ENUMASName.AS01];

            flowValveLengthUC.SetVisibility(ItemVisibility.s_listValve[ENUMValveName.InA]
                                                        , ItemVisibility.s_listValve[ENUMValveName.InB]
                                                        , ItemVisibility.s_listValve[ENUMValveName.InC]
                                                        , ItemVisibility.s_listValve[ENUMValveName.InD]
                                                        , ItemVisibility.s_listValve[ENUMValveName.BPV]
                                                        , ItemVisibility.s_listValve[ENUMValveName.Out]
                                                        , ItemVisibility.s_listPump[ENUMPumpName.FITB]
                                                        , ItemVisibility.s_listPump[ENUMPumpName.FITC]
                                                        , ItemVisibility.s_listPump[ENUMPumpName.FITD]);

            mixerUC.SetVisibility(ItemVisibility.s_listMixer[ENUMMixerName.Mixer01]);

            bpvUC.SetVisibility(ItemVisibility.s_listValve[ENUMValveName.BPV]);

            uvResetUC.SetVisibility(ItemVisibility.s_listUV[ENUMUVName.UV01]);

            sampleApplicationTechUC.SetVisibility(ItemVisibility.s_listValve[ENUMValveName.InS]);

            valveSelectionUC.SetVisibility(ItemVisibility.s_listValve[ENUMValveName.InA]
                                                , ItemVisibility.s_listValve[ENUMValveName.InB]
                                                , ItemVisibility.s_listValve[ENUMValveName.InC]
                                                , ItemVisibility.s_listValve[ENUMValveName.InD]
                                                , ItemVisibility.s_listValve[ENUMValveName.BPV]
                                                , ItemVisibility.s_listPump[ENUMPumpName.FITB]
                                                , ItemVisibility.s_listPump[ENUMPumpName.FITC]
                                                , ItemVisibility.s_listPump[ENUMPumpName.FITD]);


            flowRatePerUC.SetVisibility(ItemVisibility.s_listPump[ENUMPumpName.FITB],
                                            ItemVisibility.s_listPump[ENUMPumpName.FITC],
                                            ItemVisibility.s_listPump[ENUMPumpName.FITD]);

            cipUC.SetVisibility(ItemVisibility.s_listPump[ENUMPumpName.FITA]
                                            , ItemVisibility.s_listPump[ENUMPumpName.FITB]
                                            , ItemVisibility.s_listPump[ENUMPumpName.FITC]
                                            , ItemVisibility.s_listPump[ENUMPumpName.FITD]
                                            , ItemVisibility.s_listPump[ENUMPumpName.FITS]
                                            , ItemVisibility.s_listValve[ENUMValveName.InA]
                                            , ItemVisibility.s_listValve[ENUMValveName.InB]
                                            , ItemVisibility.s_listValve[ENUMValveName.InC]
                                            , ItemVisibility.s_listValve[ENUMValveName.InD]
                                            , ItemVisibility.s_listValve[ENUMValveName.InS]
                                            , ItemVisibility.s_listValve[ENUMValveName.CPV_1]
                                            , ItemVisibility.s_listValve[ENUMValveName.Out]);

            collUC.SetVisibility(ItemVisibility.s_listValve[ENUMValveName.Out], ItemVisibility.s_listCollector[ENUMCollectorName.Collector01]);

            //趋势图
            methodCanvas.SetVisible(ItemVisibility.s_listPump[ENUMPumpName.FITA], ItemVisibility.s_listPump[ENUMPumpName.FITB], ItemVisibility.s_listPump[ENUMPumpName.FITC], ItemVisibility.s_listPump[ENUMPumpName.FITD]);
        }

        /// <summary>
        /// 绑定-方法设置
        /// </summary>
        private void DataContextMethodSettings(MethodSettingsVM phase)
        {
            labUserName.DataContext = phase;
            labCreateTime.DataContext = phase;
            labModifyTime.DataContext = phase;

            intLoop.DataContext = phase;

            cboxColumnName.DataContext = phase;
            doubleCV.DataContext = phase;
            cboxCPV.DataContext = phase;

            cboxMBU.DataContext = phase;
            cboxFRU.DataContext = phase;

            doubleFlowRate.DataContext = phase;
            labFlowRateUnitStr.DataContext = phase;

            cboxInletA.DataContext = phase;
            cboxInletB.DataContext = phase;
            cboxInletC.DataContext = phase;
            cboxInletD.DataContext = phase;
            cboxBPV.DataContext = phase;

            ucUV.DataContext = new UVValueVM() { MItem = phase.MUVValue };

            for (int i = 0; i < stackAirSensorAlarm.Children.Count; i++)
            {
                ((ASMethodParaUC)stackAirSensorAlarm.Children[i]).DataContext = phase.MASParaList[i];
            }
        }

        /// <summary>
        /// 创建新阶段
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private Label CreateNewPhase(EnumPhaseType type, string nameType, string name = null)
        {
            Label btn = new Label();
            btn.Template = FindResource("btnPhase") as ControlTemplate;
            if (null != name)
            {
                btn.Content = name;
            }
            else
            {
                switch (type)
                {
                    case EnumPhaseType.Personal:
                        btn.Content = nameType;
                        break;
                    default:
                        btn.Content = Share.ReadXaml.GetEnum(type, "ME_EnumPhaseType_");
                        break;
                }
                
            }
            btn.MouseDown += new MouseButtonEventHandler(btnPhase_MouseDown);
            btn.MouseMove += new MouseEventHandler(btnPhase_MouseMove);
            btn.MouseUp += new MouseButtonEventHandler(btnPhase_MouseUp);
            btn.MouseDoubleClick += new MouseButtonEventHandler(btnPhase_MouseDoubleClick);
            switch (type)
            {
                case EnumPhaseType.Personal:
                    btn.ToolTip = nameType;
                    break;
                default:
                    btn.ToolTip = Share.ReadXaml.GetEnum(type, "ME_EnumPhaseType_");
                    break;
            }
            

            return btn;
        }

        private void SetDataContext(int index)
        {
            switch (MMethod.MPhaseList[index].MItemBase.MType)
            {
                case EnumPhaseType.Miscellaneous:
                    tabControl.SelectedIndex = 2;
                    {
                        foreach (UIElement element in gridMiscellaneous.Children)
                        {
                            ((FrameworkElement)element).DataContext = (MiscellaneousVM)MMethod.MPhaseList[index];
                        }
                    }
                    break;
                default:
                    tabControl.SelectedIndex = 1;
                    {
                        flowValveLengthUC.DataContext = null;
                        flowRateUC.DataContext = null;
                        bpvUC.DataContext = null;
                        flowRatePerUC.DataContext = null;
                        sampleApplicationTechUC.DataContext = null;
                        tvcvUC.DataContext = null;
                        valveSelectionUC.DataContext = null;
                        pHCdUVUnitUC.DataContext = null;
                        collUC.DataContext = null;
                        uvResetUC.DataContext = null;
                        cipUC.DataContext = null;

                        foreach (var it in MMethod.MPhaseList[index].MListGroup)
                        {
                            it.MVisible = Visibility.Visible;
                            switch (it.MType)
                            {
                                case EnumGroupType.FlowRate:
                                    flowRateUC.DataContext = (FlowRateVM)it;
                                    break;
                                case EnumGroupType.ValveSelection:
                                    if (Visibility.Visible == it.MVisible)
                                    {
                                        ValveSelectionVM itt = (ValveSelectionVM)it;
                                        if (!itt.MVisibPer && !itt.MVisibWash
                                            && Visibility.Visible != ItemVisibility.s_listValve[ENUMValveName.InA]
                                                && Visibility.Visible != ItemVisibility.s_listValve[ENUMValveName.InB]
                                                && Visibility.Visible != ItemVisibility.s_listValve[ENUMValveName.InC]
                                                && Visibility.Visible != ItemVisibility.s_listValve[ENUMValveName.InD]
                                                && Visibility.Visible != ItemVisibility.s_listValve[ENUMValveName.BPV])
                                        {
                                            it.MVisible = Visibility.Collapsed;
                                        }
                                    }
                                    valveSelectionUC.DataContext = (ValveSelectionVM)it;
                                    break;
                                case EnumGroupType.Mixer:
                                    mixerUC.DataContext = (MixerVM)it;
                                    break;
                                case EnumGroupType.BPV:
                                    bpvUC.DataContext = (BPVValveVM)it;
                                    break;
                                case EnumGroupType.UVReset:
                                    uvResetUC.DataContext = (UVResetVM)it;
                                    break;
                                case EnumGroupType.SampleApplicationTech:
                                    sampleApplicationTechUC.DataContext = (SampleApplicationTechVM)it;
                                    break;
                                case EnumGroupType.TVCV:
                                    tvcvUC.DataContext = (BaseTVCVVM)it;
                                    break;
                                case EnumGroupType.FlowValveLength:
                                    flowValveLengthUC.DataContext = (FlowValveLengthVM)it;
                                    break;
                                case EnumGroupType.FlowRatePer:
                                    flowRatePerUC.DataContext = (FlowRatePerVM)it;
                                    break; 
                                case EnumGroupType.PHCDUVUntil:
                                    pHCdUVUnitUC.DataContext = (PHCDUVUntilVM)it;
                                    break;
                                case EnumGroupType.CollValveCollector:
                                    collUC.DataContext = (CollValveCollectorVM)it;
                                    break;
                                case EnumGroupType.CIP:
                                    cipUC.DataContext = (CIPVM)it;
                                    break;
                            }
                        }

                        MethodBaseValue methodBaseValue = new MethodBaseValue();
                        GroupFactory groupFactory = new GroupFactory();
                        foreach (EnumGroupType it in Enum.GetValues(typeof(EnumGroupType)))
                        {
                            switch (it)
                            {
                                case EnumGroupType.FlowRate:
                                    if (null == flowRateUC.DataContext)
                                        flowRateUC.DataContext = groupFactory.GetGroupVM(it, methodBaseValue);
                                    break;
                                case EnumGroupType.ValveSelection:
                                    if (null == valveSelectionUC.DataContext)
                                        valveSelectionUC.DataContext = groupFactory.GetGroupVM(it, methodBaseValue);
                                    break;
                                case EnumGroupType.Mixer:
                                    if (null == mixerUC.DataContext)
                                        mixerUC.DataContext = groupFactory.GetGroupVM(it, methodBaseValue);
                                    break;
                                case EnumGroupType.BPV:
                                    if (null == bpvUC.DataContext)
                                        bpvUC.DataContext = groupFactory.GetGroupVM(it, methodBaseValue);
                                    break;
                                case EnumGroupType.UVReset:
                                    if (null == uvResetUC.DataContext)
                                        uvResetUC.DataContext = groupFactory.GetGroupVM(it, methodBaseValue);
                                    break;
                                case EnumGroupType.SampleApplicationTech:
                                    if (null == sampleApplicationTechUC.DataContext)
                                        sampleApplicationTechUC.DataContext = groupFactory.GetGroupVM(it, methodBaseValue);
                                    break;
                                case EnumGroupType.TVCV:
                                    if (null == tvcvUC.DataContext)
                                        tvcvUC.DataContext = groupFactory.GetGroupVM(it, methodBaseValue);
                                    break;
                                case EnumGroupType.FlowValveLength:
                                    if (null == flowValveLengthUC.DataContext)
                                        flowValveLengthUC.DataContext = groupFactory.GetGroupVM(it, methodBaseValue);
                                    break;
                                case EnumGroupType.FlowRatePer:
                                    if (null == flowRatePerUC.DataContext)
                                        flowRatePerUC.DataContext = groupFactory.GetGroupVM(it, methodBaseValue);
                                    break;
                                case EnumGroupType.PHCDUVUntil:
                                    if (null == pHCdUVUnitUC.DataContext)
                                        pHCdUVUnitUC.DataContext = groupFactory.GetGroupVM(it, methodBaseValue);
                                    break;
                                case EnumGroupType.CollValveCollector:
                                    if (null == collUC.DataContext)
                                        collUC.DataContext = groupFactory.GetGroupVM(it, methodBaseValue);
                                    break;
                                case EnumGroupType.CIP:
                                    if (null == cipUC.DataContext)
                                        cipUC.DataContext = groupFactory.GetGroupVM(it, methodBaseValue);
                                    break;
                            }
                        }
                    }
                    break;
            }        
        }

        /// <summary>
        /// 设置当前阶段边框
        /// </summary>
        /// <param name="btn"></param>
        private void UpdateButtonBorder(Label btn)
        {
            if (null != m_lastBtn)
            {
                m_lastBtn.Foreground = Brushes.Black;
            }

            m_lastBtn = btn;
            m_lastBtn.Foreground = Brushes.Blue;

            RefreshGradient();
        }

        /// <summary>
        /// 更新梯度图
        /// </summary>
        private bool RefreshGradient()
        {
            methodCanvas.MCurrClickIndex = stackPanelPhaseList.Children.IndexOf(m_lastBtn);

            methodCanvas.ClearRawData();
            BasePhase.ClearStatic();
            StringBuilderSplit sb = new StringBuilderSplit("\n");
            for (int i = 0; i < MMethod.MPhaseList.Count; i++)
            {
                string error = MMethod.MPhaseList[i].MItemBase.StatisticsAllStep(MMethod.MMethodSetting.MColumnVol);
                if (null != error)
                {
                    sb.Append(error);
                }
                methodCanvas.AddRawData(MMethod.MPhaseList[i].MItemBase.MStepT
                    , MMethod.MPhaseList[i].MItemBase.MStepV
                    , MMethod.MPhaseList[i].MItemBase.MStepCV
                    , MMethod.MPhaseList[i].MItemBase.MPerA
                    , MMethod.MPhaseList[i].MItemBase.MPerB
                    , MMethod.MPhaseList[i].MItemBase.MPerC
                    , MMethod.MPhaseList[i].MItemBase.MPerD
                    , MMethod.MPhaseList[i].MItemBase.MNamePhase
                    , MMethod.MPhaseList[i].MItemBase.MNameStep);
            }
            methodCanvas.CalRawData();
            if (!string.IsNullOrEmpty(sb.ToString()))
            {
                MessageBoxWin.Show(sb.ToString());
                return false;
            }

            return true;
        }


        /// <summary>
        /// 初始化界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title = Title + " - " + MMethod.MItem.MName;

            for (int i = 0; i < MMethod.MPhaseList.Count; i++)
            {
                stackPanelPhaseList.Children.Add(CreateNewPhase(MMethod.MPhaseList[i].MItemBase.MType, MMethod.MPhaseList[i].MItemBase.MNameType, MMethod.MPhaseList[i].MItemBase.MNamePhase));
            }

            //默认点击方法设置
            btnMethodSettings.Focus();
            btnMethodSettings_MouseDown(btnMethodSettings, null);
        }

        /// <summary>
        /// 新建自定义阶段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            PersonalPhaseWin dlg = new PersonalPhaseWin();
            if (true == dlg.ShowDialog())
            {
                InitPersonalPhases();
            }
        }

        /// <summary>
        /// 删除自定义阶段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            StringBoolWin win = new StringBoolWin(btnDel.ToolTip.ToString());
            MethodManager methodManager = new MethodManager();
            List<string> listName = new List<string>();
            if (null == methodManager.GetPhaseNameList(out listName))
            {
                foreach (var it in listName)
                {
                    win.AddItem(new StringBool(it, false));
                }
            }
            if (true == win.ShowDialog())
            {
                StringBuilderSplit sb = new StringBuilderSplit();
                for (int i = 0; i < listName.Count; i++)
                {
                    if (win.GetItem(i).MCheck)
                    {
                        string error = methodManager.DelPhase(listName[i]);
                        if (null == error)
                        {
                            sb.Append(listName[i]);
                        }
                        else
                        {
                            sb.Append(listName[i] + error);
                        }
                    }
                }

                if (0 < sb.MLength)
                {
                    InitPersonalPhases();
                    AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(btnDel.ToolTip.ToString(), sb.ToString());
                    MessageBoxWin.Show(sb.ToString());
                }
            }
        }

        /// <summary>
        /// 复制阶段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            int currIndex = stackPanelPhaseList.Children.IndexOf(m_lastBtn);
            if (-1 != currIndex)
            {
                btnPaste.IsEnabled = true;

                MMethod.CopyPhase(currIndex);
            }
        }

        /// <summary>
        /// 粘贴阶段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPaste_Click(object sender, RoutedEventArgs e)
        {
            Label newBtn = CreateNewPhase(MMethod.MCopyPhase.MType, MMethod.MCopyPhase.MNameType, MMethod.MCopyPhase.MNamePhase);

            stackPanelPhaseList.Children.Add(newBtn);

            MMethod.PastePhase();

            btnPhase_MouseDown(newBtn, null);
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (0 == MMethod.MPhaseList.Count)
            {
                Share.MessageBoxWin.Show(ReadXaml.GetResources("ME_Msg_NullPhase"));
                return;
            }

            if (!RefreshGradient())
            {
                return;
            }

            MathodFlowDocument mathodFlowDocument = new MathodFlowDocument();
            FlowDocument doc = mathodFlowDocument.GetFlowDocument(MMethod.MItem);
            StringBuilderSplit sb = new StringBuilderSplit();
            foreach (Block it in doc.Blocks)
            {
                if (it is Paragraph)
                {
                    StringBuilderSplit sb2 = new StringBuilderSplit(" ");
                    foreach (Run it2 in ((Paragraph)it).Inlines)
                    {
                        if (8 < it2.Text.Length && it2.Text.Substring(0, 8).Equals("        "))
                        {
                            sb2.Append(it2.Text.Remove(0, 8));
                        }
                        else if (4 < it2.Text.Length && it2.Text.Substring(0, 4).Equals("    "))
                        {
                            sb2.Append(it2.Text.Remove(0, 4));
                        }
                        else if (2 < it2.Text.Length && it2.Text.Substring(0, 2).Equals("  "))
                        {
                            sb2.Append(it2.Text.Remove(0, 2));
                        }
                        else
                        {
                            sb2.Append(it2.Text);
                        } 
                    }

                    Run first = (Run)((Paragraph)it).Inlines.First();
                    if (4 < first.Text.Length && first.Text.Substring(0, 4).Equals("    "))
                    {
                        sb.AppendLast(sb2.ToString());
                    }
                    else
                    {
                        sb.Append(sb2.ToString());
                    }
                }
                else if (it is List)
                {
                    foreach(var it2 in ((List)it).ListItems)
                    {
                        foreach (Block it3 in it2.Blocks)
                        {
                            if (it3 is Paragraph)
                            {
                                StringBuilderSplit sb2 = new StringBuilderSplit(" ");
                                foreach (Run it4 in ((Paragraph)it3).Inlines)
                                {
                                    sb2.Append(it4.Text);
                                }
                                sb.Append(sb2.ToString());
                            }
                        }
                    }
                }
            }

            MethodManager manager = new MethodManager();
            if (-1 == MMethod.MItem.MID)
            {
                MMethod.MMethodSetting.MModifyTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string error = manager.AddMethod(MMethod.MItem);
                if (null == error)
                {
                    m_clickSave = true;
                    AuditTrails.AuditTrailsStatic.Instance().InsertRowMethod(Share.ReadXaml.GetResources("ME_Desc_New"), MMethod.MItem.MName);
                    Share.MessageBoxWin.Show(ReadXaml.GetResources("ME_Msg_SaveYes"));
                }
                else
                {
                    Share.MessageBoxWin.Show(error);
                }
            }
            else
            {
                MMethod.MMethodSetting.MModifyTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string error = manager.UpdateMethod(MMethod.MItem);
                if (null == error)
                {
                    AuditTrails.AuditTrailsStatic.Instance().InsertRowMethod(Share.ReadXaml.GetResources("ME_Desc_Update"), MMethod.MItem.MName);
                    Share.MessageBoxWin.Show(ReadXaml.GetResources("ME_Msg_UpdateYes"));
                }
                else
                {
                    Share.MessageBoxWin.Show(error);
                }
            }
        }

        /// <summary>
        /// 发送方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(MSelectEvent, new MethodType(MMethod.MItem.MID, MMethod.MItem.MName, MMethod.MItem.MType));
            RaiseEvent(args);

            AuditTrails.AuditTrailsStatic.Instance().InsertRowMethod(Share.ReadXaml.GetResources("ME_Desc_Send"), MMethod.MItem.MName);
        }

        /// <summary>
        /// 阶段模板-按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            m_isPressCopy = true;
            m_labCopy = null;
            m_clickPtCopy = e.GetPosition(gridPhase);
        }

        /// <summary>
        /// 阶段模板-移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_isPressCopy && null == m_labCopy && Math.Abs((e.GetPosition(gridPhase) - m_clickPtCopy).Length) > 10)
            {
                m_labCopy = sender;
                DataObject obj = new DataObject(typeof(Label), (Label)sender);
                DragDrop.DoDragDrop((Label)sender, ((Label)sender).Content, DragDropEffects.Copy);
            }
        }

        /// <summary>
        /// 阶段模板-弹起
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_MouseUp(object sender, MouseButtonEventArgs e)
        {
            m_isPressCopy = false;
            m_labCopy = null;
        }

        /// <summary>
        /// 阶段模板布局内的拖拽-放下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridPhase_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {

            }
            else if (e.Data.GetDataPresent(typeof(Label)))
            {
                int currIndex = stackPanelPhaseList.Children.IndexOf(m_lastBtn);
                if (-1 != currIndex)
                {
                    if (MessageBoxResult.No == Share.MessageBoxWin.Show(Share.ReadXaml.GetResources("labDel"), Share.ReadXaml.GetResources("btnDel"), MessageBoxButton.YesNo, MessageBoxImage.Question))
                    {
                        for (int i = 0; i < stackPanelPhaseList.Children.Count; i++)
                        {
                            ((Label)stackPanelPhaseList.Children[i]).Margin = new Thickness(0);
                        }
                        return;
                    }

                    stackPanelPhaseList.Children.RemoveAt(currIndex);
                    for (int i = 0; i < stackPanelPhaseList.Children.Count; i++)
                    {
                        ((Label)stackPanelPhaseList.Children[i]).Margin = new Thickness(0);
                    }

                    MMethod.RemoveAtPhase(currIndex);

                    m_lastBtn = null;

                    //默认点击方法设置
                    btnMethodSettings.Focus();
                    btnMethodSettings_MouseDown(btnMethodSettings, null);
                }
            }
        }

        /// <summary>
        /// 方法阶段按钮-按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPhase_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (null != e)
            {
                m_isPressMove = true;
                m_labMove = null;
                m_clickPtMove = e.GetPosition(stackPanelPhaseList);
            }

            SetDataContext(stackPanelPhaseList.Children.IndexOf((UIElement)sender));

            UpdateButtonBorder((Label)sender);
        }

        /// <summary>
        /// 方法阶段按钮-移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPhase_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_isPressMove && null == m_labMove && Math.Abs((e.GetPosition(stackPanelPhaseList) - m_clickPtMove).Length) > 10)
            {
                m_labMove = sender;
                ((Label)sender).Opacity = 0.5;
                DragDrop.DoDragDrop((Label)sender, new DataObject(typeof(Label), (Label)sender), DragDropEffects.Move);
            }
        }

        /// <summary>
        /// 方法阶段按钮-弹起
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPhase_MouseUp(object sender, MouseButtonEventArgs e)
        {
            m_isPressMove = false;
            m_labMove = null;

            if (m_doubleClick)
            {
                m_doubleClick = false;
                int currPhase = stackPanelPhaseList.Children.IndexOf((UIElement)sender);
                RenamePhaseNameWin dlg = new RenamePhaseNameWin();
                dlg.MName = MMethod.MPhaseList[currPhase].MItemBase.MNamePhase;
                dlg.Owner = this;
                if (true == dlg.ShowDialog())
                {
                    ((Label)sender).Content = dlg.MName;
                    MMethod.MPhaseList[currPhase].MItemBase.MNamePhase = dlg.MName;
                }
            }
        }

        /// <summary>
        /// 方法阶段按钮-双击重命名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPhase_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            m_doubleClick = true;
        }

        /// <summary>
        /// 方法阶段布局内的拖拽-进入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stackPanelPhaseList_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {

            }
            else
            {
                Label obj = e.Data.GetData(typeof(Label)) as Label;
                obj.Opacity = 0.5;
            }
        }

        /// <summary>
        /// 方法阶段布局内的拖拽-移动中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stackPanelPhaseList_DragOver(object sender, DragEventArgs e)
        {
            if (m_isPressMove || m_isPressCopy)
            {
                if (0 == stackPanelPhaseList.Children.Count)
                {
                    return;
                }

                double y2 = e.GetPosition(stackPanelPhaseList).Y;
                if (y2 <= VisualTreeHelper.GetOffset(stackPanelPhaseList.Children[0]).Y + ((Label)stackPanelPhaseList.Children[0]).ActualHeight / 2)
                {
                    ((Label)stackPanelPhaseList.Children[0]).Margin = new Thickness(0, 10, 0, 0);
                    for (int i = 1; i < stackPanelPhaseList.Children.Count; i++)
                    {
                        ((Label)stackPanelPhaseList.Children[i]).Margin = new Thickness(0);
                    }
                }
                else
                {
                    for (int i = 0; i < stackPanelPhaseList.Children.Count - 1; i++)
                    {
                        double y1 = VisualTreeHelper.GetOffset(stackPanelPhaseList.Children[i]).Y/* + ((Label)stackPanelPhaseList.Children[i]).ActualHeight*/;
                        double y3 = VisualTreeHelper.GetOffset(stackPanelPhaseList.Children[i + 1]).Y;
                        if (y1 <= y2 && y2 <= y3)
                        {
                            ((Label)stackPanelPhaseList.Children[i]).Margin = new Thickness(0, 0, 0, 10);
                            ((Label)stackPanelPhaseList.Children[i + 1]).Margin = new Thickness(0, 10, 0, 0);
                        }
                        else
                        {
                            ((Label)stackPanelPhaseList.Children[i]).Margin = new Thickness(0);
                            ((Label)stackPanelPhaseList.Children[i + 1]).Margin = new Thickness(0);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 方法阶段布局内的拖拽-离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stackPanelPhaseList_DragLeave(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {

            }
            else
            {
                Label obj = e.Data.GetData(typeof(Label)) as Label;
                obj.Opacity = 1;
            }
        }

        /// <summary>
        /// 方法阶段布局内的拖拽-放下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stackPanelPhaseList_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                Label obj = null;
                EnumPhaseType curr = EnumPhaseType.Personal;
                foreach (EnumPhaseType it in Enum.GetValues(typeof(EnumPhaseType)))
                {
                    if (Share.ReadXaml.GetEnum(it, "ME_EnumPhaseType_").Equals(e.Data.GetData(DataFormats.Text)))
                    {
                        curr = it;
                        obj = CreateNewPhase(curr, (string)e.Data.GetData(DataFormats.Text));
                        break;
                    }
                }
                if (EnumPhaseType.Personal == curr)
                {
                    obj = CreateNewPhase(curr, (string)e.Data.GetData(DataFormats.Text));
                }

                for (int i = 0; i < stackPanelPhaseList.Children.Count; i++)
                {
                    if (e.GetPosition(stackPanelPhaseList).Y <= VisualTreeHelper.GetOffset(stackPanelPhaseList.Children[i]).Y)
                    {
                        ((Label)stackPanelPhaseList.Children[i]).Margin = new Thickness(0);
                        if (0 < i)
                        {
                            ((Label)stackPanelPhaseList.Children[i - 1]).Margin = new Thickness(0);
                        }
                        stackPanelPhaseList.Children.Insert(i, obj);
                        MMethod.InsertPhase(i, curr, (string)e.Data.GetData(DataFormats.Text));
                        btnPhase_MouseDown(obj, null);
                        m_isPressCopy = false;
                        m_labCopy = null;
                        return;
                    }
                }

                stackPanelPhaseList.Children.Add(obj);
                MMethod.AddPhase(curr, (string)e.Data.GetData(DataFormats.Text));
                btnPhase_MouseDown(obj, null);
                m_isPressCopy = false;
                m_labCopy = null;
            }
            else if (e.Data.GetDataPresent(typeof(Label)))
            {
                Label obj = e.Data.GetData(typeof(Label)) as Label;
                obj.Opacity = 1;
                obj.Margin = new Thickness(0);

                int index = stackPanelPhaseList.Children.IndexOf(obj);
                stackPanelPhaseList.Children.Remove(obj);
                BasePhaseVM curr = MMethod.MPhaseList[index];
                MMethod.MPhaseList.RemoveAt(index);

                if (0 == e.GetPosition(stackPanelPhaseList).X && 0 == e.GetPosition(stackPanelPhaseList).Y)
                {
                    stackPanelPhaseList.Children.Insert(index, obj);
                    MMethod.MPhaseList.Insert(index, curr);
                    ((Label)stackPanelPhaseList.Children[index]).Margin = new Thickness(0);
                    if (0 < index)
                    {
                        ((Label)stackPanelPhaseList.Children[index - 1]).Margin = new Thickness(0);
                    }
                    if(stackPanelPhaseList.Children.Count - 1 > index)
                    {
                        ((Label)stackPanelPhaseList.Children[index + 1]).Margin = new Thickness(0);
                    }
                    m_isPressMove = false;
                    m_labMove = null;
                }
                else
                {
                    for (int i = 0; i < stackPanelPhaseList.Children.Count; i++)
                    {
                        if (e.GetPosition(stackPanelPhaseList).Y <= VisualTreeHelper.GetOffset(stackPanelPhaseList.Children[i]).Y)
                        {
                            ((Label)stackPanelPhaseList.Children[i]).Margin = new Thickness(0);
                            if (0 < i)
                            {
                                ((Label)stackPanelPhaseList.Children[i - 1]).Margin = new Thickness(0);
                            }
                            stackPanelPhaseList.Children.Insert(i, obj);
                            MMethod.MPhaseList.Insert(i, curr);
                            m_isPressMove = false;
                            m_labMove = null;
                            return;
                        }
                    }

                    stackPanelPhaseList.Children.Add(obj);
                    MMethod.MPhaseList.Add(curr);
                    m_isPressMove = false;
                    m_labMove = null;
                }
            }
        }


        /// <summary>
        /// 点击方法设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMethodSettings_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UpdateButtonBorder((Label)sender);
            tabControl.SelectedIndex = 0;
        }

        /// <summary>
        /// 点击方法设置-显示柱子详细信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnColumn_Click(object sender, RoutedEventArgs e)
        {
            ColumnManager manager = new ColumnManager();
            ColumnItem item = new ColumnItem();
            if (null == manager.GetColumn(MMethod.MMethodSetting.MColumnId, item))
            {
                ColumnItemWin win = new ColumnItemWin();
                win.Owner = this;
                win.DataContext = item;
                win.ShowDialog();
            }   
        }

        /// <summary>
        /// 点击方法设置-警报警告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAlarmWarning_Click(object sender, RoutedEventArgs e)
        {
            AlarmWarningWin dlg = new AlarmWarningWin();
            dlg.Owner = this;
            for (int i = 0; i < StaticAlarmWarning.SAlarmWarningOriginal.MList.Count; i++)
            {
                MMethod.MMethodSetting.MAlarmWarning.MList[i].ResetValue(StaticAlarmWarning.SAlarmWarningOriginal.MList[i]);
            }
            dlg.MAlarmWarning = MMethod.MMethodSetting.MAlarmWarning;
            dlg.ShowDialog();
        }

        /// <summary>
        /// 方法设置-结果名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRNL_Click(object sender, RoutedEventArgs e)
        {
            ResultNameWin win = new ResultNameWin();
            win.Owner = this;
            win.MItem = MMethod.MMethodSetting.MResultName;
            win.ShowDialog();
        }

        /// <summary>
        /// 方法设置-启动协议
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSP_Click(object sender, RoutedEventArgs e)
        {
            DefineQuestionsWin win = new DefineQuestionsWin();
            win.Owner = this;
            win.MItem = MMethod.MMethodSetting.MDefineQuestions;
            win.ShowDialog();
        }

        /// <summary>
        /// 方法设置-方法记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMNSP_Click(object sender, RoutedEventArgs e)
        {
            NotesWin win = new NotesWin(this, MMethod.MMethodSetting.MMethodNotes);
            if (true == win.ShowDialog())
            {
                MMethod.MMethodSetting.MMethodNotes = win.MNote;
            }
        }


        /// <summary>
        /// 点击梯度图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void methodCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (-1 != methodCanvas.MCurrClickIndex && methodCanvas.MCurrClickIndex != stackPanelPhaseList.Children.IndexOf(m_lastBtn))
            {
                btnPhase_MouseDown((Label)stackPanelPhaseList.Children[methodCanvas.MCurrClickIndex], null);
            }
        }
    }
}