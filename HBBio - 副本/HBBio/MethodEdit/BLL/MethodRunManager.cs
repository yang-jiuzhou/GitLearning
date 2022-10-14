using HBBio.Collection;
using HBBio.Communication;
using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio.MethodEdit
{
    /**
     * ClassName: MethodRunManager
     * Description: 方法运行管理
     * Version: 1.0
     * Create:  2020/08/31
     * Author:  wangkai
     * Company: jshanbon
     **/
    public class MethodRunManager
    {
        private static MethodRunManager s_instance = null;

        private ComConfStatic m_comconfStatic = null;           //传入参数，用于给通讯仪器赋值

        private double m_T = 0;                                 //设置谱图累计运行时间(外部赋值)
        private double m_V = 0;                                 //设置谱图累计运行体积(外部赋值)

        public MethodState m_state = MethodState.Free;          //当前方法运行状态

        private MethodType m_methodType = null;                 //区分方法或者队列
        public MethodType MMethodType
        {
            get
            {
                return m_methodType;
            }
        }

        private MethodQueue m_methodQueue = null;               //当前运行的方法队列信息
        public MethodQueue MMethodQueue
        {
            get
            {
                return m_methodQueue;
            }
        }

        private Method m_method = null;                         //当前运行的方法信息
        public Method MMethod
        {
            get
            {
                return m_method;
            }
        }

        private int m_indexCurrMethod = -1;                     //当前执行方法序号(-1表示空闲状态)
        public int MIndexCurrMethod
        {
            get
            {
                return m_indexCurrMethod;
            }
        }
        private MRUN m_runMethod = MRUN.No;

        private int m_indexCurrLoop = -1;                      //当前执行方法循环(-1表示空闲状态)
        public int MIndexCurrLoop
        {
            get
            {
                return m_indexCurrLoop;
            }
        }

        private int m_indexCurrPhase = -1;                      //当前执行阶段序号(-1表示空闲状态)
        public int MIndexCurrPhase
        {
            get
            {
                return m_indexCurrPhase;
            }
        }
        private MRUN m_run = MRUN.No;                           //当前执行阶段状态
        private double m_phaseStartT = 0;                       //当前执行阶段的开始时间点
        private double m_phaseStartV = 0;                       //当前执行阶段的开始体积点
        private double m_phaseStopT = 0;                        //当前执行阶段的结束时间点
        private double m_phaseStopV = 0;                        //当前执行阶段的结束体积点
        private double m_phaseRunT = 0;                         //当前执行阶段的理论运行时间累计(不包含挂起)
        private double m_phaseRunV = 0;                         //当前执行阶段的理论运行体积累计(不包含挂起)
        private double[] m_phaseRunTVCV = new double[3];
        private double[] MPhaseRunTVCV
        {
            get
            {
                m_phaseRunTVCV[0] = m_phaseRunT;
                m_phaseRunTVCV[1] = m_phaseRunV;
                m_phaseRunTVCV[2] = m_phaseRunV / MMethod.MMethodSetting.MColumnVol;
                return m_phaseRunTVCV;
            }
        }
        public double MPhaseRunTime
        {
            get
            {
                return m_phaseRunT;
            }
        }
        public double MHoldRunTime
        {
            get
            {
                return Math.Round(m_T - m_phaseStartT, 2);
            }
        }                           //当前执行阶段的实际运行累计（包含挂起）
        public double MHoldRunV
        {
            get
            {
                return Math.Round(m_V - m_phaseStartV, 2);
            }
        }                              //当前执行阶段的实际运行累计（包含挂起）
        private double m_holdStartT = 0;                        //当前执行阶段的一次挂起时间点
        private double m_holdStartV = 0;                        //当前执行阶段的一次挂起体积点
        private double m_holdTotalT = 0;                        //当前执行阶段的一次挂起时间累计
        private double m_holdTotalV = 0;                        //当前执行阶段的一次挂起体积累计
        private bool m_isHold = false;                          //当前执行阶段是否挂起
        public bool MIsHold
        {
            get
            {
                return m_isHold;
            }
            set
            {
                if (m_isHold == value)
                {
                    return;
                }

                m_isHold = value;

                if (m_isHold)
                {
                    m_holdStartT = m_T;
                    m_holdStartV = m_V;
                    WriteDataToDB();
                }
                else
                {
                    m_holdTotalT += (m_T - m_holdStartT);
                    m_holdTotalV += (m_V - m_holdStartV);
                    m_phaseStopT += (m_T - m_holdStartT);
                    m_phaseStopV += (m_V - m_holdStartV);
                    WriteDataToDB();
                }
            }
        }

        private bool m_isBreak = false;
        public bool MIsBreak
        {
            get
            {
                return m_isBreak;
            }
        }

        public MethodTempValue m_methodTempValue = new MethodTempValue();
        public CollectionValve MCollectionValve { get; set; }
        public CollectionCollector MCollectionCollector { get; set; }
        private List<bool> m_listRun = new List<bool>();
        private double m_listDis = 0;


        //创建一个自定义委托，用于审计跟踪
        public delegate void MAuditTrailsDdelegate(object sender1, object sender2);
        //声明一个审计跟踪事件
        public MAuditTrailsDdelegate MAuditTrailsHandler;

        //创建一个自定义委托，用于自定义的信号
        public delegate void MHandlerDdelegate(object sender);
        //声明一个新建标记事件
        public MHandlerDdelegate MMarkerHandler;
        //声明一个暂停事件
        public MHandlerDdelegate MPauseHandler;
        //声明一个弹窗事件
        public MHandlerDdelegate MShowMessageHandler;
        //声明一个重新计时事件
        public MHandlerDdelegate MMethodBeginHandler;
        //声明一个泵洗事件
        public MHandlerDdelegate MWashHandler;


        /// <summary>
        /// 私有构造函数
        /// </summary>
        private MethodRunManager(ComConfStatic comconfStatic)
        {
            m_comconfStatic = comconfStatic;

            ReadDataFromDB();
        }

        /// <summary>
        /// 单例模式
        /// </summary>
        /// <returns></returns>
        public static MethodRunManager Instance(ComConfStatic comconfStatic)
        {
            if (null == s_instance)
            {
                s_instance = new MethodRunManager(comconfStatic);
            }

            return s_instance;
        }

        /// <summary>
        /// 发送方法或方法队列
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string SendMethodOrQueue(MethodType type)
        {
            if (MethodState.Free != m_state)
            {
                return ReadXaml.GetResources("ME_Desc_Run");
            }

            m_methodType = type;

            MethodManager manager = new MethodManager();
            switch (m_methodType.MType)
            {
                case EnumMethodType.MethodQueue:
                    string error = manager.GetMethodQueue(m_methodType.MID, out m_methodQueue);
                    if (null == error)
                    {
                        return manager.GetMethod(MMethodQueue.MMethodList[0], out m_method);
                    }
                    else
                    {
                        return error;
                    }
                default:
                    return manager.GetMethod(m_methodType.MID, out m_method);
            }
        }

        /// <summary>
        /// 赋值显示信息
        /// </summary>
        /// <param name="methodQueueName"></param>
        /// <param name="methodCount"></param>
        /// <param name="methodName"></param>
        /// <param name="phaseCount"></param>
        /// <param name="phaseName"></param>
        /// <param name="phaseRunning"></param>
        public void GetInfo(ref string methodQueueName, ref string methodCount, ref string methodName,
            ref string loopCount, ref string loopIndex,
            ref string phaseCount, ref string phaseName, ref string phaseRunning)
        {
            if (null != m_methodType)
            {
                try
                {
                    switch (m_methodType.MType)
                    {
                        case EnumMethodType.MethodQueue:
                            methodQueueName = MMethodQueue.MName;
                            methodCount = MMethodQueue.MMethodList.Count.ToString();
                            if (-1 == MIndexCurrMethod)
                            {
                                methodName = "";
                                loopCount = "";
                                loopIndex = "";
                                phaseCount = "";
                                phaseName = "";
                                phaseRunning = "";
                            }
                            else
                            {
                                methodName = MMethod.MName + "(" + (MIndexCurrMethod + 1) + ")";
                                loopCount = MMethod.MMethodSetting.MLoop.ToString();
                                loopIndex = (MIndexCurrLoop + 1).ToString();
                                phaseCount = MMethod.MPhaseList.Count.ToString();
                                if (-1 == MIndexCurrPhase || MIndexCurrPhase == MMethod.MPhaseList.Count)
                                {
                                    phaseName = "";
                                    phaseRunning = "";
                                }
                                else
                                {
                                    phaseName = MMethod.MPhaseList[MIndexCurrPhase].MNamePhase + "(" + (MIndexCurrPhase + 1) + ")";

                                    if (MHoldRunTime > MPhaseRunTime)
                                    {
                                        phaseRunning = MHoldRunTime.ToString() + "(" + MPhaseRunTime.ToString() + ")";
                                    }
                                    else
                                    {
                                        phaseRunning = MPhaseRunTime.ToString();
                                    }
                                }
                            }
                            break;
                        default:
                            methodQueueName = "";
                            methodCount = "";
                            methodName = MMethod.MName;
                            loopCount = MMethod.MMethodSetting.MLoop.ToString();
                            loopIndex = (MIndexCurrLoop + 1).ToString();
                            phaseCount = MMethod.MPhaseList.Count.ToString();
                            if (-1 == MIndexCurrPhase || MIndexCurrPhase == MMethod.MPhaseList.Count)
                            {
                                phaseName = "";
                                phaseRunning = "";
                            }
                            else
                            {
                                phaseName = MMethod.MPhaseList[MIndexCurrPhase].MNamePhase + "(" + (MIndexCurrPhase + 1) + ")";

                                if (MHoldRunTime > MPhaseRunTime)
                                {
                                    phaseRunning = MHoldRunTime.ToString() + "(" + MPhaseRunTime.ToString() + ")";
                                }
                                else
                                {
                                    phaseRunning = MPhaseRunTime.ToString();
                                }
                            }
                            break;
                    }
                }
                catch { }
            }
        }

        /*-------------方法运行状态机 开始-------------------*/

        /// <summary>
        /// 空闲->运行
        /// </summary>
        public void FreeToRun()
        {
            switch (m_methodType.MType)
            {
                case EnumMethodType.MethodQueue:
                    m_indexCurrMethod = 0;
                    m_runMethod = MRUN.No;
                    break;
                default:
                    m_indexCurrLoop = 0;
                    m_indexCurrPhase = 0;
                    m_run = MRUN.No;
                    m_phaseStopT = 0;
                    m_phaseStopV = 0;
                    //在此执行方法设置中的内容
                    RefreshMethodSetting(MMethod.MMethodSetting);
                    break;
            }

            m_state = MethodState.Run;
        }

        /// <summary>
        /// 运行
        /// </summary>
        public bool Run(double time, double vol)
        {
            bool result = false;

            m_T = time;
            m_V = vol;

            switch (m_methodType.MType)
            {
                case EnumMethodType.MethodQueue:
                    //运行队列
                    if (m_indexCurrMethod < MMethodQueue.MMethodList.Count)
                    {
                        result = RefreshMethod();
                    }
                    //运行结束
                    if (m_indexCurrMethod == MMethodQueue.MMethodList.Count)
                    {
                        m_state = MethodState.Stop;
                    }
                    else
                    {
                        result = result || RefreshMethod();
                    }
                    break;
                default:
                    //运行方法
                    if (m_indexCurrLoop < MMethod.MMethodSetting.MLoop)
                    {
                        if (m_indexCurrPhase < MMethod.MPhaseList.Count)
                        {
                            RefreshPhase();
                        }
                        //单次结束
                        if (m_indexCurrPhase == MMethod.MPhaseList.Count)
                        {
                            m_indexCurrLoop++;
                            m_indexCurrPhase = 0;
                        }
                    }
                    //运行结束
                    if (m_indexCurrLoop == MMethod.MMethodSetting.MLoop)
                    {
                        m_state = MethodState.Stop;
                    }
                    else
                    {
                        RefreshPhase();
                    }
                    break;
            }

            return result;
        }

        /// <summary>
        /// 运行->暂停
        /// </summary>
        public void RunToPause()
        {
            m_state = MethodState.Pause;

            m_comconfStatic.SetPumpPause(true);
        }

        /// <summary>
        /// 跳行
        /// </summary>
        public void RunToNext()
        {
            m_comconfStatic.SetPumpSample(0);
            m_methodTempValue.MFlowSample = 0;

            if (MRUN.Ing == m_run)
            {
                if (m_indexCurrPhase < MMethod.MPhaseList.Count - 1)
                {
                    //下一阶段，设置为执行
                    m_indexCurrPhase++;
                    m_run = MRUN.No;
                    m_phaseStopT = m_phaseStartT + MHoldRunTime;
                    m_phaseStopV = m_phaseStartV + MHoldRunV;
                }
                else
                {
                    //最后一个阶段，直接结束
                    m_run = MRUN.Done;
                }
            }

            m_state = MethodState.Run;
        }

        /// <summary>
        /// 暂停->运行
        /// </summary>
        public void PauseToRun()
        {
            m_state = MethodState.Run;

            m_comconfStatic.SetPumpPause(false);
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            m_state = MethodState.Free;

            switch (m_methodType.MType)
            {
                case EnumMethodType.MethodQueue:
                    m_indexCurrMethod = -1;
                    m_method = null;
                    MAuditTrailsHandler?.Invoke(Share.ReadXaml.GetResources("ME_Msg_Queue_Stop"), "N/A");
                    break;
                default:
                    MAuditTrailsHandler?.Invoke(Share.ReadXaml.GetResources("ME_Msg_Stop"), "N/A");
                    break;
            }
            m_indexCurrMethod = -1;
            m_indexCurrLoop = -1;
            m_indexCurrPhase = -1;
            m_phaseStartT = 0;
            m_phaseStartV = 0;
            m_phaseStopT = 0;
            m_phaseStopV = 0;
            ClearDataToDB();

            m_comconfStatic.SetPumpPause(false);
            RefreshStopSABCD();

            CollectorItem item = m_comconfStatic.GetItem(ENUMCollectorName.Collector01);
            if (null != item)
            {
                item.MStatusSet = false;
            }

            m_methodType = null;
        }

        /// <summary>
        /// 中断->暂停
        /// </summary>
        public void BreakToPause(double time, double vol)
        {
            m_T = time;
            m_V = vol;
            if (!m_isHold)
            {
                m_phaseRunT = Math.Round(m_T - m_phaseStartT - m_holdTotalT, 2);
                m_phaseRunV = Math.Round(m_V - m_phaseStartV - m_holdTotalV, 2);
            }
            m_state = MethodState.Pause;

            m_comconfStatic.SetPumpPause(true);
        }


        /// <summary>
        /// 执行队列的方法
        /// </summary>
        private bool RefreshMethod()
        {
            if (MRUN.No == m_runMethod)//第一次运行该方法
            {
                //获取方法
                MethodManager manager = new MethodManager();
                manager.GetMethod(MMethodQueue.MMethodList[m_indexCurrMethod], out m_method);

                if (!MMethodQueue.MOnly && 0 < m_indexCurrMethod)
                {
                    m_phaseStartT = 0;
                    m_phaseStartV = 0;
                    m_phaseStopT = 0;
                    m_phaseStopV = 0;
                }

                m_indexCurrLoop = 0;
                m_indexCurrPhase = 0;
                m_run = MRUN.No;
                //在此执行方法设置中的内容
                RefreshMethodSetting(MMethod.MMethodSetting);

                m_runMethod = MRUN.Ing;
            }
            else if (MRUN.Ing == m_runMethod)
            {
                if (m_indexCurrLoop < MMethod.MMethodSetting.MLoop)
                {
                    if (m_indexCurrPhase < MMethod.MPhaseList.Count)
                    {
                        RefreshPhase();
                    }

                    if (m_indexCurrPhase == MMethod.MPhaseList.Count)
                    {
                        m_indexCurrLoop++;
                        m_indexCurrPhase = 0;
                    }
                }

                if (m_indexCurrLoop == MMethod.MMethodSetting.MLoop)
                {
                    if (!MMethodQueue.MOnly && m_indexCurrMethod < MMethodQueue.MMethodList.Count - 1)
                    {
                        MMethodBeginHandler?.Invoke(m_indexCurrMethod + 2);
                    }

                    m_indexCurrMethod++;
                    m_runMethod = MRUN.No;

                    return true;
                }
                else
                {
                    RefreshPhase();
                }
            }
            else
            {
                if (!MMethodQueue.MOnly && m_indexCurrMethod < MMethodQueue.MMethodList.Count - 1)
                {
                    MMethodBeginHandler?.Invoke(m_indexCurrMethod + 2);
                }

                m_indexCurrMethod++;
                m_runMethod = MRUN.No;

                return true;
            }

            return false;
        }

        /// <summary>
        /// 执行方法设置
        /// </summary>
        /// <param name="ms"></param>
        private void RefreshMethodSetting(MethodSettings ms)
        {
            m_methodTempValue.Clear();

            StaticValue.SVolToLen = 60.0 / ms.MColumnArea;
            SwitchValve(ENUMValveName.CPV_1, ms.MCPV);

            RefreshShareSystemFlowVol(ms.MFlowVol);

            RefreshShareIn(ms.MInA, ms.MInB, ms.MInC, ms.MInD, ms.MBPV);

            RefreshShareUV(ms.MUVValue);

            StaticAlarmWarning.SAlarmWarning = ms.MAlarmWarning;
        }

        /// <summary>
        /// 执行方法阶段
        /// </summary>
        private void RefreshPhase()
        {
            if (MRUN.No == m_run)//第一次运行该阶段
            {
                m_phaseStartT = m_phaseStopT;
                m_phaseStartV = m_phaseStopV;
                foreach (var it in MMethod.MPhaseList[m_indexCurrPhase].MStepT)
                {
                    m_phaseStopT += it;
                }
                foreach (var it in MMethod.MPhaseList[m_indexCurrPhase].MStepV)
                {
                    m_phaseStopV += it;
                }
                m_phaseStopT = Math.Round(m_phaseStopT, 2);
                m_phaseStopV = Math.Round(m_phaseStopV, 2);
                m_phaseRunT = 0;
                m_phaseRunV = 0;
                m_holdTotalT = 0;
                m_holdTotalV = 0;
                m_isHold = false;

                MAuditTrailsHandler?.Invoke(ReadXamlMethod.C_PhaseName, MMethod.MPhaseList[m_indexCurrPhase].MNamePhase);

                //执行详细的阶段内容
                RefreshPhaseCase();

                foreach (var it in MMethod.MMethodSetting.MASParaList)
                {
                    if (EnumMonitorActionMethod.Ignore != it.MAction)
                    {
                        it.m_update = true;
                    }
                }

                //强制排废
                ForceWaste();

                WriteDataToDB();
            }

            if (MRUN.Ing == m_run)
            {
                if (!m_isHold)
                {
                    m_phaseRunT = Math.Round(m_T - m_phaseStartT - m_holdTotalT, 2);
                    m_phaseRunV = Math.Round(m_V - m_phaseStartV - m_holdTotalV, 2);
                }

                if (m_methodTempValue.MChange)
                {
                    m_methodTempValue.MChange = false;
                    m_comconfStatic.SetPumpSystem(m_methodTempValue.MFlow, m_methodTempValue.MPerB, m_methodTempValue.MPerC, m_methodTempValue.MPerD);
                }

                //执行详细的阶段内容
                RefreshPhaseCase();

                //监控-AS
                JudgeAS();

                if (MRUN.Done == m_run)
                {
                    foreach (var it in MMethod.MMethodSetting.MASParaList)
                    {
                        it.Clear();
                    }
                    m_indexCurrPhase++;
                    m_run = MRUN.No;
                }
            }
            else
            {
                foreach (var it in MMethod.MMethodSetting.MASParaList)
                {
                    it.Clear();
                }
                m_indexCurrPhase++;
                m_run = MRUN.No;
            }
        }

        /// <summary>
        /// 阀、收集器强制排废
        /// </summary>
        private void ForceWaste()
        {
            //强制出口阀排废
            if (0 != m_comconfStatic.GetValveSet(ENUMValveName.Out))
            {
                m_comconfStatic.SetValve(ENUMValveName.Out, 0);
                MAuditTrailsHandler?.Invoke(ReadXamlCollection.C_CollMarkA, "WASTE");
            }

            //强制收集器排废
            CollectorItem item = m_comconfStatic.GetItem(ENUMCollectorName.Collector01);
            if (null != item)
            {
                if (item.MStatusSet)
                {
                    item.MStatusSet = false;
                    MAuditTrailsHandler?.Invoke(ReadXamlCollection.C_CollMarkA, "WASTE");
                }
            }
        }

        /// <summary>
        /// 执行气泡传感器的判断
        /// </summary>
        private void JudgeAS()
        {
            int index = 0;
            int indexBPV = m_comconfStatic.GetValveSet(ENUMValveName.BPV);
            foreach (var it in MMethod.MMethodSetting.MASParaList)
            {
                if (it.m_update)
                {
                    it.m_update = false;

                    it.Init();
                }

                if (it.m_signal)
                {
                    if (m_comconfStatic.GetASGet((ENUMASName)index))
                    {
                        switch (it.MAction)
                        {
                            case EnumMonitorActionMethod.Bypass:
                                switch (it.JudgeASYes(MHoldRunTime, MHoldRunV, MHoldRunV / MMethod.MMethodSetting.MColumnVol, indexBPV))
                                {
                                    case 1:
                                        {
                                            MAuditTrailsHandler?.Invoke(it.MHeader + Share.ReadXaml.S_InfoASYes, Share.ReadXaml.GetEnum(it.MAction, "EnumMonitorAction_"));
                                        }
                                        break;
                                    case 2:
                                        {
                                            MAuditTrailsHandler?.Invoke(it.MHeader + Share.ReadXaml.S_InfoASYes, Share.ReadXaml.GetEnum(it.MAction, "EnumMonitorAction_") + " " + EnumBPVInfo.NameList[indexBPV] + " -> " + EnumBPVInfo.NameList[0]);
                                            indexBPV = 0;
                                            m_comconfStatic.SetValve(ENUMValveName.BPV, 0);
                                        }
                                        break;
                                }
                                break;
                            case EnumMonitorActionMethod.Next:
                                MAuditTrailsHandler?.Invoke(((ENUMASName)index).ToString() + Share.ReadXaml.S_InfoASYes, Share.ReadXaml.GetEnum(it.MAction, "EnumMonitorAction_"));
                                m_run = MRUN.Done;
                                break;
                            case EnumMonitorActionMethod.Pause:
                                m_state = MethodState.RunToPause;
                                MAuditTrailsHandler?.Invoke(((ENUMASName)index).ToString() + Share.ReadXaml.S_InfoASYes, Share.ReadXaml.GetEnum(it.MAction, "EnumMonitorAction_"));
                                break;
                            case EnumMonitorActionMethod.Stop:
                                m_state = MethodState.Stop;
                                MAuditTrailsHandler?.Invoke(((ENUMASName)index).ToString() + Share.ReadXaml.S_InfoASYes, Share.ReadXaml.GetEnum(it.MAction, "EnumMonitorAction_"));
                                it.m_signal = false;
                                break;
                        }
                    }
                    else
                    {
                        switch (it.MAction)
                        {
                            case EnumMonitorActionMethod.Bypass:
                                switch (it.JudgeASNo(MHoldRunTime, MHoldRunV, MHoldRunV / MMethod.MMethodSetting.MColumnVol, ref indexBPV))
                                {
                                    case 1:
                                        MAuditTrailsHandler?.Invoke(((ENUMASName)index).ToString() + Share.ReadXaml.S_InfoASNo, Share.ReadXaml.GetEnum(it.MAction, "EnumMonitorAction_"));
                                        break;
                                    case 2:
                                        m_comconfStatic.SetValve(ENUMValveName.BPV, indexBPV);
                                        MAuditTrailsHandler?.Invoke(((ENUMASName)index).ToString() + Share.ReadXaml.S_InfoASNo, Share.ReadXaml.GetEnum(it.MAction, "EnumMonitorAction_") + " " + EnumBPVInfo.NameList[0] + " -> " + EnumBPVInfo.NameList[indexBPV]);
                                        break;
                                }
                                break;
                        }
                    }
                }

                index++;
            }
        }

        /// <summary>
        /// 执行具体阶段
        /// </summary>
        private void RefreshPhaseCase()
        {
            switch (MMethod.MPhaseList[m_indexCurrPhase].MType)
            {
                case EnumPhaseType.Miscellaneous:
                    RefreshMiscellaneous((Miscellaneous)MMethod.MPhaseList[m_indexCurrPhase]);
                    break;
                default:
                    RefreshNoIngPhase((DlyPhase)MMethod.MPhaseList[m_indexCurrPhase]);
                    break;
            }
        }

        /// <summary>
        /// 执行方法阶段-其它
        /// </summary>
        /// <param name="phase"></param>
        private void RefreshMiscellaneous(Miscellaneous phase)
        {
            if (MRUN.No == m_run)//第一次运行该序列行
            {
                m_run = MRUN.Ing;

                if (phase.MEnableSetMark)
                {
                    //添加标记事件
                    MMarkerHandler?.Invoke(phase.MSetMark);
                }
            }
            else if (MRUN.Ing == m_run)//正在运行该序列行
            {
                if (m_isHold)
                {

                }
                else
                {
                    //先判断是否需要延迟
                    bool enableMethodDelayFlag = false;
                    if (phase.MEnableMethodDelay)
                    {
                        if (m_phaseRunT < phase.MMethodDelay.MT)
                        {
                            enableMethodDelayFlag = false;
                        }
                        else
                        {
                            enableMethodDelayFlag = true;
                        }
                    }
                    else
                    {
                        enableMethodDelayFlag = true;
                    }

                    //延迟完成之后，再判断是否弹窗
                    bool enableMessageFlag = false;
                    if (enableMethodDelayFlag)
                    {
                        if (phase.MEnableMessage)
                        {
                            //弹窗事件
                            MShowMessageHandler?.Invoke(phase.MMessage);

                            if (phase.MEnablePauseAfterMessage)
                            {
                                m_state = MethodState.RunToPause;
                            }
                        }

                        enableMessageFlag = true;
                    }

                    //弹窗之后，如果没有暂停，再判断暂停
                    bool enablePauseTimerFlag = false;
                    if (enableMessageFlag)
                    {
                        if (phase.MEnablePauseTimer)
                        {
                            m_state = MethodState.RunToPause;
                            //暂停自动结束事件
                            MPauseHandler?.Invoke(phase.MPauseTimer);
                        }

                        enablePauseTimerFlag = true;
                    }

                    if (enablePauseTimerFlag)
                    {
                        m_run = MRUN.Done;
                    }
                }
            }
        }

        /// <summary>
        /// 执行方法阶段-通用
        /// </summary>
        /// <param name="phase"></param>
        private void RefreshNoIngPhase(DlyPhase phase)
        {
            if (MRUN.No == m_run)//第一次运行该序列行
            {
                m_run = MRUN.Ing;
                m_listRun.Clear();
                m_listDis = 0;

                m_comconfStatic.SetMixer(ENUMMixerName.Mixer01, false);
                MCollectionValve = null;
                MCollectionCollector = null;

                foreach (var it in phase.MListGroup)
                {
                    if (-1 != it.MIndex)
                    {
                        m_listRun.Add(false);
                    }

                    switch (it.MType)
                    {
                        case EnumGroupType.FlowRate:
                            RefreshNoFlowRate(it);
                            break;
                        case EnumGroupType.ValveSelection:
                            RefreshNoValveSelection(it);
                            break;
                        case EnumGroupType.SampleApplicationTech:
                            RefreshNoSampleApplicationTech(it);
                            break;
                        case EnumGroupType.Mixer:
                            RefreshNoMixer(it);
                            break;
                        case EnumGroupType.BPV:
                            RefreshNoBPV(it);
                            break;
                        case EnumGroupType.UVReset:
                            RefreshNoUVReset(phase, it);
                            break;
                        case EnumGroupType.FlowValveLength:
                            RefreshNoFlowValveLength(phase, it);
                            break;
                        case EnumGroupType.FlowRatePer:
                            RefreshNoFlowRatePer(phase, it);
                            break;
                        case EnumGroupType.PHCDUVUntil:
                            RefreshNoPHCDUVUntil(it);
                            break;
                        case EnumGroupType.CollValveCollector:
                            RefreshNoCollValveCollector(it);
                            break;
                        case EnumGroupType.CIP:
                            RefreshNoCIP(phase, it);
                            break;
                    }
                }

                if (0 != m_listRun.Count)
                {
                    //从第一个开始执行
                    m_listRun[0] = true;
                }
            }
            else if (MRUN.Ing == m_run)//正在运行该序列行
            {
                if (m_isHold)
                {

                }
                else
                {
                    foreach (var it in phase.MListGroup)
                    {
                        switch (it.MType)
                        {

                            case EnumGroupType.SampleApplicationTech:
                                RefreshIngSampleApplicationTech(it, m_listDis, ref m_run);
                                //if (m_listRun[it.MIndex])
                                //{
                                //    RefreshIngSampleApplicationTech(it, m_listDis, ref m_run);
                                //    if (MRUN.Done == m_run)
                                //    {
                                //        if (it.MIndex < m_listRun.Count - 1)
                                //        {
                                //            m_run = MRUN.Ing;
                                //            m_listRun[it.MIndex] = false;
                                //            m_listRun[it.MIndex + 1] = true;
                                //            m_listDis = m_phaseRunT;
                                //        }
                                //    }
                                //}
                                break;
                            case EnumGroupType.TVCV:
                                RefreshIngTVCV(phase, it, m_listDis, ref m_run);
                                //if (m_listRun[it.MIndex])
                                //{
                                //    RefreshIngTVCV(phase, it, m_listDis, ref m_run);
                                //    if (MRUN.Done == m_run)
                                //    {
                                //        if (it.MIndex < m_listRun.Count - 1)
                                //        {
                                //            m_run = MRUN.Ing;
                                //            m_listRun[it.MIndex] = false;
                                //            m_listRun[it.MIndex + 1] = true;
                                //            m_listDis = m_phaseRunT;
                                //        }
                                //    }
                                //}
                                break;
                            case EnumGroupType.FlowValveLength:
                                RefreshIngFlowValveLength(phase, it, ref m_run);
                                break;
                            case EnumGroupType.FlowRatePer:
                                RefreshIngFlowRatePer(phase, it, ref m_run);
                                break;
                            case EnumGroupType.PHCDUVUntil:
                                RefreshIngPHCDUVUntil(it, ref m_run);
                                break;
                            case EnumGroupType.CIP:
                                RefreshIngCIP(phase, it, ref m_run);
                                break;
                        }
                    }
                }

                foreach (var it in phase.MListGroup)
                {
                    switch (it.MType)
                    {
                        case EnumGroupType.CollValveCollector:
                            RefreshIngCollValveCollector(it, ref m_run);
                            break;
                    }
                }
            }
        }

        private void RefreshNoFlowValveLength(DlyPhase phase, BaseGroup baseGroup)
        {
            FlowValveLength tmp = (FlowValveLength)baseGroup;
            if (null == phase.MArrIsRun || phase.MArrIsRun.Length != tmp.MList.Count)
            {
                phase.MArrIsRun = new bool[tmp.MList.Count];
                phase.MArrIsIncubation = new bool[tmp.MList.Count];
            }
            for (int i = 0; i < phase.MArrIsRun.Length; i++)
            {
                phase.MArrIsRun[i] = false;
                phase.MArrIsIncubation[i] = false;
            }
        }
        private void RefreshIngFlowValveLength(DlyPhase phase, BaseGroup baseGroup, ref MRUN run)
        {
            FlowValveLength tmp = (FlowValveLength)baseGroup;
            double total = 0;
            bool ing = false;//列表是否还在执行
            for (int i = 0; i < tmp.MList.Count; i++)
            {
                if (m_phaseRunT >= total && m_phaseRunT < Math.Round(total + tmp.MList[i].MBaseTVCV.MT, 2))
                {
                    ing = true;

                    FlowValveLengthItem item = tmp.MList[i];
                    if (!phase.MArrIsRun[i])
                    {
                        phase.MArrIsRun[i] = true;

                        RefreshShareIn(item.MInA, item.MInB, item.MInC, item.MInD, item.MBPV);
                        SwitchValve(ENUMValveName.Out, item.MVOut);

                        if (!string.IsNullOrWhiteSpace(item.MNote))
                        {
                            MAuditTrailsHandler?.Invoke(phase.MNamePhase, item.MNote);
                        }

                        switch (item.MFillSystem)
                        {
                            case 1:
                            case 2:
                                MWashHandler?.Invoke(item.MFillSystem);
                                break;
                        }
                    }

                    if (0 != item.MBaseTVCV.MT)
                    {
                        RefreshShareSystemFlowVol(item.MFlowVolLen.MFlowVol,
                            (m_phaseRunT - total) / item.MBaseTVCV.MT * (item.MPerBE - item.MPerBS) + item.MPerBS,
                            (m_phaseRunT - total) / item.MBaseTVCV.MT * (item.MPerCE - item.MPerCS) + item.MPerCS,
                            (m_phaseRunT - total) / item.MBaseTVCV.MT * (item.MPerDE - item.MPerDS) + item.MPerDS);
                    }

                    break;
                }
                total = Math.Round(total + tmp.MList[i].MBaseTVCV.MT, 2);

                if (m_phaseRunT >= total && m_phaseRunT < Math.Round(total + tmp.MList[i].MIncubation, 2))
                {
                    ing = true;

                    if (!phase.MArrIsIncubation[i])
                    {
                        phase.MArrIsIncubation[i] = true;

                        RefreshStopSABCD();
                    }

                    break;
                }
                total = Math.Round(total + tmp.MList[i].MIncubation, 2);
            }

            if (!ing)
            {
                run = MRUN.Done;
            }
        }

        private void RefreshNoFlowRate(BaseGroup baseGroup)
        {
            FlowRate tmp = (FlowRate)baseGroup;
            RefreshShareSystemFlowVol(tmp.MFlowVolLen.MFlowVol);
        }

        private void RefreshNoMixer(BaseGroup baseGroup)
        {
            Mixer tmp = (Mixer)baseGroup;
            m_comconfStatic.SetMixer(ENUMMixerName.Mixer01, tmp.MOnoff);
        }

        private void RefreshNoBPV(BaseGroup baseGroup)
        {
            BPVValve tmp = (BPVValve)baseGroup;
            SwitchValve(ENUMValveName.BPV, tmp.MBPV);
        }

        private void RefreshNoUVReset(DlyPhase phase, BaseGroup baseGroup)
        {
            UVReset tmp = (UVReset)baseGroup;
            if (tmp.MEnableResetUV)
            {
                m_comconfStatic.SetUVClear(ENUMUVName.UV01);
                MAuditTrailsHandler?.Invoke(phase.MNamePhase, ReadXaml.GetResources("ME_ResetUVMonitor"));
            }
        }

        private void RefreshNoSampleApplicationTech(BaseGroup baseGroup)
        {
            SampleApplicationTech tmp = (SampleApplicationTech)baseGroup;
            switch (tmp.MEnumSAT)
            {
                case EnumSAT.ManualLoopFilling:
                    break;
                case EnumSAT.SamplePumpLoopFilling:
                    SwitchValve(ENUMValveName.InS, tmp.MInS);
                    RefreshSample(true, m_methodTempValue.MFlowSystem);
                    break;
                case EnumSAT.ISDOC:
                    SwitchValve(ENUMValveName.InS, tmp.MInS);
                    RefreshSample(true, m_methodTempValue.MFlowSystem);
                    break;
            }
        }
        private void RefreshIngSampleApplicationTech(BaseGroup baseGroup, double dis, ref MRUN run)
        {
            SampleApplicationTech tmp = (SampleApplicationTech)baseGroup;
            switch (tmp.MEnumSAT)
            {
                case EnumSAT.ManualLoopFilling:
                    if ((m_phaseRunT - dis) >= tmp.MEmptyLoopWith / m_methodTempValue.MFlow)
                    {
                        SwitchValve(ENUMValveName.IJV, 0);
                        run = MRUN.Done;
                    }
                    else
                    {
                        SwitchValve(ENUMValveName.IJV, 1);
                    }
                    break;
                case EnumSAT.SamplePumpLoopFilling:
                    if ((m_phaseRunT - dis) >= tmp.MFillLoopWith / m_methodTempValue.MFlow + tmp.MEmptyLoopWith / m_methodTempValue.MFlow)
                    {
                        if (0 == tmp.MEmptyLoopWith)
                        {
                            RefreshSample(false, m_methodTempValue.MFlow);
                        }

                        SwitchValve(ENUMValveName.IJV, 0);
                        run = MRUN.Done;
                    }
                    else if ((m_phaseRunT - dis) >= tmp.MFillLoopWith / m_methodTempValue.MFlow)
                    {
                        SwitchValve(ENUMValveName.IJV, 1);
                        RefreshSample(false, m_methodTempValue.MFlow);
                    }
                    else
                    {
                        SwitchValve(ENUMValveName.IJV, 0);
                    }
                    break;
                case EnumSAT.ISDOC:
                    if ((m_phaseRunT - dis) >= tmp.MSampleTVCV.MT)
                    {
                        RefreshSample(false, m_methodTempValue.MFlow);
                        run = MRUN.Done;
                    }
                    break;
            }
        }

        private void RefreshIngTVCV(DlyPhase phase, BaseGroup baseGroup, double dis, ref MRUN run)
        {
            BaseTVCV tmp = (BaseTVCV)baseGroup;
            if ((m_phaseRunT - dis) >= tmp.MT)
            {
                run = MRUN.Done;
            }
        }

        private void RefreshNoFlowRatePer(DlyPhase phase, BaseGroup baseGroup)
        {
            FlowRatePer tmp = (FlowRatePer)baseGroup;
            if (null == phase.MArrIsRun || phase.MArrIsRun.Length != tmp.MList.Count)
            {
                phase.MArrIsRun = new bool[tmp.MList.Count];
                phase.MArrIsIncubation = new bool[tmp.MList.Count];
            }
            for (int i = 0; i < phase.MArrIsRun.Length; i++)
            {
                phase.MArrIsRun[i] = false;
                phase.MArrIsIncubation[i] = false;
            }
        }
        private void RefreshIngFlowRatePer(DlyPhase phase, BaseGroup baseGroup, ref MRUN run)
        {
            FlowRatePer tmp = (FlowRatePer)baseGroup;
            double total = 0;
            bool ing = false;
            for (int i = 0; i < tmp.MList.Count; i++)
            {
                if (m_phaseRunT >= total && m_phaseRunT < Math.Round(total + tmp.MList[i].MBaseTVCV.MT, 2))
                {
                    ing = true;

                    FlowRatePerItem item = tmp.MList[i];

                    if (!phase.MArrIsRun[i])
                    {
                        phase.MArrIsRun[i] = true;

                        //清洗事件
                        switch (item.MFillSystem)
                        {
                            case 1:
                            case 2:
                            MWashHandler?.Invoke(item.MFillSystem);
                                break;
                        }
                    }

                    if (0 != item.MBaseTVCV.MT)
                    {
                        RefreshShareSystemFlowVol((m_phaseRunT - total) / item.MBaseTVCV.MT * (item.MPerBE - item.MPerBS) + item.MPerBS,
                            (m_phaseRunT - total) / item.MBaseTVCV.MT * (item.MPerCE - item.MPerCS) + item.MPerCS,
                            (m_phaseRunT - total) / item.MBaseTVCV.MT * (item.MPerDE - item.MPerDS) + item.MPerDS);
                    }

                    break;
                }
                total = Math.Round(total + tmp.MList[i].MBaseTVCV.MT, 2);
            }

            if (!ing)
            {
                m_run = MRUN.Done;
            }
        }

        private void RefreshNoValveSelection(BaseGroup baseGroup)
        {
            ValveSelection tmp = (ValveSelection)baseGroup;
            RefreshShareIn(tmp.MInA, tmp.MInB, tmp.MInC, tmp.MInD, tmp.MBPV);
            if (tmp.MVisibPer)
            {
                RefreshShareSystemFlowVol(tmp.MPerB, tmp.MPerC, tmp.MPerD);
            }
            if (tmp.MVisibWash)
            {
                if (tmp.MEnableWash)
                {
                    MWashHandler?.Invoke(1);
                }
            }
        }

        private void RefreshNoPHCDUVUntil(BaseGroup baseGroup)
        {
            PHCDUVUntil tmp = (PHCDUVUntil)baseGroup;
            tmp.JudgeInit();
        }
        private void RefreshIngPHCDUVUntil(BaseGroup baseGroup, ref MRUN run)
        {
            PHCDUVUntil tmp = (PHCDUVUntil)baseGroup;
            switch (tmp.MUntilType)
            {
                case EnumPHCDUVUntil.Total:
                    if (m_phaseRunT >= tmp.MTotalTVCV.MT)
                    {
                        run = MRUN.Done;
                    }
                    break;
                case EnumPHCDUVUntil.Met:
                    if (m_phaseRunT >= tmp.MMaxTVCV.MT)
                    {
                        run = MRUN.Done;
                    }
                    else
                    {
                        switch (EnumMonitorInfo.NameList[tmp.MMonitorIndex])
                        {
                            case "pH01":
                                if (tmp.JudgeFinish(m_comconfStatic.GetPHGet(ENUMPHName.pH01), m_phaseRunT))
                                {
                                    m_run = MRUN.Done;
                                }
                                break;
                            case "pH02":
                                if (tmp.JudgeFinish(m_comconfStatic.GetPHGet(ENUMPHName.pH02), m_phaseRunT))
                                {
                                    m_run = MRUN.Done;
                                }
                                break;
                            case "Cd01":
                                if (tmp.JudgeFinish(m_comconfStatic.GetCDGet(ENUMCDName.Cd01), m_phaseRunT))
                                {
                                    m_run = MRUN.Done;
                                }
                                break;
                            case "Cd02":
                                if (tmp.JudgeFinish(m_comconfStatic.GetCDGet(ENUMCDName.Cd02), m_phaseRunT))
                                {
                                    m_run = MRUN.Done;
                                }
                                break;
                            default:
                                if (tmp.JudgeFinish(m_comconfStatic.GetUVGet(ENUMUVName.UV01, Convert.ToInt32(EnumMonitorInfo.NameList[tmp.MMonitorIndex].Remove(0, 5)) - 1), m_phaseRunT))
                                {
                                    m_run = MRUN.Done;
                                }
                                break;
                        }
                    }
                    break;
            }
        }

        private void RefreshNoCollValveCollector(BaseGroup baseGroup)
        {
            CollValveCollector tmp = (CollValveCollector)baseGroup;
            switch (tmp.MEnum)
            {
                case EnumCollectionType.Waste:
                    {
                        SwitchValve(ENUMValveName.Out, 0);
                    }
                    break;
                case EnumCollectionType.Valve:
                    {
                        int outValve = m_comconfStatic.GetValveSet(ENUMValveName.Out);
                        tmp.MValve.Init(ref outValve, MPhaseRunTVCV);
                        SwitchValve(ENUMValveName.Out, outValve);
                        MCollectionValve = tmp.MValve;
                    }
                    break;
                case EnumCollectionType.Collector:
                    {
                        CollectorItem item = m_comconfStatic.GetItem(ENUMCollectorName.Collector01);
                        if (null != item)
                        {
                            CollTextIndex index = new CollTextIndex(item.m_txtSet, item.m_indexSet, item.m_ingSet);
                            tmp.MCollector.Init(ref index, MPhaseRunTVCV);
                            if (index.MStatus != item.MStatusSet)
                            {
                                item.MStatusSet = index.MStatus;
                                MAuditTrailsHandler?.Invoke(ReadXamlCollection.C_CollMarkA, "WASTE");
                            }
                            MCollectionCollector = tmp.MCollector;
                        }
                    }
                    break;
            }
        }
        private void RefreshIngCollValveCollector(BaseGroup baseGroup, ref MRUN run)
        {
            CollValveCollector tmp = (CollValveCollector)baseGroup;
            switch (run)
            {
                case MRUN.Ing:
                    switch (tmp.MEnum)
                    {
                        case EnumCollectionType.Waste:
                            break;
                        case EnumCollectionType.Valve:
                            {
                                int outValve = m_comconfStatic.GetValveSet(ENUMValveName.Out);
                                tmp.MValve.JudgeCondition(ref outValve, MPhaseRunTVCV, EnumMonitorInfo.ValueList, EnumMonitorInfo.ValueMinList, EnumMonitorInfo.ValueMaxList, EnumMonitorInfo.SlopeList, EnumMonitorInfo.SlopeLastList);
                                m_comconfStatic.SetValve(ENUMValveName.Out, outValve);
                                string desc = null;
                                string oper = null;
                                while (tmp.MValve.GetLogDescOper(ref desc, ref oper))
                                {
                                    MAuditTrailsHandler?.Invoke(desc, oper);
                                }
                            }
                            break;
                        case EnumCollectionType.Collector:
                            {
                                CollectorItem item = m_comconfStatic.GetItem(ENUMCollectorName.Collector01);
                                if (null != item)
                                {
                                    CollTextIndex index = new CollTextIndex(item.m_txtSet, item.m_indexSet, item.m_ingSet);
                                    tmp.MCollector.JudgeCondition(ref index, MPhaseRunTVCV, EnumMonitorInfo.ValueList, EnumMonitorInfo.ValueMinList, EnumMonitorInfo.ValueMaxList, EnumMonitorInfo.SlopeList, EnumMonitorInfo.SlopeLastList);
                                    item.MIndexSet = index.MStr;
                                    item.MStatusSet = index.MStatus;
                                    string desc = null;
                                    string oper = null;
                                    while (tmp.MCollector.GetLogDescOper(ref desc, ref oper))
                                    {
                                        MAuditTrailsHandler?.Invoke(desc, oper);
                                    }
                                }
                            }
                            break;
                    }
                    break;
                case MRUN.Done:
                    switch (tmp.MEnum)
                    {
                        case EnumCollectionType.Waste:
                            break;
                        case EnumCollectionType.Valve:
                            {
                                tmp.MValve.JudgeFinish();
                                if (0 != m_comconfStatic.GetValveSet(ENUMValveName.Out))
                                {
                                    SwitchValve(ENUMValveName.Out, 0);
                                    MAuditTrailsHandler?.Invoke(ReadXamlCollection.C_CollMarkA, "WASTE");
                                }
                            }
                            break;
                        case EnumCollectionType.Collector:
                            {
                                tmp.MCollector.JudgeFinish();
                                CollectorItem item = m_comconfStatic.GetItem(ENUMCollectorName.Collector01);
                                if (null != item)
                                {
                                    if (item.MStatusSet)
                                    {
                                        item.MStatusSet = false;
                                        MAuditTrailsHandler?.Invoke(ReadXamlCollection.C_CollMarkA, "WASTE");
                                    }
                                }
                            }
                            break;
                    }
                    break;
            }
        }

        private void RefreshNoCIP(DlyPhase phase, BaseGroup baseGroup)
        {
            CIP tmp = (CIP)baseGroup;
            if (!string.IsNullOrWhiteSpace(tmp.MNote))
            {
                MAuditTrailsHandler?.Invoke(phase.MNamePhase, tmp.MNote);
            }

            if (tmp.MPause)
            {
                m_state = MethodState.RunToPause;
            }

            SwitchValve(ENUMValveName.BPV, 1);
        }
        private void RefreshIngCIP(DlyPhase phase, BaseGroup baseGroup, ref MRUN run)
        {
            CIP tmp = (CIP)baseGroup;
            if (m_phaseRunT >= tmp.MVolumeTotal / tmp.MFlowRate.MFlowVolLen.MFlowVol)
            {
                m_run = MRUN.Done;
                return;
            }

            int indexCurr = (int)(m_phaseRunT / (tmp.MVolumePerPosition / tmp.MFlowRate.MFlowVolLen.MFlowVol));
            int index = 0;

            for (int i = 0; i < tmp.MListInA.Count; i++)
            {
                if (tmp.MListInA[i].MIsSelected)
                {
                    if (index == indexCurr)
                    {
                        SwitchValve(ENUMValveName.InA, i);
                        RefreshShareSystemFlowVol(tmp.MFlowRate.MFlowVolLen.MFlowVol, 0, 0, 0);
                    }
                    index++;
                }
            }
            if (index <= indexCurr)
            {
                for (int i = 0; i < tmp.MListInB.Count; i++)
                {
                    if (tmp.MListInB[i].MIsSelected)
                    {
                        if (index == indexCurr)
                        {
                            SwitchValve(ENUMValveName.InB, i);
                            RefreshShareSystemFlowVol(tmp.MFlowRate.MFlowVolLen.MFlowVol, 100, 0, 0);
                        }
                        index++;
                    }
                }
            }
            if (index <= indexCurr)
            {
                for (int i = 0; i < tmp.MListInC.Count; i++)
                {
                    if (tmp.MListInC[i].MIsSelected)
                    {
                        if (index == indexCurr)
                        {
                            SwitchValve(ENUMValveName.InC, i);
                            RefreshShareSystemFlowVol(tmp.MFlowRate.MFlowVolLen.MFlowVol, 0, 100, 0);
                        }
                        index++;
                    }
                }
            }
            if (index <= indexCurr)
            {
                for (int i = 0; i < tmp.MListInD.Count; i++)
                {
                    if (tmp.MListInD[i].MIsSelected)
                    {
                        if (index == indexCurr)
                        {
                            SwitchValve(ENUMValveName.InD, i);
                            RefreshShareSystemFlowVol(tmp.MFlowRate.MFlowVolLen.MFlowVol, 0, 0, 100);
                        }
                        index++;
                    }
                }
            }
            if (index <= indexCurr)
            {
                for (int i = 0; i < tmp.MListInS.Count; i++)
                {
                    if (tmp.MListInS[i].MIsSelected)
                    {
                        if (index == indexCurr)
                        {
                            SwitchValve(ENUMValveName.InS, i);
                            RefreshSample(true, tmp.MFlowRate.MFlowVolLen.MFlowVol);
                        }
                        index++;
                    }
                }
            }

            bool useCPV = false;
            index = 0;
            for (int i = 0; i < tmp.MListCPV.Count; i++)
            {
                if (tmp.MListCPV[i].MIsSelected)
                {
                    if (index == indexCurr)
                    {
                        SwitchValve(ENUMValveName.BPV, 1);
                        SwitchValve(ENUMValveName.CPV_1, i);
                        useCPV = true;
                    }
                    index++;
                }
            }
            if (!useCPV)
            {
                SwitchValve(ENUMValveName.BPV, 0);
            }

            index = 0;
            for (int i = 0; i < tmp.MListOut.Count; i++)
            {
                if (tmp.MListOut[i].MIsSelected)
                {
                    if (index == indexCurr)
                    {
                        SwitchValve(ENUMValveName.Out, i);
                    }
                    index++;
                }
            }
        }

        /// <summary>
        /// 恢复方法阶段-其它
        /// </summary>
        /// <param name="phase"></param>
        private void RefreshMiscellaneousFirst(Miscellaneous phase)
        {

        }

        /// <summary>
        /// 恢复方法阶段-通用
        /// </summary>
        /// <param name="phase"></param>
        private void RefreshBreakPhase(DlyPhase phase)
        {
            foreach (var it in phase.MListGroup)
            {
                switch (it.MType)
                {
                    case EnumGroupType.FlowRate:
                        RefreshBreakFlowRate(phase, it);
                        break;
                    case EnumGroupType.ValveSelection:
                        RefreshBreakValveSelection(phase, it);
                        break;
                    case EnumGroupType.Mixer:
                        RefreshBreakMixer(phase, it);
                        break;
                    case EnumGroupType.BPV:
                        RefreshBreakBPVValve(phase, it);
                        break;
                    case EnumGroupType.UVReset:
                        RefreshBreakUVReset(phase, it);
                        break;
                    case EnumGroupType.SampleApplicationTech:
                        RefreshBreakSampleApplicationTech(phase, it);
                        break;
                    case EnumGroupType.FlowValveLength:
                        RefreshBreakFlowValveLength(phase, it);
                        break;
                    case EnumGroupType.FlowRatePer:
                        RefreshBreakFlowRatePer(phase, it);
                        break;
                    case EnumGroupType.PHCDUVUntil:
                        RefreshBreakPHCDUVUntil(phase, it);
                        break;
                    case EnumGroupType.CIP:
                        RefreshBreakCIP(phase, it);
                        break;
                }
            }
        }

        private void RefreshBreakFlowValveLength(DlyPhase phase, BaseGroup baseGroup)
        {
            FlowValveLength tmp = (FlowValveLength)baseGroup;
            if (null == phase.MArrIsRun || phase.MArrIsRun.Length != tmp.MList.Count)
            {
                phase.MArrIsRun = new bool[tmp.MList.Count];
                phase.MArrIsIncubation = new bool[tmp.MList.Count];
            }
            for (int i = 0; i < phase.MArrIsRun.Length; i++)
            {
                phase.MArrIsRun[i] = false;
                phase.MArrIsIncubation[i] = false;
            }
        }
        private void RefreshBreakFlowRate(DlyPhase phase, BaseGroup baseGroup)
        {
            FlowRate tmp = (FlowRate)baseGroup;
            RefreshShareSystemFlowVol(tmp.MFlowVolLen.MFlowVol);
        }
        private void RefreshBreakMixer(DlyPhase phase, BaseGroup baseGroup)
        {
            Mixer tmp = (Mixer)baseGroup;
            m_comconfStatic.SetMixer(ENUMMixerName.Mixer01, tmp.MOnoff);
        }
        private void RefreshBreakBPVValve(DlyPhase phase, BaseGroup baseGroup)
        {
            BPVValve tmp = (BPVValve)baseGroup;
            SwitchValve(ENUMValveName.BPV, tmp.MBPV);
        }
        private void RefreshBreakUVReset(DlyPhase phase, BaseGroup baseGroup)
        {
            UVReset tmp = (UVReset)baseGroup;
            if (tmp.MEnableResetUV)
            {
                m_comconfStatic.SetUVClear(ENUMUVName.UV01);
                MAuditTrailsHandler?.Invoke(phase.MNamePhase, ReadXaml.GetResources("ME_ResetUVMonitor"));
            }
        }
        private void RefreshBreakSampleApplicationTech(DlyPhase phase, BaseGroup baseGroup)
        {
            SampleApplicationTech tmp = (SampleApplicationTech)baseGroup;
            switch (tmp.MEnumSAT)
            {
                case EnumSAT.ManualLoopFilling:
                    break;
                case EnumSAT.SamplePumpLoopFilling:
                    SwitchValve(ENUMValveName.InS, tmp.MInS);
                    break;
                case EnumSAT.ISDOC:
                    SwitchValve(ENUMValveName.InS, tmp.MInS);
                    break;
            }
        }
        private void RefreshBreakFlowRatePer(DlyPhase phase, BaseGroup baseGroup)
        {
            FlowRatePer tmp = (FlowRatePer)baseGroup;
            if (null == phase.MArrIsRun || phase.MArrIsRun.Length != tmp.MList.Count)
            {
                phase.MArrIsRun = new bool[tmp.MList.Count];
                phase.MArrIsIncubation = new bool[tmp.MList.Count];
            }
            for (int i = 0; i < phase.MArrIsRun.Length; i++)
            {
                phase.MArrIsRun[i] = false;
                phase.MArrIsIncubation[i] = false;
            }
        }
        private void RefreshBreakValveSelection(DlyPhase phase, BaseGroup baseGroup)
        {
            ValveSelection tmp = (ValveSelection)baseGroup;
            RefreshShareIn(tmp.MInA, tmp.MInB, tmp.MInC, tmp.MInD, tmp.MBPV);
            if (tmp.MVisibPer)
            {
                RefreshShareSystemFlowVol(tmp.MPerB, tmp.MPerC, tmp.MPerD);
            }
            if (tmp.MVisibWash)
            {
                if (tmp.MEnableWash)
                {
                    MWashHandler?.Invoke(1);
                }
            }
        }
        private void RefreshBreakPHCDUVUntil(DlyPhase phase, BaseGroup baseGroup)
        {
            PHCDUVUntil tmp = (PHCDUVUntil)baseGroup;
            tmp.JudgeInit();
        }
        private void RefreshBreakCIP(DlyPhase phase, BaseGroup baseGroup)
        {
            CIP tmp = (CIP)baseGroup;
            if (tmp.MPause)
            {
                m_state = MethodState.RunToPause;
            }
        }

        /// <summary>
        /// 快捷切阀
        /// </summary>
        /// <param name="name"></param>
        /// <param name="index"></param>
        private void SwitchValve(ENUMValveName name, int index)
        {
            if (Visibility.Visible != ItemVisibility.s_listValve[name])
            {
                return;
            }

            int indexOld = m_comconfStatic.GetValveSet(name);
            if (indexOld != index)
            {
                m_comconfStatic.SetValve(name, index);
                string[] arr = StaticValue.GetNameList(name);
                MAuditTrailsHandler?.Invoke(name.ToString(), arr[indexOld] + " -> " + arr[index]);
            }
        }

        /// <summary>
        /// 快捷设置阀
        /// </summary>
        /// <param name="inA"></param>
        /// <param name="inB"></param>
        /// <param name="inC"></param>
        /// <param name="inD"></param>
        private void RefreshShareIn(int inA, int inB, int inC, int inD, int bpv)
        {
            SwitchValve(ENUMValveName.InA, inA);
            SwitchValve(ENUMValveName.InB, inB);
            SwitchValve(ENUMValveName.InC, inC);
            SwitchValve(ENUMValveName.InD, inD);
            SwitchValve(ENUMValveName.BPV, bpv);
        }

        /// <summary>
        /// 快捷设置紫外检测器
        /// </summary>
        /// <param name="uvValue"></param>
        private void RefreshShareUV(UVValue uvValue)
        {
            if (Visibility.Visible != ItemVisibility.s_listUV[ENUMUVName.UV01])
            {
                return;
            }

            m_comconfStatic.SetUVWave(ENUMUVName.UV01, uvValue);

            if (uvValue.MOnoff)
            {
                m_comconfStatic.SetUVLamp(ENUMUVName.UV01, true);
                MAuditTrailsHandler?.Invoke(ReadXamlMethod.S_MethodSettings, ReadXamlMethod.S_UVSettings + "-" + ReadXaml.GetResources("labUVOn"));
            }
            else
            {
                m_comconfStatic.SetUVLamp(ENUMUVName.UV01, false);
                MAuditTrailsHandler?.Invoke(ReadXamlMethod.S_MethodSettings, ReadXamlMethod.S_UVSettings + "-" + ReadXaml.GetResources("labUVOff"));
            }

            if (uvValue.MClear)
            {
                m_comconfStatic.SetUVClear(ENUMUVName.UV01);
                MAuditTrailsHandler?.Invoke(ReadXamlMethod.S_MethodSettings, ReadXamlMethod.S_UVSettings + "-" + ReadXaml.GetResources("labUVReset"));
            }
        }

        /// <summary>
        /// 快捷设置系统泵流速和百分比
        /// </summary>
        /// <param name="flowVol"></param>
        /// <param name="perB"></param>
        /// <param name="perC"></param>
        /// <param name="perD"></param>
        private void RefreshShareSystemFlowVol(double flowVol, double perB, double perC, double perD)
        {
            m_comconfStatic.SetPumpSystem(flowVol, perB, perC, perD);
            m_methodTempValue.MFlowSystem = flowVol;
            m_methodTempValue.MPerB = perB;
            m_methodTempValue.MPerC = perC;
            m_methodTempValue.MPerD = perD;
        }

        /// <summary>
        /// 快捷设置系统泵流速
        /// </summary>
        /// <param name="flowVol"></param>
        private void RefreshShareSystemFlowVol(double flowVol)
        {
            m_comconfStatic.SetPumpSystem(flowVol, m_methodTempValue.MPerB, m_methodTempValue.MPerC, m_methodTempValue.MPerD);
            m_methodTempValue.MFlowSystem = flowVol;
        }

        /// <summary>
        /// 快捷设置系统泵百分比
        /// </summary>
        /// <param name="perB"></param>
        /// <param name="perC"></param>
        /// <param name="perD"></param>
        private void RefreshShareSystemFlowVol(double perB, double perC, double perD)
        {
            m_comconfStatic.SetPumpSystem(m_methodTempValue.MFlowSystem, perB, perC, perD);
            m_methodTempValue.MPerB = perB;
            m_methodTempValue.MPerC = perC;
            m_methodTempValue.MPerD = perD;
        }

        /// <summary>
        /// 快捷设置上样泵流速
        /// </summary>
        /// <param name="flowVol"></param>
        private void RefreshShareSampleFlowVol(double flowVol)
        {
            m_comconfStatic.SetPumpSample(flowVol);
            m_methodTempValue.MFlowSample = flowVol;
        }

        /// <summary>
        /// 快捷切换系统泵和上样泵的启停
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="flow"></param>
        private void RefreshSample(bool flag, double flow)
        {
            if (flag)
            {
                RefreshShareSystemFlowVol(0);
                RefreshShareSampleFlowVol(flow);
            }
            else
            {
                RefreshShareSystemFlowVol(flow);
                RefreshShareSampleFlowVol(0);
            }
        }

        /// <summary>
        /// 快捷停泵
        /// </summary>
        private void RefreshStopSABCD()
        {
            RefreshShareSystemFlowVol(0, 0, 0, 0);
            RefreshShareSampleFlowVol(0);
        }

        /*-------------方法运行状态机 结束-------------------*/

        /// <summary>
        /// 写静态变量到临时文件
        /// </summary>
        private void WriteDataToDB()
        {
            MethodManager manager = new MethodManager();
            MethodTemp item = new MethodTemp();
            item.MID = m_methodType.MID;
            item.MName = m_methodType.MName;
            item.MType = (int)m_methodType.MType;
            item.MIndexCurrMethod = m_indexCurrMethod;
            item.MIndexCurrPhase = m_indexCurrPhase;
            item.MPhaseStartT = m_phaseStartT;                          //当前执行阶段的开始时间点
            item.MPhaseStartV = m_phaseStartV;                          //当前执行阶段的开始时间点
            item.MPhaseStopT = m_phaseStopT;                            //当前执行阶段的结束时间点
            item.MPhaseStopV = m_phaseStopV;                            //当前执行阶段的结束时间点
            item.MPhaseRunTime = m_phaseRunT;
            item.MHoldStartT = m_holdStartT;                            //当前执行阶段的一次挂起时间点
            item.MHoldStartV = m_holdStartV;                            //当前执行阶段的一次挂起时间点
            item.MHoldTotalT = m_holdTotalT;
            item.MHoldTotalV = m_holdTotalV;
            item.MIsHold = m_isHold;
            manager.AddMethodTemp(item);
        }

        /// <summary>
        /// 读临时文件数据写入到静态变量
        /// </summary>
        private bool ReadDataFromDB()
        {
            MethodManager manager = new MethodManager();
            MethodTemp item = null;
            if (null == manager.GetMethodTemp(out item))
            {
                if (-1 != item.MID)
                {
                    SendMethodOrQueue(new MethodType(item.MID, item.MName, (EnumMethodType)item.MType));
                    switch (m_methodType.MType)
                    {
                        case EnumMethodType.MethodQueue:
                            m_indexCurrMethod = item.MIndexCurrMethod;
                            manager.GetMethod(MMethodQueue.MMethodList[m_indexCurrMethod], out m_method);
                            RefreshMethodSetting(MMethod.MMethodSetting);
                            m_runMethod = MRUN.Ing;
                            break;
                        default:
                            break;
                    }
                    m_indexCurrPhase = item.MIndexCurrPhase;
                    switch (MMethod.MPhaseList[m_indexCurrPhase].MType)
                    {
                        case EnumPhaseType.Miscellaneous:
                            RefreshMiscellaneousFirst((Miscellaneous)MMethod.MPhaseList[m_indexCurrPhase]);
                            break;
                        default:
                            RefreshBreakPhase((DlyPhase)MMethod.MPhaseList[m_indexCurrPhase]);
                            break;
                    }

                    m_run = MRUN.Ing;
                    m_phaseStartT = item.MPhaseStartT;                      //当前执行阶段的开始时间点
                    m_phaseStartV = item.MPhaseStartV;                      //当前执行阶段的开始时间点
                    m_phaseStopT = item.MPhaseStopT;                        //当前执行阶段的结束时间点
                    m_phaseStopV = item.MPhaseStopV;                        //当前执行阶段的结束时间点
                    m_phaseRunT = item.MPhaseRunTime;
                    m_holdStartT = item.MHoldStartT;                        //当前执行阶段的一次挂起时间点
                    m_holdStartV = item.MHoldStartV;                        //当前执行阶段的一次挂起时间点
                    m_holdTotalT = item.MHoldTotalT;
                    m_holdTotalV = item.MHoldTotalV;
                    m_isHold = item.MIsHold;

                    m_isBreak = true;

                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 清除临时文件
        /// </summary>
        private void ClearDataToDB()
        {
            MethodManager manager = new MethodManager();
            manager.DelMethodTemp();
        }
    }
}
