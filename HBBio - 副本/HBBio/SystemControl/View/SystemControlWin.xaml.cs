using HBBio.Administration;
using HBBio.AuditTrails;
using HBBio.ColumnList;
using HBBio.Communication;
using HBBio.Chromatogram;
using HBBio.MethodEdit;
using HBBio.ProjectManager;
using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HBBio.Result;
using HBBio.Manual;
using System.Collections.ObjectModel;
using HBBio.TubeStand;
using HBBio.Collection;
using System.Threading;
using System.Runtime.InteropServices;

namespace HBBio.SystemControl
{
    /// <summary>
    /// SystemControlWin.xaml 的交互逻辑
    /// </summary>
    public partial class SystemControlWin : Window
    {
        /// <summary>
        /// 程序唯一运行的标识
        /// </summary>
        public static bool s_new = true;
        private DateTime m_lastTime;            //鼠标上一次移动的时间点
        private POINT m_lastPt;                 //鼠标上一次所在的位置
        private POINT m_currPt;                 //鼠标当前所在的位置
        private ObservableCollection<StringString> m_listAlarm = new ObservableCollection<StringString>();
        private ObservableCollection<StringString> m_listWarning = new ObservableCollection<StringString>();
        private List<Window> m_listChild = new List<Window>();                  //子窗口集合
        private MessageBoxWin m_winAW = new MessageBoxWin("");                  //警报警告弹窗
        private ScreenLockWin m_screenLockWin = new ScreenLockWin();            //解锁窗口

        private bool m_threadFlagValveMulti = false;                            //出口阀阀位循环的标志
        private bool m_threadFlagCollectorMulti = false;                        //收集器阀位循环的标志
        
        public struct POINT
        {
            public int X;
            public int Y;
            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }
        /// <summary>   
        /// 获取鼠标的坐标   
        /// </summary>   
        /// <param name="lpPoint">传址参数，坐标point类型</param>   
        /// <returns>获取成功返回真</returns>   
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetCursorPos(out POINT pt);

        /// <summary>
        /// 构造函数
        /// </summary>
        public SystemControlWin()
        {
            InitializeComponent();

            //登录窗口
            if (!s_new)
            {
                return;
            }

            SystemControlManager.Start();

            //登录窗口
            LoginWin win = new LoginWin();
            win.MAddUser += DlyAddProjectNode;
            win.MSelfCheck += DlySelfCheck;
            if (true == win.ShowDialog())
            {
                SetAvailable();
            }
            else
            {
                Close();
                return;
            }

            //谱图
            this.chromatogramUC.IsReal = true;
            chromatogramUC.MUpdateCurve += DlyUpdateCurve;
            chromatogramUC.MUpdateAxisScale += DlyUpdateAxisScale;
            chromatogramUC.MUpdateMarker += DlyUpdateMarker;
            chromatogramUC.MUpdateAxis += DlyUpdateAxis;
            
            //流路图
            this.processPicture.MClick += ProcessPictureClick;

            //审计跟踪
            auditTrailsUC.ItemsSource = AuditTrailsStatic.Instance().MlogIngoList;
            AuditTrailsManager manager = new AuditTrailsManager();
            auditTrailsUC.SetColumnVisible(manager.GetColumnVisibility());

            //系统运行
            SystemControlManager.MResultEvent += ResultChanged;
            SystemControlManager.m_manualRun.MAuditTrailsHandler += DlyManualRunAuditTrails;
            SystemControlManager.m_methodRun.MAuditTrailsHandler += DlyMethodRunAuditTrails;
            SystemControlManager.m_methodRun.MShowMessageHandler += DlyMethodRunShowMessage;
            SystemControlManager.m_methodRun.MMarkerHandler += DlyMarker;
            SystemControlManager.m_manualRun.MMarkerHandler += DlyMarker;
            SystemControlManager.s_dbAutoBackupManager.MAuditTrailsEvent += DlyDBAutoBackup;
            SystemControlManager.s_comconfStatic.GetItem(ENUMUVName.UV01).MIJVHandler += DlyUVIJV;

            m_screenLockWin.MScreenLock += DlyScreenLock;

            listAlarm.ItemsSource = m_listAlarm;
            listWarning.ItemsSource = m_listWarning;

            //根据配置相关的设置
            UpdateComConf();

            UpdateCountdown();

            //设置配置
            menuChinese.DataContext = SystemControlManager.s_confCheckable;
            menuEnglish.DataContext = SystemControlManager.s_confCheckable;
            menuRemember.DataContext = SystemControlManager.s_confCheckable;

            //设置内容显隐
            foreach (var it in menuView.Items)
            {
                ((MenuItem)it).DataContext = SystemControlManager.s_viewVisibility;
                foreach (var it2 in ((MenuItem)it).Items)
                {
                    ((MenuItem)it2).DataContext = SystemControlManager.s_viewVisibility;
                }
            }
        }

        /// <summary>
        /// 加载界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;

            RecoverFromBreak();

            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += timer1_Tick;
            timer.Start();

            m_winAW.Visibility = Visibility.Hidden;
            //m_winAW.ShowInTaskbar = false;

            m_screenLockWin.Owner = this;
        }

        /// <summary>
        /// 关闭界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (null == dgvRunData.ItemsSource)
            {
                Application.Current.Shutdown();
                return;
            }

            if (!AdministrationStatic.Instance().ShowPermission(EnumPermission.SoftExit))
            {
                e.Cancel = true;
                MessageBoxWin.Show(Share.ReadXaml.GetResources("A_ErrorPermission"));
                return;
            }

            if (MessageBoxResult.Yes == MessageBoxWin.Show(Share.ReadXaml.GetResources("labClose"), "", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                SystemControlManager.SetConfCheckable(SystemControlManager.s_confCheckable);
                SystemControlManager.SetViewVisibility(SystemControlManager.s_viewVisibility);

                if (StopComConf())
                {
                    Application.Current.Shutdown();
                }
                else
                {
                    e.Cancel = true;
                }

                AuditTrailsStatic.Instance().InsertRowSystem(Share.ReadXaml.GetResources("labLogOut"));
            }
            else
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 添加新的用户项目结点(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DlyAddProjectNode(object sender, RoutedEventArgs e)
        {
            string error = null;
            AdministrationManager aManager = new AdministrationManager();
            UserInfo userInfo = null;
            error = aManager.GetUserNew(out userInfo);
            if (null == error)
            {
                ProjectTreeManager tManager = new ProjectTreeManager();
                TreeNode temp = new TreeNode(0, userInfo.MID, userInfo.MUserName, EnumType.Other);
                error = tManager.AddFirstItem(temp);
                if (null == error)
                {
                    userInfo.MProjectID = temp.MId;
                    error = aManager.EditUserProjectID(userInfo);
                    if (null == error)
                    {
                        return;
                    }
                }
            }

            Share.MessageBoxWin.Show(error);
        }

        /// <summary>
        /// 修改当前用户的权限(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DlyUpdatePermission(object sender, RoutedEventArgs e)
        {
            SetAvailable();
            JudgeWinAvailability();
        }

        /// <summary>
        /// 软件自检(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DlySelfCheck(object sender, RoutedEventArgs e)
        {
            DBSelfCheck.GetInstance().CheckAll();
        }

        /// <summary>
        /// 修改曲线信号(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DlyUpdateCurve(object sender, RoutedEventArgs e)
        {
            List<NameUnitColorShow> list = (List<NameUnitColorShow>)e.OriginalSource;

            for (int i = 0; i < list.Count; i++)
            {
                SystemControlManager.s_comconfStatic.m_signalList[i].MDlyName = list[i].MName;
                SystemControlManager.s_comconfStatic.m_signalList[i].MColorNew = ValueTrans.DrawToMedia(list[i].MColor);
                SystemControlManager.s_comconfStatic.m_signalList[i].MShowNew = list[i].MShow;
            }
            SystemControlManager.s_comconfStatic.UpdateSignalList();
        }

        /// <summary>
        /// 修改曲线最值(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DlyUpdateAxisScale(object sender, RoutedEventArgs e)
        {
            List<AxisScale> list = (List<AxisScale>)e.OriginalSource;

            ChromatogramManager chromatogramManager = new ChromatogramManager();
            chromatogramManager.UpdateAxisScaleList(SystemControlManager.s_comconfStatic.m_cs.MId, list);
        }

        /// <summary>
        /// 修改标记(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DlyUpdateMarker(object sender, RoutedEventArgs e)
        {
            ResultManager manager = new ResultManager();
            manager.UpdateMarkerInfo(e.OriginalSource.ToString());
        }

        /// <summary>
        /// 修改标记(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DlyUpdateAxis(object sender, RoutedEventArgs e)
        {
            auditTrailsUC.SetColumnAxisVisible((EnumBase)e.OriginalSource);
        }

        /// <summary>
        /// 点击流路图(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessPictureClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Primitives.Thumb thumb = (System.Windows.Controls.Primitives.Thumb)e.OriginalSource;

            if (Enum.GetNames(typeof(ENUMPHName)).Contains(thumb.ToolTip.ToString())
                || Enum.GetNames(typeof(ENUMCDName)).Contains(thumb.ToolTip.ToString())
                || Enum.GetNames(typeof(ENUMTTName)).Contains(thumb.ToolTip.ToString()))
            {
                return;
            }

            Point pointThumb = PointToScreen(thumb.TransformToAncestor(Window.GetWindow(thumb)).Transform(new Point(0, 0)));
            Point pointProcessPicture = PointToScreen(processPicture.TransformToAncestor(Window.GetWindow(processPicture)).Transform(new Point(0, 0)));

            switch (SystemControlManager.MSystemState)
            {
                case SystemState.Manual:
                    if (!AdministrationStatic.Instance().ShowPermission(EnumPermission.Manual))
                    {
                        return;
                    }

                    if (!AdministrationStatic.Instance().ShowSignerReviewerWin(this, EnumSignerReviewer.Manual))
                    {
                        return;
                    }
                    break;
                case SystemState.Method:
                    //出口阀单独控制
                    if (Enum.GetNames(typeof(ENUMValveName)).Contains(thumb.ToolTip.ToString())
                        && ENUMValveName.Out == (ENUMValveName)Enum.Parse(typeof(ENUMValveName), thumb.ToolTip.ToString()))
                    {
                        if (!AdministrationStatic.Instance().ShowPermission(EnumPermission.Manual))
                        {
                            return;
                        }

                        if (!AdministrationStatic.Instance().ShowSignerReviewerWin(this, EnumSignerReviewer.Manual))
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (!AdministrationStatic.Instance().ShowPermission(EnumPermission.Project_Method_Intervene))
                        {
                            return;
                        }

                        if (!AdministrationStatic.Instance().ShowSignerReviewerWin(this, EnumSignerReviewer.Project_Method_Intervene))
                        {
                            return;
                        }
                    }
                    break;
            }

            if (thumb.ToolTip.ToString().Contains("FITS"))
            {
                switch (SystemControlManager.MSystemState)
                {
                    case SystemState.Method:
                        {
                            MethodEdit.PumpSampleWin win = new MethodEdit.PumpSampleWin(this);
                            win.MMethodTempValue = SystemControlManager.m_methodRun.m_methodTempValue;
                            win.Left = pointThumb.X + thumb.ActualWidth;
                            win.Top = pointProcessPicture.Y;
                            win.ShowDialog();
                        }
                        break;
                    default:
                        {
                            Manual.PumpSampleWin win = new Manual.PumpSampleWin(this, SystemControlManager.m_manualRun.m_manualValue.m_pumpSampleValue, SystemControlManager.m_manualRun.m_manualValue.m_valveValue);
                            if (SystemState.Free == SystemControlManager.MSystemState)
                            {
                                win.MEnabledFlow = false;
                            }
                            win.Left = pointThumb.X + thumb.ActualWidth;
                            win.Top = pointProcessPicture.Y;
                            win.MComconfStatic = SystemControlManager.s_comconfStatic;
                            win.MWashSystem = SystemControlManager.s_wash;
                            win.ShowDialog();
                        }
                        break;
                }
            }
            else if (thumb.ToolTip.ToString().Contains("FIT"))
            {
                switch (SystemControlManager.MSystemState)
                {
                    case SystemState.Method:
                        {
                            MethodEdit.PumpSystemWin win = new MethodEdit.PumpSystemWin(this);
                            win.MMethodTempValue = SystemControlManager.m_methodRun.m_methodTempValue;
                            win.Left = pointThumb.X + thumb.ActualWidth;
                            win.Top = pointProcessPicture.Y;
                            win.ShowDialog();
                        }
                        break;
                    default:
                        {
                            Manual.PumpSystemWin win = new Manual.PumpSystemWin(this, SystemControlManager.m_manualRun.m_manualValue.m_pumpSystemValue, SystemControlManager.m_manualRun.m_manualValue.m_valveValue);
                            if (SystemState.Free == SystemControlManager.MSystemState)
                            {
                                win.MEnabledFlow = false;
                            }
                            win.Left = pointThumb.X + thumb.ActualWidth;
                            win.Top = pointProcessPicture.Y - 50;
                            win.MComconfStatic = SystemControlManager.s_comconfStatic;
                            win.MWashSystem = SystemControlManager.s_wash;
                            win.ShowDialog();
                        }
                        break;

                }
            }
            else if (Enum.GetNames(typeof(ENUMValveName)).Contains(thumb.ToolTip.ToString()))
            {
                ENUMValveName index = (ENUMValveName)Enum.Parse(typeof(ENUMValveName), thumb.ToolTip.ToString());

                switch (index)
                {
                    case ENUMValveName.Out:
                        {
                            OutWin win = new OutWin(this, thumb.ToolTip.ToString(), StaticValue.GetNameList(index), SystemControlManager.s_comconfStatic.GetValveGet(index));
                            if (SystemState.Free == SystemControlManager.MSystemState)
                            {
                                win.MRealDelayVisibility = Visibility.Collapsed;
                                win.MMultipleVisibility = Visibility.Collapsed;
                            }
                            else
                            {
                                if (0 == StaticSystemConfig.SSystemConfig.MDelayVol)
                                {
                                    win.MRealDelayVisibility = Visibility.Collapsed;
                                }
                            }
                            
                            win.MMultipleFlag = m_threadFlagValveMulti;
                            switch (SystemControlManager.MSystemState)
                            {
                                case SystemState.Manual:
                                    win.MCollectionValve = SystemControlManager.m_manualRun.m_manualValue.m_collValveValue;
                                    break;
                                case SystemState.Method:
                                    win.MCollectionValve = SystemControlManager.m_methodRun.MCollectionValve;
                                    break;
                            }
                            win.Left = pointThumb.X + thumb.ActualWidth;
                            win.Top = pointProcessPicture.Y;
                            win.MSingle += DlyOutSingle;
                            win.MMultipleStart += DlyOutSwitchMultipleStart;
                            win.MMultipleStop += DlyOutSwitchMultipleStop;
                            if (true == win.ShowDialog())
                            {
                                if (SystemState.Manual == SystemControlManager.MSystemState)
                                {
                                    SystemControlManager.m_manualRun.m_manualValue.m_valveValue.MListValave[(int)index].MIndex = win.MIndex;
                                    SystemControlManager.m_manualRun.m_manualValue.m_valveValue.m_update = true;
                                }
                                else
                                {
                                    SystemControlManager.m_manualRun.ValveSwitchOut(win.MIndex);
                                }
                            }
                            win.MSingle -= DlyOutSingle;
                            win.MMultipleStart -= DlyOutSwitchMultipleStart;
                            win.MMultipleStop -= DlyOutSwitchMultipleStop;
                        }
                        break;
                    default:
                        {
                            if (SystemState.Free == SystemControlManager.MSystemState)
                            {
                                return;
                            }

                            ValveWin win = new ValveWin(this, thumb.ToolTip.ToString(), StaticValue.GetNameList(index), SystemControlManager.s_comconfStatic.GetValveGet(index));
                            win.Left = pointThumb.X + thumb.ActualWidth;
                            win.Top = pointProcessPicture.Y;
                            if (true == win.ShowDialog())
                            {
                                if (SystemState.Manual == SystemControlManager.MSystemState)
                                {
                                    SystemControlManager.m_manualRun.m_manualValue.m_valveValue.MListValave[(int)index].MIndex = win.MIndex;
                                    SystemControlManager.m_manualRun.m_manualValue.m_valveValue.m_update = true;
                                }
                                else
                                {
                                    SystemControlManager.m_manualRun.ValveSwitchOther(index, win.MIndex);
                                }
                            }
                        }
                        break;
                }
            }
            else if (Enum.GetNames(typeof(ENUMASName)).Contains(thumb.ToolTip.ToString()))
            {
                switch (SystemControlManager.MSystemState)
                {
                    case SystemState.Manual:
                        {
                            ENUMASName index = (ENUMASName)Enum.Parse(typeof(ENUMASName), thumb.ToolTip.ToString());
                            Manual.ASWin win = new Manual.ASWin(this, SystemControlManager.m_manualRun.m_manualValue.m_ASValue.MList[(int)index]);
                            win.Left = pointThumb.X + thumb.ActualWidth;
                            win.Top = pointProcessPicture.Y;
                            win.ShowDialog();
                        }
                        break;
                    case SystemState.Method:
                        {
                            ENUMASName index = (ENUMASName)Enum.Parse(typeof(ENUMASName), thumb.ToolTip.ToString());
                            MethodEdit.ASWin win = new MethodEdit.ASWin(this, SystemControlManager.m_methodRun.MMethod.MMethodSetting.MASParaList[(int)index]);
                            win.Left = pointThumb.X + thumb.ActualWidth;
                            win.Top = pointProcessPicture.Y;
                            win.ShowDialog();
                        }
                        break;
                }
            }
            else if (Enum.GetNames(typeof(ENUMUVName)).Contains(thumb.ToolTip.ToString()))
            {
                UVValue uvValue = new UVValue();
                uvValue.SetCurrInfo(SystemControlManager.s_comconfStatic.GetItem(ENUMUVName.UV01));

                ENUMUVName index = (ENUMUVName)Enum.Parse(typeof(ENUMUVName), thumb.ToolTip.ToString());
                UVWin win = new UVWin(this, uvValue, SystemControlManager.s_comconfStatic.GetItem(ENUMUVName.UV01));
                win.Left = pointThumb.X + thumb.ActualWidth;
                win.Top = pointProcessPicture.Y;
                win.MUpdateUV += DlyUpdateUV;
                win.ShowDialog();
                win.MUpdateUV -= DlyUpdateUV;
            }
            else if (Enum.GetNames(typeof(ENUMRIName)).Contains(thumb.ToolTip.ToString()))
            {
                RIValue riValue = new RIValue();
                riValue.SetCurrInfo(SystemControlManager.s_comconfStatic.GetItem(ENUMRIName.RI01));

                ENUMRIName index = (ENUMRIName)Enum.Parse(typeof(ENUMRIName), thumb.ToolTip.ToString());
                RIWin win = new RIWin(this, riValue);
                win.Left = pointThumb.X + thumb.ActualWidth;
                win.Top = pointProcessPicture.Y;
                win.MUpdateRI += DlyUpdateRI;
                win.ShowDialog();
                win.MUpdateRI -= DlyUpdateRI;
            }
            else if (Enum.GetNames(typeof(ENUMMixerName)).Contains(thumb.ToolTip.ToString()))
            {
                if (SystemState.Free == SystemControlManager.MSystemState)
                {
                    return;
                }

                MixerValue mixerValue = new MixerValue();
                mixerValue.SetCurrInfo(SystemControlManager.s_comconfStatic.GetItem(ENUMMixerName.Mixer01));

                ENUMMixerName index = (ENUMMixerName)Enum.Parse(typeof(ENUMMixerName), thumb.ToolTip.ToString());
                MixerWin win = new MixerWin(this, mixerValue);
                win.Left = pointThumb.X + thumb.ActualWidth;
                win.Top = pointProcessPicture.Y;
                if (true == win.ShowDialog())
                {
                    SystemControlManager.s_comconfStatic.SetMixer(index, mixerValue.MOnoff);
                }
            }
            else if (Enum.GetNames(typeof(ENUMCollectorName)).Contains(thumb.ToolTip.ToString()))
            {
                CollectorWin win = new CollectorWin();
                win.Owner = this;
                win.Left = pointThumb.X + thumb.ActualWidth;
                win.Top = pointProcessPicture.Y - 50;
                if (SystemState.Free == SystemControlManager.MSystemState)
                {
                    win.MRealDelayVisibility = Visibility.Collapsed;
                    win.MMultipleVisibility = Visibility.Collapsed;
                }
                else
                {
                    if (0 == StaticSystemConfig.SSystemConfig.MDelayVol + Communication.StaticSystemConfig.SSystemConfig.MConfCollector.MGLTJ)
                    {
                        win.MRealDelayVisibility = Visibility.Collapsed;
                    }
                }

                win.MMultipleFlag = m_threadFlagCollectorMulti;
                win.MItemShow = SystemControlManager.s_comconfStatic.GetItem(ENUMCollectorName.Collector01);
                if (0 != EnumCollectorInfo.CountL)
                {
                    win.MTubeNames = EnumCollectorInfo.NameList;
                }
                else
                {
                    win.MTubeNames = EnumCollectorInfo.NameList;
                    win.MTubeNames.Insert(0, "WASTE");
                }
                switch (SystemControlManager.MSystemState)
                {
                    case SystemState.Manual:
                        win.MCollectionCollector = SystemControlManager.m_manualRun.m_manualValue.m_collCollectorValue;
                        break;
                    case SystemState.Method:
                        win.MCollectionCollector = SystemControlManager.m_methodRun.MCollectionCollector;
                        break;
                }
                win.MSingle += DlyCollectorSingle;
                win.MMultipleStart += DlyCollectorSwitchMultipleStart;
                win.MMultipleStop += DlyCollectorSwitchMultipleStop;
                win.ShowDialog();
                win.MSingle -= DlyCollectorSingle;
                win.MMultipleStart -= DlyCollectorSwitchMultipleStart;
                win.MMultipleStop -= DlyCollectorSwitchMultipleStop;
            }
        }

        /// <summary>
        /// 发送当前需要执行的方法或者方法序列(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendMethodOrQueue(object sender, RoutedEventArgs e)
        {
            string error = SystemControlManager.m_methodRun.SendMethodOrQueue((MethodType)e.OriginalSource);
            if (null == error)
            {
                Share.MessageBoxWin.Show(Share.ReadXaml.GetResources("ME_Msg_SendYes"), Share.DlyBase.c_sleep10);
            }
            else
            {
                switch (((MethodType)e.OriginalSource).MType)
                {
                    case EnumMethodType.Method:
                        AuditTrails.AuditTrailsStatic.Instance().InsertRowError(Share.ReadXaml.GetResources("ME_Desc_Send"), error);
                        break;
                    case EnumMethodType.MethodQueue:
                        AuditTrails.AuditTrailsStatic.Instance().InsertRowError(Share.ReadXaml.GetResources("ME_Desc_Queue_Send"), error);
                        break;
                }
                Share.MessageBoxWin.Show(error);
            }
        }

        /// <summary>
        /// 发送当前需要设置的背景谱图(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendCurve(object sender, RoutedEventArgs e)
        {
            this.chromatogramUC.ClearBGLineItemData();

            ResultManager manager = new ResultManager();
            System.IO.Stream ms;
            double cv;
            double ch;
            string attachment;
            string markerInfo;
            if (null == manager.GetCurveData(((ResultTitle)e.OriginalSource).MID, SystemControl.SystemControlManager.s_comconfStatic.m_listSmooth, this.chromatogramUC.MBGListList, out ms, out cv, out ch, out attachment, out markerInfo))
            {
                this.chromatogramUC.RestoreBGLineItemData();
                this.chromatogramUC.SetLineAndBGLine(true);
                this.chromatogramUC.UpdateDraw();
            }
        }

        /// <summary>
        /// 更新手动运行(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DlyUpdateManual(object sender, RoutedEventArgs e)
        {
            if (SystemControlManager.s_dbAutoBackupManager.MAutoIng)
            {
                Share.MessageBoxWin.Show(Database.ReadXamlDatabase.GetResources(Database.ReadXamlDatabase.C_AutoBackupIng));
                return;
            }

            if (ManualState.Free == SystemControlManager.m_manualRun.m_state)
            {
                RunLocationResultWin winLR = new RunLocationResultWin(AdministrationStatic.Instance().MUserInfo.MProjectID, true);
                if (true == winLR.ShowDialog())
                {
                    SystemControlManager.SetSystemStateFreeToManual(winLR.MResultName, winLR.MProjectID);
                    runStatusUC.SetEnumMethodType(EnumResultIconType.Manual);
                    AuditTrailsStatic.Instance().InsertRowManual(menuManual.Header.ToString(), winLR.Title + " : " + winLR.MResultPath);
                }
                else
                {
                    return;
                }
            }

            AuditTrailsStatic.Instance().InsertRowManual(menuManual.Header.ToString(), (string)e.OriginalSource);
        }

        /// <summary>
        /// 更新紫外设置(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DlyUpdateUV(object sender, RoutedEventArgs e)
        {
            UVValue uvValue = (UVValue)e.OriginalSource;
            SystemControlManager.s_comconfStatic.SetUVWave(ENUMUVName.UV01, uvValue);
            SystemControlManager.s_comconfStatic.SetUVLamp(ENUMUVName.UV01, uvValue.MOnoff);
            if (uvValue.MClear)
            {
                SystemControlManager.s_comconfStatic.SetUVClear(ENUMUVName.UV01);
            }
        }

        /// <summary>
        /// 更新出口阀切换设置(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DlyOutSingle(object sender, RoutedEventArgs e)
        {
            SwitchValve(true, (int)e.OriginalSource);
        }

        /// <summary>
        /// 更新出口阀循环收集设置(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DlyOutSwitchMultipleStart(object sender, RoutedEventArgs e)
        {
            m_threadFlagValveMulti = true;

            Thread thread = new Thread(ThreadFunValveMulti);
            thread.IsBackground = true;
            thread.Start();
        }

        /// <summary>
        /// 更新出口阀循环收集设置(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DlyOutSwitchMultipleStop(object sender, RoutedEventArgs e)
        {
            m_threadFlagValveMulti = false;
        }

        /// <summary>
        /// 阀位循环的子线程
        /// </summary>
        private void ThreadFunValveMulti()
        {
            double volStart = SystemControlManager.m_curveStatic.MV;
            bool delay = OutWin.s_multipleDelay;

            //立即
            SwitchValve(delay, OutWin.s_multipleIndex + 1);

            //循环
            while (m_threadFlagValveMulti && SystemState.Free != SystemControlManager.MSystemState)
            {
                if (Math.Round(SystemControlManager.m_curveStatic.MV - volStart, 2) >= OutWin.s_multipleVol)
                {
                    volStart = SystemControlManager.m_curveStatic.MV;

                    SwitchValve(delay);
                }
                Thread.Sleep(Share.DlyBase.c_sleep3);
            }

            //排废
            if (SystemState.Free != SystemControlManager.MSystemState)
            {
                SwitchValve(delay, 0);
            }

            m_threadFlagValveMulti = false;
        }

        private void SwitchValve(bool delay, int indexSetNew = -1)
        {
            if (-1 == indexSetNew)
            {
                indexSetNew = SystemControlManager.s_comconfStatic.GetValveSet(ENUMValveName.Out);
                indexSetNew++;
                if (indexSetNew >= EnumOutInfo.Count)
                {
                    indexSetNew = 1;
                }
            }

            if (indexSetNew < EnumOutInfo.Count)
            {
                if (delay)
                {
                    DlyManualRunAuditTrails(ReadXamlCollection.C_CollMarkM, EnumOutInfo.NameList[indexSetNew]);
                    Thread thread = new Thread(ThreadFunDelaySwitchValve);
                    thread.IsBackground = true;
                    thread.Start(indexSetNew);
                }
                else
                {
                    SystemControlManager.s_comconfStatic.SetValve(ENUMValveName.Out, indexSetNew);
                    DlyManualRunAuditTrails(ReadXamlCollection.C_CollMarkM, EnumOutInfo.NameList[indexSetNew]);
                }
            }
        }

        private void ThreadFunDelaySwitchValve(object e)
        {
            double volStart = SystemControlManager.m_curveStatic.MV;
            while (Math.Round(SystemControlManager.m_curveStatic.MV - volStart, 2) < Communication.StaticSystemConfig.SSystemConfig.MDelayVol && SystemState.Free != SystemControlManager.MSystemState)
            {
                Thread.Sleep(DlyBase.c_sleep3);
            }

            if (SystemState.Free != SystemControlManager.MSystemState)
            {
                SystemControlManager.s_comconfStatic.SetValve(ENUMValveName.Out, (int)e);
                DlyManualRunAuditTrails(ReadXamlCollection.C_CollDelay, EnumOutInfo.NameList[(int)e]);
            }
        }

        /// <summary>
        /// 更新收集器收集设置(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DlyCollectorSingle(object sender, RoutedEventArgs e)
        {
            SwitchCollector(CollectorWin.s_singleDelay, (CollTextIndex)e.OriginalSource);
        }

        /// <summary>
        /// 更新收集器循环收集设置(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DlyCollectorSwitchMultipleStart(object sender, RoutedEventArgs e)
        {
            m_threadFlagCollectorMulti = true;

            Thread thread = new Thread(ThreadFunCollectorMulti);
            thread.IsBackground = true;
            thread.Start();
        }

        /// <summary>
        /// 更新收集器循环收集设置(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DlyCollectorSwitchMultipleStop(object sender, RoutedEventArgs e)
        {
            m_threadFlagCollectorMulti = false;
        }

        /// <summary>
        /// 收集器阀位循环的子线程
        /// </summary>
        private void ThreadFunCollectorMulti()
        {
            double volStart = SystemControlManager.m_curveStatic.MV;
            bool delay = CollectorWin.s_multipleDelay;

            //立即
            SwitchCollector(delay, new CollTextIndex(EnumCollectorInfo.NameList[CollectorWin.s_multipleIndex], true));

            //循环
            while (m_threadFlagCollectorMulti && SystemState.Free != SystemControlManager.MSystemState)
            {
                if (Math.Round(SystemControlManager.m_curveStatic.MV - volStart, 2) >= CollectorWin.s_multipleVol)
                {
                    volStart = SystemControlManager.m_curveStatic.MV;

                    SwitchCollector(delay, SystemControlManager.s_comconfStatic.GetItem(ENUMCollectorName.Collector01).GetAdd());
                }
                Thread.Sleep(Share.DlyBase.c_sleep3);
            }

            //排废
            if (SystemState.Free != SystemControlManager.MSystemState)
            {
                CollTextIndex curr = SystemControlManager.s_comconfStatic.GetItem(ENUMCollectorName.Collector01).GetCurr();
                curr.MStatus = false;
                SwitchCollector(delay, curr);
            }

            m_threadFlagCollectorMulti = false;
        }

        private void SwitchCollector(bool delay, CollTextIndex textIndexSetNew)
        {
            if (delay)
            {
                DlyManualRunAuditTrails(ReadXamlCollection.C_CollMarkM, textIndexSetNew.MStr);
                Thread thread = new Thread(ThreadFunDelaySwitchCollector);
                thread.IsBackground = true;
                thread.Start(textIndexSetNew);
            }
            else
            {
                CollectorItem curr = SystemControlManager.s_comconfStatic.GetItem(ENUMCollectorName.Collector01);
                if (null != curr)
                {
                    lock (curr.m_locker)
                    {
                        curr.MIndexSet = textIndexSetNew.MStr;
                        curr.MStatusSet = textIndexSetNew.MStatus;
                    }
                }
                DlyManualRunAuditTrails(ReadXamlCollection.C_CollMarkM, textIndexSetNew.MStr);
            }
        }

        private void ThreadFunDelaySwitchCollector(object e)
        {
            double volStart = SystemControlManager.m_curveStatic.MV;
            while (Math.Round(SystemControlManager.m_curveStatic.MV - volStart, 2) < (Communication.StaticSystemConfig.SSystemConfig.MDelayVol + Communication.StaticSystemConfig.SSystemConfig.MConfCollector.MGLTJ) && SystemState.Free != SystemControlManager.MSystemState)
            {
                Thread.Sleep(DlyBase.c_sleep3);
            }

            CollectorItem curr = SystemControlManager.s_comconfStatic.GetItem(ENUMCollectorName.Collector01);
            if (SystemState.Free != SystemControlManager.MSystemState)
            {
                if (null != curr)
                {
                    lock (curr.m_locker)
                    {
                        curr.MIndexSet = ((CollTextIndex)e).MStr;
                        curr.MStatusSet = ((CollTextIndex)e).MStatus;
                    }
                }
                DlyManualRunAuditTrails(ReadXamlCollection.C_CollDelay, ((CollTextIndex)e).MStr);
            }
        }

        /// <summary>
        /// 更新示差检测器设置(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DlyUpdateRI(object sender, RoutedEventArgs e)
        {
            RIValue riValue = (RIValue)e.OriginalSource;
            SystemControlManager.s_comconfStatic.SetRITemperature(ENUMRIName.RI01, riValue.MTemperature);
            SystemControlManager.s_comconfStatic.SetRIPurge(ENUMRIName.RI01, riValue.MOnoff);
            if (riValue.MClear)
            {
                SystemControlManager.s_comconfStatic.SetUVClear(ENUMUVName.UV01);
            }
        }

        /// <summary>
        /// 添加画图数据(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        private void ResultChanged(object sender)
        {
            //跨线程调用
            chromatogramUC.Dispatcher.Invoke(new Action(delegate ()
            {
                ResultRow temp = (ResultRow)sender;
                if (null == temp)
                {
                    chromatogramUC.Clear();
                }
                else
                {
                    chromatogramUC.AddLineItemData(temp.m_T, temp.m_V, temp.m_CV, temp.m_valList);
                    this.chromatogramUC.UpdateDraw();
                }
            }));
        }

        /// <summary>
        /// 添加标记数据(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        private void DlyMarker(object type, object val)
        {
            //跨线程调用
            chromatogramUC.Dispatcher.Invoke(new Action(delegate ()
            {
                chromatogramUC.AddMarker(new MarkerInfo((string)type, (double)val));
            }));
        }

        /// <summary>
        /// 添加画图数据(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        private void DlyDBAutoBackup(object type, object desc, object oper)
        {
            //跨线程调用
            this.Dispatcher.Invoke(new Action(delegate ()
            {
                if (Convert.ToBoolean(type))
                {
                    AuditTrailsStatic.Instance().InsertRow(EnumATType.System, (string)desc, (string)oper);
                }
                else
                {
                    AuditTrailsStatic.Instance().InsertRow(EnumATType.Error, (string)desc, (string)oper);
                }
            }));
        }

        /// <summary>
        /// 添加审计跟踪(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        private void DlyManualRunAuditTrails(object desc, object oper)
        {
            //跨线程调用
            this.Dispatcher.Invoke(new Action(delegate ()
            {
                switch ((string)desc)
                {
                    case ReadXamlCollection.C_CollMarkM:
                        AuditTrailsStatic.Instance().InsertRowColl(ReadXamlCollection.S_CollMarkM, (string)oper);
                        if (SystemState.Free != SystemControlManager.MSystemState)
                        {
                            chromatogramUC.AddCollM(new MarkerInfo((string)oper));
                        }
                        break;
                    case ReadXamlCollection.C_CollMarkA:
                        AuditTrailsStatic.Instance().InsertRowColl(ReadXamlCollection.S_CollMarkA, (string)oper);
                        if (SystemState.Free != SystemControlManager.MSystemState)
                        {
                            chromatogramUC.AddCollA(new MarkerInfo((string)oper));
                        }
                        break;
                    case ReadXamlCollection.C_CollOver:
                        AuditTrailsStatic.Instance().InsertRowColl(ReadXamlCollection.S_CollColl, (string)oper);
                        break;
                    case ReadXamlCollection.C_CollDelay:
                        AuditTrailsStatic.Instance().InsertRowColl(ReadXamlCollection.S_CollDelay, (string)oper);
                        break;
                    case ReadXamlManual.C_ValveSwitch:
                        AuditTrailsStatic.Instance().InsertRowMarker(ReadXamlManual.S_ValveSwitch, (string)oper);
                        if (SystemState.Free != SystemControlManager.MSystemState)
                        {
                            chromatogramUC.AddValve(new MarkerInfo((string)oper));
                        }
                        break;
                    default:
                        AuditTrailsStatic.Instance().InsertRowManual((string)desc, (string)oper);
                        break;
                }
            }));
        }

        /// <summary>
        /// 添加审计跟踪(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        private void DlyMethodRunAuditTrails(object desc, object oper)
        {
            //跨线程调用
            this.Dispatcher.Invoke(new Action(delegate ()
            {
                switch ((string)desc)
                {
                    case ReadXamlCollection.C_CollMarkA:
                        AuditTrailsStatic.Instance().InsertRowColl(ReadXamlCollection.S_CollMarkA, (string)oper);
                        chromatogramUC.AddCollA(new MarkerInfo((string)oper));
                        break;
                    case ReadXamlManual.C_ValveSwitch:
                        AuditTrailsStatic.Instance().InsertRowMarker(ReadXamlManual.S_ValveSwitch, (string)oper);
                        chromatogramUC.AddValve(new MarkerInfo((string)oper));
                        break;
                    case ReadXamlMethod.C_PhaseName:
                        AuditTrailsStatic.Instance().InsertRowMarker(ReadXamlMethod.S_PhaseName, (string)oper);
                        chromatogramUC.AddPhase(new MarkerInfo((string)oper));
                        break;
                    default:
                        AuditTrailsStatic.Instance().InsertRowMethod((string)desc, (string)oper);
                        break;
                }
            }));
        }

        /// <summary>
        /// 显示弹窗信息(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        private void DlyMethodRunShowMessage(object sender)
        {
            //跨线程调用
            this.Dispatcher.Invoke(new Action(delegate ()
            {
                MessageBoxWin.Show((string)sender);
            }));
        }

        /// <summary>
        /// 添加手动进样阀
        /// </summary>
        /// <param name="sender"></param>
        private void DlyUVIJV(object sender)
        {
            if (StaticSystemConfig.SSystemConfig.MConfOther.MUVIJV)
            {
                if (SystemState.Method == SystemControlManager.MSystemState)
                {
                    SystemControlManager.SetSystemStateRunToNext();
                }
            }

            //跨线程调用
            this.Dispatcher.Invoke(new Action(delegate ()
            {
                AuditTrailsStatic.Instance().InsertRowManual(Share.ReadXaml.GetResources("labIJV"));
            }));    
        }

        /// <summary>
        /// 退出锁屏状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DlyScreenLock(object sender, RoutedEventArgs e)
        {
            AuditTrailsStatic.Instance().UpdateUserName(AdministrationStatic.Instance().MUserInfo.MUserName);
            SetAvailable();
            JudgeWinAvailability();

            AuditTrailsStatic.Instance().InsertRowSystem(Share.ReadXaml.GetResources("A_ExitLock"));
        }

        /// <summary>
        /// 启动各通讯仪器
        /// </summary>
        private void UpdateComConf()
        {
            dgvRunData.ItemsSource = null;
            dgvRunData.ItemsSource = SystemControlManager.s_comconfStatic.m_runDataShowList;

            List<Curve> list = new List<Curve>();
            List<Curve> listBG = new List<Curve>();
            foreach (var it in SystemControlManager.s_comconfStatic.m_signalList)
            {
                list.Add(new Curve(it.MDlyName, it.MUnit, ValueTrans.MediaToDraw(it.MColorNew), it.MShowNew));
                listBG.Add(new Curve(it.MDlyName, it.MUnit, ValueTrans.MediaToDraw(it.MColorNew), it.MShowNew));
            }

            if (null != SystemControlManager.s_comconfStatic.m_cs)
            {
                List<AxisScale> listAxisScale = null;
                ChromatogramManager chromatogramManager = new ChromatogramManager();
                if (null == chromatogramManager.GetAxisScaleList(SystemControlManager.s_comconfStatic.m_cs.MId, out listAxisScale))
                {
                    for (int i = 1; i < listAxisScale.Count; i++)
                    {
                        switch (listAxisScale[i].MAxisScale)
                        {
                            case EnumAxisScale.Fixed:
                                list[i - 1].MAxisScale = EnumAxisScale.Fixed;
                                list[i - 1].MMaxFix = listAxisScale[i].MMax;
                                list[i - 1].MMinFix = listAxisScale[i].MMin;
                                list[i - 1].MMaxAuto = listAxisScale[i].MMax;
                                list[i - 1].MMinAuto = listAxisScale[i].MMin;
                                break;
                        }
                    }
                }
                chromatogramUC.InitDataFrame(list, listBG, 0 < listAxisScale.Count ? listAxisScale[0] : null);
            }

            this.processPicture.UpdateItems(SystemControlManager.s_comconfStatic.m_biList, SystemControlManager.s_comconfStatic.m_ipList, SystemControlManager.s_comconfStatic.m_size, SystemControlManager.s_comconfStatic.m_listCircle, SystemControlManager.s_comconfStatic.m_listColumn);

            SystemControlManager.s_comconfStatic.ThreadAllStart();
            SystemControlManager.s_comconfStatic.ThreadAllStatus(ENUMThreadStatus.WriteOrRead);
        }

        /// <summary>
        /// 停止各通讯仪器
        /// </summary>
        private bool StopComConf()
        {
            if (SystemState.Free != SystemControlManager.MSystemState)
            {
                Share.MessageBoxWin.Show(Share.ReadXaml.S_OnlyFree);
                return false;
            }

            if (SystemControlManager.s_dbAutoBackupManager.MAutoIng)
            {
                Share.MessageBoxWin.Show(Database.ReadXamlDatabase.GetResources(Database.ReadXamlDatabase.C_AutoBackupIng));
                return false;
            }

            SystemControlManager.s_comconfStatic.ThreadAllStatus(ENUMThreadStatus.Abort);
            return true;
        }

        /// <summary>
        /// 更新倒计时显隐
        /// </summary>
        private void UpdateCountdown()
        {
            btnpH.Visibility = Visibility.Collapsed;
            btnCd.Visibility = Visibility.Collapsed;
            btnUV.Visibility = Visibility.Collapsed;
            foreach (var it in StaticSystemConfig.SSystemConfig.MListConfpHCdUV)
            {
                if (it.MName.Contains("pH"))
                {
                    btnpH.Visibility = Visibility.Visible;
                }
                else if (it.MName.Contains("Cd"))
                {
                    btnCd.Visibility = Visibility.Visible;
                }
                else if (it.MName.Contains("UV"))
                {
                    btnUV.Visibility = Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// 更新按钮禁用与否状态
        /// </summary>
        private void UpdateBtnStatus()
        {
            switch (SystemControlManager.MSystemState)
            {
                case SystemState.Free:
                    btnRun.IsEnabled = !SystemControlManager.s_dbAutoBackupManager.MAutoIng && null != SystemControlManager.m_methodRun.MMethodType;
                    btnHold.IsEnabled = false;
                    btnHoldUntil.IsEnabled = false;
                    btnNext.IsEnabled = false;
                    btnPause.IsEnabled = false;
                    btnContinue.IsEnabled = false;
                    btnStop.IsEnabled = false;
                    break;
                case SystemState.Manual:
                    switch (SystemControlManager.m_manualRun.m_state)
                    {
                        case ManualState.Run:
                            btnRun.IsEnabled = false;
                            if (SystemControlManager.m_manualRun.MIsHold)
                            {
                                if (true == btnHold.IsChecked)
                                {
                                    btnHold.IsEnabled = true;
                                    btnHoldUntil.IsEnabled = false;
                                }
                                else
                                {
                                    btnHold.IsEnabled = false;
                                    btnHoldUntil.IsEnabled = true;
                                }
                            }
                            else
                            {
                                btnHold.IsEnabled = true;
                                btnHoldUntil.IsEnabled = true;
                            }
                            btnNext.IsEnabled = false;
                            btnPause.IsEnabled = true;
                            btnContinue.IsEnabled = false;
                            btnStop.IsEnabled = true;
                            break;
                        case ManualState.Pause:
                            btnRun.IsEnabled = false;
                            btnHold.IsEnabled = false;
                            btnHoldUntil.IsEnabled = false;
                            btnNext.IsEnabled = false;
                            btnPause.IsEnabled = false;
                            btnContinue.IsEnabled = true;
                            btnStop.IsEnabled = true;
                            break;
                    }
                    break;
                case SystemState.Method:
                    switch (SystemControlManager.m_methodRun.m_state)
                    {
                        case MethodState.Run:
                            btnRun.IsEnabled = false;
                            if (SystemControlManager.m_methodRun.MIsHold)
                            {
                                if (true == btnHold.IsChecked)
                                {
                                    btnHold.IsEnabled = true;
                                    btnHoldUntil.IsEnabled = false;
                                }
                                else
                                {
                                    btnHold.IsEnabled = false;
                                    btnHoldUntil.IsEnabled = true;
                                }
                            }
                            else
                            {
                                btnHold.IsEnabled = true;
                                btnHoldUntil.IsEnabled = true;
                            }
                            btnNext.IsEnabled = true;
                            btnPause.IsEnabled = true;
                            btnContinue.IsEnabled = false;
                            btnStop.IsEnabled = true;
                            break;
                        case MethodState.Pause:
                            btnRun.IsEnabled = false;
                            btnHold.IsEnabled = false;
                            btnHoldUntil.IsEnabled = false;
                            btnNext.IsEnabled = false;
                            btnPause.IsEnabled = false;
                            btnContinue.IsEnabled = true;
                            btnStop.IsEnabled = true;
                            break;
                    }
                    break;
            }
        }

        /// <summary>
        /// 异常恢复
        /// </summary>
        private void RecoverFromBreak()
        {
            DateTime time = DateTime.Now;

            if (SystemControlManager.m_methodRun.MIsBreak)
            {
                ResultManager manager = new ResultManager();
                string markerInfo = null;
                if (null == manager.GetCurveDataLast(SystemControl.SystemControlManager.s_comconfStatic.m_listSmooth, this.chromatogramUC.MListList, out markerInfo, out time))
                {
                    this.chromatogramUC.RestoreLineItemData();
                    this.chromatogramUC.SetMarkerInfo(markerInfo);
                    this.chromatogramUC.UpdateDraw();
                }

                if (SystemControlManager.m_methodRun.MIsHold)
                {
                    this.btnHold.IsChecked = true;
                }

                if (0 < this.chromatogramUC.MListList[0].Count)
                {
                    SystemControlManager.SetSystemFreeToMethodBreak(this.chromatogramUC.MListList[0].Last(), this.chromatogramUC.MListList[1].Last(), this.chromatogramUC.MListList[2].Last());
                }
                else
                {
                    SystemControlManager.SetSystemFreeToMethodBreak(0, 0, 0);
                }
            }
            else if (SystemControlManager.m_manualRun.MIsBreak)
            {
                ResultManager manager = new ResultManager();
                if (null == manager.GetCurveDataLast(SystemControl.SystemControlManager.s_comconfStatic.m_listSmooth, this.chromatogramUC.MListList, out string markerInfo, out time))
                {
                    this.chromatogramUC.RestoreLineItemData();
                    this.chromatogramUC.SetMarkerInfo(markerInfo);
                    this.chromatogramUC.UpdateDraw();
                }

                if (SystemControlManager.m_manualRun.MIsHold)
                {
                    this.btnHold.IsChecked = true;
                }

                if (0 < this.chromatogramUC.MListList[0].Count)
                {
                    SystemControlManager.SetSystemFreeToManualBreak(this.chromatogramUC.MListList[0].Last(), this.chromatogramUC.MListList[1].Last(), this.chromatogramUC.MListList[2].Last());
                }
                else
                {
                    SystemControlManager.SetSystemFreeToManualBreak(0, 0, 0);
                }
            }

            if (SystemControlManager.m_methodRun.MIsBreak || SystemControlManager.m_manualRun.MIsBreak)
            {
                AuditTrailsStatic.Instance().InsertRowSystem(Share.ReadXaml.GetResources("labRecover"));

                AuditTrailsManager atManager = new AuditTrailsManager();
                System.Data.DataTable logdt = null;
                if (null == atManager.SearchAllLog(out logdt, time, DateTime.Now, "All", "All", ""))
                {
                    //从日志中寻找收集信息
                    foreach (System.Data.DataRow it in logdt.Rows)
                    {
                        if (TextLegal.DoubleLegal(it[3].ToString()))
                        {
                            switch (it[1].ToString())
                            {
                                case "Coll":
                                    if (it[7].ToString().Equals(ReadXamlCollection.S_CollMarkM))
                                    {
                                        this.chromatogramUC.RestoreCollM(new MarkerInfo(it[8].ToString(), Convert.ToDouble(it[3])));
                                    }
                                    else if (it[7].ToString().Equals(Collection.ReadXamlCollection.S_CollMarkA))
                                    {
                                        this.chromatogramUC.RestoreCollA(new MarkerInfo(it[8].ToString(), Convert.ToDouble(it[3])));
                                    }
                                    break;
                                case "Marker":
                                    if (it[7].ToString().Equals(Manual.ReadXamlManual.S_ValveSwitch))
                                    {
                                        this.chromatogramUC.RestoreValve(new MarkerInfo(it[8].ToString(), Convert.ToDouble(it[3])));
                                    }
                                    else if (it[7].ToString().Equals(MethodEdit.ReadXamlMethod.S_PhaseName))
                                    {
                                        this.chromatogramUC.RestorePhase(new MarkerInfo(it[8].ToString(), Convert.ToDouble(it[3])));
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 设置各模块是否可用
        /// </summary>
        private void SetAvailable()
        {
            this.chromatogramUC.CurveSetIsEnabled = AdministrationStatic.Instance().ShowPermission(EnumPermission.ChromatogramSet);

            menuCommunication.IsEnabled = AdministrationStatic.Instance().ShowPermission(EnumPermission.Communication);
            menuInstrumentParameters.IsEnabled = AdministrationStatic.Instance().ShowPermission(EnumPermission.InstrumentParameters);
            menuAdministration.IsEnabled = AdministrationStatic.Instance().ShowPermission(EnumPermission.Administration);
            menuColumnHandling.IsEnabled = AdministrationStatic.Instance().ShowPermission(EnumPermission.ColumnHandling);
            menuTubeStand.IsEnabled = AdministrationStatic.Instance().ShowPermission(EnumPermission.TubeStand);
            menuManual.IsEnabled = AdministrationStatic.Instance().ShowPermission(EnumPermission.Manual);
            menuProject.IsEnabled = AdministrationStatic.Instance().ShowPermission(EnumPermission.Project);
            menuAuditTrails.IsEnabled = AdministrationStatic.Instance().ShowPermission(EnumPermission.AuditTrails);
            menuSystemMonitor.IsEnabled = AdministrationStatic.Instance().ShowPermission(EnumPermission.MonitorSet);
            menuDB.IsEnabled = AdministrationStatic.Instance().ShowPermission(EnumPermission.Databases);

            btnCommunication.IsEnabled = AdministrationStatic.Instance().ShowPermission(EnumPermission.Communication);
            btnInstrumentParameters.IsEnabled = AdministrationStatic.Instance().ShowPermission(EnumPermission.InstrumentParameters);
            btnAdministration.IsEnabled = AdministrationStatic.Instance().ShowPermission(EnumPermission.Administration);
            btnColumnHandling.IsEnabled = AdministrationStatic.Instance().ShowPermission(EnumPermission.ColumnHandling);
            btnTubeStand.IsEnabled = AdministrationStatic.Instance().ShowPermission(EnumPermission.TubeStand);
            btnManual.IsEnabled = AdministrationStatic.Instance().ShowPermission(EnumPermission.Manual);
            btnProject.IsEnabled = AdministrationStatic.Instance().ShowPermission(EnumPermission.Project);
            btnAuditTrails.IsEnabled = AdministrationStatic.Instance().ShowPermission(EnumPermission.AuditTrails);
            btnSystemMonitor.IsEnabled = AdministrationStatic.Instance().ShowPermission(EnumPermission.MonitorSet);
            btnDB.IsEnabled = AdministrationStatic.Instance().ShowPermission(EnumPermission.Databases);
        }

        /// <summary>
        /// 子窗口关闭调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChildWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            m_listChild.Remove((Window)sender);
        }

        /// <summary>
        /// 子窗口是否存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private bool IsHasWin<T>()
        {
            foreach (var it in m_listChild)
            {
                if (it is T)
                {
                    if (it.WindowState == WindowState.Minimized)
                    {
                        it.WindowState = WindowState.Normal;
                    }
                    it.Topmost = true;
                    it.Topmost = false;
                    it.Focus();
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 关闭已经打开却没有权限的子窗口
        /// </summary>
        private void JudgeWinAvailability()
        {
            for (int i = 0; i < m_listChild.Count; i++)
            {
                if (m_listChild[i] is WindowPermission)
                {
                    if (!(m_listChild[i] as WindowPermission).SetPermission(AdministrationStatic.Instance().MPermissionInfo))
                    {
                        --i;
                    }
                }
            }
        }

        /// <summary>
        /// 定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateBtnStatus();

            this.processPicture.UpdateLines(SystemControlManager.s_comconfStatic.GetPumpGet(ENUMPumpName.FITS) > 0
                , SystemControlManager.s_comconfStatic.GetPumpGet(ENUMPumpName.FITA) > 0
                , SystemControlManager.s_comconfStatic.GetPumpGet(ENUMPumpName.FITB) > 0
                , SystemControlManager.s_comconfStatic.GetPumpGet(ENUMPumpName.FITC) > 0
                , SystemControlManager.s_comconfStatic.GetPumpGet(ENUMPumpName.FITD) > 0
                , SystemControlManager.s_comconfStatic.GetValveGet(ENUMValveName.BPV)
                , SystemControlManager.s_comconfStatic.GetValveGet(ENUMValveName.IJV));

            switch (SystemControlManager.MSystemState)
            {
                case SystemState.Free:
                    {
                        if (0 != m_listAlarm.Count)
                        {
                            m_listAlarm.Clear();
                        }

                        if (0 != m_listWarning.Count)
                        {
                            m_listWarning.Clear();
                        }
                    }
                    break;
                case SystemState.Manual:
                case SystemState.Method:
                    {
                        bool isNewAlarm = false;
                        bool isNewWarning = false;

                        lock (m_listAlarm)
                        {
                            if (SystemControlManager.s_comconfStatic.GetCommunInfo(m_listAlarm))
                            {
                                isNewAlarm = true;
                            }

                            if (SystemControlManager.s_comconfStatic.GetAlarmInfo(m_listAlarm))
                            {
                                isNewAlarm = true;
                            }
                        }

                        lock (m_listWarning)
                        {
                            if (SystemControlManager.s_comconfStatic.GetWarningInfo(m_listWarning))
                            {
                                isNewWarning = true;
                            }
                        }

                        if (isNewAlarm || isNewWarning)
                        {
                            StringBuilderSplit sb = new StringBuilderSplit("\n");
                            foreach (var it in m_listAlarm)
                            {
                                sb.Append(it.MName + "  " + it.MValue);
                            }
                            foreach (var it in m_listWarning)
                            {
                                sb.Append(it.MName + "  " + it.MValue);
                            }
                            string info = sb.ToString();

                            if (isNewAlarm)
                            {
                                SystemControlManager.SetSystemStateRunToPause();
                                AuditTrailsStatic.Instance().InsertRowAlarmWarning(Share.ReadXaml.S_MsgAlarm, info);
                                AuditTrailsStatic.Instance().InsertRowManual(btnPause.ToolTip.ToString());

                                this.btnAlarm.Image = new BitmapImage(new Uri("pack://application:,,,/image/alarmYes.png"));
                            }
                            else
                            {
                                AuditTrailsStatic.Instance().InsertRowAlarmWarning(Share.ReadXaml.S_MsgWarning, info);
                            }

                            m_winAW.MText = info;
                            m_winAW.Visibility = Visibility.Visible;
                            m_winAW.Show();
                        }
                    }
                    break;
            }

            switch (SystemControlManager.MSystemState)
            {
                case SystemState.Free:
                    {
                        runStatusUC.UpdateInfo("", "", "", "", "", "", "", "");
                    }
                    break;
                case SystemState.Manual:
                    {
                        runStatusUC.UpdateInfo("", "", "", "", "", "", "", SystemControlManager.m_manualRun.MRunT);
                    }
                    break;
                case SystemState.Method:
                    {
                        string methodQueueName = "";
                        string methodCount = "";
                        string methodName = "";
                        string loopCount = "";
                        string loopIndex = "";
                        string phaseCount = "";
                        string phaseName = "";
                        string phaseRunning = "";
                        SystemControlManager.m_methodRun.GetInfo(ref methodQueueName, ref methodCount, ref methodName,
                            ref loopCount, ref loopIndex,
                            ref phaseCount, ref phaseName, ref phaseRunning);
                        runStatusUC.UpdateInfo(methodQueueName, methodCount, methodName,
                            loopCount, loopIndex,
                            phaseCount, phaseName, phaseRunning);
                    }
                    break;
            }

            //更新运行参数
            runParametersUC.UpdateInfo(SystemControlManager.m_curveStatic.m_T.ToString(), SystemControlManager.m_curveStatic.MV.ToString(), SystemControlManager.m_curveStatic.MCV.ToString()
                    , SystemControlManager.m_curveStatic.MColumnVol, SystemControlManager.m_curveStatic.MColumnHeight, SystemControlManager.m_curveStatic.MName);

            //更新状态栏
            txtSystemRunTime.Text = SystemControlManager.MSystemRunTime.ToString("f2") + DlyBase.SC_TUNIT;
            txtUserName.Text = AdministrationStatic.Instance().MUserInfo.MUserName;
            txtSystemTime.Text = DateTime.Now.ToString();

            //自动锁定
            GetCursorPos(out m_currPt);
            if (Visibility.Visible != m_screenLockWin.Visibility)
            {
                if (m_lastPt.X == m_currPt.X && m_lastPt.Y == m_currPt.Y)//鼠标不动
                {
                    TimeSpan span = DateTime.Now - m_lastTime;
                    if (0 != AdministrationStatic.Instance().MTacticsInfo.ScreenLock
                        && span.Days * 24 * 60 + span.Hours * 60 + span.Minutes >= AdministrationStatic.Instance().MTacticsInfo.ScreenLock)
                    {
                        m_screenLockWin.Show();

                        AuditTrailsStatic.Instance().InsertRowSystem(menuLockScreen.Header.ToString(), Share.ReadXaml.GetResources("A_AutoLock"));
                    }
                }
                else
                {
                    m_lastTime = DateTime.Now;
                    m_lastPt = m_currPt;
                }
            }
            else
            {
                m_lastTime = DateTime.Now;
                m_lastPt = m_currPt;
            }
        }


        /// <summary>
        /// 系统-语言-中文
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuChinese_Click(object sender, RoutedEventArgs e)
        {
            SystemControlManager.s_confCheckable.SetLanguage(EnumLanguage.Chinese);

            AuditTrailsStatic.Instance().InsertRowSystem(menuLanguage.Header.ToString(), menuChinese.Header.ToString());
        }

        /// <summary>
        /// 系统-语言-英文
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuEnglish_Click(object sender, RoutedEventArgs e)
        {
            SystemControlManager.s_confCheckable.SetLanguage(EnumLanguage.English);

            AuditTrailsStatic.Instance().InsertRowSystem(menuLanguage.Header.ToString(), menuEnglish.Header.ToString());
        }

        /// <summary>
        /// 系统-窗体尺寸-记忆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuRemember_Click(object sender, RoutedEventArgs e)
        {
            WindowSize.WindowSizeManager.s_RememberSize = SystemControlManager.s_confCheckable.MRememberSize;

            AuditTrailsStatic.Instance().InsertRowSystem(menuRemember.Header.ToString(), WindowSize.WindowSizeManager.s_RememberSize ? ReadXaml.S_Yes : ReadXaml.S_No);
        }

        /// <summary>
        /// 系统-用户切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuUserSwitchSet_Click(object sender, RoutedEventArgs e)
        {
            UserSwitchWin win = new UserSwitchWin(this, AdministrationStatic.Instance().MUserInfo.MUserName, AdministrationStatic.Instance().MUserInfo.MPwd);
            if (true == win.ShowDialog())
            {
                AuditTrailsStatic.Instance().UpdateUserName(AdministrationStatic.Instance().MUserInfo.MUserName);
                SetAvailable();
                JudgeWinAvailability();
            }
        }

        /// <summary>
        /// 系统-用户密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuUserPwdSet_Click(object sender, RoutedEventArgs e)
        {
            UserPwdWin win = new UserPwdWin(this, AdministrationStatic.Instance().MUserInfo, AdministrationStatic.Instance().MTacticsInfo);
            win.ShowDialog();
        }

        /// <summary>
        /// 系统-锁屏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuLockScreen_Click(object sender, RoutedEventArgs e)
        {
            m_screenLockWin.Show();

            AuditTrailsStatic.Instance().InsertRowSystem(menuLockScreen.Header.ToString(), Share.ReadXaml.GetResources("A_ManualLock"));
        }

        /// <summary>
        /// 工具-通信配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuCommunication_Click(object sender, RoutedEventArgs e)
        {
            if (!StopComConf())
            {
                return;
            }

            CommunSetsWin win = new CommunSetsWin(this);
            win.SetPermission(AdministrationStatic.Instance().MPermissionInfo);
            win.ShowDialog();

            SystemControlManager.s_comconfStatic.Init();
            UpdateComConf();

            UpdateCountdown();
        }

        /// <summary>
        /// 工具-仪表参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuInstrumentParameters_Click(object sender, RoutedEventArgs e)
        {
            if (SystemState.Free != SystemControlManager.MSystemState)
            {
                Share.MessageBoxWin.Show(Share.ReadXaml.S_OnlyFree);
                return;
            }

            SystemConfigWin win = new SystemConfigWin();
            win.MSystemConfig = StaticSystemConfig.SSystemConfig;
            win.MAlarmWarning = StaticAlarmWarning.SAlarmWarningOriginal;
            win.SetPermission(AdministrationStatic.Instance().ShowPermission(EnumPermission.InstrumentParameters_Edit));
            if (true == win.ShowDialog())
            {
                SystemControlManager.s_comconfStatic.SetSystemConfig(StaticSystemConfig.SSystemConfig);
                SystemControlManager.s_comconfStatic.SetAlarmWarning(StaticAlarmWarning.SAlarmWarningOriginal);
                StaticAlarmWarning.Reset();
                StaticValue.Reset();
            }
        }

        /// <summary>
        /// 工具-用户管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuAdministration_Click(object sender, RoutedEventArgs e)
        {
            if (IsHasWin<AdministrationWin>())
            {
                return;
            }

            AdministrationWin win = new AdministrationWin(this);
            win.SetPermission(AdministrationStatic.Instance().MPermissionInfo);
            win.MAddUser += DlyAddProjectNode;
            win.MUpdatePermission += DlyUpdatePermission;
            win.Show();

            win.Closing += ChildWindow_Closing;
            m_listChild.Add(win);
        }

        /// <summary>
        /// 工具-色谱柱管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuColumnHandling_Click(object sender, RoutedEventArgs e)
        {
            if (IsHasWin<ColumnHandlingWin>())
            {
                return;
            }

            ColumnHandlingWin win = new ColumnHandlingWin(this, AdministrationStatic.Instance().MUserInfo.MUserName);
            win.SetPermission(AdministrationStatic.Instance().MPermissionInfo);
            win.Show();

            win.Closing += ChildWindow_Closing;
            m_listChild.Add(win);
        }

        /// <summary>
        /// 工具-试管架管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuTubeStand_Click(object sender, RoutedEventArgs e)
        {
            if (IsHasWin<TubeStandWin>())
            {
                return;
            }

            TubeStandWin win = new TubeStandWin(this);
            win.SetPermission(AdministrationStatic.Instance().MPermissionInfo);
            win.Show();

            win.Closing += ChildWindow_Closing;
            m_listChild.Add(win);
        }

        /// <summary>
        /// 工具-手动编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuManual_Click(object sender, RoutedEventArgs e)
        {
            if (SystemState.Free != SystemControlManager.MSystemState && SystemState.Manual != SystemControlManager.MSystemState)
            {
                Share.MessageBoxWin.Show(Share.ReadXaml.S_OnlyFree);
                return;
            }

            if (!AdministrationStatic.Instance().ShowSignerReviewerWin(this, EnumSignerReviewer.Manual))
            {
                return;
            }

            for (int i = 0; i < ItemVisibility.s_listValve.Count; i++)
            {
                SystemControlManager.m_manualRun.m_manualValue.m_valveValue.MListValave[i].MIndex = SystemControlManager.s_comconfStatic.GetValveGet((ENUMValveName)i);
            }

            SystemControlManager.m_manualRun.m_manualValue.UpdateStart(StaticSystemConfig.SSystemConfig.MListConfAS);

            SystemControlManager.m_manualRun.m_manualValue.m_uvValue.SetCurrInfo(SystemControlManager.s_comconfStatic.GetItem(ENUMUVName.UV01));
            SystemControlManager.m_manualRun.m_manualValue.m_riValue.SetCurrInfo(SystemControlManager.s_comconfStatic.GetItem(ENUMRIName.RI01));
            SystemControlManager.m_manualRun.m_manualValue.m_mixerValue.SetCurrInfo(SystemControlManager.s_comconfStatic.GetItem(ENUMMixerName.Mixer01));

            ManualWin win = new ManualWin(SystemControlManager.m_manualRun.m_manualValue);
            win.MStrBase = chromatogramUC.MStrBase;
            win.MUpdateManual += DlyUpdateManual;
            win.ShowDialog();
            win.MUpdateManual -= DlyUpdateManual;
        }

        /// <summary>
        /// 工具-项目管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuProject_Click(object sender, RoutedEventArgs e)
        {
            if (null == SystemControlManager.s_comconfStatic.m_cs)
            {
                return;
            }

            if (IsHasWin<ProjectManagerWin>())
            {
                return;
            }

            ProjectManagerWin win = new ProjectManagerWin(this, AdministrationStatic.Instance().MUserInfo.MProjectID, SystemControlManager.s_comconfStatic.m_cs.MId, SystemControlManager.s_comconfStatic.m_signalList);
            win.SetPermission(AdministrationStatic.Instance().MPermissionInfo);
            win.MSelectItem += SendMethodOrQueue;
            win.MSelectCurve += SendCurve;
            win.Show();

            win.Closing += ChildWindow_Closing;
            m_listChild.Add(win);
        }

        /// <summary>
        /// 工具-审计跟踪
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuAuditTrails_Click(object sender, RoutedEventArgs e)
        {
            if (IsHasWin<AuditTrailWin>())
            {
                return;
            }

            AuditTrailWin win = new AuditTrailWin(this);
            win.SetPermission(AdministrationStatic.Instance().MPermissionInfo);
            win.Show();

            win.Closing += ChildWindow_Closing;
            m_listChild.Add(win);
        }

        /// <summary>
        /// 工具-系统监控
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuSystemMonitor_Click(object sender, RoutedEventArgs e)
        {
            if (IsHasWin<SystemMonitorWin>())
            {
                return;
            }

            SystemMonitorWin win = new SystemMonitorWin(SystemControlManager.s_comconfStatic.m_runDataSetList);
            win.Show();

            win.Closing += ChildWindow_Closing;
            m_listChild.Add(win);
        }

        /// <summary>
        /// 工具-数据管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuDB_Click(object sender, RoutedEventArgs e)
        {
            Database.DBBackupRestoreWin win = new Database.DBBackupRestoreWin(this);
            win.SetPermission(AdministrationStatic.Instance().MPermissionInfo);
            win.Show();

            win.Closing += ChildWindow_Closing;
            m_listChild.Add(win);
        }


        /// <summary>
        /// 帮助-帮助
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuHelp_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("Bio-LabChrom.chm");
        }

        /// <summary>
        /// 帮助-关于
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutWin win = new AboutWin();
            win.ShowDialog();
        }


        /// <summary>
        /// 运行控制-运行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            string resultName = "";
            int projectID = -1;

            switch (SystemControlManager.m_methodRun.MMethodType.MType)
            {
                case EnumMethodType.MethodQueue:
                    {
                        projectID = SystemControlManager.m_methodRun.MMethodQueue.MProjectID;
                        RunResultWin win = new RunResultWin();
                        if (true == win.ShowDialog())
                        {
                            resultName = win.MResultName;
                        }
                        else
                        {
                            return;
                        }

                        SystemControlManager.SetSystemStateFreeToMethodQueue(resultName, projectID);
                        runStatusUC.SetEnumMethodType(EnumResultIconType.MethodQueue);
                    }
                    break;
                default:
                    {
                        projectID = SystemControlManager.m_methodRun.MMethod.MProjectID;

                        ProjectTreeManager managerProject = new ProjectTreeManager();
                        int firstID = managerProject.GetFirstNodeID(projectID);
                        if (firstID == AdministrationStatic.Instance().MUserInfo.MProjectID
                            || firstID == 1)
                        {
                            //用户使用自己的方法
                            switch (SystemControlManager.m_methodRun.MMethod.MMethodSetting.MResultName.MType)
                            {
                                case EnumResultType.NoName:
                                    RunResultWin win = new RunResultWin();
                                    if (true == win.ShowDialog())
                                    {
                                        resultName = win.MResultName;
                                    }
                                    else
                                    {
                                        return;
                                    }
                                    break;
                                case EnumResultType.DlyName:
                                    resultName = SystemControlManager.m_methodRun.MMethod.MMethodSetting.MResultName.MDlyName;
                                    break;
                                case EnumResultType.MethodName:
                                    resultName = SystemControlManager.m_methodRun.MMethod.MName;
                                    break;
                                case EnumResultType.DateTime:
                                    resultName = DateTime.Now.ToString("yyyyMMddHHmmss");
                                    break;
                            }

                            //唯一键值
                            if (SystemControlManager.m_methodRun.MMethod.MMethodSetting.MResultName.MUnique)
                            {
                                ResultManager manager = new ResultManager();
                                manager.GetAvailableName(SystemControlManager.s_comconfStatic.m_cs.MId, projectID, ref resultName);
                            }
                        }
                        else
                        {
                            //用户使用别人的方法
                            RunLocationResultWin win = new RunLocationResultWin(AdministrationStatic.Instance().MUserInfo.MProjectID, false);
                            if (true == win.ShowDialog())
                            {
                                resultName = win.MResultName;
                                projectID = win.MProjectID;
                            }
                            else
                            {
                                return;
                            }
                        }

                        SystemControlManager.SetSystemStateFreeToMethod(resultName, projectID);
                        runStatusUC.SetEnumMethodType(EnumResultIconType.Method);
                    }
                    break;
            }

            AuditTrailsStatic.Instance().InsertRowManual(((Button)sender).ToolTip.ToString(), resultName);
        }

        /// <summary>
        /// 运行控制-挂起
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHold_Click(object sender, RoutedEventArgs e)
        {
            if (true == ((IconToggleButton)sender).IsChecked)
            {
                SystemControlManager.SetSystemStateHold(true);
                AuditTrailsStatic.Instance().InsertRowManual(((Share.IconToggleButton)sender).ToolTip.ToString(), Share.ReadXaml.GetResources("labStart"));
            }
            else
            {
                SystemControlManager.SetSystemStateHold(false);
                AuditTrailsStatic.Instance().InsertRowManual(((Share.IconToggleButton)sender).ToolTip.ToString(), Share.ReadXaml.GetResources("labEnd"));
            }
        }

        /// <summary>
        /// 运行控制-挂起直到
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHoldUntil_Click(object sender, RoutedEventArgs e)
        {
            if (true == ((IconToggleButton)sender).IsChecked)
            {
                SystemControlManager.SetSystemStateHold(true);
                AuditTrailsStatic.Instance().InsertRowManual(((Share.IconToggleButton)sender).ToolTip.ToString(), Share.ReadXaml.GetResources("labStart"));
            }
            else
            {
                SystemControlManager.SetSystemStateHold(false);
                AuditTrailsStatic.Instance().InsertRowManual(((Share.IconToggleButton)sender).ToolTip.ToString(), Share.ReadXaml.GetResources("labEnd"));
            }
        }

        /// <summary>
        /// 运行控制-下一阶段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            this.btnHold.IsChecked = false;
            SystemControlManager.SetSystemStateRunToNext();

            AuditTrailsStatic.Instance().InsertRowManual(((Button)sender).ToolTip.ToString());
        }

        /// <summary>
        /// 运行控制-暂停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            SystemControlManager.SetSystemStateRunToPause();

            AuditTrailsStatic.Instance().InsertRowManual(((Button)sender).ToolTip.ToString());
        }

        /// <summary>
        /// 运行控制-继续
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            if (0 != m_listAlarm.Count)
            {
                MessageBoxWin.Show(Share.ReadXaml.GetResources("labCannot"));
                return;
            }

            SystemControlManager.SetSystemStatePauseToRun();

            AuditTrailsStatic.Instance().InsertRowManual(((Button)sender).ToolTip.ToString());
        }

        /// <summary>
        /// 运行控制-停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            this.btnHold.IsChecked = false;
            this.btnHoldUntil.IsChecked = false;
            SystemControlManager.SetSystemStateStop();

            AuditTrailsStatic.Instance().InsertRowManual(((Button)sender).ToolTip.ToString());
        }


        /// <summary>
        /// 倒计时弹窗-pH
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnpH_Click(object sender, RoutedEventArgs e)
        {
            CountdownWin win = new CountdownWin();
            foreach (var it in StaticSystemConfig.SSystemConfig.MListConfpHCdUV)
            {
                if (it.MName.Contains("pH"))
                {
                    win.MLength = it.MVol;
                    break;
                }
            }
            win.Show();
        }

        /// <summary>
        /// 倒计时弹窗-Cd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCd_Click(object sender, RoutedEventArgs e)
        {
            CountdownWin win = new CountdownWin();
            foreach (var it in StaticSystemConfig.SSystemConfig.MListConfpHCdUV)
            {
                if (it.MName.Contains("Cd"))
                {
                    win.MLength = it.MVol;
                    break;
                }
            }
            win.Show();
        }

        /// <summary>
        /// 倒计时弹窗-UV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUV_Click(object sender, RoutedEventArgs e)
        {
            CountdownWin win = new CountdownWin();
            foreach (var it in StaticSystemConfig.SSystemConfig.MListConfpHCdUV)
            {
                if (it.MName.Contains("UV"))
                {
                    win.MLength = it.MVol;
                    break;
                }
            }
            win.Show();
        }


        /// <summary>
        /// 运行控制-消警
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAlarm_Click(object sender, RoutedEventArgs e)
        {
            lock (m_listAlarm)
            {
                m_listAlarm.Clear();
                AuditTrailsStatic.Instance().InsertRowOperate(btnAlarm.ToolTip.ToString());
            }

            this.btnAlarm.Image = new BitmapImage(new Uri("pack://application:,,,/image/alarmNo.png"));
        }


        /// <summary>
        /// 运行数据行的显隐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRunData_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Visibility = SystemControlManager.s_comconfStatic.m_runDataList[e.Row.GetIndex()].MIsShow ? Visibility.Visible : Visibility.Collapsed;
            e.Row.Foreground = SystemControlManager.s_comconfStatic.m_runDataList[e.Row.GetIndex()].MBrush;
        }

        /// <summary>
        /// 运行数据表格的双击弹窗事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRunData_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!AdministrationStatic.Instance().ShowPermission(EnumPermission.MonitorSet))
            {
                return;
            }

            StringBoolWin win = new StringBoolWin(Share.ReadXaml.GetResources("menuRunData"), true);
            foreach (var it in SystemControlManager.s_comconfStatic.m_runDataList)
            {
                if (string.IsNullOrEmpty(it.MUnit))
                {
                    win.AddItem(new StringBool(it.MDlyName, it.MBrush, it.MIsShow));
                }
                else
                {
                    win.AddItem(new StringBool(it.MDlyName + "(" + it.MUnit + ")", it.MBrush, it.MIsShow));
                }
            }
            if (true == win.ShowDialog())
            {
                for (int i = 0; i < SystemControlManager.s_comconfStatic.m_runDataList.Count; i++)
                {
                    SystemControlManager.s_comconfStatic.m_runDataList[i].MIsShow = win.GetItem(i).MCheck;
                    SystemControlManager.s_comconfStatic.m_runDataList[i].MBrush = win.GetItem(i).MBrush;
                }

                SystemControlManager.s_comconfStatic.UpdateRunDataList();

                for (int i = 0; i < SystemControlManager.s_comconfStatic.m_runDataList.Count; i++)
                {
                    DataGridRow row = dgvRunData.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    row.Visibility = SystemControlManager.s_comconfStatic.m_runDataList[i].MIsShow ? Visibility.Visible : Visibility.Collapsed;
                    row.Foreground = SystemControlManager.s_comconfStatic.m_runDataList[i].MBrush;
                }
            }
        }

        /// <summary>
        /// 警报警告的双击弹窗事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listAlarmWarning_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SystemState.Free == SystemControlManager.MSystemState)
            {
                Share.MessageBoxWin.Show(Share.ReadXaml.S_OnlyNotFree);
                return;
            }

            AlarmWarningWin dlg = new AlarmWarningWin();
            dlg.Owner = this;
            dlg.MAlarmWarning = StaticAlarmWarning.SAlarmWarning;
            dlg.ShowDialog();
        }

        /// <summary>
        /// 审计跟踪的双击弹窗事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void auditTrailsUC_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!AdministrationStatic.Instance().ShowPermission(EnumPermission.MonitorSet))
            {
                return;
            }

            AuditTrailsManager manager = new AuditTrailsManager();
            LogColumnVisibility item = null;
            string error = manager.GetColumnVisibility(out item);
            if (null == error)
            {
                List<string> columnList = Share.ReadXaml.GetEnumList<EnumLog>("AT_");
                StringBoolWin win = new StringBoolWin(Share.ReadXaml.GetResources("menuAuditTrails"));
                for (int i = 0; i < item.MArrVisib.Length; i++)
                {
                    win.AddItem(new StringBool(columnList[i], item.MArrVisib[i]));
                }
                if (true == win.ShowDialog())
                {
                    for (int i = 0; i < item.MArrVisib.Length; i++)
                    {
                        item.MArrVisib[i] = win.GetItem(i).MCheck;
                    }

                    error = manager.SetColumnVisibility(item);
                    if (null == error)
                    {
                        auditTrailsUC.SetColumnVisible(item);
                    }
                }
            }
        }
    }
}