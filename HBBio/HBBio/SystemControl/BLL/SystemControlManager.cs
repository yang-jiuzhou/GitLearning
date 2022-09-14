using HBBio.Administration;
using HBBio.AuditTrails;
using HBBio.Chromatogram;
using HBBio.Communication;
using HBBio.Database;
using HBBio.Manual;
using HBBio.MethodEdit;
using HBBio.Result;
using HBBio.Share;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio.SystemControl
{
    /**
     * ClassName: SystemControlManager
     * Description: 系统控制静态类
     * Version: 1.0
     * Create:  2018/05/28
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    public static class SystemControlManager
    {
        //主界面内容配置
        public static ConfCheckable s_confCheckable = new ConfCheckable();

        //主界面内容显隐
        public static ViewVisibility s_viewVisibility = new ViewVisibility();

        //系统配置
        public static ComConfStatic s_comconfStatic = null;

        //运行结果
        public static ResultStatic m_curveStatic = null;

        //手动运行
        public static ManualRunManager m_manualRun = null;

        //方法运行
        public static MethodRunManager m_methodRun = null;

        private static Thread m_systemThread = null;                        //系统运行线程
        private static SystemState m_systemState = SystemState.Free;        //系统运行状态
        public static SystemState MSystemState
        {
            get
            {
                return m_systemState;
            }
        }
        private static readonly DateTime m_systemStart = DateTime.Now;      //系统开始运行的时间点                    
        public static double MSystemRunTime
        {
            get
            {
                return ValueTrans.TimeSpanToMin(DateTime.Now, m_systemStart);
            }
        }                               //系统累计运行的时间

        private static DateTime m_chromatogramStart = DateTime.Now;         //谱图开始运行的时间点
        private static double m_chromatogramPauseTime = 0;                  //谱图累计暂停的时间
        private static double m_chromatogramRunTime = 0;                    //谱图累计运行的时间
        public static double MChromatogramTime
        {
            get
            {
                return m_chromatogramRunTime;
            }
        }

        private static bool s_pauseFlag = false;                            //启用自动结束暂停信号
        private static double s_pauseTime = 0;                              //自动暂停的时间
        private static DateTime s_pauseStart = DateTime.Now;                //自动暂停的开始时间点

        private static WashSystem s_wash = new WashSystem();

        private static Queue<string> s_queueAlarm = new Queue<string>();
        private static Queue<string> s_queueWarning = new Queue<string>();

        private static Thread m_dbAutoBackupThread = null;                  //系统自动备份线程
        public static bool s_autoDBIng = false;                             //系统自动备份标志

        //创建一个自定义委托，用于发送谱图数据
        public delegate void ResultEventHandler(object sender);
        //声明一个谱图数据事件
        public static ResultEventHandler MResultEvent;

        //创建一个自定义委托，用于自定义的信号
        public delegate void MHandlerDdelegateMarker(object sender);
        //声明一个新建标记事件
        public static MHandlerDdelegateMarker MMarkerHandler;


        /// <summary>
        /// 静态构造函数
        /// </summary>
        static SystemControlManager()
        {
            DBSelfCheck dbSelfCheck = new DBSelfCheck();
            dbSelfCheck.InitFirst();

            GetConfCheckable(s_confCheckable);
            GetViewVisibility(s_viewVisibility);

            s_comconfStatic = ComConfStatic.Instance();

            m_curveStatic = ResultStatic.Instance();

            m_manualRun = ManualRunManager.Instance(s_comconfStatic);
            m_methodRun = MethodRunManager.Instance(s_comconfStatic);

            if (m_methodRun.MIsBreak)
            {
                AuditTrailsStatic.Instance().ContinueTable();
            }
            else
            {
                AuditTrailsStatic.Instance().CreateTable();
            }

            m_methodRun.MMarkerHandler += DlyMethodRunMarker;
            m_methodRun.MPauseHandler += DlyMethodRunPause;
            m_methodRun.MMethodBeginHandler += DlyMethodBegin;
            m_methodRun.MWashHandler += DlyMethodRunWash;
            m_manualRun.MMarkerHandler += DlyManualdRunMarker;

            //开启运行的线程，状态机跳转
            m_systemThread = new Thread(ThreadSystemFun);
            m_systemThread.IsBackground = true;
            m_systemThread.Start();

            //开启自动备份的线程
            m_dbAutoBackupThread = new Thread(ThreadDBAutoBackupFun);
            m_dbAutoBackupThread.IsBackground = true;
            m_dbAutoBackupThread.Start();
        }

        public static void Start()
        {
            
        }

        /// <summary>
        /// 检测数据
        /// </summary>
        public static void CheckAllData()
        {
            Thread thread = new Thread(ThreadDBSelfCheck);
            thread.IsBackground = true;
            thread.Start(); 
        }

        /// <summary>
        /// 数据自检线程
        /// </summary>
        private static void ThreadDBSelfCheck()
        {
            try
            {
                DBSelfCheck dbSelfCheck = new DBSelfCheck();
                dbSelfCheck.CreateAll();
            }
            catch
            { }
        }

        /// <summary>
        /// 系统由空闲到手动
        /// </summary>
        /// <param name="resultName"></param>
        /// <param name="projectID"></param>
        public static void SetSystemStateFreeToManual(string resultName, int projectID)
        {
            MResultEvent?.Invoke(null);

            List<string> nameList = s_comconfStatic.m_signalList.Select(x => x.MConstName).ToList();
            StringBuilderSplit sbAttachmentInfo = new StringBuilderSplit(";");
            if (Visibility.Visible == ItemVisibility.s_listUV[ENUMUVName.UV01])
            {
                UVItem itemUV = s_comconfStatic.GetItem(ENUMUVName.UV01);
                foreach (var it in itemUV.m_waveGet)
                {
                    sbAttachmentInfo.Append(it);
                }
            }
            m_curveStatic.CreateTable(nameList, resultName, s_comconfStatic.m_cs.MId, projectID, AdministrationStatic.Instance().MUserInfo.MID, EnumResultIconType.Manual, null, StaticSystemConfig.SSystemConfig.MConfColumn.MColumnVol, StaticSystemConfig.SSystemConfig.MConfColumn.MColumnHeight, sbAttachmentInfo.ToString());

            ProjectManager.ProjectTreeManager manager = new ProjectManager.ProjectTreeManager();
            manager.UpdateItemCountResult(projectID);

            m_systemState = SystemState.FreeToManual;
            AuditTrailsStatic.Instance().UpdateRowBatch("0");
        }

        /// <summary>
        /// 系统由空闲到方法
        /// </summary>
        /// <param name="resultName"></param>
        /// <param name="projectID"></param>
        public static void SetSystemStateFreeToMethod(string resultName, int projectID)
        {
            MResultEvent?.Invoke(null);

            List<string> nameList = s_comconfStatic.m_signalList.Select(x => x.MConstName).ToList();
            StringBuilderSplit sbAttachmentInfo = new StringBuilderSplit(";");
            if (Visibility.Visible == ItemVisibility.s_listUV[ENUMUVName.UV01])
            {
                UVItem itemUV = s_comconfStatic.GetItem(ENUMUVName.UV01);
                foreach (var it in itemUV.m_waveGet)
                {
                    sbAttachmentInfo.Append(it);
                }
            }
            m_curveStatic.CreateTable(nameList, resultName, s_comconfStatic.m_cs.MId, projectID, AdministrationStatic.Instance().MUserInfo.MID, EnumResultIconType.Method, Share.DeepCopy.GetMemoryStream(m_methodRun.MMethod)
                , m_methodRun.MMethod.MMethodSetting.MColumnVol
                , m_methodRun.MMethod.MMethodSetting.MColumnVol / m_methodRun.MMethod.MMethodSetting.MColumnArea
                , sbAttachmentInfo.ToString());

            ProjectManager.ProjectTreeManager manager = new ProjectManager.ProjectTreeManager();
            manager.UpdateItemCountResult(projectID);

            m_systemState = SystemState.FreeToMethod;
            AuditTrailsStatic.Instance().UpdateRowBatch("0");
        }

        /// <summary>
        /// 系统由空闲到方法队列
        /// </summary>
        /// <param name="resultName"></param>
        /// <param name="projectID"></param>
        public static void SetSystemStateFreeToMethodQueue(string resultName, int projectID)
        {
            MResultEvent?.Invoke(null);

            List<string> nameList = s_comconfStatic.m_signalList.Select(x => x.MConstName).ToList();
            StringBuilderSplit sbAttachmentInfo = new StringBuilderSplit(";");
            if (Visibility.Visible == ItemVisibility.s_listUV[ENUMUVName.UV01])
            {
                UVItem itemUV = s_comconfStatic.GetItem(ENUMUVName.UV01);
                foreach (var it in itemUV.m_waveGet)
                {
                    sbAttachmentInfo.Append(it);
                }
            }
            m_curveStatic.CreateTable(nameList, resultName + "(1)", s_comconfStatic.m_cs.MId, projectID, AdministrationStatic.Instance().MUserInfo.MID, EnumResultIconType.MethodQueue, Share.DeepCopy.GetMemoryStream(m_methodRun.MMethod)
                , m_methodRun.MMethod.MMethodSetting.MColumnVol
                , m_methodRun.MMethod.MMethodSetting.MColumnVol / m_methodRun.MMethod.MMethodSetting.MColumnArea
                , sbAttachmentInfo.ToString());

            ProjectManager.ProjectTreeManager manager = new ProjectManager.ProjectTreeManager();
            manager.UpdateItemCountResult(projectID);

            m_systemState = SystemState.FreeToMethod;
            AuditTrailsStatic.Instance().UpdateRowBatch("0");
        }

        /// <summary>
        /// 系统由空闲到异常中止
        /// </summary>
        /// <param name="t"></param>
        public static void SetSystemFreeToManualBreak(double t, double v, double cv)
        {
            List<string> nameList = s_comconfStatic.m_signalList.Select(x => x.MConstName).ToList();
            m_curveStatic.ContinueTable(nameList, t, v, cv);

            m_chromatogramRunTime = t;
            m_systemState = SystemState.FreeToManualBreak;
        }
        public static void SetSystemFreeToMethodBreak(double t, double v, double cv)
        {
            List<string> nameList = s_comconfStatic.m_signalList.Select(x => x.MConstName).ToList();
            m_curveStatic.ContinueTable(nameList, t, v, cv);

            m_chromatogramRunTime = t;
            m_systemState = SystemState.FreeToMethodBreak;
        }

        /// <summary>
        /// 系统由运行到暂停
        /// </summary>
        public static void SetSystemStateRunToPause()
        {
            switch (m_systemState)
            {
                case SystemState.Manual:
                    if (ManualState.Run == m_manualRun.m_state)
                    {
                        m_manualRun.m_state = ManualState.RunToPause;
                    }
                    break;
                case SystemState.Method:
                    if (MethodState.Run == m_methodRun.m_state)
                    {
                        m_methodRun.m_state = MethodState.RunToPause;
                    }
                    break;
            }
        }

        /// <summary>
        /// 系统由运行到下一步
        /// </summary>
        public static void SetSystemStateRunToNext()
        {
            switch (m_systemState)
            {
                case SystemState.Manual:
                    break;
                case SystemState.Method:
                    m_methodRun.m_state = MethodState.RunToNext;
                    break;
            }
        }

        /// <summary>
        /// 系统由暂停到运行
        /// </summary>
        public static void SetSystemStatePauseToRun()
        {
            switch (m_systemState)
            {
                case SystemState.Manual:
                    m_manualRun.m_state = ManualState.PauseToRun;
                    break;
                case SystemState.Method:
                    m_methodRun.m_state = MethodState.PauseToRun;
                    break;
            }
        }

        /// <summary>
        /// 系统由运行到下一步
        /// </summary>
        public static void SetSystemStateHold(bool result)
        {
            switch (m_systemState)
            {
                case SystemState.Manual:
                    m_manualRun.MIsHold = result;
                    break;
                case SystemState.Method:
                    m_methodRun.MIsHold = result;
                    break;
            }
        }

        /// <summary>
        /// 系统停止
        /// </summary>
        public static void SetSystemStateStop()
        {
            switch (m_systemState)
            {
                case SystemState.Manual:
                    m_manualRun.m_state = ManualState.Stop;
                    break;
                case SystemState.Method:
                    m_methodRun.m_state = MethodState.Stop;
                    break;
            }
        }

        /// <summary>
        /// 添加谱图标记(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        private static void DlyMethodRunMarker(object sender)
        {
            MMarkerHandler?.Invoke(new MarkerInfo((string)sender));
        }

        /// <summary>
        /// 添加谱图标记(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        private static void DlyMethodBegin(object sender)
        {
            MResultEvent?.Invoke(null);

            ResultManager manager = new ResultManager();
            manager.UpdateEndTime();

            string name = null;
            int projectID = -1;
            manager.GetCurveDataLast(out name, out projectID);
            name = name.Substring(0, name.LastIndexOf("(")) + "(" + (int)sender + ")";

            List<string> nameList = s_comconfStatic.m_signalList.Select(x => x.MConstName).ToList();
            StringBuilderSplit sbAttachmentInfo = new StringBuilderSplit(";");
            if (Visibility.Visible == ItemVisibility.s_listUV[ENUMUVName.UV01])
            {
                UVItem itemUV = s_comconfStatic.GetItem(ENUMUVName.UV01);
                foreach (var it in itemUV.m_waveGet)
                {
                    sbAttachmentInfo.Append(it);
                }
            }
            m_curveStatic.CreateTable(nameList, name, s_comconfStatic.m_cs.MId, projectID, AdministrationStatic.Instance().MUserInfo.MID, EnumResultIconType.MethodQueue, Share.DeepCopy.GetMemoryStream(m_methodRun.MMethod)
                , m_methodRun.MMethod.MMethodSetting.MColumnVol
                , m_methodRun.MMethod.MMethodSetting.MColumnVol / m_methodRun.MMethod.MMethodSetting.MColumnArea
                , sbAttachmentInfo.ToString());

            ProjectManager.ProjectTreeManager managerPT = new ProjectManager.ProjectTreeManager();
            managerPT.UpdateItemCountResult(projectID);

            AuditTrailsStatic.Instance().UpdateRowBatch("0");

            m_chromatogramStart = DateTime.Now;
            m_chromatogramPauseTime = 0;
        }


        /// <summary>
        /// 添加谱图标记(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        private static void DlyManualdRunMarker(object type, object val)
        {
            MMarkerHandler?.Invoke(new MarkerInfo((string)type, (double)val));
        }

        /// <summary>
        /// 泵洗开始(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        public static void DlyWashStart(object sender, RoutedEventArgs e)
        {
            s_comconfStatic.SetValve(((WashPara)e.OriginalSource).MValve, ((WashPara)e.OriginalSource).MValveIndex);
            s_wash.Start(s_comconfStatic, ((WashPara)e.OriginalSource).MPump, ((WashPara)e.OriginalSource).MWashIndex);
        }

        /// <summary>
        /// 泵洗结束(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        public static void DlyWashStop(object sender, RoutedEventArgs e)
        {
            s_wash.Stop(s_comconfStatic, (ENUMPumpName)e.OriginalSource);
        }

        /// <summary>
        /// 启用泵洗(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        private static void DlyMethodRunWash(object sender)
        {
            s_wash.StartAll(s_comconfStatic, (int)sender);
        }

        /// <summary>
        /// 启用自动暂停(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        private static void DlyMethodRunPause(object sender)
        {
            s_pauseFlag = true;
            s_pauseTime = (double)sender;
            s_pauseStart = DateTime.Now;
        }

        /// <summary>
        /// 系统运行的线程函数
        /// </summary>
        private static void ThreadSystemFun()
        {
            while (true)
            {
                try
                {
                    s_comconfStatic.UpdateAllData();
                    
                    if (s_pauseFlag)//启用方法内的自动暂停
                    {
                        if (ValueTrans.TimeSpanToMin(DateTime.Now, s_pauseStart) >= s_pauseTime)
                        {
                            m_methodRun.m_state = MethodState.PauseToRun;
                        }
                    }

                    switch (m_systemState)
                    {
                        case SystemState.Free:
                            s_wash.Update(s_comconfStatic);
                            Thread.Sleep(DlyBase.c_sleep5);
                            break;
                        case SystemState.Wash:
                            switch (s_wash.Update(s_comconfStatic))
                            {
                                case EnumWashStatus.No:
                                    Thread.Sleep(DlyBase.c_sleep5);
                                    break;
                                case EnumWashStatus.Over:
                                    m_systemState = SystemState.Free;
                                    break;
                            }
                            break;
                        case SystemState.Manual:
                            switch (m_manualRun.m_state)
                            {
                                case ManualState.Run:
                                    switch (s_wash.Update(s_comconfStatic))
                                    {
                                        case EnumWashStatus.No:
                                            m_chromatogramRunTime = Math.Round(ValueTrans.TimeSpanToMin(DateTime.Now, m_chromatogramStart) - m_chromatogramPauseTime, 2);
                                            if (m_curveStatic.IsNewData(m_chromatogramRunTime, s_comconfStatic.m_totalFlow, s_comconfStatic.m_pumpSFlow))
                                            {
                                                AuditTrailsStatic.Instance().UpdateRowBatch(m_curveStatic.m_T, m_curveStatic.MV, m_curveStatic.MCV);
                                                for (int i = 0; i < s_comconfStatic.m_signalList.Count; i++)
                                                {
                                                    m_curveStatic.m_curveInfo.m_valList[i] = ValueTrans.JudgeRange(s_comconfStatic.m_signalList[i].MRealValue);
                                                }
                                                m_curveStatic.AddItem(m_curveStatic.m_curveInfo);
                                                MResultEvent?.Invoke(m_curveStatic.m_curveInfo);

                                                m_manualRun.Run(m_curveStatic.m_T, m_curveStatic.MV, m_curveStatic.MCV, true);
                                                Thread.Sleep(DlyBase.c_sleep5);
                                            }
                                            else
                                            {
                                                Thread.Sleep(DlyBase.c_sleep1);
                                            }
                                            break;
                                        case EnumWashStatus.Ing:
                                            Thread.Sleep(DlyBase.c_sleep5);
                                            break;
                                        case EnumWashStatus.Over:
                                            m_chromatogramPauseTime = Math.Round(ValueTrans.TimeSpanToMin(DateTime.Now, m_chromatogramStart) - m_chromatogramRunTime, 2);
                                            m_manualRun.m_state = ManualState.Run;
                                            break;
                                    }
                                    break;
                                case ManualState.Pause:
                                    m_manualRun.Run(m_curveStatic.m_T, m_curveStatic.MV, m_curveStatic.MCV, false);
                                    Thread.Sleep(DlyBase.c_sleep5);
                                    break;
                                case ManualState.Stop:
                                    m_manualRun.Stop();
                                    ResultManager manager = new ResultManager();
                                    manager.UpdateEndTime();
                                    m_systemState = SystemState.Stop;
                                    break;
                                case ManualState.FreeToRun:
                                    m_manualRun.FreeToRun();
                                    break;
                                case ManualState.RunToPause:
                                    m_manualRun.RunToPause();
                                    break;
                                case ManualState.PauseToRun:
                                    m_chromatogramPauseTime = Math.Round(ValueTrans.TimeSpanToMin(DateTime.Now, m_chromatogramStart) - m_chromatogramRunTime, 2);
                                    m_manualRun.PauseToRun();
                                    break;
                                case ManualState.BreakToPause:
                                    m_manualRun.BreakToPause(m_curveStatic.m_T, m_curveStatic.MV, m_curveStatic.MCV);
                                    break;
                            }
                            break;
                        case SystemState.Method:
                            switch (m_methodRun.m_state)
                            {
                                case MethodState.Run:
                                    switch (s_wash.Update(s_comconfStatic))
                                    {
                                        case EnumWashStatus.No:
                                            m_chromatogramRunTime = Math.Round(ValueTrans.TimeSpanToMin(DateTime.Now, m_chromatogramStart) - m_chromatogramPauseTime, 2);
                                            if (m_curveStatic.IsNewData(m_chromatogramRunTime, s_comconfStatic.m_totalFlow, s_comconfStatic.m_pumpSFlow))
                                            {
                                                AuditTrailsStatic.Instance().UpdateRowBatch(m_curveStatic.m_T, m_curveStatic.MV, m_curveStatic.MCV);
                                                for (int i = 0; i < s_comconfStatic.m_signalList.Count; i++)
                                                {
                                                    m_curveStatic.m_curveInfo.m_valList[i] = ValueTrans.JudgeRange(s_comconfStatic.m_signalList[i].MRealValue);
                                                }
                                                m_curveStatic.AddItem(m_curveStatic.m_curveInfo);
                                                MResultEvent?.Invoke(m_curveStatic.m_curveInfo);

                                                m_methodRun.Run(m_curveStatic.m_T, m_curveStatic.MV);
                                                Thread.Sleep(DlyBase.c_sleep5);
                                            }
                                            else
                                            {
                                                Thread.Sleep(DlyBase.c_sleep1);
                                            }
                                            break;
                                        case EnumWashStatus.Ing:
                                            Thread.Sleep(DlyBase.c_sleep5);
                                            break;
                                        case EnumWashStatus.Over:
                                            m_chromatogramPauseTime = Math.Round(ValueTrans.TimeSpanToMin(DateTime.Now, m_chromatogramStart) - m_chromatogramRunTime, 2);
                                            m_methodRun.m_state = MethodState.Run;
                                            break;
                                    }
                                    break;
                                case MethodState.Pause:
                                    Thread.Sleep(DlyBase.c_sleep5);
                                    break;
                                case MethodState.Stop:
                                    m_methodRun.Stop();
                                    ResultManager manager = new ResultManager();
                                    manager.UpdateEndTime();
                                    m_systemState = SystemState.Stop;
                                    break;
                                case MethodState.FreeToRun:
                                    m_methodRun.FreeToRun();
                                    break;
                                case MethodState.RunToNext:
                                    m_methodRun.RunToNext();
                                    break;
                                case MethodState.RunToPause:
                                    m_methodRun.RunToPause();
                                    break;
                                case MethodState.PauseToRun:
                                    s_pauseFlag = false;
                                    m_chromatogramPauseTime = Math.Round(ValueTrans.TimeSpanToMin(DateTime.Now, m_chromatogramStart) - m_chromatogramRunTime, 2);
                                    m_methodRun.PauseToRun();
                                    break;
                                case MethodState.BreakToPause:
                                    m_methodRun.BreakToPause(m_chromatogramRunTime, 0);
                                    break;
                            }
                            break;
                        case SystemState.FreeToManual:
                            m_chromatogramStart = DateTime.Now;
                            m_chromatogramPauseTime = 0;
                            m_manualRun.m_state = ManualState.FreeToRun;
                            m_systemState = SystemState.Manual;
                            break;
                        case SystemState.FreeToMethod:
                            m_chromatogramStart = DateTime.Now;
                            m_chromatogramPauseTime = 0;
                            m_methodRun.m_state = MethodState.FreeToRun;
                            m_systemState = SystemState.Method;
                            break;
                        case SystemState.FreeToManualBreak:
                            m_chromatogramStart = DateTime.Now.AddMinutes(-m_chromatogramRunTime);
                            m_manualRun.m_state = ManualState.BreakToPause;
                            m_systemState = SystemState.Manual;
                            break;
                        case SystemState.FreeToMethodBreak:
                            m_chromatogramStart = DateTime.Now.AddMinutes(-m_chromatogramRunTime);
                            m_methodRun.m_state = MethodState.BreakToPause;
                            m_systemState = SystemState.Method;
                            break;
                        case SystemState.Stop:
                            s_pauseFlag = false;
                            m_systemState = SystemState.Free;
                            AuditTrailsStatic.Instance().UpdateRowBatch();
                            StaticAlarmWarning.Reset();
                            s_comconfStatic.SetMixer(ENUMMixerName.Mixer01, false);
                            if (StaticSystemConfig.SSystemConfig.MConfOther.MResetValve)
                            {
                                s_comconfStatic.ResetValve();
                            }
                            if (StaticSystemConfig.SSystemConfig.MConfOther.MCloseUV)
                            {
                                s_comconfStatic.SetUVLamp(ENUMUVName.UV01, false);
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    SystemLog.SystemLogManager.LogWrite(ex);
                }
            }
        }


        /// <summary>
        /// 从数据库获取语言并设置
        /// </summary>
        private static void GetConfCheckable(ConfCheckable item)
        {
            ConfCheckableTable table = new ConfCheckableTable();
            table.GetRow(item);

            SetLanguage(item.MEnumLanguage);
            WindowSize.WindowSizeManager.s_RememberSize = item.MRememberSize;
        }

        /// <summary>
        /// 读各单元显隐
        /// </summary>
        /// <param name="item"></param>
        private static void GetViewVisibility(ViewVisibility item)
        {
            ViewVisibilityTable table = new ViewVisibilityTable();
            table.GetRow(item);
        }

        /// <summary>
        /// 写语言并设置
        /// </summary>
        /// <param name="language"></param>
        public static void SetLanguage(EnumLanguage language)
        {
            switch (language)
            {
                case EnumLanguage.Chinese:
                    LanguageHelper.LoadLanguageFile("/Bio-LabChrom;component/Dictionary/zh-cn.xaml");
                    break;
                case EnumLanguage.English:
                    LanguageHelper.LoadLanguageFile("/Bio-LabChrom;component/Dictionary/en-us.xaml");
                    break;
            }
        }

        /// <summary>
        /// 设置语言
        /// </summary>
        public static void SetConfCheckable(ConfCheckable item)
        {
            ConfCheckableTable table = new ConfCheckableTable();
            table.UpdateRow(item);
        }

        /// <summary>
        /// 写各单元显隐
        /// </summary>
        /// <param name="item"></param>
        public static void SetViewVisibility(ViewVisibility item)
        {
            ViewVisibilityTable table = new ViewVisibilityTable();
            table.UpdateRow(item);
        }


        /// <summary>
        /// 自动备份的线程函数
        /// </summary>
        private static void ThreadDBAutoBackupFun()
        {
            DBManager manager = new DBManager();
            DBAutoBackupInfo info = null;
            if (null == manager.GetDBAutoBackup(out info))
            {
                while (true)
                {
                    try
                    {
                        if (info.MEnabled)
                        {
                            #region 本地
                            if (info.MLocal)
                            {
                                DirectoryInfo dir = new DirectoryInfo(info.MPathLocal);
                                DirectoryInfo[] arrDir = dir.GetDirectories("*Auto");
                                List<DateTime> listDate = new List<DateTime>();
                                foreach (var it in arrDir)
                                {
                                    listDate.Add(DateTime.ParseExact(it.Name.Replace("Auto", ""), "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture));
                                }
                                if (0 == arrDir.Length)
                                {
                                    if (SystemState.Free == MSystemState)
                                    {
                                        DBBackupLocal(info.MPathLocal);
                                    }
                                }
                                else
                                {
                                    while (listDate.Count > info.MCount)
                                    {
                                        Directory.Delete(info.MPathLocal + "/" + listDate.Min().ToString("yyyyMMddHHmmss") + "Auto", true);
                                        listDate.Remove(listDate.Min());
                                    }

                                    DateTime max = listDate.Max();
                                    if ((DateTime.Now - max).TotalDays > info.MFrequency)
                                    {
                                        if (SystemState.Free == MSystemState)
                                        {
                                            DBBackupLocal(info.MPathLocal);
                                        }
                                    }
                                }
                            }
                            #endregion

                            #region 远程
                            if (info.MRemote)
                            {
                                DirectoryInfo dir = new DirectoryInfo("\\\\" + info.MIP + "\\" + info.MPathRemote);
                                if (dir.Exists)
                                {
                                    DirectoryInfo[] arrDir = dir.GetDirectories("*Auto");
                                    List<DateTime> listDate = new List<DateTime>();
                                    foreach (var it in arrDir)
                                    {
                                        listDate.Add(DateTime.ParseExact(it.Name.Replace("Auto", ""), "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture));
                                    }
                                    if (0 == arrDir.Length)
                                    {
                                        if (SystemState.Free == MSystemState)
                                        {
                                            UpdateDBBackupServer(info.MIP, info.MUserName, info.MPwd, info.MPathRemote);
                                        }
                                    }
                                    else
                                    {
                                        BaseDB db = new BaseDB();
                                        while (listDate.Count > info.MCount)
                                        {
                                            db.RemoveBackupDirectory(listDate.Min().ToString("yyyyMMddHHmmss"), info.MIP, info.MUserName, info.MPwd, info.MPathRemote);
                                            listDate.Remove(listDate.Min());
                                        }

                                        DateTime max = listDate.Max();
                                        if ((DateTime.Now - max).TotalDays > info.MFrequency)
                                        {
                                            if (SystemState.Free == MSystemState)
                                            {
                                                UpdateDBBackupServer(info.MIP, info.MUserName, info.MPwd, info.MPathRemote);
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                    catch (Exception ex)
                    {
                        SystemLog.SystemLogManager.LogWrite(ex);
                    }
                    finally
                    {
                        Thread.Sleep(DlyBase.c_sleep600);
                    }
                }
            }  
        }

        /// <summary>
        /// 自动备份到本地
        /// </summary>
        /// <param name="localPath"></param>
        private static void DBBackupLocal(string localPath)
        {
            s_autoDBIng = true;
            string notime = DateTime.Now.ToString("yyyyMMddHHmmss");

            string path = localPath + "/" + notime + "Auto";
            Directory.CreateDirectory(path);

            BaseDB db = new BaseDB();
            if (db.BackupLocal(notime, path))
            {
                AuditTrailsStatic.Instance().InsertRowOperate(ReadXamlDatabase.GetResources(ReadXamlDatabase.C_Local) + ReadXamlDatabase.GetResources(ReadXamlDatabase.C_AutoBackupSuccess), path);
            }
            else
            {
                AuditTrailsStatic.Instance().InsertRowError(ReadXamlDatabase.GetResources(ReadXamlDatabase.C_Local) + ReadXamlDatabase.GetResources(ReadXamlDatabase.C_AutoBackupFail), path);
            }
            s_autoDBIng = false;
        }

        /// <summary>
        /// 自动备份到远程
        /// </summary>
        /// <param name="remoteIP"></param>
        /// <param name="remoteName"></param>
        /// <param name="remotePwd"></param>
        /// <param name="remotePath"></param>
        private static void UpdateDBBackupServer(string remoteIP, string remoteName, string remotePwd, string remotePath)
        {
            s_autoDBIng = true;
            string notime = DateTime.Now.ToString("yyyyMMddHHmmss");

            string path = remotePath + "/" + notime + "Auto";
            BaseDB db = new BaseDB();
            db.CreateBackupDirectory(notime, remoteIP, remoteName, remotePwd, remotePath);

            if (db.BackupServer(notime, remoteIP, remoteName, remotePwd, path))
            {
                AuditTrailsStatic.Instance().InsertRowOperate(ReadXamlDatabase.GetResources(ReadXamlDatabase.C_Remote) + ReadXamlDatabase.GetResources(ReadXamlDatabase.C_AutoBackupSuccess), path);
            }
            else
            {
                AuditTrailsStatic.Instance().InsertRowError(ReadXamlDatabase.GetResources(ReadXamlDatabase.C_Remote) + ReadXamlDatabase.GetResources(ReadXamlDatabase.C_AutoBackupFail), path);
            }
            s_autoDBIng = false;
        }
    }

    /// <summary>
    /// 系统运行状态
    /// </summary>
    public enum SystemState
    {
        Free,       	//空闲
        Wash,           //清洗
        Manual,         //手动
        Method,         //自动
        FreeToManual,
        FreeToMethod,
        FreeToManualBreak,
        FreeToMethodBreak,
        Stop
    }

    /// <summary>
    /// 谱图状态枚举
    /// </summary>
    public enum PtState
    {
        Free,       	//空闲
        Run             //运行
    }
}
